using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Invoice;
using ErpMobile.Api.Data;

// SqlDataReader için uzantı metodu
public static class SqlDataReaderExtensions
{
    public static bool HasColumn(this SqlDataReader reader, string columnName)
    {
        for (int i = 0; i < reader.FieldCount; i++)
        {
            if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                return true;
        }
        return false;
    }
}

namespace ErpMobile.Api.Repositories.Invoice
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ILogger<InvoiceRepository> _logger;
        private readonly NanoServiceDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly string _connectionString;

 
        public InvoiceRepository(
            ILogger<InvoiceRepository> logger,
            NanoServiceDbContext context,
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            _env = env;
            _connectionString = _configuration.GetConnectionString("ErpConnection");
        }

        

         

       
        // Son fatura numarasını getiren metot
        public async Task<string> GetLastInvoiceNumberByProcessCodeAsync(string processCode)
        {
            try
            {
                // Process kodu kontrolü
                if (string.IsNullOrEmpty(processCode))
                {
                    throw new ArgumentException("Process kodu boş olamaz", nameof(processCode));
                }

                // Veritabanından son fatura numarasını al
                string query = @"
                    SELECT TOP 1 InvoiceNumber 
                    FROM dbo.trInvoiceHeader WITH (NOLOCK)
                    WHERE ProcessCode = @ProcessCode
                    ORDER BY InvoiceHeaderID DESC";

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProcessCode", processCode);
                        var result = await command.ExecuteScalarAsync();
                        return result?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Son fatura numarası alınırken hata oluştu. ProcessCode: {processCode}");
                throw;
            }
        }

        // ProcessCode'a göre fatura tipi açıklamasını döndüren metot
        private string GetInvoiceTypeDescription(string processCode)
        {
            switch (processCode)
            {
                case "WS":
                    return "Toptan Satış";
                case "BP":
                    return "Toptan Alış";
                case "EXP":
                    return "Masraf Satış";
                case "EP":
                    return "Masraf Alış";
                default:
                    return processCode;
            }
        }

       
        // SQL parametrelerini SqlCommand nesnesine ekleyen yardımcı metot
        private void AddParametersToCommand(SqlCommand command, List<SqlParameter> parameters)
        {
            foreach (var parameter in parameters)
            {
                command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
            }
        }

        // Toptan satış faturaları
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetWholesaleInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", request.LangCode ?? "TR")
                };

                var whereConditions = new List<string>();

             
                // String kodları tinyint değerlerine dönüştür
                whereConditions.Add("trInvoiceHeader.ProcessCode IN ('WS')"); // WS=Toptan Satış, TSF=Toptan Satış İade

                if (!string.IsNullOrEmpty(request.InvoiceNumber))
                {
                    whereConditions.Add("trInvoiceHeader.InvoiceNumber LIKE @InvoiceNumber");
                    parameters.Add(new SqlParameter("@InvoiceNumber", $"%{request.InvoiceNumber}%"));
                }

                if (!string.IsNullOrEmpty(request.CustomerCode))
                {
                    whereConditions.Add("trInvoiceHeader.CurrAccTypeCode = 3 AND trInvoiceHeader.CurrAccCode = @CustomerCode");
                    parameters.Add(new SqlParameter("@CustomerCode", request.CustomerCode));
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

                if (!string.IsNullOrEmpty(request.CompanyCode))
                {
                    whereConditions.Add("trInvoiceHeader.CompanyCode = @CompanyCode");
                    parameters.Add(new SqlParameter("@CompanyCode", request.CompanyCode));
                }

                if (!string.IsNullOrEmpty(request.StoreCode))
                {
                    whereConditions.Add("trInvoiceHeader.StoreCode = @StoreCode");
                    parameters.Add(new SqlParameter("@StoreCode", request.StoreCode));
                }

                if (!string.IsNullOrEmpty(request.WarehouseCode))
                {
                    whereConditions.Add("trInvoiceHeader.WarehouseCode = @WarehouseCode");
                    parameters.Add(new SqlParameter("@WarehouseCode", request.WarehouseCode));
                }

                // ProcessCode filtresi (string değeri tinyint'e dönüştür)
                if (!string.IsNullOrEmpty(request.ProcessCode))
                {
                    whereConditions.Add("trInvoiceHeader.ProcessCode = @ProcessCode");
                    parameters.Add(new SqlParameter("@ProcessCode", request.ProcessCode));
                }
                
               

                var whereClause = whereConditions.Count > 0
                    ? $"WHERE {string.Join(" AND ", whereConditions)}"
                    : string.Empty;

                // Sayfalama ve sıralama
                var offset = (request.Page - 1) * request.PageSize;
                var orderBy = $"ORDER BY trInvoiceHeader.{request.SortBy} {request.SortDirection}";
                var pagination = $"OFFSET {offset} ROWS FETCH NEXT {request.PageSize} ROWS ONLY";

                // Toplam kayıt sayısını al
                var countQuery = $@"
                    SELECT COUNT(*)
                    FROM trInvoiceHeader WITH (NOLOCK) 
                    LEFT OUTER JOIN prSubCurrAcc WITH (NOLOCK)
                        ON prSubCurrAcc.SubCurrAccID = trInvoiceHeader.SubCurrAccID            
                    LEFT OUTER JOIN bsApplicationDesc WITH(NOLOCK)
                        ON bsApplicationDesc.ApplicationCode = trInvoiceHeader.ApplicationCode
                        AND bsApplicationDesc.LangCode = @LangCode
                    LEFT OUTER JOIN cdCurrAccDesc WITH (NOLOCK)
                        ON cdCurrAccDesc.CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode 
                        AND cdCurrAccDesc.CurrAccCode = trInvoiceHeader.CurrAccCode
                        AND cdCurrAccDesc.LangCode = @LangCode
                    LEFT OUTER JOIN tpInvoiceHeaderExtension WITH(NOLOCK)
                        ON tpInvoiceHeaderExtension.InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID
                    {whereClause}";

                // Ana sorgu
                var query = $@"
                    SELECT InvoiceNumber = trInvoiceHeader.InvoiceNumber
                         , IsReturn = trInvoiceHeader.IsReturn
                         , IsEInvoice = trInvoiceHeader.IsEInvoice
                         , InvoiceDate = trInvoiceHeader.InvoiceDate
                         , InvoiceTime = trInvoiceHeader.InvoiceTime 
                         , CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode
                         , VendorCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 1 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , VendorDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 1 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END     
                         , CustomerCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 3 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , CustomerDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 3 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END
                         , RetailCustomerCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 4 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , StoreCurrAccCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 5 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , StoreDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 5 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END
                         , EmployeeCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 8 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , FirstLastName = CASE WHEN trInvoiceHeader.CurrAccTypeCode IN (4,8)
                                               THEN ISNULL((SELECT FirstLastName FROM tpInvoicePostalAddress 
                                                            WHERE trInvoiceHeader.InvoiceHeaderID = tpInvoicePostalAddress.InvoiceHeaderID), 
                                                                (SELECT FirstLastName FROM cdCurrAcc 
                                                                     WHERE trInvoiceHeader.CurrAccCode = cdCurrAcc.CurrAccCode 
                                                                        AND trInvoiceHeader.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode)) 
                                                                         ELSE SPACE(0) END    
                         , SubCurrAccCode = ISNULL (SubCurrAccCode , SPACE(0))
                         , SubCurrAccCompanyName = ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
                         , IsCreditSale = trInvoiceHeader.IsCreditSale
                         , ProcessCode = trInvoiceHeader.ProcessCode
                         , TransTypeCode = trInvoiceHeader.TransTypeCode
                         , DocCurrencyCode = trInvoiceHeader.DocCurrencyCode
                         , Series = trInvoiceHeader.Series
                         , SeriesNumber = trInvoiceHeader.SeriesNumber
                         , EInvoiceNumber = trInvoiceHeader.EInvoiceNumber
                         , CompanyCode = trInvoiceHeader.CompanyCode
                         , OfficeCode = trInvoiceHeader.OfficeCode
                         , StoreCode = trInvoiceHeader.StoreCode
                         , WarehouseCode = trInvoiceHeader.WarehouseCode
                         , ImportFileNumber = trInvoiceHeader.ImportFileNumber
                         , ExportFileNumber = trInvoiceHeader.ExportFileNumber
                         , ExportTypeCode = ISNULL(ExportTypeCode, SPACE(0))
                         , PosTerminalID = trInvoiceHeader.PosTerminalID
                         , TaxTypeCode = trInvoiceHeader.TaxTypeCode
                         , IsCompleted = trInvoiceHeader.IsCompleted
                         , IsSuspended = trInvoiceHeader.IsSuspended
                         , IsLocked = trInvoiceHeader.IsLocked
                         , TotalGrossAmount = ISNULL((SELECT SUM(Qty1 * Price) FROM trInvoiceLine WHERE trInvoiceLine.InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID), 0)
                         , TotalVatAmount = ISNULL((SELECT SUM(Qty1 * Price * 0.18) FROM trInvoiceLine WHERE trInvoiceLine.InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID), 0)
                         , TotalDiscountAmount = ISNULL((SELECT SUM(Qty1 * Price * 0.05) FROM trInvoiceLine WHERE trInvoiceLine.InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID), 0)
                         , TotalNetAmount = ISNULL((SELECT SUM(Qty1 * Price * 1.18 * 0.95) FROM trInvoiceLine WHERE trInvoiceLine.InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID), 0)
                         , IsOrderBase = trInvoiceHeader.IsOrderBase
                         , IsShipmentBase = trInvoiceHeader.IsShipmentBase
                         , IsPostingJournal = trInvoiceHeader.IsPostingJournal
                         , JournalNumber = CASE trInvoiceHeader.IsPostingJournal WHEN 0 THEN SPACE(0) ELSE 
                                                   ISNULL(REPLACE(N'QWERTY'+(SELECT DISTINCT N', ' +  trJournalHeader.JournalNumber 
                                                                    FROM trJournalHeader WITH(NOLOCK)  
                                                                    WHERE trJournalHeader.ApplicationCode = N'Invoi'
                                                                 AND trJournalHeader.ApplicationID     = trInvoiceHeader.InvoiceHeaderID
                                                                     FOR XML PATH('')), N'QWERTY, ', SPACE(0)),SPACE(0)) END
                         , IsPrinted = trInvoiceHeader.IsPrinted
                         , ApplicationCode = trInvoiceHeader.ApplicationCode
                         , ApplicationDescription = ISNULL(bsApplicationDesc.ApplicationDescription , SPACE(0))
                         , ApplicationID = trInvoiceHeader.ApplicationID                      
                         , InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID    
                         
                         , FormType = trInvoiceHeader.FormType
                         , DocumentTypeCode = trInvoiceHeader.DocumentTypeCode
                    FROM trInvoiceHeader WITH (NOLOCK) 
                        LEFT OUTER JOIN prSubCurrAcc WITH (NOLOCK)
                            ON prSubCurrAcc.SubCurrAccID = trInvoiceHeader.SubCurrAccID            
                        LEFT OUTER JOIN bsApplicationDesc WITH(NOLOCK)
                            ON bsApplicationDesc.ApplicationCode = trInvoiceHeader.ApplicationCode
                            AND bsApplicationDesc.LangCode = @LangCode
                        LEFT OUTER JOIN cdCurrAccDesc WITH (NOLOCK)
                            ON cdCurrAccDesc.CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode 
                            AND cdCurrAccDesc.CurrAccCode = trInvoiceHeader.CurrAccCode
                            AND cdCurrAccDesc.LangCode = @LangCode    
                        LEFT OUTER JOIN tpInvoiceHeaderExtension WITH(NOLOCK)
                            ON tpInvoiceHeaderExtension.InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID
                    {whereClause}
                    {orderBy}
                    {pagination}";

                // Veritabanı bağlantısı oluştur
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Toplam kayıt sayısını al
                    var countCommand = new SqlCommand(countQuery, connection);
                    foreach (var parameter in parameters)
                    {
                        countCommand.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }
                    var totalCount = (int)await countCommand.ExecuteScalarAsync();

                    // Verileri al
                    var command = new SqlCommand(query, connection);
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }

                    var invoices = new List<InvoiceHeaderModel>();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var invoice = MapToInvoiceHeaderModel(reader);
                            invoices.Add(invoice);
                        }
                    }

                    return (invoices, totalCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan satış faturaları getirilirken hata oluştu");
                throw;
            }
        }

        // Toptan satış faturası detaylarını getir
        public async Task<InvoiceHeaderModel> GetWholesaleInvoiceByIdAsync(string invoiceHeaderId)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@InvoiceHeaderId", invoiceHeaderId),
                    new SqlParameter("@LangCode", "TR")
                };

                var query = @"
                    SELECT InvoiceNumber = trInvoiceHeader.InvoiceNumber
                         , IsReturn = trInvoiceHeader.IsReturn
                         , IsEInvoice = trInvoiceHeader.IsEInvoice
                         , InvoiceDate = trInvoiceHeader.InvoiceDate
                         , InvoiceTime = trInvoiceHeader.InvoiceTime 
                         , CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode
                         , VendorCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 1 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , VendorDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 1 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END     
                         , CustomerCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 3 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , CustomerDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 3 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END
                         , RetailCustomerCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 4 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , StoreCurrAccCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 5 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , StoreDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 5 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END
                         , EmployeeCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 8 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , FirstLastName = CASE WHEN trInvoiceHeader.CurrAccTypeCode IN (4,8)
                                               THEN ISNULL((SELECT FirstLastName FROM tpInvoicePostalAddress 
                                                            WHERE trInvoiceHeader.InvoiceHeaderID = tpInvoicePostalAddress.InvoiceHeaderID), 
                                                                (SELECT FirstLastName FROM cdCurrAcc 
                                                                     WHERE trInvoiceHeader.CurrAccCode = cdCurrAcc.CurrAccCode 
                                                                        AND trInvoiceHeader.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode)) 
                                                                         ELSE SPACE(0) END    
                         , SubCurrAccCode = ISNULL (SubCurrAccCode , SPACE(0))
                         , SubCurrAccCompanyName = ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
                         , IsCreditSale = trInvoiceHeader.IsCreditSale
                         , ProcessCode = trInvoiceHeader.ProcessCode
                         , TransTypeCode = trInvoiceHeader.TransTypeCode
                         , DocCurrencyCode = trInvoiceHeader.DocCurrencyCode
                         , Series = trInvoiceHeader.Series
                         , SeriesNumber = trInvoiceHeader.SeriesNumber
                         , EInvoiceNumber = trInvoiceHeader.EInvoiceNumber
                         , CompanyCode = trInvoiceHeader.CompanyCode
                         , OfficeCode = trInvoiceHeader.OfficeCode
                         , StoreCode = trInvoiceHeader.StoreCode
                         , WarehouseCode = trInvoiceHeader.WarehouseCode
                         , ImportFileNumber = trInvoiceHeader.ImportFileNumber
                         , ExportFileNumber = trInvoiceHeader.ExportFileNumber
                         , ExportTypeCode = ISNULL(ExportTypeCode, SPACE(0))
                         , PosTerminalID = trInvoiceHeader.PosTerminalID
                         , TaxTypeCode = trInvoiceHeader.TaxTypeCode
                         , IsCompleted = trInvoiceHeader.IsCompleted
                         , IsSuspended = trInvoiceHeader.IsSuspended
                         , IsLocked = trInvoiceHeader.IsLocked
                         , IsOrderBase = trInvoiceHeader.IsOrderBase
                         , IsShipmentBase = trInvoiceHeader.IsShipmentBase
                         , IsPostingJournal = trInvoiceHeader.IsPostingJournal
                         , JournalNumber = CASE trInvoiceHeader.IsPostingJournal WHEN 0 THEN SPACE(0) ELSE 
                                                   ISNULL(REPLACE(N'QWERTY'+(SELECT DISTINCT N', ' +  trJournalHeader.JournalNumber 
                                                                    FROM trJournalHeader WITH(NOLOCK)  
                                                                    WHERE trJournalHeader.ApplicationCode = N'Invoi'
                                                                 AND trJournalHeader.ApplicationID     = trInvoiceHeader.InvoiceHeaderID
                                                                     FOR XML PATH('')), N'QWERTY, ', SPACE(0)),SPACE(0)) END
                         , IsPrinted = trInvoiceHeader.IsPrinted
                         , ApplicationCode = trInvoiceHeader.ApplicationCode
                         , ApplicationDescription = ISNULL(bsApplicationDesc.ApplicationDescription , SPACE(0))
                         , ApplicationID = trInvoiceHeader.ApplicationID                      
                         , InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID    
                         , FormType = trInvoiceHeader.FormType
                         , DocumentTypeCode = trInvoiceHeader.DocumentTypeCode
                    FROM trInvoiceHeader WITH (NOLOCK) 
                        LEFT OUTER JOIN prSubCurrAcc WITH (NOLOCK)
                            ON prSubCurrAcc.SubCurrAccID = trInvoiceHeader.SubCurrAccID            
                        LEFT OUTER JOIN bsApplicationDesc WITH(NOLOCK)
                            ON bsApplicationDesc.ApplicationCode = trInvoiceHeader.ApplicationCode
                            AND bsApplicationDesc.LangCode = @LangCode
                        LEFT OUTER JOIN cdCurrAccDesc WITH (NOLOCK)
                            ON cdCurrAccDesc.CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode 
                            AND cdCurrAccDesc.CurrAccCode = trInvoiceHeader.CurrAccCode
                            AND cdCurrAccDesc.LangCode = @LangCode    
                        LEFT OUTER JOIN tpInvoiceHeaderExtension WITH(NOLOCK)
                            ON tpInvoiceHeaderExtension.InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID
                    WHERE trInvoiceHeader.InvoiceHeaderID = @InvoiceHeaderId
                      AND trInvoiceHeader.ProcessCode IN ('WS')";

                // Veritabanı bağlantısı oluştur
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Verileri al
                    var command = new SqlCommand(query, connection);
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new InvoiceHeaderModel
                            {
                                InvoiceNumber = reader["InvoiceNumber"].ToString(),
                                IsReturn = (bool)reader["IsReturn"],
                                IsEInvoice = (bool)reader["IsEInvoice"],
                                
                                InvoiceDate = (DateTime)reader["InvoiceDate"],
                                InvoiceTime = reader["InvoiceTime"].ToString(),
                                CurrAccTypeCode = (int)reader["CurrAccTypeCode"],
                                VendorCode = reader["VendorCode"].ToString(),
                                VendorDescription = reader["VendorDescription"].ToString(),
                                CustomerCode = reader["CustomerCode"].ToString(),
                                CustomerDescription = reader["CustomerDescription"].ToString(),
                                RetailCustomerCode = reader["RetailCustomerCode"].ToString(),
                                StoreCurrAccCode = reader["StoreCurrAccCode"].ToString(),
                                StoreDescription = reader["StoreDescription"].ToString(),
                                EmployeeCode = reader["EmployeeCode"].ToString(),
                                FirstLastName = reader["FirstLastName"].ToString(),
                                SubCurrAccCode = reader["SubCurrAccCode"].ToString(),
                                SubCurrAccCompanyName = reader["SubCurrAccCompanyName"].ToString(),
                                IsCreditSale = (bool)reader["IsCreditSale"],
                                ProcessCode = reader["ProcessCode"].ToString(),
                                TransTypeCode = reader["TransTypeCode"].ToString(),
                                DocCurrencyCode = reader["DocCurrencyCode"].ToString(),
                                Series = reader["Series"].ToString(),
                                SeriesNumber = reader["SeriesNumber"].ToString(),
                                EInvoiceNumber = reader["EInvoiceNumber"].ToString(),
                                CompanyCode = reader["CompanyCode"].ToString(),
                                OfficeCode = reader["OfficeCode"].ToString(),
                                StoreCode = reader["StoreCode"].ToString(),
                                WarehouseCode = reader["WarehouseCode"].ToString(),
                                ImportFileNumber = reader["ImportFileNumber"].ToString(),
                                ExportFileNumber = reader["ExportFileNumber"].ToString(),
                                ExportTypeCode = reader["ExportTypeCode"].ToString(),
                                PosTerminalID = reader["PosTerminalID"].ToString(),
                                TaxTypeCode = reader["TaxTypeCode"].ToString(),
                                IsCompleted = (bool)reader["IsCompleted"],
                                IsSuspended = (bool)reader["IsSuspended"],
                                IsLocked = (bool)reader["IsLocked"],
                                IsOrderBase = (bool)reader["IsOrderBase"],
                                IsShipmentBase = (bool)reader["IsShipmentBase"],
                                IsPostingJournal = (bool)reader["IsPostingJournal"],
                                JournalNumber = reader["JournalNumber"].ToString(),
                                IsPrinted = (bool)reader["IsPrinted"],
                                ApplicationCode = reader["ApplicationCode"].ToString(),
                                ApplicationDescription = reader["ApplicationDescription"].ToString(),
                                ApplicationID = reader["ApplicationID"].ToString(),
                                InvoiceHeaderID = reader["InvoiceHeaderID"].ToString(),
                                FormType = reader["FormType"].ToString(),
                                DocumentTypeCode = reader["DocumentTypeCode"].ToString(),
                                Status = (bool)reader["IsCompleted"] ? "Tamamlandı" : "Bekliyor"
                            };
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatura getirilirken hata oluştu");
                throw;
            }
        }

        // Toptan satış faturası detaylarını getir (int parametre ile)
        public async Task<InvoiceHeaderModel> GetWholesaleInvoiceByIdAsync(int invoiceHeaderId)
        {
            return await GetWholesaleInvoiceByIdAsync(invoiceHeaderId.ToString());
        }

        // Fatura detaylarını getir
        public async Task<List<InvoiceDetailModel>> GetInvoiceDetailsAsync(string invoiceHeaderId)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@InvoiceHeaderId", invoiceHeaderId),
                    new SqlParameter("@LangCode", "TR")
                };

                var query = @"
                    SELECT 
                        InvoiceDetailID = trInvoiceDetail.InvoiceDetailID,
                        InvoiceHeaderID = trInvoiceDetail.InvoiceHeaderID,
                        LineNumber = trInvoiceDetail.LineNumber,
                        ProductCode = trInvoiceDetail.ProductCode,
                        ProductDescription = ISNULL(cdProductDesc.ProductDescription, SPACE(0)),
                        Qty = trInvoiceDetail.Qty,
                        UnitCode = trInvoiceDetail.UnitCode,
                        UnitPrice = trInvoiceDetail.UnitPrice,
                        DiscountRate = trInvoiceDetail.DiscountRate,
                        VatRate = trInvoiceDetail.VatRate,
                        Amount = trInvoiceDetail.Amount,
                        DiscountAmount = trInvoiceDetail.DiscountAmount,
                        VatAmount = trInvoiceDetail.VatAmount,
                        LineNetAmount = trInvoiceDetail.LineNetAmount,
                        LineTotalAmount = trInvoiceDetail.LineTotalAmount,
                        CurrencyCode = trInvoiceDetail.CurrencyCode,
                        ExchangeRate = trInvoiceDetail.ExchangeRate,
                        IsGift = trInvoiceDetail.IsGift,
                        IsPromotional = trInvoiceDetail.IsPromotional,
                        WarehouseCode = trInvoiceDetail.WarehouseCode,
                        SalesPersonCode = trInvoiceDetail.SalesPersonCode,
                        ProductTypeCode = trInvoiceDetail.ProductTypeCode,
                        PromotionCode = trInvoiceDetail.PromotionCode
                    FROM trInvoiceDetail WITH (NOLOCK)
                    LEFT OUTER JOIN cdProductDesc WITH (NOLOCK)
                        ON cdProductDesc.ProductCode = trInvoiceDetail.ProductCode
                        AND cdProductDesc.LangCode = @LangCode
                    WHERE trInvoiceDetail.InvoiceHeaderID = @InvoiceHeaderId
                    ORDER BY trInvoiceDetail.LineNumber";

                // Veritabanı bağlantısı oluştur
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Verileri al
                    var command = new SqlCommand(query, connection);
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }

                    var details = new List<InvoiceDetailModel>();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var detail = new InvoiceDetailModel
                            {
                                InvoiceDetailID = Convert.ToInt32(reader["InvoiceDetailID"]),
                                InvoiceHeaderID = reader["InvoiceHeaderID"].ToString(),
                                LineNumber = Convert.ToInt32(reader["LineNumber"]),
                                ProductCode = reader["ProductCode"].ToString(),
                                ProductDescription = reader["ProductDescription"].ToString(),
                                Qty = Convert.ToDecimal(reader["Qty"]),
                                UnitCode = reader["UnitCode"].ToString(),
                                UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                DiscountRate = Convert.ToDecimal(reader["DiscountRate"]),
                                VatRate = Convert.ToDecimal(reader["VatRate"]),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                                VatAmount = Convert.ToDecimal(reader["VatAmount"]),
                                LineNetAmount = Convert.ToDecimal(reader["LineNetAmount"]),
                                LineTotalAmount = Convert.ToDecimal(reader["LineTotalAmount"]),
                                CurrencyCode = reader["CurrencyCode"].ToString(),
                                ExchangeRate = Convert.ToDecimal(reader["ExchangeRate"]),
                                IsGift = (bool)reader["IsGift"],
                                IsPromotional = (bool)reader["IsPromotional"],
                                WarehouseCode = reader["WarehouseCode"].ToString(),
                                SalesPersonCode = reader["SalesPersonCode"].ToString(),
                                ProductTypeCode = reader["ProductTypeCode"].ToString(),
                                PromotionCode = reader["PromotionCode"].ToString()
                            };

                            details.Add(detail);
                        }
                    }

                    return details;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatura detayları getirilirken hata oluştu");
                throw;
            }
        }

        // Fatura detaylarını getir (int parametre ile)
        public async Task<List<InvoiceDetailModel>> GetInvoiceDetailsAsync(int invoiceHeaderId)
        {
            return await GetInvoiceDetailsAsync(invoiceHeaderId.ToString());
        }

        // Toptan alış faturaları
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetWholesalePurchaseInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", request.LangCode ?? "TR")
                };

                var whereConditions = new List<string>();

                // ProcessCode filtresi (Toptan Alış Faturaları için)
                whereConditions.Add("trInvoiceHeader.ProcessCode IN ('BP')");

                if (!string.IsNullOrEmpty(request.InvoiceNumber))
                {
                    whereConditions.Add("trInvoiceHeader.InvoiceNumber LIKE @InvoiceNumber");
                    parameters.Add(new SqlParameter("@InvoiceNumber", $"%{request.InvoiceNumber}%"));
                }

                if (!string.IsNullOrEmpty(request.VendorCode))
                {
                    whereConditions.Add("trInvoiceHeader.CurrAccTypeCode = 1 AND trInvoiceHeader.CurrAccCode = @VendorCode");
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

                if (!string.IsNullOrEmpty(request.CompanyCode))
                {
                    whereConditions.Add("trInvoiceHeader.CompanyCode = @CompanyCode");
                    parameters.Add(new SqlParameter("@CompanyCode", request.CompanyCode));
                }

                if (!string.IsNullOrEmpty(request.StoreCode))
                {
                    whereConditions.Add("trInvoiceHeader.StoreCode = @StoreCode");
                    parameters.Add(new SqlParameter("@StoreCode", request.StoreCode));
                }

                if (!string.IsNullOrEmpty(request.WarehouseCode))
                {
                    whereConditions.Add("trInvoiceHeader.WarehouseCode = @WarehouseCode");
                    parameters.Add(new SqlParameter("@WarehouseCode", request.WarehouseCode));
                }

  

                var whereClause = whereConditions.Count > 0
                    ? $"WHERE {string.Join(" AND ", whereConditions)}"
                    : string.Empty;

                // Sayfalama ve sıralama
                var offset = (request.Page - 1) * request.PageSize;
                var orderBy = $"ORDER BY trInvoiceHeader.{request.SortBy} {request.SortDirection}";
                var pagination = $"OFFSET {offset} ROWS FETCH NEXT {request.PageSize} ROWS ONLY";

                // Toplam kayıt sayısını al
                var countQuery = $@"
                    SELECT COUNT(*)
                    FROM trInvoiceHeader WITH (NOLOCK) 
                    LEFT OUTER JOIN prSubCurrAcc WITH (NOLOCK)
                        ON prSubCurrAcc.SubCurrAccID = trInvoiceHeader.SubCurrAccID            
                    LEFT OUTER JOIN bsApplicationDesc WITH(NOLOCK)
                        ON bsApplicationDesc.ApplicationCode = trInvoiceHeader.ApplicationCode
                        AND bsApplicationDesc.LangCode = @LangCode
                    LEFT OUTER JOIN cdCurrAccDesc WITH (NOLOCK)
                        ON cdCurrAccDesc.CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode 
                        AND cdCurrAccDesc.CurrAccCode = trInvoiceHeader.CurrAccCode
                        AND cdCurrAccDesc.LangCode = @LangCode
                    LEFT OUTER JOIN tpInvoiceHeaderExtension WITH(NOLOCK)
                        ON tpInvoiceHeaderExtension.InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID
                    {whereClause}";

                // Ana sorgu
                var query = $@"
                    SELECT InvoiceNumber = trInvoiceHeader.InvoiceNumber
                         , IsReturn = trInvoiceHeader.IsReturn
                         , IsEInvoice = trInvoiceHeader.IsEInvoice
                          , InvoiceDate = trInvoiceHeader.InvoiceDate
                         , InvoiceTime = trInvoiceHeader.InvoiceTime 
                         , CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode
                          , VendorCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 1 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , VendorDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 1 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END     
                         , CustomerCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 3 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , CustomerDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 3 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END
                         , RetailCustomerCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 4 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , StoreCurrAccCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 5 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , StoreDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 5 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END
                         , EmployeeCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 8 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , FirstLastName = CASE WHEN trInvoiceHeader.CurrAccTypeCode IN (4,8)
                                               THEN ISNULL((SELECT FirstLastName FROM tpInvoicePostalAddress 
                                                            WHERE trInvoiceHeader.InvoiceHeaderID = tpInvoicePostalAddress.InvoiceHeaderID), 
                                                                (SELECT FirstLastName FROM cdCurrAcc 
                                                                     WHERE trInvoiceHeader.CurrAccCode = cdCurrAcc.CurrAccCode 
                                                                        AND trInvoiceHeader.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode)) 
                                                                         ELSE SPACE(0) END    
                         , SubCurrAccCode = ISNULL (SubCurrAccCode , SPACE(0))
                         , SubCurrAccCompanyName = ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
                         , IsCreditSale = trInvoiceHeader.IsCreditSale
                         , ProcessCode = trInvoiceHeader.ProcessCode
                         , TransTypeCode = trInvoiceHeader.TransTypeCode
                         , DocCurrencyCode = trInvoiceHeader.DocCurrencyCode
                         , Series = trInvoiceHeader.Series
                         , SeriesNumber = trInvoiceHeader.SeriesNumber
                         , EInvoiceNumber = trInvoiceHeader.EInvoiceNumber
                         , CompanyCode = trInvoiceHeader.CompanyCode
                         , OfficeCode = trInvoiceHeader.OfficeCode
                         , StoreCode = trInvoiceHeader.StoreCode
                         , WarehouseCode = trInvoiceHeader.WarehouseCode
                         , ImportFileNumber = trInvoiceHeader.ImportFileNumber
                         , ExportFileNumber = trInvoiceHeader.ExportFileNumber
                         , ExportTypeCode = ISNULL(ExportTypeCode, SPACE(0))
                         , PosTerminalID = trInvoiceHeader.PosTerminalID
                         , TaxTypeCode = trInvoiceHeader.TaxTypeCode
                         , IsCompleted = trInvoiceHeader.IsCompleted
                         , IsSuspended = trInvoiceHeader.IsSuspended
                         , IsLocked = trInvoiceHeader.IsLocked
                         , IsOrderBase = trInvoiceHeader.IsOrderBase
                         , IsShipmentBase = trInvoiceHeader.IsShipmentBase
                         , IsPostingJournal = trInvoiceHeader.IsPostingJournal
                         , JournalNumber = CASE trInvoiceHeader.IsPostingJournal WHEN 0 THEN SPACE(0) ELSE 
                                                   ISNULL(REPLACE(N'QWERTY'+(SELECT DISTINCT N', ' +  trJournalHeader.JournalNumber 
                                                                    FROM trJournalHeader WITH(NOLOCK)  
                                                                    WHERE trJournalHeader.ApplicationCode = N'Invoi'
                                                                 AND trJournalHeader.ApplicationID     = trInvoiceHeader.InvoiceHeaderID
                                                                     FOR XML PATH('')), N'QWERTY, ', SPACE(0)),SPACE(0)) END
                         , IsPrinted = trInvoiceHeader.IsPrinted
                         , ApplicationCode = trInvoiceHeader.ApplicationCode
                         , ApplicationDescription = ISNULL(bsApplicationDesc.ApplicationDescription , SPACE(0))
                         , ApplicationID = trInvoiceHeader.ApplicationID                      
                         , InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID  
                         , FormType = trInvoiceHeader.FormType
                         , DocumentTypeCode = trInvoiceHeader.DocumentTypeCode
                    FROM trInvoiceHeader WITH (NOLOCK) 
                        LEFT OUTER JOIN prSubCurrAcc WITH (NOLOCK)
                            ON prSubCurrAcc.SubCurrAccID = trInvoiceHeader.SubCurrAccID            
                        LEFT OUTER JOIN bsApplicationDesc WITH(NOLOCK)
                            ON bsApplicationDesc.ApplicationCode = trInvoiceHeader.ApplicationCode
                            AND bsApplicationDesc.LangCode = @LangCode
                        LEFT OUTER JOIN cdCurrAccDesc WITH (NOLOCK)
                            ON cdCurrAccDesc.CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode 
                            AND cdCurrAccDesc.CurrAccCode = trInvoiceHeader.CurrAccCode
                            AND cdCurrAccDesc.LangCode = @LangCode    
                        LEFT OUTER JOIN tpInvoiceHeaderExtension WITH(NOLOCK)
                            ON tpInvoiceHeaderExtension.InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID
                    {whereClause}
                    {orderBy}
                    {pagination}";

                // Veritabanı bağlantısı oluştur
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Toplam kayıt sayısını al
                    var countCommand = new SqlCommand(countQuery, connection);
                    foreach (var parameter in parameters)
                    {
                        countCommand.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }
                    var totalCount = (int)await countCommand.ExecuteScalarAsync();

                    // Verileri al
                    var command = new SqlCommand(query, connection);
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }

                    var invoices = new List<InvoiceHeaderModel>();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var invoice = new InvoiceHeaderModel
                            {
                                InvoiceNumber = reader["InvoiceNumber"].ToString(),
                                IsReturn = Convert.ToBoolean(reader["IsReturn"]),
                                IsEInvoice = Convert.ToBoolean(reader["IsEInvoice"]),
                                InvoiceDate = (DateTime)reader["InvoiceDate"],
                                InvoiceTime = reader["InvoiceTime"].ToString(),
                                CurrAccTypeCode = Convert.ToInt32(reader["CurrAccTypeCode"]),
                                VendorCode = reader["VendorCode"].ToString(),
                                VendorDescription = reader["VendorDescription"].ToString(),
                                CustomerCode = reader["CustomerCode"].ToString(),
                                CustomerDescription = reader["CustomerDescription"].ToString(),
                                RetailCustomerCode = reader["RetailCustomerCode"].ToString(),
                                StoreCurrAccCode = reader["StoreCurrAccCode"].ToString(),
                                StoreDescription = reader["StoreDescription"].ToString(),
                                EmployeeCode = reader["EmployeeCode"].ToString(),
                                FirstLastName = reader["FirstLastName"].ToString(),
                                SubCurrAccCode = reader["SubCurrAccCode"].ToString(),
                                SubCurrAccCompanyName = reader["SubCurrAccCompanyName"].ToString(),
                                IsCreditSale = Convert.ToBoolean(reader["IsCreditSale"]),
                                ProcessCode = reader["ProcessCode"].ToString(),
                                TransTypeCode = reader["TransTypeCode"].ToString(),
                                DocCurrencyCode = reader["DocCurrencyCode"].ToString(),
                                Series = reader["Series"].ToString(),
                                SeriesNumber = reader["SeriesNumber"].ToString(),
                                EInvoiceNumber = reader["EInvoiceNumber"].ToString(),
                                CompanyCode = reader["CompanyCode"].ToString(),
                                OfficeCode = reader["OfficeCode"].ToString(),
                                StoreCode = reader["StoreCode"].ToString(),
                                WarehouseCode = reader["WarehouseCode"].ToString(),
                                ImportFileNumber = reader["ImportFileNumber"].ToString(),
                                ExportFileNumber = reader["ExportFileNumber"].ToString(),
                                ExportTypeCode = reader["ExportTypeCode"].ToString(),
                                PosTerminalID = reader["PosTerminalID"].ToString(),
                                TaxTypeCode = reader["TaxTypeCode"].ToString(),
                                IsCompleted = Convert.ToBoolean(reader["IsCompleted"]),
                                IsSuspended = Convert.ToBoolean(reader["IsSuspended"]),
                                IsLocked = Convert.ToBoolean(reader["IsLocked"]),
                                IsOrderBase = Convert.ToBoolean(reader["IsOrderBase"]),
                                IsShipmentBase = Convert.ToBoolean(reader["IsShipmentBase"]),
                                IsPostingJournal = Convert.ToBoolean(reader["IsPostingJournal"]),
                                JournalNumber = reader["JournalNumber"].ToString(),
                                IsPrinted = Convert.ToBoolean(reader["IsPrinted"]),
                                ApplicationCode = reader["ApplicationCode"].ToString(),
                                ApplicationDescription = reader["ApplicationDescription"].ToString(),
                                ApplicationID = reader["ApplicationID"].ToString(),
                                InvoiceHeaderID = reader["InvoiceHeaderID"].ToString(),
                                FormType = reader["FormType"].ToString(),
                                DocumentTypeCode = reader["DocumentTypeCode"].ToString(),
                                Status = Convert.ToBoolean(reader["IsCompleted"]) ? "Tamamlandı" : "Bekliyor"
                            };

                            invoices.Add(invoice);
                        }
                    }

                    return (invoices, totalCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan alış faturaları getirilirken hata oluştu");
                throw;
            }
        }

        public async Task<InvoiceHeaderModel> GetWholesalePurchaseInvoiceByIdAsync(string invoiceHeaderId)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@InvoiceHeaderId", invoiceHeaderId),
                    new SqlParameter("@LangCode", "TR")
                };

                var query = @"
                    SELECT InvoiceNumber = trInvoiceHeader.InvoiceNumber
                         , IsReturn = trInvoiceHeader.IsReturn
                         , IsEInvoice = trInvoiceHeader.IsEInvoice
                         , InvoiceDate = trInvoiceHeader.InvoiceDate
                         , InvoiceTime = trInvoiceHeader.InvoiceTime 
                         , CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode
                         , VendorCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 1 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , VendorDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 1 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END     
                         , CustomerCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 3 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , CustomerDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 3 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END
                         , RetailCustomerCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 4 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , StoreCurrAccCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 5 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , StoreDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 5 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END
                         , EmployeeCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 8 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , FirstLastName = CASE WHEN trInvoiceHeader.CurrAccTypeCode IN (4,8)
                                               THEN ISNULL((SELECT FirstLastName FROM tpInvoicePostalAddress 
                                                            WHERE trInvoiceHeader.InvoiceHeaderID = tpInvoicePostalAddress.InvoiceHeaderID), 
                                                                (SELECT FirstLastName FROM cdCurrAcc 
                                                                     WHERE trInvoiceHeader.CurrAccCode = cdCurrAcc.CurrAccCode 
                                                                        AND trInvoiceHeader.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode)) 
                                                                         ELSE SPACE(0) END    
                         , SubCurrAccCode = ISNULL (SubCurrAccCode , SPACE(0))
                         , SubCurrAccCompanyName = ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
                         , IsCreditSale = trInvoiceHeader.IsCreditSale
                         , ProcessCode = trInvoiceHeader.ProcessCode
                         , TransTypeCode = trInvoiceHeader.TransTypeCode
                         , DocCurrencyCode = trInvoiceHeader.DocCurrencyCode
                         , Series = trInvoiceHeader.Series
                         , SeriesNumber = trInvoiceHeader.SeriesNumber
                         , EInvoiceNumber = trInvoiceHeader.EInvoiceNumber
                         , CompanyCode = trInvoiceHeader.CompanyCode
                         , OfficeCode = trInvoiceHeader.OfficeCode
                         , StoreCode = trInvoiceHeader.StoreCode
                         , WarehouseCode = trInvoiceHeader.WarehouseCode
                         , ImportFileNumber = trInvoiceHeader.ImportFileNumber
                         , ExportFileNumber = trInvoiceHeader.ExportFileNumber
                         , ExportTypeCode = ISNULL(ExportTypeCode, SPACE(0))
                         , PosTerminalID = trInvoiceHeader.PosTerminalID
                         , TaxTypeCode = trInvoiceHeader.TaxTypeCode
                         , IsCompleted = trInvoiceHeader.IsCompleted
                         , IsSuspended = trInvoiceHeader.IsSuspended
                         , IsLocked = trInvoiceHeader.IsLocked
                         , IsOrderBase = trInvoiceHeader.IsOrderBase
                         , IsShipmentBase = trInvoiceHeader.IsShipmentBase
                         , IsPostingJournal = trInvoiceHeader.IsPostingJournal
                         , JournalNumber = CASE trInvoiceHeader.IsPostingJournal WHEN 0 THEN SPACE(0) ELSE 
                                                   ISNULL(REPLACE(N'QWERTY'+(SELECT DISTINCT N', ' +  trJournalHeader.JournalNumber 
                                                                    FROM trJournalHeader WITH(NOLOCK)  
                                                                    WHERE trJournalHeader.ApplicationCode = N'Invoi'
                                                                 AND trJournalHeader.ApplicationID     = trInvoiceHeader.InvoiceHeaderID
                                                                     FOR XML PATH('')), N'QWERTY, ', SPACE(0)),SPACE(0)) END
                         , IsPrinted = trInvoiceHeader.IsPrinted
                         , ApplicationCode = trInvoiceHeader.ApplicationCode
                         , ApplicationDescription = ISNULL(bsApplicationDesc.ApplicationDescription , SPACE(0))
                         , ApplicationID = trInvoiceHeader.ApplicationID                      
                         , InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID    
                         , FormType = trInvoiceHeader.FormType
                         , DocumentTypeCode = trInvoiceHeader.DocumentTypeCode
                    FROM trInvoiceHeader WITH (NOLOCK) 
                        LEFT OUTER JOIN prSubCurrAcc WITH (NOLOCK)
                            ON prSubCurrAcc.SubCurrAccID = trInvoiceHeader.SubCurrAccID            
                        LEFT OUTER JOIN bsApplicationDesc WITH(NOLOCK)
                            ON bsApplicationDesc.ApplicationCode = trInvoiceHeader.ApplicationCode
                            AND bsApplicationDesc.LangCode = @LangCode
                        LEFT OUTER JOIN cdCurrAccDesc WITH (NOLOCK)
                            ON cdCurrAccDesc.CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode 
                            AND cdCurrAccDesc.CurrAccCode = trInvoiceHeader.CurrAccCode
                            AND cdCurrAccDesc.LangCode = @LangCode    
                        LEFT OUTER JOIN tpInvoiceHeaderExtension WITH(NOLOCK)
                            ON tpInvoiceHeaderExtension.InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID
                    WHERE trInvoiceHeader.InvoiceHeaderID = @InvoiceHeaderId
                      AND trInvoiceHeader.ProcessCode IN ('BP')";

                // Veritabanı bağlantısı oluştur
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Verileri al
                    var command = new SqlCommand(query, connection);
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new InvoiceHeaderModel
                            {
                                InvoiceNumber = reader["InvoiceNumber"].ToString(),
                                IsReturn = (bool)reader["IsReturn"],
                                IsEInvoice = (bool)reader["IsEInvoice"],
                                InvoiceDate = (DateTime)reader["InvoiceDate"],
                                InvoiceTime = reader["InvoiceTime"].ToString(),
                                CurrAccTypeCode = (int)reader["CurrAccTypeCode"],
                                VendorCode = reader["VendorCode"].ToString(),
                                VendorDescription = reader["VendorDescription"].ToString(),
                                CustomerCode = reader["CustomerCode"].ToString(),
                                CustomerDescription = reader["CustomerDescription"].ToString(),
                                RetailCustomerCode = reader["RetailCustomerCode"].ToString(),
                                StoreCurrAccCode = reader["StoreCurrAccCode"].ToString(),
                                StoreDescription = reader["StoreDescription"].ToString(),
                                EmployeeCode = reader["EmployeeCode"].ToString(),
                                FirstLastName = reader["FirstLastName"].ToString(),
                                SubCurrAccCode = reader["SubCurrAccCode"].ToString(),
                                SubCurrAccCompanyName = reader["SubCurrAccCompanyName"].ToString(),
                                IsCreditSale = (bool)reader["IsCreditSale"],
                                ProcessCode = reader["ProcessCode"].ToString(),
                                TransTypeCode = reader["TransTypeCode"].ToString(),
                                DocCurrencyCode = reader["DocCurrencyCode"].ToString(),
                                Series = reader["Series"].ToString(),
                                SeriesNumber = reader["SeriesNumber"].ToString(),
                                EInvoiceNumber = reader["EInvoiceNumber"].ToString(),
                                CompanyCode = reader["CompanyCode"].ToString(),
                                OfficeCode = reader["OfficeCode"].ToString(),
                                StoreCode = reader["StoreCode"].ToString(),
                                WarehouseCode = reader["WarehouseCode"].ToString(),
                                ImportFileNumber = reader["ImportFileNumber"].ToString(),
                                ExportFileNumber = reader["ExportFileNumber"].ToString(),
                                ExportTypeCode = reader["ExportTypeCode"].ToString(),
                                PosTerminalID = reader["PosTerminalID"].ToString(),
                                TaxTypeCode = reader["TaxTypeCode"].ToString(),
                                IsCompleted = (bool)reader["IsCompleted"],
                                IsSuspended = (bool)reader["IsSuspended"],
                                IsLocked = (bool)reader["IsLocked"],
                                IsOrderBase = (bool)reader["IsOrderBase"],
                                IsShipmentBase = (bool)reader["IsShipmentBase"],
                                IsPostingJournal = (bool)reader["IsPostingJournal"],
                                JournalNumber = reader["JournalNumber"].ToString(),
                                IsPrinted = (bool)reader["IsPrinted"],
                                ApplicationCode = reader["ApplicationCode"].ToString(),
                                ApplicationDescription = reader["ApplicationDescription"].ToString(),
                                ApplicationID = reader["ApplicationID"].ToString(),
                                InvoiceHeaderID = reader["InvoiceHeaderID"].ToString(),
                                FormType = reader["FormType"].ToString(),
                                DocumentTypeCode = reader["DocumentTypeCode"].ToString(),
                                Status = (bool)reader["IsCompleted"] ? "Tamamlandı" : "Bekliyor"
                            };
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan alış faturası getirilirken hata oluştu");
                throw;
            }
        }

        public async Task<InvoiceHeaderModel> GetWholesalePurchaseInvoiceByIdAsync(int invoiceHeaderId)
        {
            return await GetWholesalePurchaseInvoiceByIdAsync(invoiceHeaderId.ToString());
        }

        public async Task<InvoiceHeaderModel> CreateWholesalePurchaseInvoiceAsync(CreateInvoiceRequest request)
        {
            try
            {
                // SQL Transaction başlat
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // 1. Fatura başlık kaydını oluştur
                            var invoiceHeaderId = await CreateInvoiceHeaderAsync(connection, transaction, request, "WP");
                            
                            // 2. Fatura detaylarını oluştur
                            foreach (var detail in request.Details)
                            {
                                await CreateInvoiceDetailAsync(connection, transaction, invoiceHeaderId.ToString(), detail);
                            }
                            
                            // 3. Fatura toplamlarını güncelle
                            await UpdateInvoiceTotalsAsync(connection, transaction, invoiceHeaderId.ToString());
                            
                            // 4. Transaction'ı commit et
                            transaction.Commit();
                            
                            // 5. Oluşturulan faturayı getir
                            return await GetWholesalePurchaseInvoiceByIdAsync(invoiceHeaderId.ToString());
                        }
                        catch (Exception ex)
                        {
                            // Hata durumunda transaction'ı rollback yap
                            transaction.Rollback();
                            _logger.LogError(ex, "Toptan alış faturası oluşturulurken hata oluştu");
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan alış faturası oluşturulurken hata oluştu");
                throw;
            }
        }

        public async Task<InvoiceHeaderModel> CreateWholesaleInvoiceAsync(CreateInvoiceRequest request)
        {
            try
            {
                // SQL Transaction başlat
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // 1. Fatura başlık kaydını oluştur
                            var invoiceHeaderId = await CreateInvoiceHeaderAsync(connection, transaction, request, "TS");
                            
                            // 2. Fatura detaylarını oluştur
                            foreach (var detail in request.Details)
                            {
                                await CreateInvoiceDetailAsync(connection, transaction, invoiceHeaderId.ToString(), detail);
                            }
                            
                            // 3. Fatura toplamlarını güncelle
                            await UpdateInvoiceTotalsAsync(connection, transaction, invoiceHeaderId.ToString());
                            
                            // 4. Transaction'ı commit et
                            transaction.Commit();
                            
                            // 5. Oluşturulan faturayı getir
                            return await GetWholesaleInvoiceByIdAsync(invoiceHeaderId.ToString());
                        }
                        catch (Exception ex)
                        {
                            // Hata durumunda transaction'ı rollback yap
                            transaction.Rollback();
                            _logger.LogError(ex, "Toptan satış faturası oluşturulurken hata oluştu");
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan satış faturası oluşturulurken hata oluştu");
                throw;
            }
        }

        public async Task<InvoiceHeaderModel> CreateExpenseInvoiceAsync(CreateInvoiceRequest request)
        {
            try
            {
                // SQL Transaction başlat
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // 1. Fatura başlık kaydını oluştur
                            var invoiceHeaderId = await CreateInvoiceHeaderAsync(connection, transaction, request, "MAF");
                            
                            // 2. Fatura detaylarını oluştur
                            foreach (var detail in request.Details)
                            {
                                await CreateInvoiceDetailAsync(connection, transaction, invoiceHeaderId.ToString(), detail);
                            }
                            
                            // 3. Fatura toplamlarını güncelle
                            await UpdateInvoiceTotalsAsync(connection, transaction, invoiceHeaderId.ToString());
                            
                            // 4. Transaction'ı commit et
                            transaction.Commit();
                            
                            // 5. Oluşturulan faturayı getir
                            return await GetExpenseInvoiceByIdAsync(invoiceHeaderId.ToString());
                        }
                        catch (Exception ex)
                        {
                            // Hata durumunda transaction'ı rollback yap
                            transaction.Rollback();
                            _logger.LogError(ex, "Masraf faturası oluşturulurken hata oluştu");
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Masraf faturası oluşturulurken hata oluştu");
                throw;
            }
        }

        // Yardımcı metotlar
        private async Task<int> CreateInvoiceHeaderAsync(SqlConnection connection, SqlTransaction transaction, CreateInvoiceRequest request, string processCode)
        {
            var query = @"
                INSERT INTO trInvoiceHeader (
                    InvoiceNumber,
                    InvoiceDate,
                    InvoiceTime,
                    CurrAccTypeCode,
                    CurrAccCode,
                    SubCurrAccID,
                    DocCurrencyCode,
                    ExchangeRate,
                    CompanyCode,
                    OfficeCode,
                    StoreCode,
                    WarehouseCode,
                    IsCreditSale,
                    IsReturn,
                    IsEInvoice,
                    IsCompleted,
                    IsSuspended,
                    IsLocked,
                    IsOrderBase,
                    IsShipmentBase,
                    IsPostingJournal,
                    IsPrinted,
                    ProcessCode,
                    TransTypeCode,
                    ApplicationCode,
                    ApplicationID,
                    FormType,
                    DocumentTypeCode,
                    CreatedUserName,
                    CreatedDate
                ) VALUES (
                    @InvoiceNumber,
                    @InvoiceDate,
                    @InvoiceTime,
                    @CurrAccTypeCode,
                    @CurrAccCode,
                    @SubCurrAccID,
                    @DocCurrencyCode,
                    @ExchangeRate,
                    @CompanyCode,
                    @OfficeCode,
                    @StoreCode,
                    @WarehouseCode,
                    @IsCreditSale,
                    @IsReturn,
                    @IsEInvoice,
                    @IsCompleted,
                    @IsSuspended,
                    @IsLocked,
                    @IsOrderBase,
                    @IsShipmentBase,
                    @IsPostingJournal,
                    @IsPrinted,
                    @ProcessCode,
                    @TransTypeCode,
                    @ApplicationCode,
                    @ApplicationID,
                    @FormType,
                    @DocumentTypeCode,
                    @CreatedUserName,
                    GETDATE()
                );
                SELECT SCOPE_IDENTITY();";

            var command = new SqlCommand(query, connection, transaction);
            
            // Müşteri veya tedarikçi bilgilerini ayarla
            int currAccTypeCode;
            string currAccCode;
            
            if  (processCode.StartsWith("WS") || processCode.StartsWith("EXP")) // Toptan Satış
            {
                currAccTypeCode = 3; // Müşteri
                currAccCode = request.CustomerCode;
            }
            else if (processCode.StartsWith("BP") || processCode.StartsWith("EP")) // Toptan Alış veya Masraf
            {
                currAccTypeCode = 1; // Tedarikçi
                currAccCode = request.VendorCode;
            }
            else
            {
                throw new ArgumentException($"Geçersiz fatura tipi: {processCode}");
            }

            command.Parameters.AddWithValue("@InvoiceNumber", request.InvoiceNumber);
            command.Parameters.AddWithValue("@InvoiceDate", request.InvoiceDate);
            command.Parameters.AddWithValue("@InvoiceTime", DateTime.Now.ToString("HH:mm:ss"));
            command.Parameters.AddWithValue("@CurrAccTypeCode", currAccTypeCode);
            command.Parameters.AddWithValue("@CurrAccCode", currAccCode);
            command.Parameters.AddWithValue("@SubCurrAccID", request.SubCurrAccID.HasValue ? (object)request.SubCurrAccID.Value : DBNull.Value);
            command.Parameters.AddWithValue("@DocCurrencyCode", string.IsNullOrEmpty(request.DocCurrencyCode) ? "TRY" : request.DocCurrencyCode);
            command.Parameters.AddWithValue("@ExchangeRate", request.ExchangeRate.HasValue ? request.ExchangeRate.Value : 1.0m);
            command.Parameters.AddWithValue("@CompanyCode", string.IsNullOrEmpty(request.CompanyCode) ? "1" : request.CompanyCode);
            command.Parameters.AddWithValue("@OfficeCode", string.IsNullOrEmpty(request.OfficeCode) ? "001" : request.OfficeCode);
            command.Parameters.AddWithValue("@StoreCode", string.IsNullOrEmpty(request.StoreCode) ? "" : request.StoreCode);
            command.Parameters.AddWithValue("@WarehouseCode", string.IsNullOrEmpty(request.WarehouseCode) ? "001" : request.WarehouseCode);
            command.Parameters.AddWithValue("@IsCreditSale", request.IsCreditSale);
            command.Parameters.AddWithValue("@IsReturn", request.IsReturn);
            command.Parameters.AddWithValue("@IsEInvoice", request.IsEInvoice);
            command.Parameters.AddWithValue("@IsCompleted", request.IsCompleted);
            command.Parameters.AddWithValue("@IsSuspended", false);
            command.Parameters.AddWithValue("@IsLocked", false);
            command.Parameters.AddWithValue("@IsOrderBase", false);
            command.Parameters.AddWithValue("@IsShipmentBase", false);
            command.Parameters.AddWithValue("@IsPostingJournal", false);
            command.Parameters.AddWithValue("@IsPrinted", false);
            command.Parameters.AddWithValue("@ProcessCode", string.IsNullOrEmpty(request.ProcessCode) ? "01" : request.ProcessCode);
            command.Parameters.AddWithValue("@TransTypeCode", string.IsNullOrEmpty(request.TransTypeCode) ? "01" : request.TransTypeCode);
            command.Parameters.AddWithValue("@ApplicationCode", string.IsNullOrEmpty(request.ApplicationCode) ? "Invoi" : request.ApplicationCode);
            command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID.HasValue ? (object)request.ApplicationID.Value : DBNull.Value);
            command.Parameters.AddWithValue("@FormType", string.IsNullOrEmpty(request.FormType) ? "01" : request.FormType);
            command.Parameters.AddWithValue("@DocumentTypeCode", string.IsNullOrEmpty(request.DocumentTypeCode) ? "01" : request.DocumentTypeCode);
            command.Parameters.AddWithValue("@CreatedUserName", "API");

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        private async Task CreateInvoiceDetailAsync(SqlConnection connection, SqlTransaction transaction, string invoiceHeaderId, CreateInvoiceDetailRequest detail)
        {
            var query = @"
                INSERT INTO trInvoiceDetail (
                    InvoiceHeaderID,
                    LineNumber,
                    ProductCode,
                    Qty,
                    UnitCode,
                    UnitPrice,
                    DiscountRate,
                    VatRate,
                    Amount,
                    DiscountAmount,
                    VatAmount,
                    LineNetAmount,
                    LineTotalAmount,
                    CurrencyCode,
                    ExchangeRate,
                    IsGift,
                    IsPromotional,
                    WarehouseCode,
                    SalesPersonCode,
                    ProductTypeCode,
                    PromotionCode,
                    CreatedUserName,
                    CreatedDate
                ) VALUES (
                    @InvoiceHeaderID,
                    @LineNumber,
                    @ProductCode,
                    @Qty,
                    @UnitCode,
                    @UnitPrice,
                    @DiscountRate,
                    @VatRate,
                    @Amount,
                    @DiscountAmount,
                    @VatAmount,
                    @LineNetAmount,
                    @LineTotalAmount,
                    @CurrencyCode,
                    @ExchangeRate,
                    @IsGift,
                    @IsPromotional,
                    @WarehouseCode,
                    @SalesPersonCode,
                    @ProductTypeCode,
                    @PromotionCode,
                    @CreatedUserName,
                    GETDATE()
                );";

            var command = new SqlCommand(query, connection, transaction);
            
            // Hesaplamalar
            decimal amount = detail.Qty * detail.UnitPrice;
            decimal discountAmount = amount * (detail.DiscountRate / 100);
            decimal lineNetAmount = amount - discountAmount;
            decimal vatAmount = lineNetAmount * (detail.VatRate / 100);
            decimal lineTotalAmount = lineNetAmount + vatAmount;
            
            command.Parameters.AddWithValue("@InvoiceHeaderID", invoiceHeaderId);
            command.Parameters.AddWithValue("@LineNumber", detail.LineNumber);
            command.Parameters.AddWithValue("@ProductCode", detail.ItemCode);
            command.Parameters.AddWithValue("@Qty", detail.Qty);
            command.Parameters.AddWithValue("@UnitCode", detail.UnitCode);
            command.Parameters.AddWithValue("@UnitPrice", detail.UnitPrice);
            command.Parameters.AddWithValue("@DiscountRate", detail.DiscountRate);
            command.Parameters.AddWithValue("@VatRate", detail.VatRate);
            command.Parameters.AddWithValue("@Amount", amount);
            command.Parameters.AddWithValue("@DiscountAmount", discountAmount);
            command.Parameters.AddWithValue("@VatAmount", vatAmount);
            command.Parameters.AddWithValue("@LineNetAmount", lineNetAmount);
            command.Parameters.AddWithValue("@LineTotalAmount", lineTotalAmount);
            command.Parameters.AddWithValue("@CurrencyCode", string.IsNullOrEmpty(detail.CurrencyCode) ? "TRY" : detail.CurrencyCode);
            command.Parameters.AddWithValue("@ExchangeRate", detail.ExchangeRate.HasValue ? detail.ExchangeRate.Value : 1.0m);
            command.Parameters.AddWithValue("@IsGift", detail.IsGift);
            command.Parameters.AddWithValue("@IsPromotional", detail.IsPromotional);
            command.Parameters.AddWithValue("@WarehouseCode", string.IsNullOrEmpty(detail.WarehouseCode) ? "001" : detail.WarehouseCode);
            command.Parameters.AddWithValue("@SalesPersonCode", string.IsNullOrEmpty(detail.SalesPersonCode) ? DBNull.Value : (object)detail.SalesPersonCode);
            command.Parameters.AddWithValue("@ProductTypeCode", string.IsNullOrEmpty(detail.ProductTypeCode) ? "01" : detail.ProductTypeCode);
            command.Parameters.AddWithValue("@PromotionCode", string.IsNullOrEmpty(detail.PromotionCode) ? DBNull.Value : (object)detail.PromotionCode);
            command.Parameters.AddWithValue("@CreatedUserName", "API");

            await command.ExecuteNonQueryAsync();
        }

        private async Task UpdateInvoiceTotalsAsync(SqlConnection connection, SqlTransaction transaction, string invoiceHeaderId)
        {
            var query = @"
                UPDATE trInvoiceHeader
                SET 
                    TotalAmount = (SELECT SUM(Amount) FROM trInvoiceLineCurrency WHERE InvoiceLineID IN (SELECT InvoiceLineID FROM trInvoiceLine WHERE InvoiceHeaderID = @InvoiceHeaderID)),
                    TotalVatAmount = (SELECT SUM(Vat) FROM trInvoiceLineCurrency WHERE InvoiceLineID IN (SELECT InvoiceLineID FROM trInvoiceLine WHERE InvoiceHeaderID = @InvoiceHeaderID)),
                    TotalNetAmount = (SELECT SUM(NetAmount) FROM trInvoiceLineCurrency WHERE InvoiceLineID IN (SELECT InvoiceLineID FROM trInvoiceLine WHERE InvoiceHeaderID = @InvoiceHeaderID)),
                    LastUpdatedUserName = @LastUpdatedUserName,
                    LastUpdatedDate = GETDATE()
                WHERE InvoiceHeaderID = @InvoiceHeaderID";

            var command = new SqlCommand(query, connection, transaction);
            command.Parameters.AddWithValue("@InvoiceHeaderID", invoiceHeaderId);
            command.Parameters.AddWithValue("@LastUpdatedUserName", "API");

            await command.ExecuteNonQueryAsync();
        }

        // Masraf faturaları
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetExpenseInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", request.LangCode ?? "TR")
                };

                var whereConditions = new List<string>();

                // ProcessCode filtresi (Masraf Alış Faturaları için)
                whereConditions.Add("trInvoiceHeader.ProcessCode IN ('EP')"); // MASRAF ALIŞ FATURASI

                if (!string.IsNullOrEmpty(request.InvoiceNumber))
                {
                    whereConditions.Add("trInvoiceHeader.InvoiceNumber LIKE @InvoiceNumber");
                    parameters.Add(new SqlParameter("@InvoiceNumber", $"%{request.InvoiceNumber}%"));
                }

                if (!string.IsNullOrEmpty(request.VendorCode))
                {
                    whereConditions.Add("trInvoiceHeader.CurrAccTypeCode = 1 AND trInvoiceHeader.CurrAccCode = @VendorCode");
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

                if (!string.IsNullOrEmpty(request.CompanyCode))
                {
                    whereConditions.Add("trInvoiceHeader.CompanyCode = @CompanyCode");
                    parameters.Add(new SqlParameter("@CompanyCode", request.CompanyCode));
                }

                if (!string.IsNullOrEmpty(request.StoreCode))
                {
                    whereConditions.Add("trInvoiceHeader.StoreCode = @StoreCode");
                    parameters.Add(new SqlParameter("@StoreCode", request.StoreCode));
                }

                if (!string.IsNullOrEmpty(request.WarehouseCode))
                {
                    whereConditions.Add("trInvoiceHeader.WarehouseCode = @WarehouseCode");
                    parameters.Add(new SqlParameter("@WarehouseCode", request.WarehouseCode));
                }

                var whereClause = whereConditions.Count > 0
                    ? $"WHERE {string.Join(" AND ", whereConditions)}"
                    : string.Empty;

                // Sayfalama ve sıralama
                var offset = (request.Page - 1) * request.PageSize;
                var orderBy = $"ORDER BY trInvoiceHeader.{request.SortBy} {request.SortDirection}";
                var pagination = $"OFFSET {offset} ROWS FETCH NEXT {request.PageSize} ROWS ONLY";

                // Toplam kayıt sayısını al
                var countQuery = $@"
                    SELECT COUNT(*)
                    FROM trInvoiceHeader WITH (NOLOCK) 
                    LEFT OUTER JOIN prSubCurrAcc WITH (NOLOCK)
                        ON prSubCurrAcc.SubCurrAccID = trInvoiceHeader.SubCurrAccID            
                    LEFT OUTER JOIN bsApplicationDesc WITH(NOLOCK)
                        ON bsApplicationDesc.ApplicationCode = trInvoiceHeader.ApplicationCode
                        AND bsApplicationDesc.LangCode = @LangCode
                    LEFT OUTER JOIN cdCurrAccDesc WITH (NOLOCK)
                        ON cdCurrAccDesc.CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode 
                        AND cdCurrAccDesc.CurrAccCode = trInvoiceHeader.CurrAccCode
                        AND cdCurrAccDesc.LangCode = @LangCode
                    LEFT OUTER JOIN tpInvoiceHeaderExtension WITH(NOLOCK)
                        ON tpInvoiceHeaderExtension.InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID
                    {whereClause}";

                // Ana sorgu
                var query = $@"
                    SELECT InvoiceNumber = trInvoiceHeader.InvoiceNumber
                         , IsReturn = trInvoiceHeader.IsReturn
                         , IsEInvoice = trInvoiceHeader.IsEInvoice
                        , InvoiceDate = trInvoiceHeader.InvoiceDate
                         , InvoiceTime = trInvoiceHeader.InvoiceTime 
                         , CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode
                         
                         , VendorCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 1 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , VendorDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 1 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END     
                         , CustomerCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 3 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , CustomerDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 3 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END
                         , RetailCustomerCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 4 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , StoreCurrAccCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 5 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , StoreDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 5 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END
                         , EmployeeCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 8 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , FirstLastName = CASE WHEN trInvoiceHeader.CurrAccTypeCode IN (4,8)
                                               THEN ISNULL((SELECT FirstLastName FROM tpInvoicePostalAddress 
                                                            WHERE trInvoiceHeader.InvoiceHeaderID = tpInvoicePostalAddress.InvoiceHeaderID), 
                                                                (SELECT FirstLastName FROM cdCurrAcc 
                                                                     WHERE trInvoiceHeader.CurrAccCode = cdCurrAcc.CurrAccCode 
                                                                        AND trInvoiceHeader.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode)) 
                                                                         ELSE SPACE(0) END    
                         , SubCurrAccCode = ISNULL (SubCurrAccCode , SPACE(0))
                         , SubCurrAccCompanyName = ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
                         , IsCreditSale = trInvoiceHeader.IsCreditSale
                         , ProcessCode = trInvoiceHeader.ProcessCode
                         , TransTypeCode = trInvoiceHeader.TransTypeCode
                         , DocCurrencyCode = trInvoiceHeader.DocCurrencyCode
                         , Series = trInvoiceHeader.Series
                         , SeriesNumber = trInvoiceHeader.SeriesNumber
                         , EInvoiceNumber = trInvoiceHeader.EInvoiceNumber
                         , CompanyCode = trInvoiceHeader.CompanyCode
                         , OfficeCode = trInvoiceHeader.OfficeCode
                         , StoreCode = trInvoiceHeader.StoreCode
                         , WarehouseCode = trInvoiceHeader.WarehouseCode
                         , ImportFileNumber = trInvoiceHeader.ImportFileNumber
                         , ExportFileNumber = trInvoiceHeader.ExportFileNumber
                         , ExportTypeCode = ISNULL(ExportTypeCode, SPACE(0))
                         , PosTerminalID = trInvoiceHeader.PosTerminalID
                         , TaxTypeCode = trInvoiceHeader.TaxTypeCode
                         , IsCompleted = trInvoiceHeader.IsCompleted
                         , IsSuspended = trInvoiceHeader.IsSuspended
                         , IsLocked = trInvoiceHeader.IsLocked
                         , IsOrderBase = trInvoiceHeader.IsOrderBase
                         , IsShipmentBase = trInvoiceHeader.IsShipmentBase
                         , IsPostingJournal = trInvoiceHeader.IsPostingJournal
                         , JournalNumber = CASE trInvoiceHeader.IsPostingJournal WHEN 0 THEN SPACE(0) ELSE 
                                                   ISNULL(REPLACE(N'QWERTY'+(SELECT DISTINCT N', ' +  trJournalHeader.JournalNumber 
                                                                    FROM trJournalHeader WITH(NOLOCK)  
                                                                    WHERE trJournalHeader.ApplicationCode = N'Invoi'
                                                                 AND trJournalHeader.ApplicationID     = trInvoiceHeader.InvoiceHeaderID
                                                                     FOR XML PATH('')), N'QWERTY, ', SPACE(0)),SPACE(0)) END
                         , IsPrinted = trInvoiceHeader.IsPrinted
                         , ApplicationCode = trInvoiceHeader.ApplicationCode
                         , ApplicationDescription = ISNULL(bsApplicationDesc.ApplicationDescription , SPACE(0))
                         , ApplicationID = trInvoiceHeader.ApplicationID                      
                         , InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID    
                        
                         , FormType = trInvoiceHeader.FormType
                         , DocumentTypeCode = trInvoiceHeader.DocumentTypeCode
                    FROM trInvoiceHeader WITH (NOLOCK) 
                        LEFT OUTER JOIN prSubCurrAcc WITH (NOLOCK)
                            ON prSubCurrAcc.SubCurrAccID = trInvoiceHeader.SubCurrAccID            
                        LEFT OUTER JOIN bsApplicationDesc WITH(NOLOCK)
                            ON bsApplicationDesc.ApplicationCode = trInvoiceHeader.ApplicationCode
                            AND bsApplicationDesc.LangCode = @LangCode
                        LEFT OUTER JOIN cdCurrAccDesc WITH (NOLOCK)
                            ON cdCurrAccDesc.CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode 
                            AND cdCurrAccDesc.CurrAccCode = trInvoiceHeader.CurrAccCode
                            AND cdCurrAccDesc.LangCode = @LangCode    
                        LEFT OUTER JOIN tpInvoiceHeaderExtension WITH(NOLOCK)
                            ON tpInvoiceHeaderExtension.InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID
                    {whereClause}
                    {orderBy}
                    {pagination}";

                // Veritabanı bağlantısı oluştur
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Toplam kayıt sayısını al
                    var countCommand = new SqlCommand(countQuery, connection);
                    foreach (var parameter in parameters)
                    {
                        countCommand.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }
                    var totalCount = (int)await countCommand.ExecuteScalarAsync();

                    // Verileri al
                    var command = new SqlCommand(query, connection);
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }

                    var invoices = new List<InvoiceHeaderModel>();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var invoice = new InvoiceHeaderModel
                            {
                                InvoiceNumber = reader["InvoiceNumber"].ToString(),
                                IsReturn = (bool)reader["IsReturn"],
                                IsEInvoice = (bool)reader["IsEInvoice"],
                                
                                InvoiceDate = (DateTime)reader["InvoiceDate"],
                                InvoiceTime = reader["InvoiceTime"].ToString(),
                                CurrAccTypeCode = (int)reader["CurrAccTypeCode"],
                                VendorCode = reader["VendorCode"].ToString(),
                                VendorDescription = reader["VendorDescription"].ToString(),
                                CustomerCode = reader["CustomerCode"].ToString(),
                                CustomerDescription = reader["CustomerDescription"].ToString(),
                                RetailCustomerCode = reader["RetailCustomerCode"].ToString(),
                                StoreCurrAccCode = reader["StoreCurrAccCode"].ToString(),
                                StoreDescription = reader["StoreDescription"].ToString(),
                                EmployeeCode = reader["EmployeeCode"].ToString(),
                                FirstLastName = reader["FirstLastName"].ToString(),
                                SubCurrAccCode = reader["SubCurrAccCode"].ToString(),
                                SubCurrAccCompanyName = reader["SubCurrAccCompanyName"].ToString(),
                                IsCreditSale = (bool)reader["IsCreditSale"],
                                ProcessCode = reader["ProcessCode"].ToString(),
                                TransTypeCode = reader["TransTypeCode"].ToString(),
                                DocCurrencyCode = reader["DocCurrencyCode"].ToString(),
                                Series = reader["Series"].ToString(),
                                SeriesNumber = reader["SeriesNumber"].ToString(),
                                EInvoiceNumber = reader["EInvoiceNumber"].ToString(),
                                CompanyCode = reader["CompanyCode"].ToString(),
                                OfficeCode = reader["OfficeCode"].ToString(),
                                StoreCode = reader["StoreCode"].ToString(),
                                WarehouseCode = reader["WarehouseCode"].ToString(),
                                ImportFileNumber = reader["ImportFileNumber"].ToString(),
                                ExportFileNumber = reader["ExportFileNumber"].ToString(),
                                ExportTypeCode = reader["ExportTypeCode"].ToString(),
                                PosTerminalID = reader["PosTerminalID"].ToString(),
                                TaxTypeCode = reader["TaxTypeCode"].ToString(),
                                IsCompleted = (bool)reader["IsCompleted"],
                                IsSuspended = (bool)reader["IsSuspended"],
                                IsLocked = (bool)reader["IsLocked"],
                                IsOrderBase = (bool)reader["IsOrderBase"],
                                IsShipmentBase = (bool)reader["IsShipmentBase"],
                                IsPostingJournal = (bool)reader["IsPostingJournal"],
                                JournalNumber = reader["JournalNumber"].ToString(),
                                IsPrinted = (bool)reader["IsPrinted"],
                                ApplicationCode = reader["ApplicationCode"].ToString(),
                                ApplicationDescription = reader["ApplicationDescription"].ToString(),
                                ApplicationID = reader["ApplicationID"].ToString(),
                                InvoiceHeaderID = reader["InvoiceHeaderID"].ToString(),
                                FormType = reader["FormType"].ToString(),
                                DocumentTypeCode = reader["DocumentTypeCode"].ToString(),
                                // Ek alanlar varsayılan değerlerle doldurulabilir
                                Status = (bool)reader["IsCompleted"] ? "Tamamlandı" : "Bekliyor"
                            };

                            invoices.Add(invoice);
                        }
                    }

                    return (invoices, totalCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Masraf faturaları getirilirken hata oluştu");
                throw;
            }
        }

        public async Task<InvoiceHeaderModel> GetExpenseInvoiceByIdAsync(string invoiceHeaderId)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@InvoiceHeaderId", invoiceHeaderId),
                    new SqlParameter("@LangCode", "TR")
                };

                var query = @"
                    SELECT InvoiceNumber = trInvoiceHeader.InvoiceNumber
                         , IsReturn = trInvoiceHeader.IsReturn
                         , IsEInvoice = trInvoiceHeader.IsEInvoice
                         , InvoiceDate = trInvoiceHeader.InvoiceDate
                         , InvoiceTime = trInvoiceHeader.InvoiceTime 
                         , CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode
                         
                         , VendorCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 1 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , VendorDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 1 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END     
                         , CustomerCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 3 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , CustomerDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 3 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END
                         , RetailCustomerCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 4 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , StoreCurrAccCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 5 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , StoreDescription = CASE trInvoiceHeader.CurrAccTypeCode WHEN 5 THEN ISNULL(CurrAccDescription, SPACE(0)) ELSE SPACE(0) END
                         , EmployeeCode = CASE trInvoiceHeader.CurrAccTypeCode WHEN 8 THEN trInvoiceHeader.CurrAccCode ELSE SPACE(0) END
                         , FirstLastName = CASE WHEN trInvoiceHeader.CurrAccTypeCode IN (4,8)
                                               THEN ISNULL((SELECT FirstLastName FROM tpInvoicePostalAddress 
                                                            WHERE trInvoiceHeader.InvoiceHeaderID = tpInvoicePostalAddress.InvoiceHeaderID), 
                                                                (SELECT FirstLastName FROM cdCurrAcc 
                                                                     WHERE trInvoiceHeader.CurrAccCode = cdCurrAcc.CurrAccCode 
                                                                        AND trInvoiceHeader.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode)) 
                                                                         ELSE SPACE(0) END    
                         , SubCurrAccCode = ISNULL (SubCurrAccCode , SPACE(0))
                         , SubCurrAccCompanyName = ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
                         , IsCreditSale = trInvoiceHeader.IsCreditSale
                         , ProcessCode = trInvoiceHeader.ProcessCode
                         , TransTypeCode = trInvoiceHeader.TransTypeCode
                         , DocCurrencyCode = trInvoiceHeader.DocCurrencyCode
                         , Series = trInvoiceHeader.Series
                         , SeriesNumber = trInvoiceHeader.SeriesNumber
                         , EInvoiceNumber = trInvoiceHeader.EInvoiceNumber
                         , CompanyCode = trInvoiceHeader.CompanyCode
                         , OfficeCode = trInvoiceHeader.OfficeCode
                         , StoreCode = trInvoiceHeader.StoreCode
                         , WarehouseCode = trInvoiceHeader.WarehouseCode
                         , ImportFileNumber = trInvoiceHeader.ImportFileNumber
                         , ExportFileNumber = trInvoiceHeader.ExportFileNumber
                         , ExportTypeCode = ISNULL(ExportTypeCode, SPACE(0))
                         , PosTerminalID = trInvoiceHeader.PosTerminalID
                         , TaxTypeCode = trInvoiceHeader.TaxTypeCode
                         , IsCompleted = trInvoiceHeader.IsCompleted
                         , IsSuspended = trInvoiceHeader.IsSuspended
                         , IsLocked = trInvoiceHeader.IsLocked
                         , IsOrderBase = trInvoiceHeader.IsOrderBase
                         , IsShipmentBase = trInvoiceHeader.IsShipmentBase
                         , IsPostingJournal = trInvoiceHeader.IsPostingJournal
                         , JournalNumber = CASE trInvoiceHeader.IsPostingJournal WHEN 0 THEN SPACE(0) ELSE 
                                                   ISNULL(REPLACE(N'QWERTY'+(SELECT DISTINCT N', ' +  trJournalHeader.JournalNumber 
                                                                    FROM trJournalHeader WITH(NOLOCK)  
                                                                    WHERE trJournalHeader.ApplicationCode = N'Invoi'
                                                                 AND trJournalHeader.ApplicationID     = trInvoiceHeader.InvoiceHeaderID
                                                                     FOR XML PATH('')), N'QWERTY, ', SPACE(0)),SPACE(0)) END
                         , IsPrinted = trInvoiceHeader.IsPrinted
                         , ApplicationCode = trInvoiceHeader.ApplicationCode
                         , ApplicationDescription = ISNULL(bsApplicationDesc.ApplicationDescription , SPACE(0))
                         , ApplicationID = trInvoiceHeader.ApplicationID                      
                         , InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID 
                         , FormType = trInvoiceHeader.FormType
                         , DocumentTypeCode = trInvoiceHeader.DocumentTypeCode
                    FROM trInvoiceHeader WITH (NOLOCK) 
                        LEFT OUTER JOIN prSubCurrAcc WITH (NOLOCK)
                            ON prSubCurrAcc.SubCurrAccID = trInvoiceHeader.SubCurrAccID            
                        LEFT OUTER JOIN bsApplicationDesc WITH(NOLOCK)
                            ON bsApplicationDesc.ApplicationCode = trInvoiceHeader.ApplicationCode
                            AND bsApplicationDesc.LangCode = @LangCode
                        LEFT OUTER JOIN cdCurrAccDesc WITH (NOLOCK)
                            ON cdCurrAccDesc.CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode 
                            AND cdCurrAccDesc.CurrAccCode = trInvoiceHeader.CurrAccCode
                            AND cdCurrAccDesc.LangCode = @LangCode    
                        LEFT OUTER JOIN tpInvoiceHeaderExtension WITH(NOLOCK)
                            ON tpInvoiceHeaderExtension.InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID
                    WHERE trInvoiceHeader.InvoiceHeaderID = @InvoiceHeaderId";

                // Veritabanı bağlantısı oluştur
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Verileri al
                    var command = new SqlCommand(query, connection);
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new InvoiceHeaderModel
                            {
                                InvoiceNumber = reader["InvoiceNumber"].ToString(),
                                IsReturn = (bool)reader["IsReturn"],
                                IsEInvoice = (bool)reader["IsEInvoice"],
                                InvoiceDate = (DateTime)reader["InvoiceDate"],
                                InvoiceTime = reader["InvoiceTime"].ToString(),
                                CurrAccTypeCode = (int)reader["CurrAccTypeCode"],
                                VendorCode = reader["VendorCode"].ToString(),
                                VendorDescription = reader["VendorDescription"].ToString(),
                                CustomerCode = reader["CustomerCode"].ToString(),
                                CustomerDescription = reader["CustomerDescription"].ToString(),
                                RetailCustomerCode = reader["RetailCustomerCode"].ToString(),
                                StoreCurrAccCode = reader["StoreCurrAccCode"].ToString(),
                                StoreDescription = reader["StoreDescription"].ToString(),
                                EmployeeCode = reader["EmployeeCode"].ToString(),
                                FirstLastName = reader["FirstLastName"].ToString(),
                                SubCurrAccCode = reader["SubCurrAccCode"].ToString(),
                                SubCurrAccCompanyName = reader["SubCurrAccCompanyName"].ToString(),
                                IsCreditSale = (bool)reader["IsCreditSale"],
                                ProcessCode = reader["ProcessCode"].ToString(),
                                TransTypeCode = reader["TransTypeCode"].ToString(),
                                DocCurrencyCode = reader["DocCurrencyCode"].ToString(),
                                Series = reader["Series"].ToString(),
                                SeriesNumber = reader["SeriesNumber"].ToString(),
                                EInvoiceNumber = reader["EInvoiceNumber"].ToString(),
                                CompanyCode = reader["CompanyCode"].ToString(),
                                OfficeCode = reader["OfficeCode"].ToString(),
                                StoreCode = reader["StoreCode"].ToString(),
                                WarehouseCode = reader["WarehouseCode"].ToString(),
                                ImportFileNumber = reader["ImportFileNumber"].ToString(),
                                ExportFileNumber = reader["ExportFileNumber"].ToString(),
                                ExportTypeCode = reader["ExportTypeCode"].ToString(),
                                PosTerminalID = reader["PosTerminalID"].ToString(),
                                TaxTypeCode = reader["TaxTypeCode"].ToString(),
                                IsCompleted = (bool)reader["IsCompleted"],
                                IsSuspended = (bool)reader["IsSuspended"],
                                IsLocked = (bool)reader["IsLocked"],
                                IsOrderBase = (bool)reader["IsOrderBase"],
                                IsShipmentBase = (bool)reader["IsShipmentBase"],
                                IsPostingJournal = (bool)reader["IsPostingJournal"],
                                JournalNumber = reader["JournalNumber"].ToString(),
                                IsPrinted = (bool)reader["IsPrinted"],
                                ApplicationCode = reader["ApplicationCode"].ToString(),
                                ApplicationDescription = reader["ApplicationDescription"].ToString(),
                                ApplicationID = reader["ApplicationID"].ToString(),
                                InvoiceHeaderID = reader["InvoiceHeaderID"].ToString(),
                                FormType = reader["FormType"].ToString(),
                                DocumentTypeCode = reader["DocumentTypeCode"].ToString(),
                                Status = (bool)reader["IsCompleted"] ? "Tamamlandı" : "Bekliyor"
                            };
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Masraf faturası getirilirken hata oluştu");
                throw;
            }
        }

        public async Task<InvoiceHeaderModel> GetExpenseInvoiceByIdAsync(int invoiceHeaderId)
        {
            return await GetExpenseInvoiceByIdAsync(invoiceHeaderId.ToString());
        }

        // Fatura ödeme detaylarını getir
        public async Task<List<InvoicePaymentDetailModel>> GetInvoicePaymentDetailsAsync(string invoiceHeaderId)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@InvoiceHeaderId", invoiceHeaderId)
                };

                var query = @"
                    SELECT 
                    [DebitLineID] = [trDebitLineCurrency].[DebitLineID]
                    ,[CurrencyCode] = RTRIM(LTRIM([trDebitLineCurrency].[CurrencyCode]))
                    ,[ExchangeRate] = [trDebitLineCurrency].[ExchangeRate]
                    ,[RelationCurrencyCode] = RTRIM(LTRIM([trDebitLineCurrency].[RelationCurrencyCode]))
                    ,[Debit] = [trDebitLineCurrency].[Debit]
                    ,[Credit] = [trDebitLineCurrency].[Credit]
                    FROM [trDebitLineCurrency] WITH(NOLOCK)
                    WHERE 
                    ([trDebitLineCurrency].[DebitLineID] IN (SELECT 
                    [trDebitLine].[DebitLineID]
                    FROM [trDebitLine] WITH(NOLOCK)
                    WHERE 
                    ([trDebitLine].[DebitHeaderID] = @InvoiceHeaderId)))
                ";

                // Veritabanı bağlantısı oluştur
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Verileri al
                    var command = new SqlCommand(query, connection);
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }

                    var paymentDetails = new List<InvoicePaymentDetailModel>();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var paymentDetail = new InvoicePaymentDetailModel
                            {
                                DebitLineID = reader["DebitLineID"].ToString(),
                                CurrencyCode = reader["CurrencyCode"].ToString(),
                                ExchangeRate = reader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(reader["ExchangeRate"]) : 0,
                                RelationCurrencyCode = reader["RelationCurrencyCode"].ToString(),
                                Debit = reader["Debit"] != DBNull.Value ? Convert.ToDecimal(reader["Debit"]) : 0,
                                Credit = reader["Credit"] != DBNull.Value ? Convert.ToDecimal(reader["Credit"]) : 0
                            };
                            paymentDetails.Add(paymentDetail);
                        }
                    }

                    return paymentDetails;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatura ödeme detayları getirilirken hata oluştu");
                throw;
            }
        }

        // Yardımcı metotlar
        private InvoiceHeaderModel MapToInvoiceHeaderModel(SqlDataReader reader)
        {
            try
            {
                var processCode = reader["ProcessCode"].ToString();
                var currAccTypeCode = Convert.ToInt32(reader["CurrAccTypeCode"]);
                
                // CurrAccTypeCode'a göre doğru alanları al
                string customerCode = "";
                string customerDescription = "";
                string vendorCode = "";
                string vendorDescription = "";
                
                // Toptan satış ve alış faturaları için farklı alanları kontrol et
                if (reader.HasColumn("CustomerCode") && reader.HasColumn("CustomerDescription"))
                {
                    // SQL sorgusunda CASE ile oluşturulan alanları kullan
                    customerCode = reader["CustomerCode"].ToString();
                    customerDescription = reader["CustomerDescription"].ToString();
                    vendorCode = reader["VendorCode"].ToString();
                    vendorDescription = reader["VendorDescription"].ToString();
                }
                else
                {
                    // Doğrudan CurrAccCode ve CurrAccDescription kullan
                    if (currAccTypeCode == 3) // Müşteri
                    {
                        customerCode = reader["CurrAccCode"].ToString();
                        customerDescription = reader["CurrAccDescription"] != DBNull.Value ? reader["CurrAccDescription"].ToString() : "";
                    }
                    else if (currAccTypeCode == 1) // Tedarikçi
                    {
                        vendorCode = reader["CurrAccCode"].ToString();
                        vendorDescription = reader["CurrAccDescription"] != DBNull.Value ? reader["CurrAccDescription"].ToString() : "";
                    }
                }
                
                var invoice = new InvoiceHeaderModel
                {
                    InvoiceHeaderID = reader["InvoiceHeaderID"].ToString(),
                    InvoiceNumber = reader["InvoiceNumber"].ToString(),
                    InvoiceDate = Convert.ToDateTime(reader["InvoiceDate"]),
                    InvoiceTime = reader["InvoiceTime"].ToString(),
                    IsReturn = Convert.ToBoolean(reader["IsReturn"]),
                    IsEInvoice = Convert.ToBoolean(reader["IsEInvoice"]),
                    CurrAccTypeCode = currAccTypeCode,
                    CustomerCode = customerCode,
                    CustomerDescription = customerDescription,
                    VendorCode = vendorCode,
                    VendorDescription = vendorDescription,
                    DocCurrencyCode = reader["DocCurrencyCode"].ToString(),
                    CompanyCode = reader["CompanyCode"].ToString(),
                    OfficeCode = reader["OfficeCode"].ToString(),
                    StoreCode = reader["StoreCode"].ToString(),
                    WarehouseCode = reader["WarehouseCode"].ToString(),
                    ProcessCode = processCode,
                    IsCompleted = Convert.ToBoolean(reader["IsCompleted"]),
                    IsSuspended = Convert.ToBoolean(reader["IsSuspended"]),
                    IsLocked = Convert.ToBoolean(reader["IsLocked"]),
                    IsPrinted = Convert.ToBoolean(reader["IsPrinted"]),
                    Status = Convert.ToBoolean(reader["IsCompleted"]) ? "Tamamlandı" : "Bekliyor"
                };
                
                // Opsiyonel alanları kontrol et
                if (reader.HasColumn("TotalGrossAmount"))
                    invoice.TotalAmount = reader["TotalGrossAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalGrossAmount"]) : 0;
                    
                if (reader.HasColumn("TotalVatAmount"))
                    invoice.TotalTax = reader["TotalVatAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalVatAmount"]) : 0;
                    
                if (reader.HasColumn("TotalDiscountAmount"))
                    invoice.TotalDiscount = reader["TotalDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalDiscountAmount"]) : 0;
                    
                if (reader.HasColumn("TotalNetAmount"))
                    invoice.NetAmount = reader["TotalNetAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalNetAmount"]) : 0;
                    
                if (reader.HasColumn("CreatedUserName"))
                {
                    invoice.CreatedBy = reader["CreatedUserName"].ToString();
                    invoice.CreatedUserName = reader["CreatedUserName"].ToString();
                }
                    
                if (reader.HasColumn("CreatedDate"))
                    invoice.CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue;
                    
                if (reader.HasColumn("LastUpdatedUserName"))
                {
                    invoice.ModifiedBy = reader["LastUpdatedUserName"].ToString();
                    invoice.LastUpdatedUserName = reader["LastUpdatedUserName"].ToString();
                }
                    
                if (reader.HasColumn("LastUpdatedDate"))
                {
                    invoice.ModifiedDate = reader["LastUpdatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["LastUpdatedDate"]) : null;
                    invoice.LastUpdatedDate = reader["LastUpdatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["LastUpdatedDate"]) : null;
                }
                
                return invoice;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatura verisi dönüştürülürken hata oluştu: {Message}", ex.Message);
                // Hata durumunda boş bir model döndür
                return new InvoiceHeaderModel
                {
                    InvoiceNumber = "Hata",
                    Status = "Hata: " + ex.Message
                };
            }
        }

        // Direkt satış faturalarını getiren metot
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetDirectSalesInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", request.LangCode ?? "TR")
                };

                var whereConditions = new List<string>();

                // Direkt toptan satış faturaları için ProcessCode = WS  
                whereConditions.Add("trInvoiceHeader.ProcessCode = @ProcessCode");
                parameters.Add(new SqlParameter("@ProcessCode", "WS"));

                // Fatura tipi filtresi (eğer belirtilmişse ve RS değilse override et)
                if (!string.IsNullOrEmpty(request.ProcessCode) && request.ProcessCode != "WS")
                {
                    _logger.LogWarning($"Direkt satış faturaları için ProcessCode={request.ProcessCode} belirtilmiş, ancak WS olarak override edildi.");
                }

                // Diğer filtreler
                if (!string.IsNullOrEmpty(request.InvoiceNumber))
                {
                    whereConditions.Add("trInvoiceHeader.InvoiceNumber LIKE @InvoiceNumber");
                    parameters.Add(new SqlParameter("@InvoiceNumber", $"%{request.InvoiceNumber}%"));
                }

                if (!string.IsNullOrEmpty(request.CustomerCode))
                {
                    whereConditions.Add("trInvoiceHeader.CurrAccCode = @CustomerCode");
                    parameters.Add(new SqlParameter("@CustomerCode", request.CustomerCode));
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

                // SQL sorgusunu oluştur
                string sqlQuery = File.ReadAllText(Path.Combine(_env.ContentRootPath, "Repositories", "Invoice", "SQL", "DirectSalesInvoicesQuery.sql"));

                // WHERE koşullarını ekle
                if (whereConditions.Count > 0)
                {
                    sqlQuery += "\nWHERE " + string.Join("\nAND ", whereConditions);
                }

                // Sıralama ekle
                sqlQuery += "\nORDER BY trInvoiceHeader.InvoiceDate DESC, trInvoiceHeader.InvoiceNumber DESC";

                // Sayfalama için toplam sayıyı al
                string countQuery = $"SELECT COUNT(*) FROM trInvoiceHeader WITH (NOLOCK) WHERE {string.Join(" AND ", whereConditions)}"; 

                // Sayfalama ekle
                if (request.Page > 0 && request.PageSize > 0)
                {
                    int offset = (request.Page - 1) * request.PageSize;
                    sqlQuery += $"\nOFFSET {offset} ROWS FETCH NEXT {request.PageSize} ROWS ONLY";
                }

                // SQL parametrelerini {LangCode} ile değiştir
                sqlQuery = sqlQuery.Replace("{LangCode}", "@LangCode");

                _logger.LogInformation($"Direkt satış fatura sorgusu: {sqlQuery}");

                // Sorguyu çalıştır
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    // Toplam sayıyı al
                    int totalCount = 0;
                    using (var countCommand = new SqlCommand(countQuery, connection))
                    {
                        foreach (var parameter in parameters)
                        {
                            countCommand.Parameters.Add(parameter);
                        }

                        var countResult = await countCommand.ExecuteScalarAsync();
                        totalCount = Convert.ToInt32(countResult);
                    }

                    // Faturaları al
                    var invoices = new List<InvoiceHeaderModel>();
                    using (var command = new SqlCommand(sqlQuery, connection))
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var invoice = MapToInvoiceHeaderModel(reader);
                                invoices.Add(invoice);
                            }
                        }
                    }

                    return (invoices, totalCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Direkt satış fatura listesi alınırken hata oluştu");
                throw;
            }
        }

        // İrsaliye bazlı faturaları getiren metot
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetShipmentBasedInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", request.LangCode ?? "TR")
                };

                var whereConditions = new List<string>();

                // Fatura tipi filtresi (eğer belirtilmişse)
                if (!string.IsNullOrEmpty(request.ProcessCode))
                {
                    whereConditions.Add("trInvoiceHeader.ProcessCode = @ProcessCode");
                    parameters.Add(new SqlParameter("@ProcessCode", request.ProcessCode));
                }

                // İrsaliye bazlı faturaları filtrele
                whereConditions.Add("trInvoiceHeader.IsShipmentBase = 1");

                // Diğer filtreler
                if (!string.IsNullOrEmpty(request.InvoiceNumber))
                {
                    whereConditions.Add("trInvoiceHeader.InvoiceNumber LIKE @InvoiceNumber");
                    parameters.Add(new SqlParameter("@InvoiceNumber", $"%{request.InvoiceNumber}%"));
                }

                if (!string.IsNullOrEmpty(request.CustomerCode))
                {
                    whereConditions.Add("trInvoiceHeader.CurrAccCode = @CustomerCode");
                    parameters.Add(new SqlParameter("@CustomerCode", request.CustomerCode));
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

                // SQL sorgusunu oluştur
                string sqlQuery = File.ReadAllText(Path.Combine(_env.ContentRootPath, "Repositories", "Invoice", "SQL", "ShipmentBasedInvoicesQuery.sql"));

                // WHERE koşullarını ekle
                if (whereConditions.Count > 0)
                {
                    sqlQuery += "\nWHERE " + string.Join("\nAND ", whereConditions);
                }

                // Sıralama ekle
                sqlQuery += "\nORDER BY trInvoiceHeader.InvoiceDate DESC, trInvoiceHeader.InvoiceNumber DESC";

                // Sayfalama için toplam sayıyı al
                string countQuery = $"SELECT COUNT(*) FROM trInvoiceHeader WITH (NOLOCK) WHERE {string.Join(" AND ", whereConditions)}"; 

                // Sayfalama ekle
                if (request.Page > 0 && request.PageSize > 0)
                {
                    int offset = (request.Page - 1) * request.PageSize;
                    sqlQuery += $"\nOFFSET {offset} ROWS FETCH NEXT {request.PageSize} ROWS ONLY";
                }

                // SQL parametrelerini {LangCode} ile değiştir
                sqlQuery = sqlQuery.Replace("{LangCode}", "@LangCode");

                _logger.LogInformation($"İrsaliye bazlı fatura sorgusu: {sqlQuery}");

                // Sorguyu çalıştır
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    // Toplam sayıyı al
                    int totalCount = 0;
                    using (var countCommand = new SqlCommand(countQuery, connection))
                    {
                        foreach (var parameter in parameters)
                        {
                            countCommand.Parameters.Add(parameter);
                        }

                        var countResult = await countCommand.ExecuteScalarAsync();
                        totalCount = Convert.ToInt32(countResult);
                    }

                    // Faturaları al
                    var invoices = new List<InvoiceHeaderModel>();
                    using (var command = new SqlCommand(sqlQuery, connection))
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var invoice = MapToInvoiceHeaderModel(reader);
                                invoices.Add(invoice);
                            }
                        }
                    }

                    return (invoices, totalCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İrsaliye bazlı fatura listesi alınırken hata oluştu");
                throw;
            }
        }

        // Sipariş bazlı faturaları getiren metot
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetOrderBasedInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", request.LangCode ?? "TR")
                };

                var whereConditions = new List<string>();

                // Fatura tipi filtresi (eğer belirtilmişse)
                if (!string.IsNullOrEmpty(request.ProcessCode))
                {
                    whereConditions.Add("trInvoiceHeader.ProcessCode = @ProcessCode");
                    parameters.Add(new SqlParameter("@ProcessCode", request.ProcessCode));
                }

                // Sipariş bazlı faturaları filtrele
                whereConditions.Add("trInvoiceHeader.IsOrderBase = 1");

                // Diğer filtreler
                if (!string.IsNullOrEmpty(request.InvoiceNumber))
                {
                    whereConditions.Add("trInvoiceHeader.InvoiceNumber LIKE @InvoiceNumber");
                    parameters.Add(new SqlParameter("@InvoiceNumber", $"%{request.InvoiceNumber}%"));
                }

                if (!string.IsNullOrEmpty(request.CustomerCode))
                {
                    whereConditions.Add("trInvoiceHeader.CurrAccCode = @CustomerCode");
                    parameters.Add(new SqlParameter("@CustomerCode", request.CustomerCode));
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

                // SQL sorgusunu oluştur
                string sqlQuery = File.ReadAllText(Path.Combine(_env.ContentRootPath, "Repositories", "Invoice", "SQL", "OrderBasedInvoicesQuery.sql"));

                // WHERE koşullarını ekle
                if (whereConditions.Count > 0)
                {
                    sqlQuery += "\nWHERE " + string.Join("\nAND ", whereConditions);
                }

                // Sıralama ekle
                sqlQuery += "\nORDER BY trInvoiceHeader.InvoiceDate DESC, trInvoiceHeader.InvoiceNumber DESC";

                // Sayfalama için toplam sayıyı al
                string countQuery = $"SELECT COUNT(*) FROM trInvoiceHeader WITH (NOLOCK) WHERE {string.Join(" AND ", whereConditions)}"; 

                // Sayfalama ekle
                if (request.Page > 0 && request.PageSize > 0)
                {
                    int offset = (request.Page - 1) * request.PageSize;
                    sqlQuery += $"\nOFFSET {offset} ROWS FETCH NEXT {request.PageSize} ROWS ONLY";
                }

                // SQL parametrelerini {LangCode} ile değiştir
                sqlQuery = sqlQuery.Replace("{LangCode}", "@LangCode");

                _logger.LogInformation($"Sipariş bazlı fatura sorgusu: {sqlQuery}");

                // Sorguyu çalıştır
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    // Toplam sayıyı al
                    int totalCount = 0;
                    using (var countCommand = new SqlCommand(countQuery, connection))
                    {
                        foreach (var parameter in parameters)
                        {
                            countCommand.Parameters.Add(parameter);
                        }

                        var countResult = await countCommand.ExecuteScalarAsync();
                        totalCount = Convert.ToInt32(countResult);
                    }

                    // Faturaları al
                    var invoices = new List<InvoiceHeaderModel>();
                    using (var command = new SqlCommand(sqlQuery, connection))
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var invoice = MapToInvoiceHeaderModel(reader);
                                invoices.Add(invoice);
                            }
                        }
                    }

                    return (invoices, totalCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sipariş bazlı fatura listesi alınırken hata oluştu");
                throw;
            }
        }

        // Sipariş bazlı toptan alış faturalarını getiren metot
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetOrderBasedPurchaseInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                // SQL dosyasını oku
                string sqlFilePath = Path.Combine(_env.ContentRootPath, "Repositories", "Invoice", "SQL", "OrderBasedPurchaseInvoicesQuery.sql");
                string sqlTemplate = await File.ReadAllTextAsync(sqlFilePath);
                
                // Sayfalama parametreleri
                int pageSize = request.PageSize <= 0 ? 10 : request.PageSize;
                int offset = (request.Page <= 0 ? 0 : request.Page - 1) * pageSize;
                
                // Filtreleri hazırla
                string invoiceNumberFilter = string.IsNullOrEmpty(request.InvoiceNumber) ? "" : "AND trInvoiceHeader.InvoiceNumber LIKE @InvoiceNumber";
                string vendorCodeFilter = string.IsNullOrEmpty(request.VendorCode) ? "" : "AND trInvoiceHeader.CurrAccCode = @VendorCode";
                string companyCodeFilter = string.IsNullOrEmpty(request.CompanyCode) ? "" : "AND trInvoiceHeader.CompanyCode = @CompanyCode";
                string storeCodeFilter = string.IsNullOrEmpty(request.StoreCode) ? "" : "AND trInvoiceHeader.StoreCode = @StoreCode";
                string warehouseCodeFilter = string.IsNullOrEmpty(request.WarehouseCode) ? "" : "AND trInvoiceHeader.WarehouseCode = @WarehouseCode";
                
                // Tarih filtreleri
                string startDateFilter = request.StartDate.HasValue ? "AND trInvoiceHeader.InvoiceDate >= @StartDate" : "";
                string endDateFilter = request.EndDate.HasValue ? "AND trInvoiceHeader.InvoiceDate <= @EndDate" : "";
                
                // SQL sorgusu oluştur
                string sql = sqlTemplate
                    .Replace("{LangCode}", "@LangCode")
                    .Replace("{InvoiceNumberFilter}", invoiceNumberFilter)
                    .Replace("{VendorCodeFilter}", vendorCodeFilter)
                    .Replace("{CompanyCodeFilter}", companyCodeFilter)
                    .Replace("{StoreCodeFilter}", storeCodeFilter)
                    .Replace("{WarehouseCodeFilter}", warehouseCodeFilter)
                    .Replace("{StartDateFilter}", startDateFilter)
                    .Replace("{EndDateFilter}", endDateFilter)
                    .Replace("{Offset}", offset.ToString())
                    .Replace("{PageSize}", pageSize.ToString());
                
                // Toplam kayıt sayısı için sorgu
                string countSql = $@"SELECT COUNT(*) 
                    FROM trInvoiceHeader WITH (NOLOCK) 
                    WHERE 1=1 
                   AND trInvoiceHeader.IsOrderBase = 1 --Sipariş bazlı
                    AND trInvoiceHeader.IsShipmentBase = 0 --İrsaliye bazlı
                   
                    AND trInvoiceHeader.ProcessCode = 'BP' --Toptan Alış Faturası
                    AND trInvoiceHeader.CurrAccTypeCode = 1 --Tedarikçi
                    {invoiceNumberFilter} 
                    {vendorCodeFilter} 
                    {companyCodeFilter} 
                    {storeCodeFilter} 
                    {warehouseCodeFilter} 
                    {startDateFilter} 
                    {endDateFilter}";
                
                // Parametreleri hazırla
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", request.LangCode ?? "TR")
                };
                
                if (!string.IsNullOrEmpty(request.InvoiceNumber))
                    parameters.Add(new SqlParameter("@InvoiceNumber", $"%{request.InvoiceNumber}%"));
                    
                if (!string.IsNullOrEmpty(request.VendorCode))
                    parameters.Add(new SqlParameter("@VendorCode", request.VendorCode));
                    
                if (!string.IsNullOrEmpty(request.CompanyCode))
                    parameters.Add(new SqlParameter("@CompanyCode", request.CompanyCode));
                    
                if (!string.IsNullOrEmpty(request.StoreCode))
                    parameters.Add(new SqlParameter("@StoreCode", request.StoreCode));
                    
                if (!string.IsNullOrEmpty(request.WarehouseCode))
                    parameters.Add(new SqlParameter("@WarehouseCode", request.WarehouseCode));
                    
              
                if (request.StartDate.HasValue)
                    parameters.Add(new SqlParameter("@StartDate", request.StartDate.Value));
                    
                if (request.EndDate.HasValue)
                    parameters.Add(new SqlParameter("@EndDate", request.EndDate.Value));
                
                // Veritabanı bağlantısı oluştur
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Toplam kayıt sayısını al
                    var countCommand = new SqlCommand(countSql, connection);
                    foreach (var parameter in parameters)
                    {
                        countCommand.Parameters.Add(parameter);
                    }
                    var totalCount = (int)await countCommand.ExecuteScalarAsync();
                    
                    // Verileri al
                    var command = new SqlCommand(sql, connection);
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                    
                    var invoices = new List<InvoiceHeaderModel>();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var invoice = MapToInvoiceHeaderModel(reader);
                            invoices.Add(invoice);
                        }
                    }
                    
                    return (invoices, totalCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sipariş bazlı alış faturaları getirilirken hata oluştu");
                throw;
            }
        }

        // İrsaliye bazlı alış faturalarını getiren metot
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetShipmentBasedPurchaseInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                // SQL dosyasını oku
                string sqlFilePath = Path.Combine(_env.ContentRootPath, "Repositories", "Invoice", "SQL", "ShipmentBasedPurchaseInvoicesQuery.sql");
                string sqlTemplate = await File.ReadAllTextAsync(sqlFilePath);
                
                // Sayfalama parametreleri
                int pageSize = request.PageSize <= 0 ? 10 : request.PageSize;
                int offset = (request.Page <= 0 ? 0 : request.Page - 1) * pageSize;
                
                // Filtreleri hazırla
                string invoiceNumberFilter = string.IsNullOrEmpty(request.InvoiceNumber) ? "" : "AND trInvoiceHeader.InvoiceNumber LIKE @InvoiceNumber";
                string vendorCodeFilter = string.IsNullOrEmpty(request.VendorCode) ? "" : "AND trInvoiceHeader.CurrAccCode = @VendorCode";
                string companyCodeFilter = string.IsNullOrEmpty(request.CompanyCode) ? "" : "AND trInvoiceHeader.CompanyCode = @CompanyCode";
                string storeCodeFilter = string.IsNullOrEmpty(request.StoreCode) ? "" : "AND trInvoiceHeader.StoreCode = @StoreCode";
                string warehouseCodeFilter = string.IsNullOrEmpty(request.WarehouseCode) ? "" : "AND trInvoiceHeader.WarehouseCode = @WarehouseCode";
               
                // Tarih filtreleri
                string startDateFilter = request.StartDate.HasValue ? "AND trInvoiceHeader.InvoiceDate >= @StartDate" : "";
                string endDateFilter = request.EndDate.HasValue ? "AND trInvoiceHeader.InvoiceDate <= @EndDate" : "";
                
                // SQL sorgusu oluştur
                string sql = sqlTemplate
                    .Replace("{LangCode}", "@LangCode")
                    .Replace("{InvoiceNumberFilter}", invoiceNumberFilter)
                    .Replace("{VendorCodeFilter}", vendorCodeFilter)
                    .Replace("{CompanyCodeFilter}", companyCodeFilter)
                    .Replace("{StoreCodeFilter}", storeCodeFilter)
                    .Replace("{WarehouseCodeFilter}", warehouseCodeFilter)
                    .Replace("{StartDateFilter}", startDateFilter)
                    .Replace("{EndDateFilter}", endDateFilter)
                    .Replace("{Offset}", offset.ToString())
                    .Replace("{PageSize}", pageSize.ToString());
                
                // Toplam kayıt sayısı için sorgu
                string countSql = $@"SELECT COUNT(*) 
                    FROM trInvoiceHeader WITH (NOLOCK) 
                    WHERE 1=1 
                    AND trInvoiceHeader.IsShipmentBase = 1 
                    AND trInvoiceHeader.ProcessCode = 'BP' 
                    AND trInvoiceHeader.CurrAccTypeCode = 1 
                    {invoiceNumberFilter} 
                    {vendorCodeFilter} 
                    {companyCodeFilter} 
                    {storeCodeFilter} 
                    {warehouseCodeFilter} 
                    {startDateFilter} 
                    {endDateFilter}";
                
                // Parametreleri hazırla
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", request.LangCode ?? "TR")
                };
                
                if (!string.IsNullOrEmpty(request.InvoiceNumber))
                    parameters.Add(new SqlParameter("@InvoiceNumber", $"%{request.InvoiceNumber}%"));
                    
                if (!string.IsNullOrEmpty(request.VendorCode))
                    parameters.Add(new SqlParameter("@VendorCode", request.VendorCode));
                    
                if (!string.IsNullOrEmpty(request.CompanyCode))
                    parameters.Add(new SqlParameter("@CompanyCode", request.CompanyCode));
                    
                if (!string.IsNullOrEmpty(request.StoreCode))
                    parameters.Add(new SqlParameter("@StoreCode", request.StoreCode));
                    
                if (!string.IsNullOrEmpty(request.WarehouseCode))
                    parameters.Add(new SqlParameter("@WarehouseCode", request.WarehouseCode));
                    
                if (request.StartDate.HasValue)
                    parameters.Add(new SqlParameter("@StartDate", request.StartDate.Value));
                    
                if (request.EndDate.HasValue)
                    parameters.Add(new SqlParameter("@EndDate", request.EndDate.Value));
                
                // Veritabanı bağlantısı oluştur
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Toplam kayıt sayısını al
                    var countCommand = new SqlCommand(countSql, connection);
                    foreach (var parameter in parameters)
                    {
                        countCommand.Parameters.Add(parameter);
                    }
                    var totalCount = (int)await countCommand.ExecuteScalarAsync();
                    
                    // Verileri al
                    var command = new SqlCommand(sql, connection);
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                    
                    var invoices = new List<InvoiceHeaderModel>();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var invoice = MapToInvoiceHeaderModel(reader);
                            invoices.Add(invoice);
                        }
                    }
                    
                    return (invoices, totalCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İrsaliye bazlı alış faturaları getirilirken hata oluştu");
                throw;
            }
        }

        // Direkt toptan alış faturalarını getiren metot
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetDirectWholesalePurchaseInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                // SQL dosyasını oku
                string sqlFilePath = Path.Combine(_env.ContentRootPath, "Repositories", "Invoice", "SQL", "DirectWholesalePurchaseInvoicesQuery.sql");
                string sqlTemplate = await File.ReadAllTextAsync(sqlFilePath);
                
                // Sayfalama parametreleri
                int pageSize = request.PageSize <= 0 ? 10 : request.PageSize;
                int offset = (request.Page <= 0 ? 0 : request.Page - 1) * pageSize;
                
                // Filtreleri hazırla
                string invoiceNumberFilter = string.IsNullOrEmpty(request.InvoiceNumber) ? "" : "AND trInvoiceHeader.InvoiceNumber LIKE @InvoiceNumber";
                string vendorCodeFilter = string.IsNullOrEmpty(request.VendorCode) ? "" : "AND trInvoiceHeader.CurrAccCode = @VendorCode";
                string companyCodeFilter = string.IsNullOrEmpty(request.CompanyCode) ? "" : "AND trInvoiceHeader.CompanyCode = @CompanyCode";
                string storeCodeFilter = string.IsNullOrEmpty(request.StoreCode) ? "" : "AND trInvoiceHeader.StoreCode = @StoreCode";
                string warehouseCodeFilter = string.IsNullOrEmpty(request.WarehouseCode) ? "" : "AND trInvoiceHeader.WarehouseCode = @WarehouseCode";
                
                // Tarih filtreleri
                string startDateFilter = request.StartDate.HasValue ? "AND trInvoiceHeader.InvoiceDate >= @StartDate" : "";
                string endDateFilter = request.EndDate.HasValue ? "AND trInvoiceHeader.InvoiceDate <= @EndDate" : "";
                
                // SQL sorgusu oluştur
                string sql = sqlTemplate
                    .Replace("{LangCode}", "@LangCode")
                    .Replace("{InvoiceNumberFilter}", invoiceNumberFilter)
                    .Replace("{VendorCodeFilter}", vendorCodeFilter)
                    .Replace("{CompanyCodeFilter}", companyCodeFilter)
                    .Replace("{StoreCodeFilter}", storeCodeFilter)
                    .Replace("{WarehouseCodeFilter}", warehouseCodeFilter)
                    .Replace("{StartDateFilter}", startDateFilter)
                    .Replace("{EndDateFilter}", endDateFilter)
                    .Replace("{Offset}", offset.ToString())
                    .Replace("{PageSize}", pageSize.ToString());
                
                // Toplam kayıt sayısı için sorgu
                string countSql = $@"SELECT COUNT(*) 
                    FROM trInvoiceHeader WITH (NOLOCK) 
                    WHERE 1=1 
                    AND trInvoiceHeader.IsOrderBase = 0 --Sipariş bazlı
                    AND trInvoiceHeader.IsShipmentBase = 0 --İrsaliye bazlı
                    AND trInvoiceHeader.ProcessCode = 'BP' --Toptan Alış Faturası
                    AND trInvoiceHeader.CurrAccTypeCode = 1 --Tedarikçi
                    {invoiceNumberFilter} 
                    {vendorCodeFilter} 
                    {companyCodeFilter} 
                    {storeCodeFilter} 
                    {warehouseCodeFilter} 
                    {startDateFilter} 
                    {endDateFilter}";
                
                // Parametreleri hazırla
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", request.LangCode ?? "TR")
                };
                
                if (!string.IsNullOrEmpty(request.InvoiceNumber))
                    parameters.Add(new SqlParameter("@InvoiceNumber", $"%{request.InvoiceNumber}%"));
                    
                if (!string.IsNullOrEmpty(request.VendorCode))
                    parameters.Add(new SqlParameter("@VendorCode", request.VendorCode));
                    
                if (!string.IsNullOrEmpty(request.CompanyCode))
                    parameters.Add(new SqlParameter("@CompanyCode", request.CompanyCode));
                    
                if (!string.IsNullOrEmpty(request.StoreCode))
                    parameters.Add(new SqlParameter("@StoreCode", request.StoreCode));
                    
                if (!string.IsNullOrEmpty(request.WarehouseCode))
                    parameters.Add(new SqlParameter("@WarehouseCode", request.WarehouseCode));
                    
                if (request.StartDate.HasValue)
                    parameters.Add(new SqlParameter("@StartDate", request.StartDate.Value));
                    
                if (request.EndDate.HasValue)
                    parameters.Add(new SqlParameter("@EndDate", request.EndDate.Value));
                
                // Veritabanı bağlantısı oluştur
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Toplam kayıt sayısını al
                    var countCommand = new SqlCommand(countSql, connection);
                    foreach (var parameter in parameters)
                    {
                        countCommand.Parameters.Add(parameter);
                    }
                    var totalCount = (int)await countCommand.ExecuteScalarAsync();
                    
                    // Verileri al
                    var command = new SqlCommand(sql, connection);
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                    
                    var invoices = new List<InvoiceHeaderModel>();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var invoice = MapToInvoiceHeaderModel(reader);
                            invoices.Add(invoice);
                        }
                    }
                    
                    return (invoices, totalCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Direkt toptan alış faturaları getirilirken hata oluştu");
                throw;
            }
        }

        // Tüm fatura tiplerini getiren genel metot
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetAllInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", request.LangCode ?? "TR")
                };

                var whereConditions = new List<string>();

                // Fatura tipi filtresi (eğer belirtilmişse)
                if (!string.IsNullOrEmpty(request.ProcessCode))
                {
                    // ProcessCode'u doğrudan ProcessCode olarak kullan
                    whereConditions.Add("trInvoiceHeader.ProcessCode = @ProcessCode");
                    parameters.Add(new SqlParameter("@ProcessCode", request.ProcessCode));
                    
                    Console.WriteLine($"Filtreleme: ProcessCode = {request.ProcessCode}");
                }

                if (!string.IsNullOrEmpty(request.InvoiceNumber))
                {
                    whereConditions.Add("trInvoiceHeader.InvoiceNumber LIKE @InvoiceNumber");
                    parameters.Add(new SqlParameter("@InvoiceNumber", $"%{request.InvoiceNumber}%"));
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

                if (!string.IsNullOrEmpty(request.CompanyCode))
                {
                    whereConditions.Add("trInvoiceHeader.CompanyCode = @CompanyCode");
                    parameters.Add(new SqlParameter("@CompanyCode", request.CompanyCode));
                }

                if (!string.IsNullOrEmpty(request.StoreCode))
                {
                    whereConditions.Add("trInvoiceHeader.StoreCode = @StoreCode");
                    parameters.Add(new SqlParameter("@StoreCode", request.StoreCode));
                }

                if (!string.IsNullOrEmpty(request.WarehouseCode))
                {
                    whereConditions.Add("trInvoiceHeader.WarehouseCode = @WarehouseCode");
                    parameters.Add(new SqlParameter("@WarehouseCode", request.WarehouseCode));
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

                // Sıralama için
                string orderBy = "trInvoiceHeader.InvoiceDate DESC";
                if (!string.IsNullOrEmpty(request.SortBy))
                {
                    orderBy = $"trInvoiceHeader.{request.SortBy} {(request.SortDirection?.ToLower() == "asc" ? "ASC" : "DESC")}";
                }

                // Sayfalama için
                int page = request.Page > 0 ? request.Page : 1;
                int pageSize = request.PageSize > 0 ? request.PageSize : 10;
                int offset = (page - 1) * pageSize;

                // WHERE koşullarını birleştir
                string whereClause = whereConditions.Count > 0 ? "WHERE " + string.Join(" AND ", whereConditions) : "";

                // Toplam kayıt sayısını almak için sorgu
                string countQuery = $@"
                    SELECT COUNT(*)
                    FROM trInvoiceHeader WITH (NOLOCK)
                    {whereClause}";

                // SQL sorgusunu ve parametreleri logla
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n===== GetAllInvoicesAsync COUNT QUERY =====\n");
                Console.WriteLine(countQuery);
                Console.WriteLine("\n===== PARAMETERS =====\n");
                foreach (var param in parameters)
                {
                    Console.WriteLine($"{param.ParameterName}: {param.Value}");
                }
                Console.ResetColor();

                // Verileri almak için sorgu - AllInvoicesQuery.sql dosyasındaki sorguyu baz alarak
                string query = $@"
                    SELECT 
                        dbo.trInvoiceHeader.InvoiceHeaderID, 
                        dbo.trInvoiceHeader.TransTypeCode, 
                        dbo.trInvoiceHeader.ProcessCode, 
                        dbo.trInvoiceHeader.InvoiceNumber, 
                        dbo.trInvoiceHeader.IsReturn, 
                        dbo.trInvoiceHeader.InvoiceDate,
                        dbo.trInvoiceHeader.InvoiceTime, 
                        dbo.trInvoiceHeader.IsEInvoice, 
                        dbo.trInvoiceHeader.Description,
                        dbo.trInvoiceHeader.CurrAccTypeCode, 
                        dbo.trInvoiceHeader.CurrAccCode,
                        dbo.trInvoiceHeader.CompanyCode, 
                        dbo.trInvoiceHeader.OfficeCode,
                        dbo.trInvoiceHeader.StoreCode, 
                        dbo.trInvoiceHeader.WarehouseCode,
                        dbo.trInvoiceHeader.IsCompleted,
                        dbo.trInvoiceHeader.IsSuspended,
                        dbo.trInvoiceHeader.IsPrinted,
                        dbo.trInvoiceHeader.IsLocked,
                        dbo.trInvoiceHeader.CreatedUserName,
                        dbo.trInvoiceHeader.CreatedDate,
                        dbo.trInvoiceHeader.LastUpdatedUserName,
                        dbo.trInvoiceHeader.LastUpdatedDate
                    FROM 
                        dbo.trInvoiceHeader WITH (NOLOCK)
                    {whereClause}
                    ORDER BY {orderBy}
                    OFFSET {offset} ROWS
                    FETCH NEXT {pageSize} ROWS ONLY";

                // Veritabanı bağlantısı oluştur
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Toplam kayıt sayısını al
                    var countCommand = new SqlCommand(countQuery, connection);
                    foreach (var parameter in parameters)
                    {
                        countCommand.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }
                    var totalCount = (int)await countCommand.ExecuteScalarAsync();

                    // Ana SQL sorgusunu logla
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n===== GetAllInvoicesAsync MAIN QUERY =====\n");
                    Console.WriteLine(query);
                    Console.ResetColor();
                    
                    // Verileri al
                    var command = new SqlCommand(query, connection);
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }

                    var invoices = new List<InvoiceHeaderModel>();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var invoice = MapToInvoiceHeaderModel(reader);
                            invoices.Add(invoice);
                        }
                    }

                    return (invoices, totalCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Faturalar getirilirken hata oluştu");
                throw;
            }
        }
    }
}
