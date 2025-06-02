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
using static erp_api.Repositories.Invoice.InvoiceRepositoryFixes;

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
    public partial class InvoiceRepository : IInvoiceRepository
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

        // Fatura satırı oluşturma metodu
        private async Task CreateInvoiceLineAsync(SqlConnection connection, SqlTransaction transaction, Guid invoiceHeaderId, CreateInvoiceDetailRequest detail, int sortOrder)
        {
            // InvoiceLineID için yeni bir GUID oluştur
            var invoiceLineId = Guid.NewGuid();

            try
            {
                // Sabit SQL sorgusu kullanarak tüm gerekli alanları ekle
                var sql = @"
            INSERT INTO trInvoiceLine (
                InvoiceLineID,
                SortOrder,
                ItemTypeCode,
                ItemCode,
                ColorCode,
                ItemDim1Code,
                ItemDim2Code,
                ItemDim3Code,
                Qty1,
                Qty2,
                VatCode,
                VatRate,
                PCTCode,
                PCTRate,
                DocCurrencyCode,
                PriceCurrencyCode,
                PriceExchangeRate,
                Price,
                BatchCode,
                SectionCode,
                OrderDeliveryDate,
                ManufactureDate,
                ExpiryDate,
                InvoiceHeaderID,
                CreatedUserName,
                CreatedDate,
                LastUpdatedUserName,
                LastUpdatedDate
            ) VALUES (
                @InvoiceLineID,
                @SortOrder,
                @ItemTypeCode,
                @ItemCode,
                @ColorCode,
                @ItemDim1Code,
                @ItemDim2Code,
                @ItemDim3Code,
                @Qty1,
                @Qty2,
                @VatCode,
                @VatRate,
                @PCTCode,
                @PCTRate,
                @DocCurrencyCode,
                @PriceCurrencyCode,
                @PriceExchangeRate,
                @Price,
                @BatchCode,
                @SectionCode,
                @OrderDeliveryDate,
                @ManufactureDate,
                @ExpiryDate,
                @InvoiceHeaderID,
                @CreatedUserName,
                GETDATE(),
                @LastUpdatedUserName,
                GETDATE()
            )";

                // SQL komutunu oluştur
                using (var command = new SqlCommand(sql, connection, transaction))
                {
                    // Zorunlu parametreler
                    command.Parameters.AddWithValue("@InvoiceLineID", invoiceLineId);
                    command.Parameters.AddWithValue("@SortOrder", sortOrder);
                    command.Parameters.AddWithValue("@ItemTypeCode", detail.ItemTypeCode.HasValue ? detail.ItemTypeCode.Value : (byte)1);

                    // ItemCode - zorunlu alan
                    var itemCode = !string.IsNullOrEmpty(detail.ItemCode) ? detail.ItemCode : "TEST001";
                    command.Parameters.AddWithValue("@ItemCode", itemCode);
                    _logger.LogWarning($"ItemCode: {itemCode} kullanılıyor. Frontend'den gelen değer: {detail.ItemCode ?? "NULL"}");

                    // ColorCode - varsayılan STD
                    var colorCode = !string.IsNullOrEmpty(detail.ColorCode) ? detail.ColorCode : "STD";
                    command.Parameters.AddWithValue("@ColorCode", colorCode);
                    _logger.LogInformation($"ColorCode: {colorCode} ekleniyor.");

                    // ItemDim1Code - varsayılan boş string
                    var itemDim1Code = !string.IsNullOrEmpty(detail.ItemDim1Code) ? detail.ItemDim1Code : "";
                    command.Parameters.AddWithValue("@ItemDim1Code", itemDim1Code);
                    _logger.LogInformation($"ItemDim1Code: {itemDim1Code} ekleniyor.");

                    // ItemDim2Code - varsayılan boş string
                    var itemDim2Code = !string.IsNullOrEmpty(detail.ItemDim2Code) ? detail.ItemDim2Code : "";
                    command.Parameters.AddWithValue("@ItemDim2Code", itemDim2Code);
                    _logger.LogInformation($"ItemDim2Code: {itemDim2Code} ekleniyor.");

                    // ItemDim3Code - varsayılan boş string
                    var itemDim3Code = !string.IsNullOrEmpty(detail.ItemDim3Code) ? detail.ItemDim3Code : "";
                    command.Parameters.AddWithValue("@ItemDim3Code", itemDim3Code);
                    _logger.LogInformation($"ItemDim3Code: {itemDim3Code} ekleniyor.");

                    // Miktar bilgileri - zorunlu
                    command.Parameters.AddWithValue("@Qty1", detail.Qty);
                    command.Parameters.AddWithValue("@Qty2", 0); // Qty2 her zaman 0 olarak ayarla

                    // KDV bilgileri - VatCode %VatRate formatında olmalı
                    var vatCode = "%" + detail.VatRate;
                    command.Parameters.AddWithValue("@VatCode", vatCode);
                    command.Parameters.AddWithValue("@VatRate", detail.VatRate);
                    _logger.LogInformation($"VatCode: {vatCode}, VatRate: {detail.VatRate} ekleniyor.");

                    // PCTCode ve PCTRate
                    command.Parameters.AddWithValue("@PCTCode", "%0");
                    command.Parameters.AddWithValue("@PCTRate", 0);
                    _logger.LogInformation($"PCTCode: %0, PCTRate: 0 ekleniyor.");

                    // Para birimi ve fiyat bilgileri - Formdan seçilen para birimi kullanılacak
                    // DocCurrencyCode - Formdan seçilen para birimi
                    var docCurrencyCode = !string.IsNullOrEmpty(detail.CurrencyCode) ? detail.CurrencyCode : "TRY";
                    command.Parameters.AddWithValue("@DocCurrencyCode", docCurrencyCode);
                    _logger.LogInformation($"Invoice Line DocCurrencyCode: {docCurrencyCode} kullanılıyor.");

                    // PriceCurrencyCode - Formdan seçilen para birimi
                    var priceCurrencyCode = !string.IsNullOrEmpty(detail.PriceCurrencyCode) ? detail.PriceCurrencyCode : docCurrencyCode;
                    command.Parameters.AddWithValue("@PriceCurrencyCode", priceCurrencyCode);
                    _logger.LogInformation($"Invoice Line PriceCurrencyCode: {priceCurrencyCode} kullanılıyor.");

                    // PriceExchangeRate - Fiyat listesinden gelirse 1 olacak
                    var priceExchangeRate = 1.0m; // Varsayılan değer 1
                    command.Parameters.AddWithValue("@PriceExchangeRate", priceExchangeRate);
                    _logger.LogInformation($"Invoice Line PriceExchangeRate: {priceExchangeRate} kullanılıyor.");

                    command.Parameters.AddWithValue("@Price", detail.UnitPrice);

                    // BatchCode ve SectionCode için boş string kullan
                    var batchCode = !string.IsNullOrEmpty(detail.BatchCode) ? detail.BatchCode : "";
                    command.Parameters.AddWithValue("@BatchCode", batchCode);
                    _logger.LogInformation($"BatchCode: {batchCode} ekleniyor.");

                    var sectionCode = !string.IsNullOrEmpty(detail.SectionCode) ? detail.SectionCode : "";
                    command.Parameters.AddWithValue("@SectionCode", sectionCode);
                    _logger.LogInformation($"SectionCode: {sectionCode} ekleniyor.");

                    // Tarih alanları için sabit tarih kullan (1900-01-01)
                    var defaultDate = new DateTime(1900, 1, 1);
                    command.Parameters.AddWithValue("@OrderDeliveryDate", defaultDate);
                    command.Parameters.AddWithValue("@ManufactureDate", defaultDate);
                    command.Parameters.AddWithValue("@ExpiryDate", defaultDate); // Son kullanma tarihi de aynı

                    // Fatura başlık ID'si - zorunlu
                    command.Parameters.AddWithValue("@InvoiceHeaderID", invoiceHeaderId);

                    // Oluşturma ve güncelleme bilgileri
                    command.Parameters.AddWithValue("@CreatedUserName", "API");
                    command.Parameters.AddWithValue("@LastUpdatedUserName", "API");

                    // SQL sorgusunu logla
                    _logger.LogInformation("===== REPOSITORY: CreateInvoiceLineAsync SQL QUERY =====\n\n" + sql);

                    // Parametreleri logla
                    _logger.LogInformation("===== PARAMETERS =====\n");
                    foreach (SqlParameter param in command.Parameters)
                    {
                        _logger.LogInformation($"{param.ParameterName}: {param.Value}");
                    }

                    // Sorguyu çalıştır
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "===== REPOSITORY: HATA - Transaction rollback ediliyor =====\n\nHata: {0}\nStack Trace: {1}", ex.Message, ex.StackTrace);
                throw;
            }
        }

        // Fatura satırlarının toplamlarını hesaplar
        private async Task<(decimal TotalQty, decimal TotalAmount, decimal TotalVatAmount, decimal TotalNetAmount, decimal TotalGrossAmount)> CalculateInvoiceTotalsAsync(SqlConnection connection, Guid invoiceHeaderId)
        {
            var totalQty = 0m;
            var totalAmount = 0m;
            var totalVatAmount = 0m;
            var totalNetAmount = 0m;
            var totalGrossAmount = 0m;

            var query = @"
                -- Toplam miktar
                SELECT ISNULL(SUM(Qty1), 0) FROM trInvoiceLine WHERE InvoiceHeaderID = @InvoiceHeaderID";

            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InvoiceHeaderID", invoiceHeaderId);

            // Toplam miktarı hesapla
            var result = await command.ExecuteScalarAsync();
            totalQty = result != DBNull.Value ? Convert.ToDecimal(result) : 0;

            // Toplam tutarları hesapla
            query = @"
                -- Toplam tutar (KDV hariç)
                SELECT ISNULL(SUM(ROUND(Qty1 * Price, 2)), 0) FROM trInvoiceLine WHERE InvoiceHeaderID = @InvoiceHeaderID";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InvoiceHeaderID", invoiceHeaderId);
            result = await command.ExecuteScalarAsync();
            totalAmount = result != DBNull.Value ? Convert.ToDecimal(result) : 0;

            // Toplam KDV tutarını hesapla
            query = @"
                -- Toplam KDV tutarı
                SELECT ISNULL(SUM(ROUND(Qty1 * Price * VatRate / 100, 2)), 0) FROM trInvoiceLine WHERE InvoiceHeaderID = @InvoiceHeaderID";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InvoiceHeaderID", invoiceHeaderId);
            result = await command.ExecuteScalarAsync();
            totalVatAmount = result != DBNull.Value ? Convert.ToDecimal(result) : 0;

            // Net ve brüt tutarları hesapla
            totalNetAmount = totalAmount;
            totalGrossAmount = totalAmount + totalVatAmount;

            return (totalQty, totalAmount, totalVatAmount, totalNetAmount, totalGrossAmount);
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
                                TransTypeCode = reader["TransTypeCode"] != DBNull.Value ? Convert.ToInt32(reader["TransTypeCode"]) : (int?)null,
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
                                FormType = SafeConvertToNullableInt(reader["FormType"]),
                                DocumentTypeCode = SafeConvertToNullableInt(reader["DocumentTypeCode"]),
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
                                TransTypeCode = reader["TransTypeCode"] != DBNull.Value ? Convert.ToInt32(reader["TransTypeCode"]) : (int?)null,
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
                                FormType = SafeConvertToNullableInt(reader["FormType"]),
                                DocumentTypeCode = SafeConvertToNullableInt(reader["DocumentTypeCode"]),
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
                        customerDescription = reader.HasColumn("CurrAccDescription") ? (reader["CurrAccDescription"] != DBNull.Value ? reader["CurrAccDescription"].ToString() : "") : "";
                    }
                    else if (currAccTypeCode == 1) // Tedarikçi
                    {
                        vendorCode = reader["CurrAccCode"].ToString();
                        vendorDescription = reader.HasColumn("CurrAccDescription") ? (reader["CurrAccDescription"] != DBNull.Value ? reader["CurrAccDescription"].ToString() : "") : "";
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
                    DocCurrencyCode = reader.HasColumn("DocCurrencyCode") ? reader["DocCurrencyCode"].ToString() : "",
                    CompanyCode = reader["CompanyCode"].ToString(),
                    OfficeCode = reader["OfficeCode"].ToString(),
                    StoreCode = reader["StoreCode"].ToString(),
                    WarehouseCode = reader["WarehouseCode"].ToString(),
                    ProcessCode = processCode,
                    TransTypeCode = reader.HasColumn("TransTypeCode") ? (reader["TransTypeCode"] != DBNull.Value ? Convert.ToInt32(reader["TransTypeCode"]) : (int?)null) : null,
                    FormType = reader.HasColumn("FormType") ? (reader["FormType"] != DBNull.Value ? Convert.ToInt32(reader["FormType"]) : (int?)null) : null,
                    DocumentTypeCode = reader.HasColumn("DocumentTypeCode") ? (reader["DocumentTypeCode"] != DBNull.Value ? Convert.ToInt32(reader["DocumentTypeCode"]) : (int?)null) : null,
                    InvoiceTypeCode = reader.HasColumn("InvoiceTypeCode") ? reader["InvoiceTypeCode"].ToString() : "",
                    InvoiceTypeDescription = reader.HasColumn("InvoiceTypeDescription") ? reader["InvoiceTypeDescription"].ToString() : "",
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
    }

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

    // InvoiceRepository sınıfına eklenecek ek metotlar
    public partial class InvoiceRepository
    {
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
                using (var connection = new SqlConnection(_connectionString))
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
                using (var connection = new SqlConnection(_connectionString))
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
                using (var connection = new SqlConnection(_connectionString))
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

                // Verileri almak için sorgu - AllInvoicesQuery.sql dosyasındaki sorguyu baz alarak
                string query = $@"
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
                     , SubCurrAccCode = ISNULL(SubCurrAccCode, SPACE(0))
                     , SubCurrAccCompanyName = ISNULL(prSubCurrAcc.CompanyName, SPACE(0))
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
                                                             AND trJournalHeader.ApplicationID = trInvoiceHeader.InvoiceHeaderID
                                                                 FOR XML PATH('')), N'QWERTY, ', SPACE(0)),SPACE(0)) END
                     , IsPrinted = trInvoiceHeader.IsPrinted
                     , ApplicationCode = trInvoiceHeader.ApplicationCode
                     , ApplicationDescription = ISNULL(bsApplicationDesc.ApplicationDescription, SPACE(0))
                     , ApplicationID = trInvoiceHeader.ApplicationID
                     , InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID
                     , ExpenseTypeCode = trInvoiceHeader.ExpenseTypeCode
                     , FormType = trInvoiceHeader.FormType
                     , DocumentTypeCode = trInvoiceHeader.DocumentTypeCode
                     , CreatedUserName = trInvoiceHeader.CreatedUserName
                     , CreatedDate = trInvoiceHeader.CreatedDate
                     , LastUpdatedUserName = trInvoiceHeader.LastUpdatedUserName
                     , LastUpdatedDate = trInvoiceHeader.LastUpdatedDate
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
