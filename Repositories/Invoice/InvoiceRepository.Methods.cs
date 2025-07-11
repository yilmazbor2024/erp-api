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
using System.Text;

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
                // Fatura numaralarını doğru sıralamak için SQL sorgusunu değiştiriyoruz
                // InvoiceNumber formatı: WS-7-X şeklinde olduğundan, son kısmı sayısal olarak sıralıyoruz
                string sql = @"SELECT TOP 1 InvoiceNumber 
                    FROM trInvoiceHeader WITH (NOLOCK) 
                    WHERE ProcessCode = @ProcessCode 
                    ORDER BY CreatedDate DESC ";

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
                // Fatura numarası kontrolü ve otomatik oluşturma
                string invoiceNumber = request.InvoiceNumber;
                if (string.IsNullOrEmpty(invoiceNumber) || invoiceNumber == "Otomatik oluşturulacak")
                {
                    // Otomatik fatura numarası oluştur
                    invoiceNumber = await GenerateNextInvoiceNumberAsync("WS");
                    _logger.LogInformation($"Otomatik fatura numarası oluşturuldu: {invoiceNumber}");
                }

                // Para birimi kontrolü
                string docCurrencyCode = !string.IsNullOrEmpty(request.DocCurrencyCode) ? request.DocCurrencyCode : "TRY";

                // Ödeme tipi ayarları
                bool isVadeli = request.IsCreditSale;
                byte formType = 0; // FormType her durumda 0 olacak
                
                // PaymentTerm değerini ödeme tipine göre ayarla
                int paymentTerm =  request.PaymentTerm;
                
                // Vade tarihi hesaplama (vadeli ise)
                DateTime? averageDueDate = null;
                if (isVadeli)
                {
                    averageDueDate = request.InvoiceDate.AddDays(paymentTerm);
                    _logger.LogInformation($"Vade tarihi hesaplandı: {averageDueDate.Value.ToString("yyyy-MM-dd")} (Fatura tarihi + {paymentTerm} gün)");
                }
                
                _logger.LogInformation($"Ödeme tipi: {(isVadeli ? "Vadeli" : "Peşin")}, " +
                    $"FormType: {formType}, " +
                    $"PaymentTerm: {paymentTerm}, " +
                    $"IsCreditSale: {isVadeli}, " +
                    $"IsCompleted: true, " +
                    $"AverageDueDate: {(averageDueDate.HasValue ? averageDueDate.Value.ToString("yyyy-MM-dd") : "null")}");
                
                // Fatura başlığı oluştur
                var invoiceHeader = new InvoiceHeaderModel
                {
                    ProcessCode = "WS", // Wholesale Sales
                    InvoiceNumber = invoiceNumber,
                    InvoiceDate = request.InvoiceDate,
                    CompanyCode = request.CompanyCode,
                    OfficeCode = request.OfficeCode, // OfficeCode parametresi eklendi
                    WarehouseCode = request.WarehouseCode,
                    CustomerCode = request.CustomerCode,
                    CurrAccCode = request.CustomerCode, // CurrAccCode parametresi için CustomerCode kullanılıyor
                    CurrAccTypeCode = 2, // Müşteri
                    DocCurrencyCode = docCurrencyCode, // Para birimi kodu eklendi
                    LocalCurrencyCode = request.LocalCurrencyCode, // Yerel para birimi kodu eklendi
                    ExchangeRate = request.ExchangeRate, // Döviz kuru eklendi
                    ShippingPostalAddressID = request.ShippingPostalAddressID.ToString(), // Teslimat adresi ID'si
                    BillingPostalAddressID = request.BillingPostalAddressID.ToString(), // Fatura adresi ID'si
                    ShipmentMethodCode = request.ShipmentMethodCode, // Sevkiyat yöntemi kodu (opsiyonel)
                    TaxTypeCode = Convert.ToByte(request.TaxTypeCode), // Vergi tipi kodu tinyint olarak kaydedilmeli
                    Notes = request.Notes, // Using Notes instead of Description
                    
                    // Ödeme tipi ile ilgili alanlar
                    FormType = 0, // Her durumda 0 olacak
                    PaymentTerm = paymentTerm, // Vadeli için formdan gelen değer, peşin için 0
                    IsCreditSale = isVadeli, // Vadeli için true, peşin için false
                    IsCompleted = true, // Her durumda true olacak
                    AverageDueDate = averageDueDate, // Vadeli ise hesaplanan vade tarihi, değilse null
                    
                    CreatedBy = "System", // CreatedBy property does not exist in CreateInvoiceRequest
                    CreatedDate = DateTime.Now
                    
                };
                _logger.LogInformation($"invoiceHeader TaxTypeCode : {invoiceHeader.TaxTypeCode}");
                // Döviz bilgilerini logla
                _logger.LogInformation($"DocCurrencyCode: {docCurrencyCode}, LocalCurrencyCode: {request.LocalCurrencyCode}, ExchangeRate: {request.ExchangeRate}");

                // Ödeme bilgilerini logla
                _logger.LogInformation($"Ödeme Tipi: {request.IsCreditSale} (0=Peşin, 1=Normal), PaymentTerm: {request.PaymentTerm} (0=Peşin, >0=Vadeli gün sayısı)");

                // Adres bilgilerini logla
                _logger.LogInformation($"Fatura için müşteri teslimat adresi ID: {request.ShippingPostalAddressID}");
                _logger.LogInformation($"Fatura için müşteri fatura adresi ID: {request.BillingPostalAddressID}");

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
                                int sortOrder = 1;
                                foreach (var detail in request.Details)
                                {
                                    await CreateInvoiceLineAsync(connection, transaction, invoiceHeaderId, detail, sortOrder);
                                    sortOrder += 1;
                                }
                            }
                            
                            // Fatura başlığı uzantısını kaydet (tpInvoiceHeaderExtension tablosu)
                            _logger.LogInformation($"InvoiceHeaderExtension kontrol ediliyor. IsCreditSale: {request.IsCreditSale}");
                            
                            try {
                                if (request.InvoiceHeaderExtension != null)
                                {
                                    _logger.LogInformation($"InvoiceHeaderExtension verisi mevcut: " + 
                                        $"PaymentMeansCode={request.InvoiceHeaderExtension.PaymentMeansCode}, " +
                                        $"PaymentChannelCode={request.InvoiceHeaderExtension.PaymentChannelCode}, " +
                                        $"IsIndividual={request.InvoiceHeaderExtension.IsIndividual}, " +
                                        $"DocumentDate={request.InvoiceHeaderExtension.DocumentDate}");
                                    
                                    bool isPesin = request.IsCreditSale == false;
                                    // Foreign key kısıtlaması nedeniyle PaymentMeansCode ve PaymentChannelCode değerlerini boş string olarak ayarlıyoruz
                                    string fullPaymentMeansCode = request.InvoiceHeaderExtension.PaymentMeansCode ?? (isPesin ? "CASH" : "CREDIT");
                                    string paymentMeansCode = "";
                                    string paymentChannelCode = "";
                                    
                                    _logger.LogInformation($"PaymentMeansCode değeri boş string olarak ayarlandı. Orijinal değer: {fullPaymentMeansCode}");
                                    
                                    _logger.LogInformation($"Ödeme bilgileri: isPesin={isPesin}, boş string değerler kullanılıyor");
                                    
                                    await CreateInvoiceHeaderExtensionAsync(connection, transaction, invoiceHeaderId, 
                                        new {
                                            PaymentMeansCode = paymentMeansCode,
                                            PaymentChannelCode = paymentChannelCode,
                                            IsIndividual = request.InvoiceHeaderExtension.IsIndividual,
                                            DocumentDate = request.InvoiceHeaderExtension.DocumentDate ?? request.InvoiceDate
                                        });
                                    _logger.LogInformation($"Fatura başlığı uzantısı kaydedildi. Ödeme Tipi: {(isPesin ? "Peşin" : "Vadeli")}, PaymentMeansCode: {paymentMeansCode}");
                                }
                                else
                                {
                                    _logger.LogWarning("InvoiceHeaderExtension verisi bulunamadı. Varsayılan değerlerle kayıt yapılıyor.");
                                    
                                    bool isPesin = request.IsCreditSale == false;
                                    // Foreign key kısıtlaması nedeniyle PaymentMeansCode ve PaymentChannelCode değerlerini boş string olarak ayarlıyoruz
                                    string fullPaymentMeansCode = isPesin ? "CASH" : "CREDIT";
                                    string paymentMeansCode = "";
                                    string paymentChannelCode = "";
                                    
                                    _logger.LogInformation($"PaymentMeansCode değeri boş string olarak ayarlandı. Orijinal değer: {fullPaymentMeansCode}");
                                    
                                    await CreateInvoiceHeaderExtensionAsync(connection, transaction, invoiceHeaderId, 
                                        new {
                                            PaymentMeansCode = paymentMeansCode,
                                            PaymentChannelCode = paymentChannelCode,
                                            IsIndividual = false,
                                            DocumentDate = request.InvoiceDate
                                        });
                                    _logger.LogInformation($"Fatura başlığı uzantısı varsayılan değerlerle kaydedildi. Ödeme Tipi: {(isPesin ? "Peşin" : "Vadeli")}, PaymentMeansCode: {paymentMeansCode}");
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "Fatura başlığı uzantısı kaydedilirken hata oluştu");
                                _logger.LogError($"Hata detayları: {ex.Message}");
                                if (ex.InnerException != null)
                                {
                                    _logger.LogError($"Inner exception: {ex.InnerException.Message}");
                                }
                                throw;
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
                    ProcessCode = "BP", // Wholesale Purchase
                    InvoiceNumber = request.InvoiceNumber,
                    InvoiceDate = request.InvoiceDate,
                    CompanyCode = request.CompanyCode,
                    OfficeCode = request.OfficeCode, // OfficeCode parametresi eklendi
                    StoreCode = request.StoreCode,
                    WarehouseCode = request.WarehouseCode,
                    VendorCode = request.VendorCode, // Using VendorCode instead of CurrAccCode
                    CurrAccCode = request.VendorCode, // CurrAccCode parametresi için VendorCode kullanılıyor
                    CurrAccTypeCode = 1, // Tedarikçi
                    TaxTypeCode = Convert.ToByte(request.TaxTypeCode), // Vergi tipi kodu string olarak kaydedilmeli
                    Notes = request.Notes, // Using Notes instead of Description
                    FormType = 0, //  HEr zaman 0
                    PaymentTerm = 0, // Peşin ödeme için PaymentTerm=0 olarak ayarlanıyor
                    CreatedBy = "System", // CreatedBy property does not exist in CreateInvoiceRequest
                    CreatedDate = DateTime.Now  
                };

                // Ödeme bilgilerini logla
                _logger.LogInformation($"FormType: {invoiceHeader.FormType} (0=Peşin, 1=Normal), PaymentTerm: {invoiceHeader.PaymentTerm} (0=Peşin, >0=Vadeli gün sayısı)");

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
                                int sortOrder = 1;
                                foreach (var detail in request.Details)
                                {
                                    await CreateInvoiceLineAsync(connection, transaction, invoiceHeaderId, detail, sortOrder);
                                    sortOrder += 1;
                                }

                               
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
                    OfficeCode = request.OfficeCode, // OfficeCode parametresi eklendi
                    StoreCode = request.StoreCode,
                    WarehouseCode = request.WarehouseCode,
                    VendorCode = request.VendorCode, // Using VendorCode instead of CurrAccCode
                    CurrAccCode = request.VendorCode, // CurrAccCode parametresi için VendorCode kullanılıyor
                    CurrAccTypeCode = 1, // Tedarikçi
                    Notes = request.Notes, // Using Notes instead of Description
                    FormType = 0, // HEr zaman 0
                    PaymentTerm = 0, // Peşin ödeme için PaymentTerm=0 olarak ayarlanıyor
                    CreatedBy = "System", // CreatedBy property does not exist in CreateInvoiceRequest
                    CreatedDate = DateTime.Now
                };

                // Ödeme bilgilerini logla
                _logger.LogInformation($"FormType: {invoiceHeader.FormType} (0=Peşin, 1=Normal), PaymentTerm: {invoiceHeader.PaymentTerm} (0=Peşin, >0=Vadeli gün sayısı)");

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
                                int sortOrder = 1;
                                foreach (var detail in request.Details)
                                {
                                    await CreateInvoiceLineAsync(connection, transaction, invoiceHeaderId, detail, sortOrder);
                                    sortOrder += 1;
                                }

                                // Fatura toplamlarını hesapla ve güncelle
                                
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
                // ShipmentMethodCode NULL ise SQL sorgusundan çıkar
                bool includeShipmentMethodCode = !string.IsNullOrEmpty(invoiceHeader.ShipmentMethodCode);
                
                // Dinamik SQL sorgusu oluştur
                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append(@"
                    INSERT INTO trInvoiceHeader (
                        InvoiceHeaderID,
                        TransTypeCode,
                        ProcessCode,
                        InvoiceNumber,
                        TaxTypeCode,
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
                        IsCompleted,
                        IsCreditSale,");
                
                if (includeShipmentMethodCode)
                {
                    sqlBuilder.Append(@"
                        ShipmentMethodCode,");
                }
                
                sqlBuilder.Append(@"
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
                        @TaxTypeCode,
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
                        @IsCompleted,
                        @IsCreditSale,");
                
                if (includeShipmentMethodCode)
                {
                    sqlBuilder.Append(@"
                        @ShipmentMethodCode,");
                }
                
                sqlBuilder.Append(@"
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
                    )");
                
                var sql = sqlBuilder.ToString();

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
                    // Örnek: WS-7-5 ise bir sonraki WS-7-6 olacak
                    command.Parameters.AddWithValue("@InvoiceNumber", invoiceHeader.InvoiceNumber);

                    command.Parameters.AddWithValue("@TaxTypeCode", invoiceHeader.TaxTypeCode);
                    // IsReturn - İade faturası ise true
                    command.Parameters.AddWithValue("@IsReturn", invoiceHeader.IsReturn);
            
                    // IsCompleted - Tamamlanmış fatura ise true
                    command.Parameters.AddWithValue("@IsCompleted", invoiceHeader.IsCompleted);
                    
                    // IsCreditSale - Vadeli fatura ise true
                    command.Parameters.AddWithValue("@IsCreditSale", invoiceHeader.IsCreditSale);
                    
                    // Tarih ve zaman bilgileri
                    // InvoiceDate ve InvoiceTime formdan gelecek
                    command.Parameters.AddWithValue("@InvoiceDate", invoiceHeader.InvoiceDate);
                    command.Parameters.AddWithValue("@InvoiceTime", invoiceHeader.InvoiceTime ?? DateTime.Now.ToString("HH:mm:ss"));
                    
                    // OperationDate ve OperationTime kayıt zamanı olacak
                    command.Parameters.AddWithValue("@OperationDate", DateTime.Now.Date);
                    command.Parameters.AddWithValue("@OperationTime", DateTime.Now.ToString("HH:mm:ss"));
                    
                    // Form tipi belirleme
                    int formType = 0; // Varsayılan peşin ödeme
                  
                    
                    // PaymentTerm - Peşin ödemeler için 0, vadeli ödemeler için formdan gelecek
                    // Peşin ödeme için 0, vadeli seçilirse 30/45/60/90/120/150/180 olabilir
                    int paymentTerm = 0; // Varsayılan olarak peşin ödeme (PaymentTerm=0)
                    
                    // Eğer özel bir ödeme vadesi belirtilmişse onu kullan
                    if (invoiceHeader.PaymentTerm.HasValue)
                    {
                        paymentTerm = invoiceHeader.PaymentTerm.Value;
                    }
                 
                    command.Parameters.AddWithValue("@PaymentTerm", paymentTerm);
                    _logger.LogInformation($"PaymentTerm: {paymentTerm} (0=Peşin, >0=Vadeli gün sayısı) kullanılıyor.");
                    
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
                    
                    // Adres bilgileri - Formdan gelen müşteri adres ID'leri kullanılacak
                    // ShippingPostalAddressID - Teslimat adresi ID'si (Formdan müşteri teslimat adresi alanından)
                    command.Parameters.AddWithValue("@ShippingPostalAddressID", string.IsNullOrEmpty(invoiceHeader.ShippingPostalAddressID) ? DBNull.Value : invoiceHeader.ShippingPostalAddressID);
                    _logger.LogInformation($"ShippingPostalAddressID: {(string.IsNullOrEmpty(invoiceHeader.ShippingPostalAddressID) ? "NULL" : invoiceHeader.ShippingPostalAddressID)} kullanılıyor.");
                    
                    // BillingPostalAddressID - Fatura adresi ID'si (Formdan müşteri fatura adresi alanından)
                    command.Parameters.AddWithValue("@BillingPostalAddressID", string.IsNullOrEmpty(invoiceHeader.BillingPostalAddressID) ? DBNull.Value : invoiceHeader.BillingPostalAddressID);
                    _logger.LogInformation($"BillingPostalAddressID: {(string.IsNullOrEmpty(invoiceHeader.BillingPostalAddressID) ? "NULL" : invoiceHeader.BillingPostalAddressID)} kullanılıyor.");
                    
                    // ShipmentMethodCode - Sevkiyat yöntemi kodu (opsiyonel)
                    if (includeShipmentMethodCode)
                    {
                        // Büyük veya küçük harfle başlayan ShipmentMethodCode değerini kontrol et
                        string shipmentMethodValue = invoiceHeader.ShipmentMethodCode;
                        command.Parameters.AddWithValue("@ShipmentMethodCode", shipmentMethodValue);
                        _logger.LogInformation($"ShipmentMethodCode: {shipmentMethodValue} kullanılıyor.");
                    }
                    else
                    {
                        _logger.LogInformation("ShipmentMethodCode: NULL olduğu için SQL sorgusundan çıkarıldı.");
                    }
                    
                    command.Parameters.AddWithValue("@TaxExemptionCode", invoiceHeader.TaxExemptionCode ?? 0);
                    
                    // Şirket ve depo bilgileri - Formdan gelecek
                    command.Parameters.AddWithValue("@CompanyCode", invoiceHeader.CompanyCode);
                    command.Parameters.AddWithValue("@OfficeCode", invoiceHeader.OfficeCode);
                    command.Parameters.AddWithValue("@WarehouseCode", invoiceHeader.WarehouseCode);
                    
                    // Form tipi belirleme - Önceden tanımlanmış formType değişkenini kullan
                    // Not: formType değişkeni daha önce tanımlandığı için burada tekrar tanımlanmıyor
                    
                    command.Parameters.AddWithValue("@FormType", formType);
                    _logger.LogInformation($"FormType: {formType} (0=Peşin, 1=Normal) kullanılıyor.");
                    
                    // Döviz bilgileri
                    // ExchangeRate - Formdan gelen döviz kuru değeri (zorunlu)
                    decimal exchangeRate = 0;
                    if (invoiceHeader.ExchangeRate.HasValue)
                    {
                        exchangeRate = invoiceHeader.ExchangeRate.Value;
                    }
                    else
                    {
                        throw new ArgumentNullException("ExchangeRate", "Döviz kuru değeri belirtilmemiş!");
                    }
                    command.Parameters.AddWithValue("@ExchangeRate", exchangeRate);
                    _logger.LogInformation($"ExchangeRate: {exchangeRate} kullanılıyor.");
                    
                    // DocCurrencyCode - Formdan seçilen para birimi (zorunlu)
                    if (string.IsNullOrEmpty(invoiceHeader.DocCurrencyCode))
                    {
                        throw new ArgumentNullException("DocCurrencyCode", "Belge para birimi belirtilmemiş!");
                    }
                    string docCurrencyCode = invoiceHeader.DocCurrencyCode;
                    command.Parameters.AddWithValue("@DocCurrencyCode", docCurrencyCode);
                    _logger.LogInformation($"DocCurrencyCode: {docCurrencyCode} kullanılıyor.");
                    
                    // LocalCurrencyCode - Formdan gelen yerel para birimi (zorunlu)
                    if (string.IsNullOrEmpty(invoiceHeader.LocalCurrencyCode))
                    {
                        throw new ArgumentNullException("LocalCurrencyCode", "Yerel para birimi belirtilmemiş!");
                    }
                    string localCurrencyCode = invoiceHeader.LocalCurrencyCode;
                    command.Parameters.AddWithValue("@LocalCurrencyCode", localCurrencyCode);
                    _logger.LogInformation($"LocalCurrencyCode: {localCurrencyCode} kullanılıyor.");
                    
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
                        SUM(Price * Qty1) AS TotalAmount,
                        SUM(Price * Qty1 * (VatRate / 100)) AS TotalVatAmount,
                        SUM(Price * Qty1 * (1 + VatRate / 100)) AS GrandTotal
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

        // Otomatik fatura numarası oluşturan metot
        public async Task<string> GenerateNextInvoiceNumberAsync(string processCode)
        {
            try
            {
                // Son fatura numarasını al
                string lastInvoiceNumber = await GetLastInvoiceNumberByProcessCodeAsync(processCode);
                
                _logger.LogInformation($"Son fatura numarası: {lastInvoiceNumber}");
                
                if (string.IsNullOrEmpty(lastInvoiceNumber))
                {
                    // Eğer hiç fatura yoksa, ilk fatura numarasını oluştur (1-ProcessCode-7-1 formatında)
                    return $"1-{processCode}-7-1";
                }

                // Fatura numarasını parse et (format: 1-WS-7-217 gibi)
                string[] parts = lastInvoiceNumber.Split('-');
                
                // Formatı kontrol et ve düzenle
                int lastNumber = 0;
                
                // "1-WS-7-217" formatı için (standart format)
                if (parts.Length == 4 && parts[0] == "1" && parts[1] == processCode)
                {
                    if (int.TryParse(parts[3], out lastNumber))
                    {
                        // Son numarayı bir artır
                        int nextNumber = lastNumber + 1;
                        return $"1-{processCode}-7-{nextNumber}";
                    }
                }
                // Eski format (WS-7-217 gibi) - geçiş dönemi için
                else if (parts.Length >= 3 && parts[0] == processCode)
                {
                    if (int.TryParse(parts[parts.Length - 1], out lastNumber))
                    {
                        // Son numarayı bir artır ve yeni formata dönüştür
                        int nextNumber = lastNumber + 1;
                        return $"1-{processCode}-7-{nextNumber}";
                    }
                }
                
                // Oluşturulan fatura numarasının benzersiz olduğunu kontrol et
                string newInvoiceNumber = $"1-{processCode}-7-{(lastNumber > 0 ? lastNumber + 1 : 1)}";
                
                // Veritabanında bu numaranın var olup olmadığını kontrol et
                bool exists = await CheckInvoiceNumberExistsAsync(newInvoiceNumber);
                
                // Eğer numara zaten varsa, benzersiz bir numara bulana kadar artır
                int attempt = 1;
                while (exists && attempt < 100) // Sonsuz döngüye girmemek için maksimum 100 deneme
                {
                    lastNumber++;
                    newInvoiceNumber = $"1-{processCode}-7-{lastNumber}";
                    exists = await CheckInvoiceNumberExistsAsync(newInvoiceNumber);
                    attempt++;
                }
                
                return newInvoiceNumber;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Otomatik fatura numarası oluşturulurken hata oluştu");
                // Hata durumunda benzersiz bir numara oluştur (yeni formatta)
                string uniqueNumber = $"1-{processCode}-7-{DateTime.Now.Ticks % 10000}";
                return uniqueNumber;
            }
        }
        
        // Fatura numarasının veritabanında var olup olmadığını kontrol eden metot
        private async Task<bool> CheckInvoiceNumberExistsAsync(string invoiceNumber)
        {
            try
            {
                string sql = @"SELECT COUNT(1) 
                    FROM trInvoiceHeader WITH (NOLOCK) 
                    WHERE InvoiceNumber = @InvoiceNumber";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@InvoiceNumber", invoiceNumber);
                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result) > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatura numarası kontrol edilirken hata oluştu: {invoiceNumber}");
                return false; // Hata durumunda false dön
            }
        }
    }
}
