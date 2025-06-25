using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using ErpMobile.Api.Data;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Common;
using Microsoft.Extensions.Logging;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ErpMobile.Api.Services
{
    public class CustomerFinancialService : ICustomerFinancialService
    {
        private readonly ILogger<CustomerFinancialService> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly ErpDbContext _erpDbContext;

        public CustomerFinancialService(ILogger<CustomerFinancialService> logger, IConfiguration configuration, ErpDbContext erpDbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _erpDbContext = erpDbContext;
        }

        /// <summary>
        /// Müşteri kredi bilgilerini getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <returns>Müşteri kredi bilgileri</returns>
        public async Task<CustomerCreditInfoResponse> GetCustomerCreditInfoAsync(string customerCode)
        {
            try
            {
                _logger.LogInformation("Müşteri kredi bilgileri getiriliyor: {CustomerCode}", customerCode);

                // Önce müşterinin var olup olmadığını kontrol et
                var checkQuery = "SELECT COUNT(1) FROM cdCurrAcc WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3";
                var checkParams = new List<SqlParameter>
                {
                    new SqlParameter("@CustomerCode", customerCode)
                };

                var exists = false;
                using (var connection = new SqlConnection(_connectionString))
                using (var reader = await connection.ExecuteReaderAsync(checkQuery, checkParams.ToArray()))
                {
                    if (await reader.ReadAsync())
                    {
                        exists = Convert.ToInt32(reader[0]) > 0;
                    }
                }

                if (!exists)
                {
                    return null;
                }

                var query = @"
                    SELECT 
                        CurrAccCode = cdCurrAcc.CurrAccCode,
                        CurrAccDescription = ISNULL(cdCurrAccDesc.CurrAccDescription, ''),
                        CreditLimit = ISNULL(cdCurrAcc.CreditLimit, 0),
                        Debit = ISNULL((SELECT SUM(tbc.Debit) FROM trCurrAccBook tb 
                                JOIN trCurrAccBookCurrency tbc ON tb.CurrAccBookID = tbc.CurrAccBookID AND tb.LocalCurrencyCode = tbc.CurrencyCode
                                WHERE tb.CurrAccCode = cdCurrAcc.CurrAccCode AND tb.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode), 0),
                        Credit = ISNULL((SELECT SUM(tbc.Credit) FROM trCurrAccBook tb 
                                JOIN trCurrAccBookCurrency tbc ON tb.CurrAccBookID = tbc.CurrAccBookID AND tb.LocalCurrencyCode = tbc.CurrencyCode
                                WHERE tb.CurrAccCode = cdCurrAcc.CurrAccCode AND tb.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode), 0),
                        Balance = ISNULL((SELECT SUM(tbc.Debit - tbc.Credit) FROM trCurrAccBook tb 
                                JOIN trCurrAccBookCurrency tbc ON tb.CurrAccBookID = tbc.CurrAccBookID AND tb.LocalCurrencyCode = tbc.CurrencyCode
                                WHERE tb.CurrAccCode = cdCurrAcc.CurrAccCode AND tb.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode), 0),
                        OpenRisk = ISNULL((SELECT SUM(Doc_Amount) FROM Cheque 
                                WHERE CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode 
                                AND CurrAccCode = cdCurrAcc.CurrAccCode 
                                AND ((ChequeTypeCode = 1 AND (ChequeTransTypeCode <= 7 OR ChequeTransTypeCode = 12)) 
                                    OR (ChequeTypeCode = 2 AND (ChequeTransTypeCode <= 27 OR ChequeTransTypeCode = 32)))
                                AND IsClosed = 0
                                AND ChequeTransTypeCode <> 0), 0),
                        BalanceAndRisk = ISNULL((SELECT SUM(tbc.Debit - tbc.Credit) FROM trCurrAccBook tb 
                                JOIN trCurrAccBookCurrency tbc ON tb.CurrAccBookID = tbc.CurrAccBookID AND tb.LocalCurrencyCode = tbc.CurrencyCode
                                WHERE tb.CurrAccCode = cdCurrAcc.CurrAccCode AND tb.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode), 0) + 
                                         ISNULL((SELECT SUM(Doc_Amount) FROM Cheque 
                                                WHERE CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode 
                                                AND CurrAccCode = cdCurrAcc.CurrAccCode 
                                                AND ((ChequeTypeCode = 1 AND (ChequeTransTypeCode <= 7 OR ChequeTransTypeCode = 12)) 
                                                    OR (ChequeTypeCode = 2 AND (ChequeTransTypeCode <= 27 OR ChequeTransTypeCode = 32)))
                                                AND IsClosed = 0
                                                AND ChequeTransTypeCode <> 0), 0),
                        RemainingCreditLimit = ISNULL(cdCurrAcc.CreditLimit, 0) - 
                                              (ISNULL((SELECT SUM(tbc.Debit - tbc.Credit) FROM trCurrAccBook tb 
                                                JOIN trCurrAccBookCurrency tbc ON tb.CurrAccBookID = tbc.CurrAccBookID AND tb.LocalCurrencyCode = tbc.CurrencyCode
                                                WHERE tb.CurrAccCode = cdCurrAcc.CurrAccCode AND tb.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode), 0) + 
                                               ISNULL((SELECT SUM(Doc_Amount) FROM Cheque 
                                                        WHERE CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode 
                                                        AND CurrAccCode = cdCurrAcc.CurrAccCode 
                                                        AND ((ChequeTypeCode = 1 AND (ChequeTransTypeCode <= 7 OR ChequeTransTypeCode = 12)) 
                                                            OR (ChequeTypeCode = 2 AND (ChequeTransTypeCode <= 27 OR ChequeTransTypeCode = 32)))
                                                        AND IsClosed = 0
                                                        AND ChequeTransTypeCode <> 0), 0))
                    FROM cdCurrAcc WITH (NOLOCK)
                    LEFT JOIN cdCurrAccDesc WITH (NOLOCK) 
                        ON cdCurrAcc.CurrAccTypeCode = cdCurrAccDesc.CurrAccTypeCode 
                        AND cdCurrAcc.CurrAccCode = cdCurrAccDesc.CurrAccCode
                        AND cdCurrAccDesc.LangCode = 'TR'
                    WHERE cdCurrAcc.CurrAccCode = @CustomerCode
                      AND cdCurrAcc.CurrAccTypeCode = 3";

                var parameters = new SqlParameter[] { new SqlParameter("@CustomerCode", customerCode) };

                CustomerCreditInfoResponse creditInfo = null;

                var result = await _erpDbContext.ExecuteReaderAsync(query, parameters);
                if (result.Read())
                {
                    creditInfo = new CustomerCreditInfoResponse
                    {
                        CustomerCode = result["CurrAccCode"].ToString(),
                        CustomerDescription = result["CurrAccDescription"].ToString(),
                        CreditLimit = Convert.ToDecimal(result["CreditLimit"]),
                        Debit = Convert.ToDecimal(result["Debit"]),
                        Credit = Convert.ToDecimal(result["Credit"]),
                        Balance = Convert.ToDecimal(result["Balance"]),
                        OpenRisk = Convert.ToDecimal(result["OpenRisk"]),
                        BalanceAndRisk = Convert.ToDecimal(result["BalanceAndRisk"]),
                        RemainingCreditLimit = Convert.ToDecimal(result["RemainingCreditLimit"])
                    };
                }

                return creditInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri kredi bilgileri getirilirken hata oluştu. CustomerCode: {CustomerCode}", customerCode);
                throw;
            }
        }

        /// <summary>
        /// Müşteri finansal bilgilerini günceller
        /// </summary>
        /// <param name="request">Güncelleme isteği</param>
        /// <returns>Güncelleme sonucu</returns>
        public async Task<CustomerFinancialUpdateResponse> UpdateCustomerFinancialAsync(CustomerFinancialUpdateRequest request)
        {
            try
            {
                _logger.LogInformation("Müşteri finansal bilgileri güncelleniyor: {CustomerCode}", request.CustomerCode);

                // Önce müşterinin var olup olmadığını kontrol et
                var checkQuery = "SELECT 1 FROM cdCurrAcc WHERE CurrAccCode = @CustomerCode";
                var checkParams = new List<SqlParameter>
                {
                    new SqlParameter("@CustomerCode", request.CustomerCode)
                };

                var exists = false;
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var reader = await connection.ExecuteReaderAsync(checkQuery, checkParams))
                    {
                        if (await reader.ReadAsync())
                        {
                            exists = true;
                        }
                    }
                }

                if (!exists)
                {
                    return new CustomerFinancialUpdateResponse
                    {
                        CustomerCode = request.CustomerCode,
                        Success = false,
                        Message = "Müşteri bulunamadı"
                    };
                }

                // Müşteri finansal bilgilerini güncelle
                var updateQuery = @"
                    UPDATE cdCurrAcc 
                    SET CurrencyCode = @CurrencyCode,
                        CreditLimit = @CreditLimit,
                        PaymentPlanCode = @PaymentPlanCode,
                        ModifiedDate = GETDATE(),
                        ModifiedUserName = @ModifiedUserName
                    WHERE CurrAccCode = @CustomerCode";

                var updateParams = new List<SqlParameter>
                {
                    new SqlParameter("@CustomerCode", request.CustomerCode),
                    new SqlParameter("@CurrencyCode", request.CurrencyCode),
                    new SqlParameter("@CreditLimit", request.CreditLimit ?? (object)DBNull.Value),
                    new SqlParameter("@PaymentPlanCode", request.PaymentPlanCode),
                    new SqlParameter("@ModifiedUserName", request.ModifiedUserName)
                };

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var rowsAffected = await connection.ExecuteAsync(updateQuery, updateParams);

                        if (rowsAffected > 0)
                    {
                        return new CustomerFinancialUpdateResponse
                        {
                            CustomerCode = request.CustomerCode,
                            CreditLimit = request.CreditLimit ?? 0,
                            CurrencyCode = request.CurrencyCode,
                            PaymentPlanCode = request.PaymentPlanCode,
                            Success = true,
                            Message = "Müşteri finansal bilgileri başarıyla güncellendi"
                        };
                    }
                    else
                    {
                        return new CustomerFinancialUpdateResponse
                        {
                            CustomerCode = request.CustomerCode,
                            Success = false,
                            Message = "Müşteri finansal bilgileri güncellenemedi"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri finansal bilgileri güncellenirken hata oluştu. CustomerCode: {CustomerCode}", request.CustomerCode);
                throw;
            }
        }

        /// <summary>
        /// Müşteri işlemlerini getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <returns>Müşteri işlemleri listesi</returns>
        public async Task<List<CustomerTransactionResponse>> GetCustomerTransactionsAsync(string customerCode, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                _logger.LogInformation("Müşteri işlemleri getiriliyor: {CustomerCode}", customerCode);

                var query = @"
                    SELECT 
                        DocumentDate = trCurrAccBook.DocumentDate,
                        DocumentNumber = trCurrAccBook.DocumentNumber,
                        RefNumber = trCurrAccBook.RefNumber,
                        LineDescription = trCurrAccBook.LineDescription,
                        DocCurrencyCode = trCurrAccBook.DocCurrencyCode,
                        Debit = ISNULL(trCurrAccBookCurrency.Debit, 0),
                        Credit = ISNULL(trCurrAccBookCurrency.Credit, 0),
                        Balance = ISNULL(trCurrAccBookCurrency.Debit, 0) - ISNULL(trCurrAccBookCurrency.Credit, 0)
                    FROM trCurrAccBook WITH (NOLOCK)
                    LEFT JOIN trCurrAccBookCurrency WITH (NOLOCK) 
                        ON trCurrAccBook.CurrAccBookID = trCurrAccBookCurrency.CurrAccBookID 
                        AND trCurrAccBook.DocCurrencyCode = trCurrAccBookCurrency.CurrencyCode
                    WHERE trCurrAccBook.CurrAccCode = @CustomerCode
                      AND trCurrAccBook.CurrAccTypeCode = 3";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@CustomerCode", customerCode)
                };

                if (startDate.HasValue)
                {
                    query += " AND trCurrAccBook.DocumentDate >= @StartDate";
                    parameters.Add(new SqlParameter("@StartDate", startDate.Value));
                }

                if (endDate.HasValue)
                {
                    query += " AND trCurrAccBook.DocumentDate <= @EndDate";
                    parameters.Add(new SqlParameter("@EndDate", endDate.Value));
                }

                query += " ORDER BY trCurrAccBook.DocumentDate DESC";

                var transactions = new List<CustomerTransactionResponse>();

                using (var connection = new SqlConnection(_connectionString))
                using (var reader = await connection.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        transactions.Add(new CustomerTransactionResponse
                        {
                            DocumentDate = reader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(reader["DocumentDate"]) : DateTime.MinValue,
                            DocumentNumber = reader["DocumentNumber"]?.ToString(),
                            RefNumber = reader["RefNumber"]?.ToString(),
                            LineDescription = reader["LineDescription"]?.ToString(),
                            DocCurrencyCode = reader["DocCurrencyCode"]?.ToString(),
                            Debit = Convert.ToDecimal(reader["Debit"]),
                            Credit = Convert.ToDecimal(reader["Credit"]),
                            Balance = Convert.ToDecimal(reader["Balance"])
                        });
                    }
                }

                return transactions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri işlemleri getirilirken hata oluştu. CustomerCode: {CustomerCode}", customerCode);
                throw;
            }
        }

        /// <summary>
        /// Müşteri işlemlerini sayfalanmış şekilde getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <param name="pageNumber">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa boyutu</param>
        /// <returns>Sayfalanmış müşteri işlemleri</returns>
        public async Task<PagedResponse<CustomerTransactionResponse>> GetCustomerTransactionsPagedAsync(string customerCode, DateTime? startDate, DateTime? endDate, int pageNumber, int pageSize)
        {
            try
            {
                _logger.LogInformation("Müşteri işlemleri sayfalanmış şekilde getiriliyor: {CustomerCode}, Sayfa: {PageNumber}, Boyut: {PageSize}", customerCode, pageNumber, pageSize);

                // Önce müşterinin var olup olmadığını kontrol et
                var checkQuery = "SELECT COUNT(1) FROM cdCurrAcc WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3";
                var checkParams = new List<SqlParameter>
                {
                    new SqlParameter("@CustomerCode", customerCode)
                };

                var exists = false;
                using (var connection = new SqlConnection(_connectionString))
                using (var reader = await connection.ExecuteReaderAsync(checkQuery, checkParams.ToArray()))
                {
                    if (await reader.ReadAsync())
                    {
                        exists = Convert.ToInt32(reader[0]) > 0;
                    }
                }

                if (!exists)
                {
                    return null;
                }

                // Toplam kayıt sayısını al
                var countQuery = @"
                    SELECT COUNT(*)
                    FROM trCurrAccBook WITH (NOLOCK)
                    WHERE CurrAccCode = @CustomerCode
                    AND CurrAccTypeCode = 3";

                var countParameters = new List<SqlParameter>
                {
                    new SqlParameter("@CustomerCode", customerCode)
                };

                // Tarih filtresi ekle
                if (startDate.HasValue)
                {
                    countQuery += " AND DocumentDate >= @StartDate";
                    countParameters.Add(new SqlParameter("@StartDate", startDate.Value));
                }

                if (endDate.HasValue)
                {
                    countQuery += " AND DocumentDate <= @EndDate";
                    countParameters.Add(new SqlParameter("@EndDate", endDate.Value));
                }

                var totalRecords = 0;
                using (var connection = new SqlConnection(_connectionString))
                using (var reader = await connection.ExecuteReaderAsync(countQuery, countParameters.ToArray()))
                {
                    if (await reader.ReadAsync())
                    {
                        totalRecords = Convert.ToInt32(reader[0]);
                    }
                }

                // Sayfalama bilgilerini hesapla
                var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                var skip = (pageNumber - 1) * pageSize;

                // Ana sorgu
                var query = @"
                    SELECT 
                        TransactionId = trCurrAccBook.CurrAccBookID,
                        TransactionDate = trCurrAccBook.DocumentDate,
                        TransactionTypeCode = trCurrAccBook.TransactionTypeCode,
                        TransactionTypeName = ISNULL((SELECT TOP 1 TransactionTypeName FROM cdTransactionType WHERE TransactionTypeCode = trCurrAccBook.TransactionTypeCode), ''),
                        DocumentNo = trCurrAccBook.DocumentNumber,
                        Description = trCurrAccBook.LineDescription,
                        DebitAmount = ISNULL(trCurrAccBookCurrency.Debit, 0),
                        CreditAmount = ISNULL(trCurrAccBookCurrency.Credit, 0),
                        Balance = ISNULL(trCurrAccBookCurrency.Debit, 0) - ISNULL(trCurrAccBookCurrency.Credit, 0),
                        CurrencyCode = trCurrAccBook.DocCurrencyCode,
                        ExchangeRate = trCurrAccBook.ExchangeRate,
                        CreatedDate = trCurrAccBook.CreatedDate,
                        CreatedUserName = trCurrAccBook.CreatedUserName
                    FROM trCurrAccBook WITH (NOLOCK)
                    LEFT JOIN trCurrAccBookCurrency WITH (NOLOCK) 
                        ON trCurrAccBook.CurrAccBookID = trCurrAccBookCurrency.CurrAccBookID 
                        AND trCurrAccBook.DocCurrencyCode = trCurrAccBookCurrency.CurrencyCode
                    WHERE trCurrAccBook.CurrAccCode = @CustomerCode
                      AND trCurrAccBook.CurrAccTypeCode = 3";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@CustomerCode", customerCode)
                };

                // Tarih filtresi ekle
                if (startDate.HasValue)
                {
                    query += " AND trCurrAccBook.DocumentDate >= @StartDate";
                    parameters.Add(new SqlParameter("@StartDate", startDate.Value));
                }

                if (endDate.HasValue)
                {
                    query += " AND trCurrAccBook.DocumentDate <= @EndDate";
                    parameters.Add(new SqlParameter("@EndDate", endDate.Value));
                }

                query += " ORDER BY trCurrAccBook.DocumentDate DESC, trCurrAccBook.CurrAccBookID DESC";
                
                // SQL Server için sayfalama
                query += " OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                parameters.Add(new SqlParameter("@Skip", skip));
                parameters.Add(new SqlParameter("@Take", pageSize));

                var transactions = new List<CustomerTransactionResponse>();

                using (var connection = new SqlConnection(_connectionString))
                using (var reader = await connection.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        transactions.Add(new CustomerTransactionResponse
                        {
                            CustomerCode = customerCode,
                            TransactionId = Convert.ToInt64(reader["TransactionId"]),
                            TransactionDate = reader["TransactionDate"] != DBNull.Value ? Convert.ToDateTime(reader["TransactionDate"]) : DateTime.MinValue,
                            TransactionTypeCode = Convert.ToInt32(reader["TransactionTypeCode"]),
                            TransactionTypeName = reader["TransactionTypeName"]?.ToString() ?? string.Empty,
                            DocumentNo = reader["DocumentNo"]?.ToString() ?? string.Empty,
                            Description = reader["Description"]?.ToString() ?? string.Empty,
                            DebitAmount = Convert.ToDecimal(reader["DebitAmount"]),
                            CreditAmount = Convert.ToDecimal(reader["CreditAmount"]),
                            Balance = Convert.ToDecimal(reader["Balance"]),
                            CurrencyCode = reader["CurrencyCode"]?.ToString() ?? string.Empty,
                            ExchangeRate = reader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(reader["ExchangeRate"]) : 0,
                            CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue,
                            CreatedUserName = reader["CreatedUserName"]?.ToString() ?? string.Empty
                        });
                    }
                }

                // Sayfalanmış yanıt oluştur
                var pagedResponse = new PagedResponse<CustomerTransactionResponse>
                {
                    Data = transactions,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    HasNextPage = pageNumber < totalPages,
                    HasPreviousPage = pageNumber > 1
                };

                return pagedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri işlemleri sayfalanmış şekilde getirilirken hata oluştu. CustomerCode: {CustomerCode}", customerCode);
                throw;
            }
        }


    }
}
