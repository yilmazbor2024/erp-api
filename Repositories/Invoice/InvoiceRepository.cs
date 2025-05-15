using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Invoice;
using ErpMobile.Api.Data;

namespace ErpMobile.Api.Repositories.Invoice
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ILogger<InvoiceRepository> _logger;
        private readonly NanoServiceDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        // String kodları tinyint değerlerine dönüştürmek için eşleştirme tablosu
        private readonly Dictionary<string, byte> _invoiceTypeCodeMapping = new Dictionary<string, byte>(StringComparer.OrdinalIgnoreCase)
        {
            { "RS", 1 },    // Perakende Satış
            { "WS", 2 },    // Toptan Satış
            { "RP", 3 },    // Perakende Alış
            { "WP", 4 },    // Toptan Alış
            { "TSI", 5 },   // Toptan Satış İade
            { "TSF", 2 }    // Toptan Satış Faturası (WS ile aynı)
            // Diğer kodlar eklenebilir
        };

        private readonly Dictionary<string, byte> _expenseTypeCodeMapping = new Dictionary<string, byte>(StringComparer.OrdinalIgnoreCase)
        {
            { "E", 1 },     // Masraf Faturası
            { "EXP", 1 }    // Masraf Faturası (Alternatif kod)
        };

        public InvoiceRepository(
            ILogger<InvoiceRepository> logger,
            NanoServiceDbContext context,
            IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ErpConnection");
        }

        // String kodu tinyint değerine dönüştüren yardımcı metot
        private byte ConvertInvoiceTypeCode(string invoiceTypeCode)
        {
            if (string.IsNullOrEmpty(invoiceTypeCode))
                return 0;
                
            if (_invoiceTypeCodeMapping.TryGetValue(invoiceTypeCode, out byte code))
                return code;
                
            // Eğer doğrudan sayısal bir değer gönderilmişse
            if (byte.TryParse(invoiceTypeCode, out byte numericCode))
                return numericCode;
                
            return 0; // Varsayılan değer
        }

        // String kodu tinyint değerine dönüştüren yardımcı metot
        private byte ConvertExpenseTypeCode(string expenseTypeCode)
        {
            if (string.IsNullOrEmpty(expenseTypeCode))
                return 0;
                
            if (_expenseTypeCodeMapping.TryGetValue(expenseTypeCode, out byte code))
                return code;
                
            // Eğer doğrudan sayısal bir değer gönderilmişse
            if (byte.TryParse(expenseTypeCode, out byte numericCode))
                return numericCode;
                
            return 0; // Varsayılan değer
        }

        // Tinyint değerini string koda dönüştüren yardımcı metot
        private string ConvertInvoiceTypeCodeToString(byte invoiceTypeCode)
        {
            switch (invoiceTypeCode)
            {
                case 1: return "RS";  // Perakende Satış
                case 2: return "WS";  // Toptan Satış
                case 3: return "RP";  // Perakende Alış
                case 4: return "WP";  // Toptan Alış
                case 5: return "TSI"; // Toptan Satış İade
                default: return invoiceTypeCode.ToString();
            }
        }

        // Tinyint değerini string koda dönüştüren yardımcı metot
        private string ConvertExpenseTypeCodeToString(byte expenseTypeCode)
        {
            switch (expenseTypeCode)
            {
                case 1: return "E";   // Masraf Faturası
                default: return expenseTypeCode.ToString();
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

                // InvoiceTypeCode filtresi (Toptan Satış Faturaları için)
                // String kodları tinyint değerlerine dönüştür
                whereConditions.Add("trInvoiceHeader.InvoiceTypeCode IN (2, 5)"); // 2=WS/TSF, 5=TSI

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

                // InvoiceTypeCode filtresi (string değeri tinyint'e dönüştür)
                if (!string.IsNullOrEmpty(request.InvoiceTypeCode))
                {
                    byte invoiceTypeCode = ConvertInvoiceTypeCode(request.InvoiceTypeCode);
                    whereConditions.Add("trInvoiceHeader.InvoiceTypeCode = @InvoiceTypeCode");
                    parameters.Add(new SqlParameter("@InvoiceTypeCode", invoiceTypeCode));
                }
                
                // ExpenseTypeCode filtresi (string değeri tinyint'e dönüştür)
                if (!string.IsNullOrEmpty(request.ExpenseTypeCode))
                {
                    byte expenseTypeCode = ConvertExpenseTypeCode(request.ExpenseTypeCode);
                    whereConditions.Add("trInvoiceHeader.ExpenseTypeCode = @ExpenseTypeCode");
                    parameters.Add(new SqlParameter("@ExpenseTypeCode", expenseTypeCode));
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
                         , InvoiceTypeCode = trInvoiceHeader.InvoiceTypeCode
                         , InvoiceTypeDescription = (SELECT InvoiceTypeDescription FROM InvoiceType(@LangCode) WHERE InvoiceType.InvoiceTypeCode = trInvoiceHeader.InvoiceTypeCode)
                         
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
                         , ExpenseTypeCode = trInvoiceHeader.ExpenseTypeCode
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
                         , InvoiceTypeCode = trInvoiceHeader.InvoiceTypeCode
                         , InvoiceTypeDescription = (SELECT InvoiceTypeDescription FROM InvoiceType(@LangCode) WHERE InvoiceType.InvoiceTypeCode = trInvoiceHeader.InvoiceTypeCode)
                         
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
                         , ExpenseTypeCode = trInvoiceHeader.ExpenseTypeCode
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
                      AND trInvoiceHeader.InvoiceTypeCode IN (2, 5)";

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
                                InvoiceTypeCode = ConvertInvoiceTypeCodeToString((byte)reader["InvoiceTypeCode"]),
                                InvoiceTypeDescription = reader["InvoiceTypeDescription"].ToString(),
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
                                ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                                InvoiceHeaderID = Convert.ToInt32(reader["InvoiceHeaderID"]),
                                ExpenseTypeCode = ConvertExpenseTypeCodeToString((byte)reader["ExpenseTypeCode"]),
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
                                InvoiceHeaderID = Convert.ToInt32(reader["InvoiceHeaderID"]),
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

                // InvoiceTypeCode filtresi (Toptan Alış Faturaları için)
                whereConditions.Add("trInvoiceHeader.InvoiceTypeCode IN (4, 6)"); // 4=WP, 6=TSI

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

                // InvoiceTypeCode filtresi (string değeri tinyint'e dönüştür)
                if (!string.IsNullOrEmpty(request.InvoiceTypeCode))
                {
                    byte invoiceTypeCode = ConvertInvoiceTypeCode(request.InvoiceTypeCode);
                    whereConditions.Add("trInvoiceHeader.InvoiceTypeCode = @InvoiceTypeCode");
                    parameters.Add(new SqlParameter("@InvoiceTypeCode", invoiceTypeCode));
                }
                
                // ExpenseTypeCode filtresi (string değeri tinyint'e dönüştür)
                if (!string.IsNullOrEmpty(request.ExpenseTypeCode))
                {
                    byte expenseTypeCode = ConvertExpenseTypeCode(request.ExpenseTypeCode);
                    whereConditions.Add("trInvoiceHeader.ExpenseTypeCode = @ExpenseTypeCode");
                    parameters.Add(new SqlParameter("@ExpenseTypeCode", expenseTypeCode));
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
                         , InvoiceTypeCode = trInvoiceHeader.InvoiceTypeCode
                         , InvoiceTypeDescription = (SELECT InvoiceTypeDescription FROM InvoiceType(@LangCode) WHERE InvoiceType.InvoiceTypeCode = trInvoiceHeader.InvoiceTypeCode)
                         
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
                         , ExpenseTypeCode = trInvoiceHeader.ExpenseTypeCode
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
                                InvoiceTypeCode = ConvertInvoiceTypeCodeToString((byte)reader["InvoiceTypeCode"]),
                                InvoiceTypeDescription = reader["InvoiceTypeDescription"].ToString(),
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
                                ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                                InvoiceHeaderID = Convert.ToInt32(reader["InvoiceHeaderID"]),
                                ExpenseTypeCode = ConvertExpenseTypeCodeToString((byte)reader["ExpenseTypeCode"]),
                                FormType = reader["FormType"].ToString(),
                                DocumentTypeCode = reader["DocumentTypeCode"].ToString(),
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
                         , InvoiceTypeCode = trInvoiceHeader.InvoiceTypeCode
                         , InvoiceTypeDescription = (SELECT InvoiceTypeDescription FROM InvoiceType(@LangCode) WHERE InvoiceType.InvoiceTypeCode = trInvoiceHeader.InvoiceTypeCode)
                         
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
                         , ExpenseTypeCode = trInvoiceHeader.ExpenseTypeCode
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
                      AND trInvoiceHeader.InvoiceTypeCode IN (4, 6)";

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
                                InvoiceTypeCode = ConvertInvoiceTypeCodeToString((byte)reader["InvoiceTypeCode"]),
                                InvoiceTypeDescription = reader["InvoiceTypeDescription"].ToString(),
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
                                ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                                InvoiceHeaderID = Convert.ToInt32(reader["InvoiceHeaderID"]),
                                ExpenseTypeCode = ConvertExpenseTypeCodeToString((byte)reader["ExpenseTypeCode"]),
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
        private async Task<int> CreateInvoiceHeaderAsync(SqlConnection connection, SqlTransaction transaction, CreateInvoiceRequest request, string invoiceTypeCode)
        {
            var query = @"
                INSERT INTO trInvoiceHeader (
                    InvoiceNumber,
                    InvoiceDate,
                    InvoiceTime,
                    InvoiceTypeCode,
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
                    ExpenseTypeCode,
                    FormType,
                    DocumentTypeCode,
                    CreatedUserName,
                    CreatedDate
                ) VALUES (
                    @InvoiceNumber,
                    @InvoiceDate,
                    @InvoiceTime,
                    @InvoiceTypeCode,
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
                    @ExpenseTypeCode,
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
            
            if (invoiceTypeCode.StartsWith("TS")) // Toptan Satış
            {
                currAccTypeCode = 3; // Müşteri
                currAccCode = request.CustomerCode;
            }
            else if (invoiceTypeCode.StartsWith("TA") || invoiceTypeCode.StartsWith("MA")) // Toptan Alış veya Masraf
            {
                currAccTypeCode = 1; // Tedarikçi
                currAccCode = request.VendorCode;
            }
            else
            {
                throw new ArgumentException($"Geçersiz fatura tipi: {invoiceTypeCode}");
            }

            command.Parameters.AddWithValue("@InvoiceNumber", request.InvoiceNumber);
            command.Parameters.AddWithValue("@InvoiceDate", request.InvoiceDate);
            command.Parameters.AddWithValue("@InvoiceTime", DateTime.Now.ToString("HH:mm:ss"));
            command.Parameters.AddWithValue("@InvoiceTypeCode", ConvertInvoiceTypeCode(invoiceTypeCode));
            command.Parameters.AddWithValue("@CurrAccTypeCode", currAccTypeCode);
            command.Parameters.AddWithValue("@CurrAccCode", currAccCode);
            command.Parameters.AddWithValue("@SubCurrAccID", request.SubCurrAccID.HasValue ? (object)request.SubCurrAccID.Value : DBNull.Value);
            command.Parameters.AddWithValue("@DocCurrencyCode", string.IsNullOrEmpty(request.DocCurrencyCode) ? "TRY" : request.DocCurrencyCode);
            command.Parameters.AddWithValue("@ExchangeRate", request.ExchangeRate.HasValue ? request.ExchangeRate.Value : 1.0m);
            command.Parameters.AddWithValue("@CompanyCode", string.IsNullOrEmpty(request.CompanyCode) ? "001" : request.CompanyCode);
            command.Parameters.AddWithValue("@OfficeCode", string.IsNullOrEmpty(request.OfficeCode) ? "001" : request.OfficeCode);
            command.Parameters.AddWithValue("@StoreCode", string.IsNullOrEmpty(request.StoreCode) ? "001" : request.StoreCode);
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
            command.Parameters.AddWithValue("@ExpenseTypeCode", string.IsNullOrEmpty(request.ExpenseTypeCode) ? DBNull.Value : (object)ConvertExpenseTypeCode(request.ExpenseTypeCode));
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

                // InvoiceTypeCode filtresi (Masraf Faturaları için)
                whereConditions.Add("trInvoiceHeader.InvoiceTypeCode IN (7, 8)"); // 7=MAF, 8=MAI

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

                if (!string.IsNullOrEmpty(request.ExpenseTypeCode))
                {
                    whereConditions.Add("trInvoiceHeader.ExpenseTypeCode = @ExpenseTypeCode");
                    parameters.Add(new SqlParameter("@ExpenseTypeCode", ConvertExpenseTypeCode(request.ExpenseTypeCode)));
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
                         , InvoiceTypeCode = trInvoiceHeader.InvoiceTypeCode
                         , InvoiceTypeDescription = (SELECT InvoiceTypeDescription FROM InvoiceType(@LangCode) WHERE InvoiceType.InvoiceTypeCode = trInvoiceHeader.InvoiceTypeCode)
                         
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
                         , ExpenseTypeCode = trInvoiceHeader.ExpenseTypeCode
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
                                InvoiceTypeCode = ConvertInvoiceTypeCodeToString((byte)reader["InvoiceTypeCode"]),
                                InvoiceTypeDescription = reader["InvoiceTypeDescription"].ToString(),
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
                                ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                                InvoiceHeaderID = Convert.ToInt32(reader["InvoiceHeaderID"]),
                                ExpenseTypeCode = ConvertExpenseTypeCodeToString((byte)reader["ExpenseTypeCode"]),
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
                         , InvoiceTypeCode = trInvoiceHeader.InvoiceTypeCode
                         , InvoiceTypeDescription = (SELECT InvoiceTypeDescription FROM InvoiceType(@LangCode) WHERE InvoiceType.InvoiceTypeCode = trInvoiceHeader.InvoiceTypeCode)
                         
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
                         , ExpenseTypeCode = trInvoiceHeader.ExpenseTypeCode
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
                      AND trInvoiceHeader.InvoiceTypeCode IN (7, 8)";

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
                                InvoiceTypeCode = ConvertInvoiceTypeCodeToString((byte)reader["InvoiceTypeCode"]),
                                InvoiceTypeDescription = reader["InvoiceTypeDescription"].ToString(),
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
                                ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                                InvoiceHeaderID = Convert.ToInt32(reader["InvoiceHeaderID"]),
                                ExpenseTypeCode = ConvertExpenseTypeCodeToString((byte)reader["ExpenseTypeCode"]),
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

        // Yardımcı metotlar
        private InvoiceHeaderModel MapToInvoiceHeaderModel(SqlDataReader reader)
        {
            return new InvoiceHeaderModel
            {
                InvoiceNumber = reader["InvoiceNumber"].ToString(),
                IsReturn = (bool)reader["IsReturn"],
                IsEInvoice = (bool)reader["IsEInvoice"],
                InvoiceTypeCode = ConvertInvoiceTypeCodeToString((byte)reader["InvoiceTypeCode"]),
                InvoiceTypeDescription = reader["InvoiceTypeDescription"].ToString(),
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
                ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                InvoiceHeaderID = Convert.ToInt32(reader["InvoiceHeaderID"]),
                ExpenseTypeCode = ConvertExpenseTypeCodeToString((byte)reader["ExpenseTypeCode"]),
                FormType = reader["FormType"].ToString(),
                DocumentTypeCode = reader["DocumentTypeCode"].ToString(),
                // Ek alanlar varsayılan değerlerle doldurulabilir
                Status = (bool)reader["IsCompleted"] ? "Tamamlandı" : "Bekliyor"
            };
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
                if (!string.IsNullOrEmpty(request.InvoiceTypeCode))
                {
                    byte invoiceTypeCode = ConvertInvoiceTypeCode(request.InvoiceTypeCode);
                    if (invoiceTypeCode > 0)
                    {
                        whereConditions.Add("trInvoiceHeader.InvoiceTypeCode = @InvoiceTypeCode");
                        parameters.Add(new SqlParameter("@InvoiceTypeCode", invoiceTypeCode));
                    }
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

                if (!string.IsNullOrEmpty(request.ExpenseTypeCode))
                {
                    byte expenseTypeCode = ConvertExpenseTypeCode(request.ExpenseTypeCode);
                    if (expenseTypeCode > 0)
                    {
                        whereConditions.Add("trInvoiceHeader.ExpenseTypeCode = @ExpenseTypeCode");
                        parameters.Add(new SqlParameter("@ExpenseTypeCode", expenseTypeCode));
                    }
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
                    FROM trInvoiceHeader
                    {whereClause}";

                // Verileri almak için sorgu
                string query = $@"
                    SELECT 
                        trInvoiceHeader.InvoiceHeaderID,
                        trInvoiceHeader.TransTypeCode,
                        trInvoiceHeader.ProcessCode,
                        trInvoiceHeader.InvoiceNumber,
                        trInvoiceHeader.IsReturn,
                        trInvoiceHeader.InvoiceDate,
                        trInvoiceHeader.InvoiceTime,
                        trInvoiceHeader.OperationDate,
                        trInvoiceHeader.Series,
                        trInvoiceHeader.SeriesNumber,
                        trInvoiceHeader.InvoiceTypeCode,
                        trInvoiceHeader.ExpenseTypeCode,
                        trInvoiceHeader.IsEInvoice,
                        trInvoiceHeader.EInvoiceNumber,
                        trInvoiceHeader.EInvoiceStatusCode,
                        trInvoiceHeader.PaymentTerm,
                        trInvoiceHeader.Description,
                        trInvoiceHeader.CurrAccTypeCode,
                        trInvoiceHeader.CurrAccCode,
                        cdCurrAcc.CurrAccDescription,
                        trInvoiceHeader.DocCurrencyCode,
                        trInvoiceHeader.ExchangeRate,
                        trInvoiceHeader.TotalAmount,
                        trInvoiceHeader.TotalDiscount,
                        trInvoiceHeader.TotalVatAmount,
                        trInvoiceHeader.NetAmount,
                        trInvoiceHeader.CompanyCode,
                        trInvoiceHeader.OfficeCode,
                        trInvoiceHeader.StoreCode,
                        trInvoiceHeader.WarehouseCode,
                        trInvoiceHeader.IsCompleted,
                        trInvoiceHeader.IsSuspended,
                        trInvoiceHeader.IsLocked,
                        trInvoiceHeader.IsOrderBase,
                        trInvoiceHeader.IsShipmentBase,
                        trInvoiceHeader.IsPostingJournal,
                        trInvoiceHeader.JournalNumber,
                        trInvoiceHeader.IsPrinted,
                        trInvoiceHeader.ApplicationCode,
                        cdCurrAcc.CurrAccDescription as CustomerDescription,
                        bsInvoiceType.InvoiceTypeDescription,
                        cdExpenseType.ExpenseTypeDescription
                    FROM 
                        trInvoiceHeader
                    LEFT JOIN 
                        cdCurrAcc ON trInvoiceHeader.CurrAccCode = cdCurrAcc.CurrAccCode AND trInvoiceHeader.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode
                    LEFT JOIN 
                        bsInvoiceType ON trInvoiceHeader.InvoiceTypeCode = bsInvoiceType.InvoiceTypeCode
                    LEFT JOIN 
                        cdExpenseType ON trInvoiceHeader.ExpenseTypeCode = cdExpenseType.ExpenseTypeCode
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
