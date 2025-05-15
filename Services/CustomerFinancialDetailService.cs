using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Responses;
using Microsoft.Extensions.Configuration;

namespace ErpMobile.Api.Services
{
    public class CustomerFinancialDetailService : ICustomerFinancialDetailService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CustomerFinancialDetailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ErpConnection");
        }

        public async Task<CustomerFinancialDetailResponse> GetCustomerFinancialDetailsByCodeAsync(string customerCode)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                // Get customer basic info
                var customerBasicSql = @"
                    SELECT 
                        CurrAccCode AS CustomerCode,
                        CurrAccDesc AS CustomerName
                    FROM cdCurrAcc WITH(NOLOCK)
                    WHERE CurrAccCode = @CustomerCode";

                var customerBasic = await connection.QueryFirstOrDefaultAsync<CustomerFinancialDetailResponse>(customerBasicSql, new { CustomerCode = customerCode });
                
                if (customerBasic == null)
                {
                    return null;
                }

                // Get customer financial summary
                var financialSummarySql = @"
                    SELECT 
                        SUM(ISNULL(Loc_Debit, 0)) AS TotalDebit,
                        SUM(ISNULL(Loc_Credit, 0)) AS TotalCredit,
                        SUM(ISNULL(Loc_Debit, 0) - ISNULL(Loc_Credit, 0)) AS Balance
                    FROM (
                        SELECT
                            cab.CurrAccBookID,
                            cab.CurrAccTypeCode,
                            cab.CurrAccCode,
                            cab.DocumentDate,
                            cab.DocumentNumber,
                            cab.DueDate,
                            cab.LineDescription,
                            cab.DocCurrencyCode AS Doc_CurrencyCode,
                            ISNULL(cabcDoc.Debit, 0) AS Doc_Debit,
                            ISNULL(cabcDoc.Credit, 0) AS Doc_Credit,
                            cab.LocalCurrencyCode AS Loc_CurrencyCode,
                            ISNULL(cabcLoc.ExchangeRate, 0) AS Loc_ExchangeRate,
                            ISNULL(cabcLoc.Debit, 0) AS Loc_Debit,
                            ISNULL(cabcLoc.Credit, 0) AS Loc_Credit,
                            gd.CompanyCurrencyCode AS Com_CurrencyCode,
                            ISNULL(cabcCom.ExchangeRate, 0) AS Com_ExchangeRate,
                            ISNULL(cabcCom.Debit, 0) AS Com_Debit,
                            ISNULL(cabcCom.Credit, 0) AS Com_Credit
                        FROM trCurrAccBook cab WITH(NOLOCK)
                        INNER JOIN dfGlobalDefault gd WITH(NOLOCK) ON gd.GlobalDefaultCode = 1
                        LEFT OUTER JOIN trCurrAccBookCurrency cabcDoc WITH(NOLOCK) ON cabcDoc.CurrAccBookID = cab.CurrAccBookID AND cab.DocCurrencyCode = cabcDoc.CurrencyCode
                        LEFT OUTER JOIN trCurrAccBookCurrency cabcLoc WITH(NOLOCK) ON cabcLoc.CurrAccBookID = cab.CurrAccBookID AND cab.LocalCurrencyCode = cabcLoc.CurrencyCode
                        LEFT OUTER JOIN trCurrAccBookCurrency cabcCom WITH(NOLOCK) ON cabcCom.CurrAccBookID = cab.CurrAccBookID AND gd.CompanyCurrencyCode = cabcCom.CurrencyCode
                        WHERE cab.CurrAccCode = @CustomerCode
                    ) AS FinancialData";

                var financialSummary = await connection.QueryFirstOrDefaultAsync<dynamic>(financialSummarySql, new { CustomerCode = customerCode });
                
                if (financialSummary != null)
                {
                    customerBasic.TotalDebit = financialSummary.TotalDebit ?? 0;
                    customerBasic.TotalCredit = financialSummary.TotalCredit ?? 0;
                    customerBasic.Balance = financialSummary.Balance ?? 0;
                }

                // Get customer debit items
                var debitItemsSql = @"
                    SELECT 
                        AllDebits.CurrAccTypeCode, 
                        AllDebits.CurrAccCode, 
                        AllDebits.DocumentDate, 
                        AllDebits.DocumentNumber, 
                        AllDebits.DueDate, 
                        AllDebits.LineDescription, 
                        AllDebits.RefNumber, 
                        AllDebits.Doc_CurrencyCode AS DocCurrencyCode, 
                        AllDebits.Doc_Debit AS DocDebit, 
                        AllDebits.Loc_CurrencyCode AS LocCurrencyCode, 
                        AllDebits.Loc_ExchangeRate AS LocExchangeRate, 
                        AllDebits.Loc_Debit AS LocDebit, 
                        AllDebits.Loc_Balance AS LocBalance, 
                        AllDebits.DebitTypeCode, 
                        AllDebits.DebitReasonCode, 
                        AllDebits.DebitLineID, 
                        AllDebits.DebitHeaderID, 
                        AllDebits.ApplicationCode, 
                        AllDebits.ApplicationID
                    FROM (
                        SELECT 
                            dbo.trDebitHeader.CurrAccTypeCode, 
                            dbo.trDebitHeader.CurrAccCode, 
                            dbo.trDebitHeader.SubCurrAccID, 
                            dbo.trDebitHeader.CompanyCode, 
                            dbo.trDebitHeader.OfficeCode, 
                            dbo.trDebitHeader.StoreTypeCode, 
                            dbo.trDebitHeader.StoreCode, 
                            dbo.trDebitHeader.DocumentDate, 
                            dbo.trDebitHeader.DocumentNumber, 
                            dbo.trDebitLine.DueDate, 
                            dbo.trDebitLine.LineDescription, 
                            dbo.trDebitHeader.RefNumber, 
                            dbo.trDebitLine.DocCurrencyCode AS Doc_CurrencyCode, 
                            dbo.trDebitHeader.LocalCurrencyCode AS Loc_CurrencyCode, 
                            ISNULL(trDebitLineCurrencyLoc.ExchangeRate, 0) AS Loc_ExchangeRate, 
                            dbo.trDebitLine.DebitLineID, 
                            dbo.trDebitLine.DebitHeaderID, 
                            dbo.trDebitHeader.DebitTypeCode, 
                            dbo.trDebitLine.DebitReasonCode, 
                            dbo.trDebitHeader.ApplicationCode, 
                            dbo.trDebitHeader.ApplicationID, 
                            CASE WHEN CurrAccTypeCode = 1 THEN ISNULL(trDebitLineCurrencyDoc.Credit, 0) ELSE ISNULL(trDebitLineCurrencyDoc.Debit, 0) END AS Doc_Debit, 
                            CASE WHEN CurrAccTypeCode = 1 THEN ISNULL(trDebitLineCurrencyLoc.Credit, 0) ELSE ISNULL(trDebitLineCurrencyLoc.Debit, 0) END AS Loc_Debit,
                            0 AS Loc_Balance
                        FROM dbo.trDebitLine WITH (NOLOCK) 
                        INNER JOIN dbo.trDebitHeader WITH (NOLOCK) ON dbo.trDebitHeader.DebitHeaderID = dbo.trDebitLine.DebitHeaderID 
                        LEFT OUTER JOIN dbo.trDebitLineCurrency AS trDebitLineCurrencyDoc WITH (NOLOCK) ON trDebitLineCurrencyDoc.DebitLineID = dbo.trDebitLine.DebitLineID AND dbo.trDebitLine.DocCurrencyCode = trDebitLineCurrencyDoc.CurrencyCode 
                        LEFT OUTER JOIN dbo.trDebitLineCurrency AS trDebitLineCurrencyLoc WITH (NOLOCK) ON trDebitLineCurrencyLoc.DebitLineID = dbo.trDebitLine.DebitLineID AND dbo.trDebitHeader.LocalCurrencyCode = trDebitLineCurrencyLoc.CurrencyCode
                        WHERE (dbo.trDebitHeader.DebitTypeCode <> 7) AND dbo.trDebitHeader.CurrAccCode = @CustomerCode
                    ) AS AllDebits 
                    LEFT OUTER JOIN (
                        SELECT 
                            dbo.trPaymentLine.DebitLineID, 
                            ABS(SUM(dbo.trPaymentLineCurrency.Payment)) AS Loc_Payment
                        FROM dbo.trPaymentLine WITH (NOLOCK) 
                        INNER JOIN dbo.trPaymentHeader WITH (NOLOCK) ON dbo.trPaymentHeader.PaymentHeaderID = dbo.trPaymentLine.PaymentHeaderID 
                        INNER JOIN dbo.trPaymentLineCurrency WITH (NOLOCK) ON dbo.trPaymentLineCurrency.PaymentLineID = dbo.trPaymentLine.PaymentLineID AND dbo.trPaymentLineCurrency.CurrencyCode = dbo.trPaymentHeader.LocalCurrencyCode
                        WHERE (dbo.trPaymentLine.DebitLineID IS NOT NULL)
                        GROUP BY dbo.trPaymentLine.DebitLineID
                    ) AS AllPayments ON AllPayments.DebitLineID = AllDebits.DebitLineID
                    WHERE (AllDebits.Loc_Debit > 0)
                    ORDER BY AllDebits.DocumentDate DESC";

                var debitItems = await connection.QueryAsync<CustomerDebitItem>(debitItemsSql, new { CustomerCode = customerCode });
                customerBasic.DebitItems = debitItems.ToList();

                // Get customer credit items
                var creditItemsSql = @"
                    SELECT 
                        CurrAccTypeCode, 
                        CurrAccCode, 
                        DocumentDate, 
                        DocumentNumber, 
                        DueDate, 
                        LineDescription, 
                        RefNumber, 
                        Doc_CurrencyCode AS DocCurrencyCode, 
                        Doc_Amount AS DocAmount, 
                        Loc_CurrencyCode AS LocCurrencyCode, 
                        Loc_ExchangeRate AS LocExchangeRate, 
                        Loc_Amount AS LocAmount, 
                        Loc_Balance AS LocBalance, 
                        ApplicationCode, 
                        ApplicationLineID, 
                        CurrAccBookID, 
                        PaymentTypeCode
                    FROM (
                        SELECT 
                            trCurrAccBook.CurrAccTypeCode, 
                            trCurrAccBook.CurrAccCode, 
                            trCurrAccBook.DocumentDate, 
                            trCurrAccBook.DocumentNumber, 
                            trCurrAccBook.RefNumber, 
                            trCurrAccBook.DueDate, 
                            trCurrAccBook.LineDescription, 
                            trCurrAccBook.DocCurrencyCode AS Doc_CurrencyCode, 
                            (ISNULL(trCurrAccBookCurrencyDoc.Debit, 0) + ISNULL(trCurrAccBookCurrencyDoc.Credit, 0)) AS Doc_Amount, 
                            trCurrAccBook.LocalCurrencyCode AS Loc_CurrencyCode, 
                            ISNULL(trCurrAccBookCurrencyLoc.ExchangeRate, 0) AS Loc_ExchangeRate, 
                            (ISNULL(trCurrAccBookCurrencyLoc.Debit, 0) + ISNULL(trCurrAccBookCurrencyLoc.Credit, 0)) AS Loc_Amount, 
                            (ISNULL(trCurrAccBookCurrencyLoc.Debit, 0) + ISNULL(trCurrAccBookCurrencyLoc.Credit, 0)) - ISNULL(AllPayments.Loc_Payment, 0) AS Loc_Balance, 
                            trCurrAccBook.ApplicationCode, 
                            trCurrAccBook.ApplicationID AS ApplicationLineID, 
                            trCurrAccBook.CurrAccBookID, 
                            5 AS PaymentTypeCode
                        FROM trCurrAccBook WITH (NOLOCK) 
                        LEFT OUTER JOIN trCurrAccBookCurrency AS trCurrAccBookCurrencyDoc WITH (NOLOCK) ON trCurrAccBookCurrencyDoc.CurrAccBookID = trCurrAccBook.CurrAccBookID AND trCurrAccBook.DocCurrencyCode = trCurrAccBookCurrencyDoc.CurrencyCode 
                        LEFT OUTER JOIN trCurrAccBookCurrency AS trCurrAccBookCurrencyLoc WITH (NOLOCK) ON trCurrAccBookCurrencyLoc.CurrAccBookID = trCurrAccBook.CurrAccBookID AND trCurrAccBook.LocalCurrencyCode = trCurrAccBookCurrencyLoc.CurrencyCode 
                        LEFT OUTER JOIN (
                            SELECT 
                                CurrAccTypeCode, 
                                CurrAccCode, 
                                ReverseDebitLineID, 
                                Loc_CurrencyCode, 
                                Loc_Payment = SUM(Loc_Payment)
                            FROM (
                                SELECT 
                                    trPaymentHeader.CurrAccTypeCode, 
                                    trPaymentHeader.CurrAccCode, 
                                    trPaymentLine.ReverseDebitLineID, 
                                    Loc_CurrencyCode = trPaymentHeader.LocalCurrencyCode, 
                                    Loc_Payment = ABS(SUM(trPaymentLineCurrency.Payment))
                                FROM trPaymentLine WITH (NOLOCK) 
                                INNER JOIN trPaymentHeader WITH (NOLOCK) ON trPAymentHeader.PaymentHeaderID = trPaymentLine.PaymentHeaderID 
                                INNER JOIN trPaymentLineCurrency WITH (NOLOCK) ON trPaymentLineCurrency.PaymentLineID = trPaymentLine.PaymentLineID AND trPaymentLineCurrency.CurrencyCode = trPaymentHeader.LocalCurrencyCode
                                WHERE ReverseDebitLineID IS NOT NULL
                                GROUP BY trPaymentHeader.CurrAccTypeCode, trPaymentHeader.CurrAccCode, trPaymentLine.ReverseDebitLineID, trPaymentHeader.LocalCurrencyCode
                                UNION ALL
                                SELECT 
                                    trPaymentHeader.CurrAccTypeCode, 
                                    trPaymentHeader.CurrAccCode, 
                                    trPaymentLine.DebitLineID, 
                                    Loc_CurrencyCode = trPaymentHeader.LocalCurrencyCode, 
                                    Loc_Payment = ABS(SUM(trPaymentLineCurrency.Payment))
                                FROM trPaymentLine WITH (NOLOCK) 
                                INNER JOIN trPaymentHeader WITH (NOLOCK) ON trPAymentHeader.PaymentHeaderID = trPaymentLine.PaymentHeaderID 
                                INNER JOIN trPaymentLineCurrency WITH (NOLOCK) ON trPaymentLineCurrency.PaymentLineID = trPaymentLine.PaymentLineID AND trPaymentLineCurrency.CurrencyCode = trPaymentHeader.LocalCurrencyCode
                                WHERE DebitLineID IS NOT NULL
                                GROUP BY trPaymentHeader.CurrAccTypeCode, trPaymentHeader.CurrAccCode, trPaymentLine.DebitLineID, trPaymentHeader.LocalCurrencyCode
                            ) AS DATA
                            GROUP BY CurrAccTypeCode, CurrAccCode, ReverseDebitLineID, Loc_CurrencyCode
                        ) AS AllPayments ON AllPayments.ReverseDebitLineID = trCurrAccBook.ApplicationID AND AllPayments.CurrAccTypeCode = trCurrAccBook.CurrAccTypeCode AND AllPayments.CurrAccCode = trCurrAccBook.CurrAccCode
                        WHERE trCurrAccBook.ApplicationCode = N'Debit' 
                        AND CASE WHEN trCurrAccBook.CurrAccTypeCode IN (1) THEN ISNULL(trCurrAccBookCurrencyLoc.Credit, 0) ELSE ISNULL(trCurrAccBookCurrencyLoc.Debit, 0) END = 0 
                        AND CASE WHEN trCurrAccBook.CurrAccTypeCode IN (1) THEN ISNULL(trCurrAccBookCurrencyLoc.Debit, 0) ELSE ISNULL(trCurrAccBookCurrencyLoc.Credit, 0) END <> 0 
                        AND (ISNULL(trCurrAccBookCurrencyLoc.Debit, 0) + ISNULL(trCurrAccBookCurrencyLoc.Credit, 0)) - ISNULL(AllPayments.Loc_Payment, 0) > 0 
                        AND trCurrAccBook.CurrAccTypeCode IN (1, 2, 3, 4, 5, 6, 7, 8, 9)
                        AND trCurrAccBook.CurrAccCode = @CustomerCode
                    ) AS CreditItems
                    ORDER BY DocumentDate DESC";

                var creditItems = await connection.QueryAsync<CustomerCreditItem>(creditItemsSql, new { CustomerCode = customerCode });
                customerBasic.CreditItems = creditItems.ToList();

                return customerBasic;
            }
        }
    }
}
