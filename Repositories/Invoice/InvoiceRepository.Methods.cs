using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Invoice;
using ErpMobile.Api.Models.Common;

namespace ErpMobile.Api.Repositories.Invoice
{
    // InvoiceRepository sınıfının eksik metodlarını içeren partial sınıf
    public partial class InvoiceRepository
    {
        // Fatura numarası oluşturmak için son fatura numarasını getiren metot
        public async Task<string> GetLastInvoiceNumberByProcessCodeAsync(string processCode)
        {
            try
            {
                string sql = @"SELECT TOP 1 InvoiceNumber 
                    FROM trInvoiceHeader WITH (NOLOCK) 
                    WHERE ProcessCode = @ProcessCode 
                    ORDER BY InvoiceHeaderID DESC";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ProcessCode", processCode);
                    var result = await command.ExecuteScalarAsync();
                    return result?.ToString();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Son fatura numarası alınırken hata oluştu");
                throw;
            }
        }

        // Toptan satış faturaları
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetWholesaleInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                // Bu metod GetAllInvoicesAsync ile benzer şekilde çalışabilir
                // Sadece ProcessCode filtresi "WS" (Wholesale Sales) olarak sabitlenir
                request.ProcessCode = "WS";
                return await GetAllInvoicesAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan satış faturaları getirilirken hata oluştu");
                throw;
            }
        }

        public async Task<InvoiceHeaderModel> GetWholesaleInvoiceByIdAsync(int invoiceHeaderId)
        {
            try
            {
                string sql = @"SELECT 
                    trInvoiceHeader.*, 
                    cdCurrAccDesc.CurrAccDescription,
                    bsCompanyDesc.CompanyDescription,
                    bsStoreDesc.StoreDescription,
                    bsWarehouseDesc.WarehouseDescription
                FROM trInvoiceHeader WITH (NOLOCK)
                LEFT OUTER JOIN cdCurrAccDesc WITH (NOLOCK)
                    ON cdCurrAccDesc.CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode 
                    AND cdCurrAccDesc.CurrAccCode = trInvoiceHeader.CurrAccCode
                    AND cdCurrAccDesc.LangCode = @LangCode
                LEFT OUTER JOIN bsCompanyDesc WITH (NOLOCK)
                    ON bsCompanyDesc.CompanyCode = trInvoiceHeader.CompanyCode
                    AND bsCompanyDesc.LangCode = @LangCode
                LEFT OUTER JOIN bsStoreDesc WITH (NOLOCK)
                    ON bsStoreDesc.CompanyCode = trInvoiceHeader.CompanyCode
                    AND bsStoreDesc.StoreCode = trInvoiceHeader.StoreCode
                    AND bsStoreDesc.LangCode = @LangCode
                LEFT OUTER JOIN bsWarehouseDesc WITH (NOLOCK)
                    ON bsWarehouseDesc.CompanyCode = trInvoiceHeader.CompanyCode
                    AND bsWarehouseDesc.WarehouseCode = trInvoiceHeader.WarehouseCode
                    AND bsWarehouseDesc.LangCode = @LangCode
                WHERE trInvoiceHeader.InvoiceHeaderID = @InvoiceHeaderId";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@InvoiceHeaderId", invoiceHeaderId);
                    command.Parameters.AddWithValue("@LangCode", "TR");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapToInvoiceHeaderModel(reader);
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan satış faturası getirilirken hata oluştu");
                throw;
            }
        }

        public async Task<InvoiceHeaderModel> CreateWholesaleInvoiceAsync(CreateInvoiceRequest request)
        {
            try
            {
                // Fatura başlığı oluştur
                var invoiceHeader = new InvoiceHeaderModel
                {
                    ProcessCode = "WS", // Wholesale Sales
                    InvoiceNumber = request.InvoiceNumber,
                    InvoiceDate = request.InvoiceDate,
                    CompanyCode = request.CompanyCode,
                    StoreCode = request.StoreCode,
                    WarehouseCode = request.WarehouseCode,
                    CustomerCode = request.CustomerCode,
                    CurrAccCode = request.CustomerCode, // CurrAccCode parametresi için CustomerCode kullanılıyor
                    CurrAccTypeCode = 2, // Müşteri
                    Notes = request.Notes, // Using Notes instead of Description
                    CreatedBy = "System", // CreatedBy property does not exist in CreateInvoiceRequest
                    CreatedDate = DateTime.Now
                };

                // Fatura başlığı ve detayları veritabanına kaydet
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Fatura başlığını kaydet
                            var invoiceHeaderId = await CreateInvoiceHeaderAsync(connection, transaction, invoiceHeader);
                            invoiceHeader.InvoiceHeaderID = invoiceHeaderId.ToString();

                            // Fatura detaylarını kaydet
                            if (request.Details != null && request.Details.Any())
                            {
                                int sortOrder = 10;
                                foreach (var detail in request.Details)
                                {
                                    await CreateInvoiceLineAsync(connection, transaction, invoiceHeaderId, detail, sortOrder);
                                    sortOrder += 10;
                                }

                                // Fatura toplamlarını hesapla ve güncelle
                                await CalculateInvoiceTotalsAsync(connection, transaction, invoiceHeaderId);
                            }

                            transaction.Commit();
                            _logger.LogInformation($"Toptan satış faturası başarıyla oluşturuldu. Fatura ID: {invoiceHeaderId}");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _logger.LogError(ex, "Fatura kaydedilirken hata oluştu. İşlem geri alındı.");
                            throw;
                        }
                    }
                }

                return invoiceHeader;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan satış faturası oluşturulurken hata oluştu");
                throw;
            }
        }

        // Toptan alış faturaları
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetWholesalePurchaseInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                // Bu metod GetAllInvoicesAsync ile benzer şekilde çalışabilir
                // Sadece ProcessCode filtresi "WP" (Wholesale Purchase) olarak sabitlenir
                request.ProcessCode = "WP";
                return await GetAllInvoicesAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan alış faturaları getirilirken hata oluştu");
                throw;
            }
        }

        public async Task<InvoiceHeaderModel> GetWholesalePurchaseInvoiceByIdAsync(int invoiceHeaderId)
        {
            try
            {
                string sql = @"SELECT 
                    trInvoiceHeader.*, 
                    cdCurrAccDesc.CurrAccDescription,
                    bsCompanyDesc.CompanyDescription,
                    bsStoreDesc.StoreDescription,
                    bsWarehouseDesc.WarehouseDescription
                FROM trInvoiceHeader WITH (NOLOCK)
                LEFT OUTER JOIN cdCurrAccDesc WITH (NOLOCK)
                    ON cdCurrAccDesc.CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode 
                    AND cdCurrAccDesc.CurrAccCode = trInvoiceHeader.CurrAccCode
                    AND cdCurrAccDesc.LangCode = @LangCode
                LEFT OUTER JOIN bsCompanyDesc WITH (NOLOCK)
                    ON bsCompanyDesc.CompanyCode = trInvoiceHeader.CompanyCode
                    AND bsCompanyDesc.LangCode = @LangCode
                LEFT OUTER JOIN bsStoreDesc WITH (NOLOCK)
                    ON bsStoreDesc.CompanyCode = trInvoiceHeader.CompanyCode
                    AND bsStoreDesc.StoreCode = trInvoiceHeader.StoreCode
                    AND bsStoreDesc.LangCode = @LangCode
                LEFT OUTER JOIN bsWarehouseDesc WITH (NOLOCK)
                    ON bsWarehouseDesc.CompanyCode = trInvoiceHeader.CompanyCode
                    AND bsWarehouseDesc.WarehouseCode = trInvoiceHeader.WarehouseCode
                    AND bsWarehouseDesc.LangCode = @LangCode
                WHERE trInvoiceHeader.InvoiceHeaderID = @InvoiceHeaderId";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@InvoiceHeaderId", invoiceHeaderId);
                    command.Parameters.AddWithValue("@LangCode", "TR");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapToInvoiceHeaderModel(reader);
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

        public async Task<InvoiceHeaderModel> CreateWholesalePurchaseInvoiceAsync(CreateInvoiceRequest request)
        {
            try
            {
                // Fatura başlığı oluştur
                var invoiceHeader = new InvoiceHeaderModel
                {
                    ProcessCode = "WP", // Wholesale Purchase
                    InvoiceNumber = request.InvoiceNumber,
                    InvoiceDate = request.InvoiceDate,
                    CompanyCode = request.CompanyCode,
                    StoreCode = request.StoreCode,
                    WarehouseCode = request.WarehouseCode,
                    VendorCode = request.VendorCode, // Using VendorCode instead of CurrAccCode
                    CurrAccTypeCode = 1, // Tedarikçi
                    Notes = request.Notes, // Using Notes instead of Description
                    CreatedBy = "System", // CreatedBy property does not exist in CreateInvoiceRequest
                    CreatedDate = DateTime.Now
                };

                // Fatura başlığı ve detayları veritabanına kaydet
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Fatura başlığını kaydet
                            var invoiceHeaderId = await CreateInvoiceHeaderAsync(connection, transaction, invoiceHeader);
                            invoiceHeader.InvoiceHeaderID = invoiceHeaderId.ToString();

                            // Fatura detaylarını kaydet
                            if (request.Details != null && request.Details.Any())
                            {
                                int sortOrder = 10;
                                foreach (var detail in request.Details)
                                {
                                    await CreateInvoiceLineAsync(connection, transaction, invoiceHeaderId, detail, sortOrder);
                                    sortOrder += 10;
                                }

                                // Fatura toplamlarını hesapla ve güncelle
                                await CalculateInvoiceTotalsAsync(connection, transaction, invoiceHeaderId);
                            }

                            transaction.Commit();
                            _logger.LogInformation($"Toptan alış faturası başarıyla oluşturuldu. Fatura ID: {invoiceHeaderId}");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _logger.LogError(ex, "Fatura kaydedilirken hata oluştu. İşlem geri alındı.");
                            throw;
                        }
                    }
                }

                return invoiceHeader;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan alış faturası oluşturulurken hata oluştu");
                throw;
            }
        }

        // Masraf faturaları
        public async Task<InvoiceHeaderModel> CreateExpenseInvoiceAsync(CreateInvoiceRequest request)
        {
            try
            {
                // Fatura başlığı oluştur
                var invoiceHeader = new InvoiceHeaderModel
                {
                    ProcessCode = "EP", // Expense Invoice
                    InvoiceNumber = request.InvoiceNumber,
                    InvoiceDate = request.InvoiceDate,
                    CompanyCode = request.CompanyCode,
                    StoreCode = request.StoreCode,
                    WarehouseCode = request.WarehouseCode,
                    VendorCode = request.VendorCode, // Using VendorCode instead of CurrAccCode
                    CurrAccTypeCode = 1, // Tedarikçi
                    Notes = request.Notes, // Using Notes instead of Description
                    CreatedBy = "System", // CreatedBy property does not exist in CreateInvoiceRequest
                    CreatedDate = DateTime.Now
                };

                // Fatura başlığı ve detayları veritabanına kaydet
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Fatura başlığını kaydet
                            var invoiceHeaderId = await CreateInvoiceHeaderAsync(connection, transaction, invoiceHeader);
                            invoiceHeader.InvoiceHeaderID = invoiceHeaderId.ToString();

                            // Fatura detaylarını kaydet
                            if (request.Details != null && request.Details.Any())
                            {
                                int sortOrder = 10;
                                foreach (var detail in request.Details)
                                {
                                    await CreateInvoiceLineAsync(connection, transaction, invoiceHeaderId, detail, sortOrder);
                                    sortOrder += 10;
                                }

                                // Fatura toplamlarını hesapla ve güncelle
                                await CalculateInvoiceTotalsAsync(connection, transaction, invoiceHeaderId);
                            }

                            transaction.Commit();
                            _logger.LogInformation($"Masraf faturası başarıyla oluşturuldu. Fatura ID: {invoiceHeaderId}");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _logger.LogError(ex, "Fatura kaydedilirken hata oluştu. İşlem geri alındı.");
                            throw;
                        }
                    }
                }

                return invoiceHeader;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Masraf faturası oluşturulurken hata oluştu");
                throw;
            }
        }

        // Fatura başlığını veritabanına kaydeden metot
        private async Task<Guid> CreateInvoiceHeaderAsync(SqlConnection connection, SqlTransaction transaction, InvoiceHeaderModel invoiceHeader)
        {
            // InvoiceHeaderID için yeni bir GUID oluştur
            var invoiceHeaderId = Guid.NewGuid();

            try
            {
                // Sabit SQL sorgusu kullanarak tüm gerekli alanları ekle
                var sql = @"
                    INSERT INTO trInvoiceHeader (
                        InvoiceHeaderID,
                        TransTypeCode,
                        ProcessCode,
                        InvoiceNumber,
                        IsReturn,
                        InvoiceDate,
                        InvoiceTime,
                        OperationDate,
                        OperationTime,
                        PaymentTerm,
                        AverageDueDate,
                        CurrAccTypeCode,
                        CurrAccCode,
                        ShippingPostalAddressID,
                        BillingPostalAddressID,
                        TaxExemptionCode,
                        CompanyCode,
                        OfficeCode,
                        WarehouseCode,
                        FormType,
                        ExchangeRate,
                        DocCurrencyCode,
                        LocalCurrencyCode,
                        DocumentTypeCode,
                        JournalDate,
                        ApplicationCode,
                        ApplicationID,
                        CreatedUserName,
                        CreatedDate,
                        LastUpdatedUserName,
                        LastUpdatedDate
                    ) VALUES (
                        @InvoiceHeaderID,
                        @TransTypeCode,
                        @ProcessCode,
                        @InvoiceNumber,
                        @IsReturn,
                        @InvoiceDate,
                        @InvoiceTime,
                        @OperationDate,
                        @OperationTime,
                        @PaymentTerm,
                        @AverageDueDate,
                        @CurrAccTypeCode,
                        @CurrAccCode,
                        @ShippingPostalAddressID,
                        @BillingPostalAddressID,
                        @TaxExemptionCode,
                        @CompanyCode,
                        @OfficeCode,
                        @WarehouseCode,
                        @FormType,
                        @ExchangeRate,
                        @DocCurrencyCode,
                        @LocalCurrencyCode,
                        @DocumentTypeCode,
                        @JournalDate,
                        @ApplicationCode,
                        @ApplicationID,
                        @CreatedUserName,
                        GETDATE(),
                        @LastUpdatedUserName,
                        GETDATE()
                    )";

                // SQL komutunu oluştur
                using (var command = new SqlCommand(sql, connection, transaction))
                {
                    // Temel bilgiler
                    command.Parameters.AddWithValue("@InvoiceHeaderID", invoiceHeaderId);
                    
                    // TransTypeCode: 1=Alış, 2=Satış
                    int transTypeCode = 1; // Varsayılan değer: Alış
                    if (invoiceHeader.ProcessCode == "WS") // Toptan satış
                        transTypeCode = 2;
                    command.Parameters.AddWithValue("@TransTypeCode", transTypeCode);
                    
                    // ProcessCode - Formdan gelecek (WS, BP, EP)
                    command.Parameters.AddWithValue("@ProcessCode", invoiceHeader.ProcessCode);
                    
                    // InvoiceNumber - Otomatik en son kullanılan bulunacak
                    // Örnek: 1-WS-7-5 ise bir sonraki 1-WS-7-6 olacak
                    command.Parameters.AddWithValue("@InvoiceNumber", invoiceHeader.InvoiceNumber);
                    
                    // IsReturn - İade faturası ise true
                    command.Parameters.AddWithValue("@IsReturn", invoiceHeader.IsReturn);
                    
                    // Tarih ve zaman bilgileri
                    // InvoiceDate ve InvoiceTime formdan gelecek
                    command.Parameters.AddWithValue("@InvoiceDate", invoiceHeader.InvoiceDate);
                    command.Parameters.AddWithValue("@InvoiceTime", invoiceHeader.InvoiceTime ?? DateTime.Now.ToString("HH:mm:ss"));
                    
                    // OperationDate ve OperationTime kayıt zamanı olacak
                    command.Parameters.AddWithValue("@OperationDate", DateTime.Now.Date);
                    command.Parameters.AddWithValue("@OperationTime", DateTime.Now.ToString("HH:mm:ss"));
                    
                    // PaymentTerm - Default 30, formdan gelecek
                    // Peşin yerine vadeli seçilirse 30/45/60/90/120/150/180 olabilir
                    int paymentTerm = invoiceHeader.PaymentTerm.HasValue ? invoiceHeader.PaymentTerm.Value : 30;
                    command.Parameters.AddWithValue("@PaymentTerm", paymentTerm);
                    
                    // AverageDueDate - InvoiceDate + PaymentTerm kadar olan tarih
                    command.Parameters.AddWithValue("@AverageDueDate", invoiceHeader.InvoiceDate.AddDays(paymentTerm));
                    
                    // CurrAccTypeCode - Müşteri WS için 3, BP için 1, EP için 7
                    int currAccTypeCode;
                    switch (invoiceHeader.ProcessCode)
                    {
                        case "WS":
                            currAccTypeCode = 3; // Müşteri - Toptan satış
                            break;
                        case "BP":
                            currAccTypeCode = 1; // Tedarikçi - Toptan alış
                            break;
                        case "EP":
                            currAccTypeCode = 7; // Masraf - Masraf alış
                            break;
                        default:
                            currAccTypeCode = 1; // Varsayılan değer
                            break;
                    }
                    command.Parameters.AddWithValue("@CurrAccTypeCode", currAccTypeCode);
                    
                    // CurrAccCode - Formdan gelecek
                    command.Parameters.AddWithValue("@CurrAccCode", invoiceHeader.CurrAccCode);
                    
                    // Adres bilgileri - Müşteriden alınacak
                    command.Parameters.AddWithValue("@ShippingPostalAddressID", string.IsNullOrEmpty(invoiceHeader.ShippingPostalAddressID) ? DBNull.Value : invoiceHeader.ShippingPostalAddressID);
                    command.Parameters.AddWithValue("@BillingPostalAddressID", string.IsNullOrEmpty(invoiceHeader.BillingPostalAddressID) ? DBNull.Value : invoiceHeader.BillingPostalAddressID);
                    command.Parameters.AddWithValue("@TaxExemptionCode", invoiceHeader.TaxExemptionCode ?? 0);
                    
                    // Şirket ve depo bilgileri - Formdan gelecek
                    command.Parameters.AddWithValue("@CompanyCode", invoiceHeader.CompanyCode);
                    command.Parameters.AddWithValue("@OfficeCode", invoiceHeader.OfficeCode);
                    command.Parameters.AddWithValue("@WarehouseCode", invoiceHeader.WarehouseCode);
                    
                    // Form tipi belirleme
                    // WS=1, EP=34, BP=1
                    int formType;
                    switch (invoiceHeader.ProcessCode)
                    {
                        case "WS":
                            formType = 1; // Toptan satış
                            break;
                        case "EP":
                            formType = 34; // Masraf alış
                            break;
                        case "BP":
                            formType = 1; // Toptan alış
                            break;
                        default:
                            formType = 1; // Varsayılan değer
                            break;
                    }
                    command.Parameters.AddWithValue("@FormType", formType);
                    
                    // Döviz bilgileri
                    // ExchangeRate - TRY için 1, diğer para birimleri için TL karşılığı
                    decimal exchangeRate = 1; // Varsayılan TRY için 1
                    if (invoiceHeader.DocCurrencyCode != null && invoiceHeader.DocCurrencyCode != "TRY")
                    {
                        // Burada döviz kuru servisi kullanılabilir
                        // Şimdilik varsayılan 1 kullanıyoruz
                    }
                    command.Parameters.AddWithValue("@ExchangeRate", exchangeRate);
                    
                    // DocCurrencyCode - Formdan gelecek (USD, TRY, GBP vb.)
                    command.Parameters.AddWithValue("@DocCurrencyCode", invoiceHeader.DocCurrencyCode ?? "TRY");
                    
                    // LocalCurrencyCode - TRY standart
                    command.Parameters.AddWithValue("@LocalCurrencyCode", "TRY");
                    
                    // DocumentTypeCode - 4 standart
                    command.Parameters.AddWithValue("@DocumentTypeCode", 4);
                    
                    // JournalDate = InvoiceDate
                    command.Parameters.AddWithValue("@JournalDate", invoiceHeader.InvoiceDate);
                    
                    // ApplicationCode - Fatura için "Invoi" standart
                    command.Parameters.AddWithValue("@ApplicationCode", "Invoi");
                    
                    // ApplicationID = InvoiceHeaderID
                    command.Parameters.AddWithValue("@ApplicationID", invoiceHeaderId);
                    
                    // Kullanıcı bilgileri
                    command.Parameters.AddWithValue("@CreatedUserName", "SYS IRFAN"); // System user
                    command.Parameters.AddWithValue("@LastUpdatedUserName", "SYS IRFAN"); // System user

                    // SQL sorgusunu ve parametreleri konsola yazdır
                    _logger.LogInformation("SQL Sorgusu: " + sql);
                    _logger.LogInformation("Parametreler:");
                    _logger.LogInformation($"  InvoiceHeaderID: {invoiceHeaderId}");
                    _logger.LogInformation($"  TransTypeCode: {transTypeCode}");
                    _logger.LogInformation($"  ProcessCode: {invoiceHeader.ProcessCode}");
                    _logger.LogInformation($"  InvoiceNumber: {invoiceHeader.InvoiceNumber}");
                    _logger.LogInformation($"  IsReturn: {false}");
                    _logger.LogInformation($"  InvoiceDate: {invoiceHeader.InvoiceDate}");
                    _logger.LogInformation($"  PaymentTerm: {paymentTerm}");
                    _logger.LogInformation($"  AverageDueDate: {invoiceHeader.InvoiceDate.AddDays(paymentTerm)}");
                    _logger.LogInformation($"  CurrAccTypeCode: {currAccTypeCode}");
                    _logger.LogInformation($"  CurrAccCode: {invoiceHeader.CurrAccCode}");
                    _logger.LogInformation($"  CompanyCode: {invoiceHeader.CompanyCode}");
                    _logger.LogInformation($"  WarehouseCode: {invoiceHeader.WarehouseCode}");
                    _logger.LogInformation($"  FormType: {formType}");
                    _logger.LogInformation($"  DocCurrencyCode: {(!string.IsNullOrEmpty(invoiceHeader.DocCurrencyCode) ? invoiceHeader.DocCurrencyCode : "TRY")}");
                    
                    // Komutu çalıştır
                    await command.ExecuteNonQueryAsync();
                    _logger.LogInformation($"Fatura başlığı kaydedildi. InvoiceHeaderID: {invoiceHeaderId}");

                    return invoiceHeaderId;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatura başlığı kaydedilirken hata oluştu. ProcessCode: {invoiceHeader.ProcessCode}");
                throw;
            }
        }

        // Fatura detaylarını veritabanına kaydeden metot artık InvoiceRepository.cs dosyasında bulunmaktadır.
        /*
                // SQL komutunu oluştur
                using (var command = new SqlCommand(sql, connection, transaction))
                {
                    // Zorunlu parametreler - Temel bilgiler
                    command.Parameters.AddWithValue("@InvoiceLineID", invoiceLineId);
                    command.Parameters.AddWithValue("@InvoiceHeaderID", invoiceHeaderId);
                    command.Parameters.AddWithValue("@SortOrder", sortOrder);
                    
                    // Ürün bilgileri
                    command.Parameters.AddWithValue("@ItemTypeCode", detail.ItemTypeCode.HasValue ? detail.ItemTypeCode.Value : (byte)1);
                    command.Parameters.AddWithValue("@ItemCode", !string.IsNullOrEmpty(detail.ItemCode) ? detail.ItemCode : "TEST001");
                    
                    // Varyant bilgileri
                    command.Parameters.AddWithValue("@ColorCode", !string.IsNullOrEmpty(detail.ColorCode) ? detail.ColorCode : "STD");
                    command.Parameters.AddWithValue("@ItemDim1Code", !string.IsNullOrEmpty(detail.ItemDim1Code) ? detail.ItemDim1Code : "");
                    command.Parameters.AddWithValue("@ItemDim2Code", !string.IsNullOrEmpty(detail.ItemDim2Code) ? detail.ItemDim2Code : "");
                    command.Parameters.AddWithValue("@ItemDim3Code", !string.IsNullOrEmpty(detail.ItemDim3Code) ? detail.ItemDim3Code : "");
                    
                    // Birim ve miktar bilgileri
                    command.Parameters.AddWithValue("@UnitCode", !string.IsNullOrEmpty(detail.UnitOfMeasureCode) ? detail.UnitOfMeasureCode : "AD");
                    command.Parameters.AddWithValue("@Qty1", detail.Quantity);
                    command.Parameters.AddWithValue("@Qty2", 0); // Qty2 her zaman 0 olarak ayarla
                    
                    // KDV bilgileri
                    var vatCode = "%" + detail.VatRate;
                    command.Parameters.AddWithValue("@VatCode", vatCode);
                    command.Parameters.AddWithValue("@VatRate", detail.VatRate);
                    
                    // PCT bilgileri
                    command.Parameters.AddWithValue("@PCTCode", "%0");
                    command.Parameters.AddWithValue("@PCTRate", 0);
                    
                    // Para birimi ve fiyat bilgileri
                    command.Parameters.AddWithValue("@DocCurrencyCode", !string.IsNullOrEmpty(detail.CurrencyCode) ? detail.CurrencyCode : "TRY");
                    command.Parameters.AddWithValue("@PriceCurrencyCode", !string.IsNullOrEmpty(detail.PriceCurrencyCode) ? detail.PriceCurrencyCode : "TRY");
                    command.Parameters.AddWithValue("@PriceExchangeRate", detail.ExchangeRate.HasValue ? detail.ExchangeRate.Value : 1.0m);
                    command.Parameters.AddWithValue("@Price", detail.UnitPrice);
                    
                    // Tutar bilgileri
                    command.Parameters.AddWithValue("@Amount", detail.Amount);
                    command.Parameters.AddWithValue("@DiscountRate", detail.DiscountRate.HasValue ? detail.DiscountRate.Value : 0);
                    command.Parameters.AddWithValue("@DiscountAmount", detail.DiscountAmount.HasValue ? detail.DiscountAmount.Value : 0);
                    command.Parameters.AddWithValue("@VatAmount", detail.VatAmount);
                    command.Parameters.AddWithValue("@TotalAmount", detail.LineTotalAmount.HasValue ? detail.LineTotalAmount.Value : detail.Amount + detail.VatAmount);
                    
                    // Diğer bilgiler
                    command.Parameters.AddWithValue("@LineDescription", !string.IsNullOrEmpty(detail.Description) ? detail.Description : "");
                    command.Parameters.AddWithValue("@WarehouseCode", !string.IsNullOrEmpty(detail.WarehouseCode) ? detail.WarehouseCode : "");
                    command.Parameters.AddWithValue("@IsGift", detail.IsGift.HasValue ? detail.IsGift.Value : false);
                    command.Parameters.AddWithValue("@IsPromotional", detail.IsPromotional.HasValue ? detail.IsPromotional.Value : false);
                    
                    // Kullanıcı bilgileri
                    command.Parameters.AddWithValue("@CreatedUserName", "System");
                    command.Parameters.AddWithValue("@LastUpdatedUserName", "System");

                    // Komutu çalıştır
                    await command.ExecuteNonQueryAsync();
                    _logger.LogInformation($"Fatura detayı kaydedildi. InvoiceLineID: {invoiceLineId}, InvoiceHeaderID: {invoiceHeaderId}, ItemCode: {detail.ItemCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatura detayı kaydedilirken hata oluştu. InvoiceHeaderID: {invoiceHeaderId}, ItemCode: {detail.ItemCode}");
                throw;
            }
        */

        // Fatura toplamlarını hesaplayıp veritabanına güncelleyen metot
        private async Task CalculateInvoiceTotalsAsync(SqlConnection connection, SqlTransaction transaction, Guid invoiceHeaderId)
        {
            try
            {
                // Fatura toplamlarını hesapla
                var sql = @"
                    SELECT 
                        SUM(Amount) AS TotalAmount,
                        SUM(VatAmount) AS TotalVatAmount,
                        SUM(TotalAmount) AS GrandTotal
                    FROM trInvoiceLine
                    WHERE InvoiceHeaderID = @InvoiceHeaderID";

                using (var command = new SqlCommand(sql, connection, transaction))
                {
                    command.Parameters.AddWithValue("@InvoiceHeaderID", invoiceHeaderId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var totalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalAmount"]) : 0.0m;
                            var totalVatAmount = reader["TotalVatAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalVatAmount"]) : 0.0m;
                            var grandTotal = reader["GrandTotal"] != DBNull.Value ? Convert.ToDecimal(reader["GrandTotal"]) : 0.0m;

                            // Fatura toplamlarını güncelle
                            sql = @"
                                UPDATE trInvoiceHeader
                                SET TotalAmount = @TotalAmount,
                                    TotalVatAmount = @TotalVatAmount,
                                    GrandTotal = @GrandTotal
                                WHERE InvoiceHeaderID = @InvoiceHeaderID";

                            using (var updateCommand = new SqlCommand(sql, connection, transaction))
                            {
                                updateCommand.Parameters.AddWithValue("@InvoiceHeaderID", invoiceHeaderId);
                                updateCommand.Parameters.AddWithValue("@TotalAmount", totalAmount);
                                updateCommand.Parameters.AddWithValue("@TotalVatAmount", totalVatAmount);
                                updateCommand.Parameters.AddWithValue("@GrandTotal", grandTotal);

                                await updateCommand.ExecuteNonQueryAsync();
                                _logger.LogInformation($"Fatura toplamları güncellendi. InvoiceHeaderID: {invoiceHeaderId}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatura toplamları güncellenirken hata oluştu. InvoiceHeaderID: {invoiceHeaderId}");
                throw;
            }
        }

        // Fatura detayları
        public async Task<List<InvoiceDetailModel>> GetInvoiceDetailsAsync(int invoiceHeaderId)
        {
            try
            {
                string sql = @"SELECT 
                    trInvoiceDetail.*,
                    cdItemDesc.ItemDescription
                FROM trInvoiceDetail WITH (NOLOCK)
                LEFT OUTER JOIN cdItemDesc WITH (NOLOCK)
                    ON cdItemDesc.ItemCode = trInvoiceDetail.ItemCode
                    AND cdItemDesc.LangCode = @LangCode
                WHERE trInvoiceDetail.InvoiceHeaderID = @InvoiceHeaderId";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@InvoiceHeaderId", invoiceHeaderId);
                    command.Parameters.AddWithValue("@LangCode", "TR");

                    var details = new List<InvoiceDetailModel>();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var detail = new InvoiceDetailModel
                            {
                                InvoiceLineID = Guid.Parse(reader["InvoiceDetailID"].ToString()),
                                InvoiceHeaderID = reader["InvoiceHeaderID"].ToString(),
                                ItemCode = reader["ItemCode"].ToString(),
                                ItemDescription = reader["ItemDescription"].ToString(),
                                Quantity = Convert.ToDecimal(reader["Quantity"]),
                                UnitPrice = Convert.ToDecimal(reader["Price"]),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                VatRate = Convert.ToDecimal(reader["VatRate"]),
                                VatAmount = Convert.ToDecimal(reader["VatAmount"]),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                Notes = reader["LineDescription"].ToString()
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
    }
}
