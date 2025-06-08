using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Data;
using ErpMobile.Api.Models.Responses;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Repositories.CashTransaction
{
    public class CashTransactionRepository : ICashTransactionRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<CashTransactionRepository> _logger;

        public CashTransactionRepository(DapperContext context, ILogger<CashTransactionRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<CashTransactionResponse>> GetCashTransactionsAsync(DateTime startDate, DateTime endDate, string cashAccountCode = null)
        {
            _logger.LogInformation("CashTransactionRepository.GetCashTransactionsAsync() called with startDate: {StartDate}, endDate: {EndDate}, cashAccountCode: {CashAccountCode}", 
                startDate, endDate, cashAccountCode ?? "null");
            
            try
            {
                _logger.LogInformation("Creating SQL query parameters");
                var parameters = new 
                { 
                    StartDate = startDate, 
                    EndDate = endDate, 
                    CashAccountCode = string.IsNullOrEmpty(cashAccountCode) ? null : cashAccountCode 
                };
                
                _logger.LogInformation("SQL Parameters: {Parameters}", parameters);
                _logger.LogInformation("Preparing SQL query for cash transactions");
                
                var query = @"
                     
SELECT Detail.* FROM (
SELECT 
	  DocumentDate				= @StartDate	
	, DocumentNumber			= ISNULL( (SELECT Description FROM NebimV3Master..bsField WITH(NOLOCK) WHERE Field = 'Turnover' and LangCode = N'TR'),'Turnover')  
	, CashTransTypeCode			= SPACE(0)
	, CashTransTypeDescription	= SPACE(0)
	, CashTransNumber			= SPACE(0)
	, RefNumber					= SPACE(0)
	, GLTypeCode				= SPACE(0)
	, Description				= SPACE(0)	
	, SumDescription			= SPACE(0) 
	, ApplicationCode			= SPACE(0) 
	, ApplicationDescription	= SPACE(0)	
	, CurrAccTypeCode			= SPACE(0) 
	, CurrAccTypeDescription	= SPACE(0) 
	, CurrAccCode				= SPACE(0)
	, CurrAccDescription		= SPACE(0)
	, GLAccCode					= SPACE(0)
	, GLAccDescription			= SPACE(0) 
		
	, LineDescription			= ISNULL( (SELECT Description FROM NebimV3Master..bsField WITH(NOLOCK) WHERE Field = 'Turnover' and LangCode = N'TR'),'Turnover')  
	, ChequeTransTypeDescription = SPACE(0)

	, Doc_CurrencyCode			= (trCashLineCurrencyDoc.CurrencyCode)
	, Doc_Debit					= SUM(trCashLineCurrencyDoc.Debit)
	, Doc_Credit				= SUM (trCashLineCurrencyDoc.Credit )  
	, Doc_Balance				= SUM(trCashLineCurrencyDoc.Debit) - SUM (trCashLineCurrencyDoc.Credit )
	, Loc_CurrencyCode			= trCashLineCurrency.CurrencyCode
	, Loc_ExchangeRate			= SPACE(0) 
	, Loc_Debit					= SUM(trCashLineCurrency.Debit) 
	, Loc_Credit				= SUM (trCashLineCurrency.Credit )   
	, Loc_Balance				= SUM(trCashLineCurrency.Debit)  - SUM (trCashLineCurrency.Credit )   

	, ATAtt01 = SPACE(0), ATAtt02 = SPACE(0), ATAtt03 = SPACE(0), ATAtt04 = SPACE(0), ATAtt05 = SPACE(0)
	, CompanyCode			= trCashLine.CompanyCode
	, OfficeCode			= SPACE(0)
	, StoreCode				= SPACE(0)
	, IsTurnover			= CAST(1 AS BIT)
	
	, IsPostingJournal		= SPACE(0)
	, JournalDate			= SPACE(0)
	, JournalNumber			= SPACE(0)
	, ImportFileNumber		= SPACE(0)
	, ExportFileNumber		= SPACE(0)

	, CashAccountCode			= CASE WHEN CashTransTypeCode = 3 THEN trCashLine.CurrAccCode 
									   WHEN CashTransTypeCode = 8 AND trCashLine.GLAccCode <> SPACE(0) THEN SPACE(0)
									   ELSE trCashHeader.CashCurrAccCode END trCashHeader.CashCurrAccCode = @cashAccountCode

	, LastUpdatedUserName		= SPACE(0)
	, LastUpdatedDate			= SPACE(0)
	, LinkedApplicationCode		= SPACE(0)
	, LinkedApplicationID		= CAST(NULL AS uniqueidentifier)
	FROM trCashHeader WITH(NOLOCK) 
		INNER JOIN trCashLine WITH(NOLOCK) ON trCashLine.CashHeaderID= trCashHeader.CashHeaderID
		LEFT OUTER JOIN trCashLineCurrency WITH(NOLOCK) ON trCashLineCurrency.CashLineID = trCashLine.CashLineID AND trCashHeader.LocalCurrencyCode = trCashLineCurrency.CurrencyCode
		LEFT OUTER JOIN trCashLineCurrency trCashLineCurrencyDoc WITH(NOLOCK) ON trCashLineCurrencyDoc.CashLineID = trCashLine.CashLineID 
															AND trCashLine.DocCurrencyCode = trCashLineCurrencyDoc.CurrencyCode
		LEFT OUTER JOIN CashATAttributesFilter ON CashATAttributesFilter.CashHeaderID = trCashHeader.CashHeaderID
	WHERE DocumentDate < CAST('20240101' AS DATETIME)
		AND 1 = 1
		AND CASE WHEN CashTransTypeCode = 3 THEN trCashLine.CurrAccCode 
				 WHEN CashTransTypeCode = 8 AND trCashLine.GLAccCode <> SPACE(0) THEN SPACE(0)
				 ELSE trCashHeader.CashCurrAccCode END <> SPACE(0)
		AND trCashHeader.CompanyCode = 1  
		AND trCashLineCurrency.CurrencyCode is not null	  
	GROUP BY trCashHeader.CashCurrAccTypeCode
			, CASE WHEN CashTransTypeCode = 3 THEN trCashLine.CurrAccCode 
				   WHEN CashTransTypeCode = 8 AND trCashLine.GLAccCode <> SPACE(0) THEN SPACE(0)
				   ELSE trCashHeader.CashCurrAccCode END
			, trCashLineCurrency.CurrencyCode
			, trCashLineCurrencyDoc.CurrencyCode
			, trCashLine.CompanyCode 
UNION ALL
SELECT 
	  DocumentDate				= trCashHeader.DocumentDate	 
	, DocumentNumber			= trCashHeader.DocumentNumber 
	, CashTransTypeCode			= trCashHeader.CashTransTypeCode
	, CashTransTypeDescription	= ISNULL((SELECT CashTransTypeDescription FROM bsCashTransTypeDesc WITH(NOLOCK) WHERE bsCashTransTypeDesc.CashTransTypeCode = trCashHeader.CashTransTypeCode AND bsCashTransTypeDesc.LangCode = N'TR'),SPACE(0))
	, CashTransNumber			= trCashHeader.CashTransNumber
	, RefNumber					= trCashHeader.RefNumber
	, GLTypeCode				= trCashHeader.GLTypeCode
	, Description				= trCashHeader.Description
	, SumDescription			= ISNULL((SELECT CashTransTypeDescription FROM bsCashTransTypeDesc WITH(NOLOCK) WHERE bsCashTransTypeDesc.CashTransTypeCode = trCashHeader.CashTransTypeCode AND bsCashTransTypeDesc.LangCode = N'TR'),SPACE(0))  + ', '
								+ CASE WHEN ApplicationCode <> SPACE(0) AND ApplicationCode <> 'Bank' THEN (SELECT ApplicationDescription FROM bsApplicationDesc AS Adesc WHERE Adesc.LangCode = N'TR' AND trCashHeader.ApplicationCode = Adesc.ApplicationCode) + ', ' ELSE SPACE(0) END 
								+ CASE WHEN RefNumber <> SPACE(0)								THEN N'Ref.No:' + RefNumber + ', '  ELSE SPACE(0) END
								+ CASE WHEN DocumentNumber NOT IN (N'0', N'-0', SPACE(0))	THEN N'Belg.No:' + DocumentNumber + ', '				ELSE SPACE(0) END

								+ CASE WHEN trCashLine.CurrAccTypeCode IN (4,8)
									THEN ISNULL((SELECT FirstLastName FROM cdCurrAcc WITH(NOLOCK) WHERE cdCurrAcc.CurrAccTypeCode = trCashLine.CurrAccTypeCode AND cdCurrAcc.CurrAccCode = trCashLine.CurrAccCode ),SPACE(0))
									ELSE ISNULL((SELECT CurrAccDescription FROM cdCurrAccDesc WITH(NOLOCK) WHERE cdCurrAccDesc.CurrAccTypeCode = trCashLine.CurrAccTypeCode AND cdCurrAccDesc.CurrAccCode = trCashLine.CurrAccCode AND cdCurrAccDesc.LangCode = N'TR'),SPACE(0)) END + ', '
								+ trCashLine.LineDescription


	, trCashHeader.ApplicationCode 
	, ApplicationDescription	= ISNULL((SELECT ApplicationDescription FROM bsApplicationDesc WITH(NOLOCK) WHERE bsApplicationDesc.ApplicationCode = trCashHeader.ApplicationCode AND bsApplicationDesc.LangCode = N'TR') , SPACE(0))
	
	, CurrAccTypeCode			= trCashLine.CurrAccTypeCode	
	, CurrAccTypeDescription	= ISNULL((SELECT CurrAccTypeDescription FROM bsCurrAccTypeDesc WITH(NOLOCK) WHERE bsCurrAccTypeDesc.CurrAccTypeCode = trCashLine.CurrAccTypeCode AND bsCurrAccTypeDesc.LangCode = N'TR') , SPACE(0))
	, CurrAccCode				= trCashLine.CurrAccCode
	, CurrAccDescription		= CASE WHEN trCashLine.CurrAccTypeCode IN (4,8) THEN ISNULL((SELECT FirstLastName FROM cdCurrAcc WITH(NOLOCK) WHERE cdCurrAcc.CurrAccTypeCode = trCashLine.CurrAccTypeCode AND cdCurrAcc.CurrAccCode = trCashLine.CurrAccCode), SPACE(0))
									   ELSE ISNULL((SELECT CurrAccDescription FROM cdCurrAccDesc WITH(NOLOCK) WHERE cdCurrAccDesc.CurrAccTypeCode = trCashLine.CurrAccTypeCode AND cdCurrAccDesc.CurrAccCode = trCashLine.CurrAccCode AND cdCurrAccDesc.LangCode = N'TR'), SPACE(0)) END
		
	, GLAccCode					= trCashLine.GLAccCode
	, GLAccDescription			= ISNULL((SELECT GLAccDescription FROM cdGLAccDesc WITH(NOLOCK) WHERE cdGLAccDesc.CompanyCode = trCashHeader.CompanyCode AND cdGLAccDesc.GLAccCode = trCashLine.GLAccCode AND cdGLAccDesc.LangCode = N'TR') , SPACE(0))
	
	, LineDescription			= trCashLine.LineDescription 

	, ChequeTransTypeDescription = ISNULL((SELECT ChequeTransTypeDescription		
												FROM trChequeHeader WITH(NOLOCK)												
												LEFT OUTER JOIN bsChequeTransTypeDesc WITH(NOLOCK) ON bsChequeTransTypeDesc.ChequeTransTypeCode = trChequeHeader.ChequeTransTypeCode AND bsChequeTransTypeDesc.LangCode = N'TR'
											WHERE trChequeHeader.ChequeHeaderID = trCashHeader.ApplicationID), SPACE(0))

	, Doc_CurrencyCode			= (trCashLineCurrencyDoc.CurrencyCode)
	, Doc_Debit					= (trCashLineCurrencyDoc.Debit) 
	, Doc_Credit				= (trCashLineCurrencyDoc.Credit)
	, Doc_Balance				= (trCashLineCurrencyDoc.Debit - trCashLineCurrencyDoc.Credit)
	, Loc_CurrencyCode			= trCashLineCurrency.CurrencyCode
	, Loc_ExchangeRate			= trCashLineCurrency.ExchangeRate 
	, Loc_Debit					= (trCashLineCurrency.Debit) 
	, Loc_Credit				= (trCashLineCurrency.Credit )
	, Loc_Balance				= (trCashLineCurrency.Debit - trCashLineCurrency.Credit )
	   
	, ATAtt01 , ATAtt02 , ATAtt03 , ATAtt04 , ATAtt05
	, trCashHeader.CompanyCode
	, trCashHeader.OfficeCode
	, trCashHeader.StoreCode
	, IsTurnover = CAST(0 AS BIT)

	, IsPostingJournal		= trCashHeader.IsPostingJournal
	, JournalDate			= trCashHeader.JournalDate
	, JournalNumber			= ISNULL((SELECT TOP 1 JournalNumber FROM trJournalHeader WITH(NOLOCK) WHERE trJournalHeader.ApplicationCode = trCashHeader.ApplicationCode AND trJournalHeader.ApplicationID = trCashHeader.ApplicationID), SPACE(0))
	, ImportFileNumber		= trCashHeader.ImportFileNumber
	, ExportFileNumber		= trCashHeader.ExportFileNumber
	
	, CashAccountCode			= CASE WHEN CashTransTypeCode = 3 THEN trCashLine.CurrAccCode 
									   WHEN CashTransTypeCode = 8 AND trCashLine.GLAccCode <> SPACE(0) THEN SPACE(0)
									   ELSE trCashHeader.CashCurrAccCode END trCashHeader.CashCurrAccCode = @cashAccountCode
	
	, LastUpdatedUserName	= trCashHeader.LastUpdatedUserName
	, LastUpdatedDate		= trCashHeader.LastUpdatedDate
	, LinkedApplicationCode	= trCashHeader.ApplicationCode
	, LinkedApplicationID	= CASE WHEN trCashHeader.ApplicationCode = 'Order' 
								   THEN ISNULL((SELECT OrderHeaderID FROM trOrderPaymentPlan WITH(NOLOCK) WHERE trOrderPaymentPlan.OrderPaymentPlanID = trCashHeader.ApplicationID),'00000000-0000-0000-0000-000000000000') 
								   ELSE trCashHeader.ApplicationID END
		
	FROM trCashHeader WITH(NOLOCK) 
		INNER JOIN trCashLine WITH(NOLOCK) ON trCashLine.CashHeaderID = trCashHeader.CashHeaderID
		LEFT OUTER JOIN trCashLineCurrency WITH(NOLOCK) ON trCashLineCurrency.CashLineID = trCashLine.CashLineID AND trCashHeader.LocalCurrencyCode = trCashLineCurrency.CurrencyCode
		LEFT OUTER JOIN trCashLineCurrency trCashLineCurrencyDoc WITH(NOLOCK) ON trCashLineCurrencyDoc.CashLineID = trCashLine.CashLineID 
															AND trCashLine.DocCurrencyCode = trCashLineCurrencyDoc.CurrencyCode

		LEFT OUTER JOIN CashATAttributesFilter ON CashATAttributesFilter.CashHeaderID = trCashHeader.CashHeaderID
	WHERE  DocumentDate  BETWEEN @StartDate AND @EndDate	
		AND CASE WHEN CashTransTypeCode = 3 THEN trCashLine.CurrAccCode 
		         WHEN CashTransTypeCode = 8 AND trCashLine.GLAccCode <> SPACE(0) THEN SPACE(0)
		         ELSE trCashHeader.CashCurrAccCode END <> SPACE(0)
) Detail
WHERE EXISTS (
SELECT * FROM (
SELECT * FROM (
SELECT * FROM 
(
	SELECT CashAccountCode				= cdCurrAcc.CurrAccCode
 	  	 , CashAccountDescription		= cdCurrAccDesc.CurrAccDescription
		 , cdCurrAcc.CurrencyCode
	FROM
		cdCurrAcc WITH(NOLOCK)
		LEFT OUTER JOIN CashAccountAttributesFilter ON CashAccountAttributesFilter.CurrAccCode = cdCurrAcc.CurrAccCode
		INNER JOIN cdCurrAccDesc WITH(NOLOCK) ON cdCurrAcc.CurrAccTypeCode = cdCurrAccDesc.CurrAccTypeCode
							AND cdCurrAcc.CurrAccCode = cdCurrAccDesc.CurrAccCode
							AND cdCurrAccDesc.LangCode = N'TR'
	WHERE cdCurrAcc.CurrAccTypeCode  = 7
		AND (1=0
			OR cdCurrAcc.CurrAccCode 
					IN (SELECT DISTINCT CASE WHEN trCashHeader.CashTransTypeCode = 3 THEN trCashLine.CurrAccCode 
									         WHEN trCashHeader.CashTransTypeCode = 8 AND  trCashLine.GLAccCode <> SPACE(0) THEN SPACE(0)
									         ELSE trCashHeader.CashCurrAccCode END 
						FROM  trCashHeader WITH(NOLOCK)
							INNER JOIN trCashLine  WITH(NOLOCK) ON trCashHeader.CashHeaderID = trCashLine.CashHeaderID
							LEFT OUTER JOIN CashATAttributesFilter  ON CashATAttributesFilter.CashHeaderID = trCashHeader.CashHeaderID
						WHERE trCashHeader.DocumentDate BETWEEN @StartDate AND @EndDate	
							--AND trCashHeader.DocCurrencyCode = cdCurrAcc.CurrencyCode		
							AND trCashHeader.CashCurrAccTypeCode = cdCurrAcc.CurrAccTypeCode
							AND CASE WHEN trCashHeader.CashTransTypeCode = 3 THEN trCashLine.CurrAccCode 
									 WHEN trCashHeader.CashTransTypeCode = 8 AND  trCashLine.GLAccCode <> SPACE(0) THEN SPACE(0)
									 ELSE trCashHeader.CashCurrAccCode END = cdCurrAcc.CurrAccCode
							AND trCashHeader.CompanyCode = cdCurrAcc.CompanyCode
			
						)	
			)
		AND cdCurrAcc.CurrAccCode <> SPACE(0)

) AS DATA
WHERE 1=1
) Query
) ReportTable 
WHERE ReportTable.CashAccountCode = Detail.CashAccountCode )
";

                _logger.LogInformation("Executing SQL query via Dapper");
                
                using (var connection = _context.CreateConnection())
                {
                    var transactions = await connection.QueryAsync<CashTransactionResponse>(query, parameters);
                    _logger.LogInformation("SQL query executed successfully. Retrieved {Count} cash transactions", transactions.Count());
                    return transactions.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cash transactions");
                throw;
            }
        }

    public async Task<IEnumerable<CashSummaryResponse>> GetCashSummaryAsync()
{
    _logger.LogInformation("CashTransactionRepository.GetCashSummaryAsync() called");
    
    try
    {
        _logger.LogInformation("Preparing SQL query for cash summary");
        
        var query = @"
SELECT 
    c.CurrAccCode AS CashAccountCode,
    cd.CurrAccDescription AS CashAccountDescription,
    c.CurrencyCode,
    ISNULL(SUM(CASE WHEN ch.DocumentDate < @StartDate THEN 
        CASE 
            WHEN ch.CashTransTypeCode = 1 THEN clc.Credit - clc.Debit  -- Giriş
            WHEN ch.CashTransTypeCode = 2 THEN clc.Debit - clc.Credit  -- Çıkış
            ELSE 0 
        END
    ELSE 0 END), 0) AS OpeningBalance,
    
    ISNULL(SUM(CASE 
        WHEN ch.DocumentDate >= @StartDate AND ch.DocumentDate <= @EndDate 
        AND ch.CashTransTypeCode = 1 THEN clc.Credit - clc.Debit  -- Giriş
        ELSE 0 
    END), 0) AS TotalDebit,
    
    ISNULL(SUM(CASE 
        WHEN ch.DocumentDate >= @StartDate AND ch.DocumentDate <= @EndDate 
        AND ch.CashTransTypeCode = 2 THEN clc.Debit - clc.Credit  -- Çıkış
        ELSE 0 
    END), 0) AS TotalCredit,
    
    ISNULL(SUM(CASE 
        WHEN ch.CashTransTypeCode = 1 THEN clc.Credit - clc.Debit  -- Giriş
        WHEN ch.CashTransTypeCode = 2 THEN clc.Debit - clc.Credit  -- Çıkış
        ELSE 0 
    END), 0) AS ClosingBalance
FROM 
    cdCurrAcc c
    INNER JOIN cdCurrAccDesc cd ON c.CurrAccTypeCode = cd.CurrAccTypeCode AND c.CurrAccCode = cd.CurrAccCode AND cd.LangCode = 'TR'
    LEFT JOIN trCashHeader ch ON c.CurrAccCode = ch.CashCurrAccCode
    LEFT JOIN trCashLine cl ON ch.CashHeaderID = cl.CashHeaderID
    LEFT JOIN trCashLineCurrency clc ON cl.CashLineID = clc.CashLineID AND clc.CurrencyCode = ch.LocalCurrencyCode
WHERE 
    c.CurrAccTypeCode = 7 -- Sadece kasa hesaplarını getir (7: Kasa hesap tipi)
    AND (@CashAccountCode IS NULL OR c.CurrAccCode = @CashAccountCode)
    AND (ch.DocumentDate IS NULL OR ch.DocumentDate <= @EndDate)
GROUP BY 
    c.CurrAccCode, 
    cd.CurrAccDescription, 
    c.CurrencyCode
ORDER BY 
    c.CurrAccCode";

        _logger.LogInformation("Executing cash summary SQL query");
        
        using (var connection = _context.CreateConnection())
        {
            var parameters = new 
            { 
                StartDate = DateTime.Today.AddMonths(-1), // Son 1 aylık veri
                EndDate = DateTime.Today,
                CashAccountCode = (string)null // Tüm hesapları getir
            };
            
            var result = await connection.QueryAsync<CashSummaryResponse>(query, parameters);
            _logger.LogInformation("Successfully retrieved {Count} cash accounts summary", result.Count());
            return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cash summary");
                throw;
            }
        }
    }
}
