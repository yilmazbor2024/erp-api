using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Repositories.Invoice
{
    public class InvoiceReportRepository : IInvoiceReportRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<InvoiceReportRepository> _logger;

        public InvoiceReportRepository(IConfiguration configuration, ILogger<InvoiceReportRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Get invoice summary by invoice types
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summaries by type</returns>
        public async Task<List<InvoiceTypeSummaryResponse>> GetInvoiceTypesSummaryAsync(InvoiceListRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", request.LangCode ?? "TR")
                };

                var whereConditions = new List<string>();

                // Filter by ProcessCode if specified
                if (!string.IsNullOrEmpty(request.ProcessCode))
                {
                    // Case-insensitive comparison for ProcessCode
                    whereConditions.Add("UPPER(trInvoiceHeader.ProcessCode) = UPPER(@ProcessCode)");
                    parameters.Add(new SqlParameter("@ProcessCode", request.ProcessCode));
                    _logger.LogInformation($"Filtering by ProcessCode: {request.ProcessCode}");
                }
                else
                {
                    _logger.LogWarning("ProcessCode not specified, listing all invoices");
                }

                if (!string.IsNullOrEmpty(request.CustomerCode))
                {
                    whereConditions.Add("trInvoiceHeader.CurrAccTypeCode = 3 AND trInvoiceHeader.CurrAccCode = @CustomerCode");
                    parameters.Add(new SqlParameter("@CustomerCode", request.CustomerCode));
                }

                if (!string.IsNullOrEmpty(request.VendorCode))
                {
                    whereConditions.Add("trInvoiceHeader.CurrAccTypeCode = 2 AND trInvoiceHeader.CurrAccCode = @VendorCode");
                    parameters.Add(new SqlParameter("@VendorCode", request.VendorCode));
                }

                if (request.StartDate.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.InvoiceDate >= @StartDate");
                    parameters.Add(new SqlParameter("@StartDate", request.StartDate.Value));
                }

                if (request.EndDate.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.InvoiceDate <= @EndDate");
                    parameters.Add(new SqlParameter("@EndDate", request.EndDate.Value));
                }

                if (request.IsCompleted.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.IsCompleted = @IsCompleted");
                    parameters.Add(new SqlParameter("@IsCompleted", request.IsCompleted.Value));
                }

                if (request.IsSuspended.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.IsSuspended = @IsSuspended");
                    parameters.Add(new SqlParameter("@IsSuspended", request.IsSuspended.Value));
                }

                if (request.IsReturn.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.IsReturn = @IsReturn");
                    parameters.Add(new SqlParameter("@IsReturn", request.IsReturn.Value));
                }

                if (request.IsEInvoice.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.IsEInvoice = @IsEInvoice");
                    parameters.Add(new SqlParameter("@IsEInvoice", request.IsEInvoice.Value));
                }

                // Combine WHERE conditions
                string whereClause = whereConditions.Count > 0 ? "WHERE " + string.Join(" AND ", whereConditions) : "";
                
                // Log SQL query
                _logger.LogInformation($"Invoice Types Summary WHERE clause: {whereClause}");
                _logger.LogInformation($"SQL Parameters: {string.Join(", ", parameters.Select(p => $"{p.ParameterName}={p.Value}"))}");

                // Query to get total amounts by invoice type
                string query = $@"
                    SELECT 
                        InvoiceTypeCode = trInvoiceHeader.InvoiceTypeCode,
                        InvoiceTypeDescription = (SELECT TOP 1 InvoiceTypeDescription FROM InvoiceType(@LangCode) WHERE InvoiceType.InvoiceTypeCode = trInvoiceHeader.InvoiceTypeCode),
                        InvoiceCount = COUNT(DISTINCT trInvoiceHeader.InvoiceHeaderID),
                        TotalAmount = SUM(trInvoiceLine.Price * trInvoiceLine.Qty1),
                        TotalVatAmount = SUM((trInvoiceLine.Price * trInvoiceLine.Qty1) * (trInvoiceLine.VatRate / 100)),
                        TotalGrossAmount = SUM((trInvoiceLine.Price * trInvoiceLine.Qty1) * (1 + (trInvoiceLine.VatRate / 100))),
                        CurrencyCode = MAX(trInvoiceHeader.DocCurrencyCode),
                        Description = MAX(trInvoiceHeader.Description)
                    FROM dbo.trInvoiceHeader WITH (NOLOCK) 
                    INNER JOIN dbo.trInvoiceLine WITH (NOLOCK) ON dbo.trInvoiceLine.InvoiceHeaderID = dbo.trInvoiceHeader.InvoiceHeaderID
                    {whereClause}
                    GROUP BY trInvoiceHeader.InvoiceTypeCode
                    ORDER BY InvoiceTypeCode";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    var result = await connection.QueryAsync<InvoiceTypeSummaryResponse>(query, parameters.ToDictionary(p => p.ParameterName, p => p.Value));
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetInvoiceTypesSummaryAsync");
                throw;
            }
        }

        /// <summary>
        /// Get invoice summary by dates
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summaries by date</returns>
        public async Task<List<InvoiceDateSummaryResponse>> GetInvoiceDatesSummaryAsync(InvoiceListRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", request.LangCode ?? "TR")
                };

                var whereConditions = new List<string>();

                // Filter by ProcessCode if specified
                if (!string.IsNullOrEmpty(request.ProcessCode))
                {
                    // Case-insensitive comparison for ProcessCode
                    whereConditions.Add("UPPER(trInvoiceHeader.ProcessCode) = UPPER(@ProcessCode)");
                    parameters.Add(new SqlParameter("@ProcessCode", request.ProcessCode));
                    _logger.LogInformation($"Filtering by ProcessCode: {request.ProcessCode}");
                }
                else
                {
                    _logger.LogWarning("ProcessCode not specified, listing all invoices");
                }

                if (!string.IsNullOrEmpty(request.CustomerCode))
                {
                    whereConditions.Add("trInvoiceHeader.CurrAccTypeCode = 3 AND trInvoiceHeader.CurrAccCode = @CustomerCode");
                    parameters.Add(new SqlParameter("@CustomerCode", request.CustomerCode));
                }

                if (!string.IsNullOrEmpty(request.VendorCode))
                {
                    whereConditions.Add("trInvoiceHeader.CurrAccTypeCode = 2 AND trInvoiceHeader.CurrAccCode = @VendorCode");
                    parameters.Add(new SqlParameter("@VendorCode", request.VendorCode));
                }

                if (request.StartDate.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.InvoiceDate >= @StartDate");
                    parameters.Add(new SqlParameter("@StartDate", request.StartDate.Value));
                }

                if (request.EndDate.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.InvoiceDate <= @EndDate");
                    parameters.Add(new SqlParameter("@EndDate", request.EndDate.Value));
                }

                if (request.IsCompleted.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.IsCompleted = @IsCompleted");
                    parameters.Add(new SqlParameter("@IsCompleted", request.IsCompleted.Value));
                }

                if (request.IsSuspended.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.IsSuspended = @IsSuspended");
                    parameters.Add(new SqlParameter("@IsSuspended", request.IsSuspended.Value));
                }

                if (request.IsReturn.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.IsReturn = @IsReturn");
                    parameters.Add(new SqlParameter("@IsReturn", request.IsReturn.Value));
                }

                if (request.IsEInvoice.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.IsEInvoice = @IsEInvoice");
                    parameters.Add(new SqlParameter("@IsEInvoice", request.IsEInvoice.Value));
                }

                // Combine WHERE conditions
                string whereClause = whereConditions.Count > 0 ? "WHERE " + string.Join(" AND ", whereConditions) : "";
                
                // Log SQL query
                _logger.LogInformation($"Invoice Dates Summary WHERE clause: {whereClause}");
                _logger.LogInformation($"SQL Parameters: {string.Join(", ", parameters.Select(p => $"{p.ParameterName}={p.Value}"))}");

                // Query to get total amounts by date
                string query = $@"
                    SELECT 
                        InvoiceDate = CAST(trInvoiceHeader.InvoiceDate AS DATE),
                        InvoiceCount = COUNT(DISTINCT trInvoiceHeader.InvoiceHeaderID),
                        TotalAmount = SUM(trInvoiceLine.Price * trInvoiceLine.Qty1),
                        TotalVatAmount = SUM((trInvoiceLine.Price * trInvoiceLine.Qty1) * (trInvoiceLine.VatRate / 100)),
                        TotalGrossAmount = SUM((trInvoiceLine.Price * trInvoiceLine.Qty1) * (1 + (trInvoiceLine.VatRate / 100))),
                        CurrencyCode = MAX(trInvoiceHeader.DocCurrencyCode),
                        Description = MAX(trInvoiceHeader.Description)
                    FROM dbo.trInvoiceHeader WITH (NOLOCK) 
                    INNER JOIN dbo.trInvoiceLine WITH (NOLOCK) ON dbo.trInvoiceLine.InvoiceHeaderID = dbo.trInvoiceHeader.InvoiceHeaderID
                    {whereClause}
                    GROUP BY CAST(trInvoiceHeader.InvoiceDate AS DATE)
                    ORDER BY InvoiceDate DESC";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    var result = await connection.QueryAsync<InvoiceDateSummaryResponse>(query, parameters.ToDictionary(p => p.ParameterName, p => p.Value));
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetInvoiceDatesSummaryAsync");
                throw;
            }
        }

        /// <summary>
        /// Get invoice summary by customers
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summaries by customer</returns>
        public async Task<List<InvoiceCustomerSummaryResponse>> GetInvoiceCustomersSummaryAsync(InvoiceListRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", request.LangCode ?? "TR")
                };

                var whereConditions = new List<string>();

                // Filter by ProcessCode if specified
                if (!string.IsNullOrEmpty(request.ProcessCode))
                {
                    // Case-insensitive comparison for ProcessCode
                    whereConditions.Add("UPPER(trInvoiceHeader.ProcessCode) = UPPER(@ProcessCode)");
                    parameters.Add(new SqlParameter("@ProcessCode", request.ProcessCode));
                    _logger.LogInformation($"Filtering by ProcessCode: {request.ProcessCode}");
                }
                else
                {
                    _logger.LogWarning("ProcessCode not specified, listing all invoices");
                }

                if (!string.IsNullOrEmpty(request.CustomerCode))
                {
                    whereConditions.Add("trInvoiceHeader.CurrAccTypeCode = 3 AND trInvoiceHeader.CurrAccCode = @CustomerCode");
                    parameters.Add(new SqlParameter("@CustomerCode", request.CustomerCode));
                }

                if (!string.IsNullOrEmpty(request.VendorCode))
                {
                    whereConditions.Add("trInvoiceHeader.CurrAccTypeCode = 2 AND trInvoiceHeader.CurrAccCode = @VendorCode");
                    parameters.Add(new SqlParameter("@VendorCode", request.VendorCode));
                }

                if (request.StartDate.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.InvoiceDate >= @StartDate");
                    parameters.Add(new SqlParameter("@StartDate", request.StartDate.Value));
                }

                if (request.EndDate.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.InvoiceDate <= @EndDate");
                    parameters.Add(new SqlParameter("@EndDate", request.EndDate.Value));
                }

                if (request.IsCompleted.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.IsCompleted = @IsCompleted");
                    parameters.Add(new SqlParameter("@IsCompleted", request.IsCompleted.Value));
                }

                if (request.IsSuspended.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.IsSuspended = @IsSuspended");
                    parameters.Add(new SqlParameter("@IsSuspended", request.IsSuspended.Value));
                }

                if (request.IsReturn.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.IsReturn = @IsReturn");
                    parameters.Add(new SqlParameter("@IsReturn", request.IsReturn.Value));
                }

                if (request.IsEInvoice.HasValue)
                {
                    whereConditions.Add("trInvoiceHeader.IsEInvoice = @IsEInvoice");
                    parameters.Add(new SqlParameter("@IsEInvoice", request.IsEInvoice.Value));
                }

                // Combine WHERE conditions
                string whereClause = whereConditions.Count > 0 ? "WHERE " + string.Join(" AND ", whereConditions) : "";
                
                // Log SQL query
                _logger.LogInformation($"Invoice Customers Summary WHERE clause: {whereClause}");
                _logger.LogInformation($"SQL Parameters: {string.Join(", ", parameters.Select(p => $"{p.ParameterName}={p.Value}"))}");

                // Query to get total amounts by customer
                string query = $@"
                    SELECT 
                        CustomerCode = trInvoiceHeader.CurrAccCode,
                        CustomerDescription = MAX(CASE 
                                        WHEN trInvoiceHeader.CurrAccTypeCode = 3 THEN 
                                            (SELECT TOP 1 Description FROM cdCurrAcc WHERE CurrAccCode = trInvoiceHeader.CurrAccCode AND CurrAccTypeCode = 3)
                                        WHEN trInvoiceHeader.CurrAccTypeCode = 2 THEN 
                                            (SELECT TOP 1 Description FROM cdCurrAcc WHERE CurrAccCode = trInvoiceHeader.CurrAccCode AND CurrAccTypeCode = 2)
                                        ELSE ''
                                      END),
                        InvoiceCount = COUNT(DISTINCT trInvoiceHeader.InvoiceHeaderID),
                        TotalAmount = SUM(trInvoiceLine.Price * trInvoiceLine.Qty1),
                        TotalVatAmount = SUM((trInvoiceLine.Price * trInvoiceLine.Qty1) * (trInvoiceLine.VatRate / 100)),
                        TotalGrossAmount = SUM((trInvoiceLine.Price * trInvoiceLine.Qty1) * (1 + (trInvoiceLine.VatRate / 100))),
                        CurrencyCode = MAX(trInvoiceHeader.DocCurrencyCode),
                        Description = MAX(trInvoiceHeader.Description)
                    FROM dbo.trInvoiceHeader WITH (NOLOCK) 
                    INNER JOIN dbo.trInvoiceLine WITH (NOLOCK) ON dbo.trInvoiceLine.InvoiceHeaderID = dbo.trInvoiceHeader.InvoiceHeaderID
                    {whereClause}
                    GROUP BY trInvoiceHeader.CurrAccCode, trInvoiceHeader.CurrAccTypeCode
                    ORDER BY CustomerCode";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    var result = await connection.QueryAsync<InvoiceCustomerSummaryResponse>(query, parameters.ToDictionary(p => p.ParameterName, p => p.Value));
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetInvoiceCustomersSummaryAsync");
                throw;
            }
        }

        /// <summary>
        /// Get all invoice summary information
        /// </summary>
        /// <param name="request">Invoice list filter parameters</param>
        /// <returns>Invoice summary information</returns>
        public async Task<InvoiceSummaryListResponse> GetInvoiceSummaryAsync(InvoiceListRequest request)
        {
            try
            {
                var invoiceTypeSummaries = await GetInvoiceTypesSummaryAsync(request);
                var invoiceDateSummaries = await GetInvoiceDatesSummaryAsync(request);
                var invoiceCustomerSummaries = await GetInvoiceCustomersSummaryAsync(request);

                return new InvoiceSummaryListResponse
                {
                    InvoiceTypeSummaries = invoiceTypeSummaries,
                    InvoiceDateSummaries = invoiceDateSummaries,
                    InvoiceCustomerSummaries = invoiceCustomerSummaries,
                    TotalCount = invoiceTypeSummaries.Sum(x => x.InvoiceCount)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetInvoiceSummaryAsync");
                throw;
            }
        }
    }
}
