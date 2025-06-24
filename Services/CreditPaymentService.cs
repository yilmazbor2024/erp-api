using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using ErpApi.Models.Payments;
using ErpApi.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Dapper;

namespace ErpApi.Services
{
    public class CreditPaymentService : ICreditPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CreditPaymentService> _logger;
        
        public CreditPaymentService(IConfiguration configuration, ILogger<CreditPaymentService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        
        public async Task<CreditPaymentResponse> CreateCreditPaymentForInvoiceAsync(CreditPaymentRequest request)
        {
            _logger.LogInformation("Vadeli ödeme işlemi başlatılıyor. Fatura No: {InvoiceNumber}, Cari Kod: {CurrAccCode}, Döviz: {DocCurrencyCode}, Kur: {ExchangeRate}", 
                request.InvoiceNumber, request.CurrAccCode, request.DocCurrencyCode, request.ExchangeRate);
                
            using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
            _logger.LogInformation("Veritabanı bağlantısı açılıyor...");
            await connection.OpenAsync();
            _logger.LogInformation("Veritabanı bağlantısı açıldı.");
            
            _logger.LogInformation("Transaction başlatılıyor...");
            using var transaction = connection.BeginTransaction();
            _logger.LogInformation("Transaction başlatıldı.");
            
            try
            {
                // 1. Borç başlık kaydı oluştur (trDebitHeader)
                _logger.LogInformation("Borç başlık kaydı oluşturuluyor (trDebitHeader)...");
                var debitHeaderId = Guid.NewGuid();
                await InsertDebitHeaderAsync(connection, transaction, debitHeaderId, request);
                _logger.LogInformation("Borç başlık kaydı oluşturuldu. DebitHeaderId: {DebitHeaderId}", debitHeaderId);
                
                // 2. Borç satır kayıtları oluştur (trDebitLine)
                int lineNumber = 1;
                Guid debitLineId = Guid.Empty;
                Guid currAccBookId = Guid.Empty;
                
                // Ödeme satırlarını kontrol et
                if (request.PaymentRows == null || request.PaymentRows.Count == 0)
                {
                    _logger.LogError("HATA: Ödeme satırları boş. Vadeli ödeme işlemi yapılamıyor.");
                    throw new InvalidOperationException("Ödeme satırları boş. Vadeli ödeme işlemi yapılamıyor.");
                }

                foreach (var paymentRow in request.PaymentRows)
                {
                    // Tutar kontrolü
                    if (paymentRow.Amount <= 0)
                    {
                        _logger.LogWarning("DİKKAT: Vadeli ödeme tutarı sıfır veya negatif: {Amount}. İşlem devam ediyor.", paymentRow.Amount);
                    }
                    
                    // Döviz kodu kontrolü
                    if (string.IsNullOrEmpty(paymentRow.CurrencyCode))
                    {
                        _logger.LogWarning("DİKKAT: Döviz kodu boş. Varsayılan olarak TRY kullanılacak.");
                        paymentRow.CurrencyCode = "TRY";
                    }
                    
                    // Kur kontrolü
                    if (paymentRow.CurrencyCode != "TRY" && paymentRow.ExchangeRate <= 1)
                    {
                        _logger.LogWarning("DİKKAT: {CurrencyCode} için döviz kuru 1 veya daha düşük: {ExchangeRate}. TRY karşılığı hesaplaması hatalı olabilir.", 
                            paymentRow.CurrencyCode, paymentRow.ExchangeRate);
                    }
                    
                    _logger.LogInformation("Borç satır kaydı oluşturuluyor (trDebitLine). Satır: {LineNumber}, Tutar: {Amount}, Döviz: {CurrencyCode}, Kur: {ExchangeRate}, TRY karşılığı: {TRYAmount}, Vade: {DueDate}", 
                        lineNumber, paymentRow.Amount, paymentRow.CurrencyCode, paymentRow.ExchangeRate, paymentRow.Amount * paymentRow.ExchangeRate, paymentRow.DueDate.ToString("yyyy-MM-dd"));
                    
                    debitLineId = Guid.NewGuid();
                    await InsertDebitLineAsync(connection, transaction, debitLineId, debitHeaderId, lineNumber, paymentRow, request);
                    
                    // 3. Borç satırı para birimi kayıtları (trDebitLineCurrency)
                    _logger.LogInformation("Borç satırı para birimi kaydı oluşturuluyor (trDebitLineCurrency). Para Birimi: {CurrencyCode}, Kur: {ExchangeRate}", 
                        paymentRow.CurrencyCode, paymentRow.ExchangeRate);
                    await InsertDebitLineCurrencyAsync(connection, transaction, debitLineId, paymentRow);
                    
                    // 4. Muhasebe kaydı oluştur (trCurrAccBook)
                    _logger.LogInformation("Muhasebe kaydı oluşturuluyor (trCurrAccBook). Satır: {LineNumber}", lineNumber);
                    currAccBookId = Guid.NewGuid();
                    await InsertCurrAccBookAsync(connection, transaction, currAccBookId, request, "Debit", debitLineId, lineNumber);
                    
                    // 5. Muhasebe para birimi kaydı (trCurrAccBookCurrency)
                    _logger.LogInformation("Muhasebe para birimi kaydı oluşturuluyor (trCurrAccBookCurrency). Para Birimi: {CurrencyCode}", 
                        paymentRow.CurrencyCode);
                    await InsertCurrAccBookCurrencyAsync(connection, transaction, currAccBookId, paymentRow, true);
                    
                    lineNumber++;
                }
                
                // 6. Öznitelik tablolarına kayıt
                _logger.LogInformation("Öznitelik tablolarına kayıt yapılıyor (tpDebitATAttribute)...");
                await InsertDebitAttributeAsync(connection, transaction, debitHeaderId, request);
                
                // 7. Fatura özniteliklerini muhasebe özniteliklerine kopyala
                if (!string.IsNullOrEmpty(request.InvoiceHeaderID))
                {
                    _logger.LogInformation("Fatura öznitelikleri muhasebe özniteliklerine kopyalanıyor. InvoiceHeaderID: {InvoiceHeaderID}", 
                        request.InvoiceHeaderID);
                    await CopyInvoiceAttributesToCurrAccBookAsync(connection, transaction, Guid.Parse(request.InvoiceHeaderID), currAccBookId);
                }
                else if (request.InvoiceId != Guid.Empty)
                {
                    _logger.LogInformation("Fatura öznitelikleri muhasebe özniteliklerine kopyalanıyor. InvoiceId: {InvoiceId}", 
                        request.InvoiceId);
                    await CopyInvoiceAttributesToCurrAccBookAsync(connection, transaction, request.InvoiceId, currAccBookId);
                }
                
                // 8. Transaction'ı commit et
                _logger.LogInformation("Transaction commit ediliyor...");
                transaction.Commit();
                _logger.LogInformation("Transaction commit edildi.");
                
                // 9. Faturayı tamamlandı olarak işaretle
                if (!string.IsNullOrEmpty(request.InvoiceHeaderID))
                {
                    _logger.LogInformation("Fatura tamamlandı olarak işaretleniyor. InvoiceHeaderID: {InvoiceHeaderID}", 
                        request.InvoiceHeaderID);
                    await MarkInvoiceAsCompletedAsync(request.InvoiceHeaderID);
                }
                else if (request.InvoiceId != Guid.Empty)
                {
                    _logger.LogInformation("Fatura tamamlandı olarak işaretleniyor. InvoiceId: {InvoiceId}", 
                        request.InvoiceId);
                    await MarkInvoiceAsCompletedAsync(request.InvoiceId.ToString());
                }
                
                // 10. Stored procedure çağrıları
                _logger.LogInformation("Stored procedure çağrıları yapılıyor...");
                await ExecuteDebitTransactionControlAsync(connection, transaction, "Debit", debitHeaderId);
                await ExecuteInvoiceControlAsync(connection, transaction, "Invoi", request.InvoiceId);
                _logger.LogInformation("Stored procedure çağrıları tamamlandı.");
                
                _logger.LogInformation("Vadeli ödeme işlemi başarıyla tamamlandı. DebitHeaderId: {DebitHeaderId}", debitHeaderId);
                return new CreditPaymentResponse
                {
                    Success = true,
                    Message = "Vadeli ödeme başarıyla kaydedildi.",
                    DebitHeaderId = debitHeaderId.ToString()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Vadeli ödeme işlemi sırasında hata oluştu: {Message}", ex.Message);
                
                try
                {
                    _logger.LogInformation("Transaction geri alınıyor...");
                    transaction.Rollback();
                    _logger.LogInformation("Transaction geri alındı.");
                }
                catch (Exception rollbackEx)
                {
                    _logger.LogError(rollbackEx, "Transaction geri alınırken hata oluştu: {Message}", rollbackEx.Message);
                }
                
                return new CreditPaymentResponse
                {
                    Success = false,
                    Message = $"Vadeli ödeme kaydedilirken hata oluştu: {ex.Message}",
                    DebitHeaderId = Guid.Empty.ToString()
                };
            }
        }
        
        // Fatura özniteliklerini muhasebe özniteliklerine kopyalayan yeni metot
        private async Task CopyInvoiceAttributesToCurrAccBookAsync(SqlConnection connection, SqlTransaction transaction, Guid invoiceHeaderId, Guid currAccBookId)
        {
            _logger.LogInformation("CopyInvoiceAttributesToCurrAccBookAsync çağrıldı. InvoiceHeaderId: {InvoiceHeaderId}, CurrAccBookId: {CurrAccBookId}", 
                invoiceHeaderId, currAccBookId);
                
            var sql = @"
            INSERT INTO tpCurrAccBookATAttribute (CurrAccBookID, tpCurrAccBookATAttribute.AttributeTypeCode, tpCurrAccBookATAttribute.AttributeCode, 
            tpCurrAccBookATAttribute.CreatedUserName, tpCurrAccBookATAttribute.CreatedDate, tpCurrAccBookATAttribute.LastUpdatedUserName, tpCurrAccBookATAttribute.LastUpdatedDate)
            SELECT @CurrAccBookID, tpInvoiceATAttribute.AttributeTypeCode, tpInvoiceATAttribute.AttributeCode, @UserName, GETDATE(), @UserName, GETDATE()
            FROM tpInvoiceATAttribute WITH(NOLOCK)
            WHERE InvoiceHeaderID = @InvoiceHeaderID";

            var parameters = new
            {
                CurrAccBookID = currAccBookId,
                InvoiceHeaderID = invoiceHeaderId,
                UserName = "UZK  Uzak"
            };

            await connection.ExecuteAsync(sql, parameters, transaction);
            _logger.LogInformation("Fatura öznitelikleri muhasebe özniteliklerine kopyalandı.");
        }

        // Stored procedure çağrıları için metotlar
        private async Task ExecuteDebitTransactionControlAsync(SqlConnection connection, SqlTransaction transaction, string transactionType, Guid transactionId)
        {
            _logger.LogInformation("ExecuteDebitTransactionControlAsync çağrıldı. TransactionType: {TransactionType}, TransactionId: {TransactionId}", 
                transactionType, transactionId);

            // Stored procedure'a doğru parametreler gönderiliyor
            // Stored procedure tanımı: CREATE PROCEDURE [dbo].[sp_DebitTransactionControl] (@ApplicationCode Char20, @ApplicationID uniqueidentifier)
            await connection.ExecuteAsync("sp_DebitTransactionControl", 
                new { 
                    ApplicationCode = "Debit", 
                    ApplicationID = transactionId
                }, 
                transaction, 
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation("sp_DebitTransactionControl stored procedure başarıyla çağrıldı.");
        }    

        private async Task ExecuteInvoiceControlAsync(SqlConnection connection, SqlTransaction transaction, string transactionType, Guid transactionId)
        {
            _logger.LogInformation("ExecuteInvoiceControlAsync çağrıldı. TransactionType: {TransactionType}, TransactionId: {TransactionId}", 
                transactionType, transactionId);
        
            // Stored procedure tanımı: CREATE PROCEDURE [dbo].[sp_InvoiceControl] (@ApplicationCode Char20, @ApplicationID uniqueidentifier)
            await connection.ExecuteAsync("sp_InvoiceControl", 
                new { 
                    ApplicationCode = "Invoi", 
                    ApplicationID = transactionId
                }, 
                transaction, 
                commandType: CommandType.StoredProcedure);
        }

        private async Task InsertDebitHeaderAsync(SqlConnection connection, SqlTransaction transaction, Guid debitHeaderId, CreditPaymentRequest request)
        {
            _logger.LogInformation("InsertDebitHeaderAsync çağrıldı. DebitHeaderId: {DebitHeaderId}", debitHeaderId);
            
            // sp_LastRefNumProcess ile işlem referans numarası oluştur
            string refNumber;
            try
            {
                _logger.LogInformation("sp_LastRefNumProcess sorgusu çağrılıyor. CompanyCode: {CompanyCode}, ProcessCode: {ProcessCode}, ProcessFlowCode: {ProcessFlowCode}", 
                    1, "WS", 7);
                    
                var refNumParams = new { CompanyCode = 1, ProcessCode = "WS", ProcessFlowCode = 7 };
                refNumber = await connection.QueryFirstOrDefaultAsync<string>(
                    "sp_LastRefNumProcess", 
                    refNumParams, 
                    transaction, 
                    commandType: CommandType.StoredProcedure
                );
                
                if (string.IsNullOrEmpty(refNumber))
                {
                    refNumber = $"WS-{DateTime.Now:yyMMdd}-{new Random().Next(1000, 9999)}";
                    _logger.LogWarning("Referans numarası oluşturulamadı, varsayılan değer kullanılıyor: {RefNumber}", refNumber);
                }
                else
                {
                    _logger.LogInformation("Referans numarası oluşturuldu: {RefNumber}", refNumber);
                }
            }
            catch (Exception ex)
            {
                refNumber = $"WS-{DateTime.Now:yyMMdd}-{new Random().Next(1000, 9999)}";
                _logger.LogError(ex, "Referans numarası oluşturulurken hata oluştu, varsayılan değer kullanılıyor: {Message}", ex.Message);
            }
            
            // sp_LastRefNumDebit ile borç numarası oluştur
            string debitNumber;
            try
            {
                _logger.LogInformation("sp_LastRefNumDebit sorgusu çağrılıyor. CompanyCode: {CompanyCode}, DebitTypeCode: {DebitTypeCode}", 
                    1, 2);
                    
                var debitNumParams = new { CompanyCode = 1, DebitTypeCode = 2 };
                debitNumber = await connection.QueryFirstOrDefaultAsync<string>(
                    "sp_LastRefNumDebit", 
                    debitNumParams, 
                    transaction, 
                    commandType: CommandType.StoredProcedure
                );
                
                if (string.IsNullOrEmpty(debitNumber))
                {
                    debitNumber = $"C-{DateTime.Now:yyMMdd}-{new Random().Next(1000, 9999)}";
                    _logger.LogWarning("Borç numarası oluşturulamadı, varsayılan değer kullanılıyor: {DebitNumber}", debitNumber);
                }
                else
                {
                    _logger.LogInformation("Borç numarası oluşturuldu: {DebitNumber}", debitNumber);
                }
            }
            catch (Exception ex)
            {
                debitNumber = $"C-{DateTime.Now:yyMMdd}-{new Random().Next(1000, 9999)}";
                _logger.LogError(ex, "Borç numarası oluşturulurken hata oluştu, varsayılan değer kullanılıyor: {Message}", ex.Message);
            }
            
            var sql = @"
            INSERT INTO [trDebitHeader]
            ([DebitHeaderID], [DebitTypeCode], [DocumentDate], [DocumentTime], [DebitNumber],
            [DocumentNumber], [RefNumber], [CurrAccCode], [CurrAccTypeCode], [StoreCode],
            [SubCurrAccID], [ContactID], [Description], [GLTypeCode], [DocCurrencyCode],
            [LocalCurrencyCode], [ExchangeRate], [IsPostingJournal], [OfficeCode], [ApplicationCode],
            [ApplicationID], [IsCompleted], [IsPrinted], [IsLocked], [CompanyCode],
            [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
            VALUES
            (@DebitHeaderID, @DebitTypeCode, @DocumentDate, @DocumentTime, @DebitNumber,
            @DocumentNumber, @RefNumber, @CurrAccCode, @CurrAccTypeCode, @StoreCode,
            NULL, NULL, @Description, @GLTypeCode, @DocCurrencyCode,
            @LocalCurrencyCode, @ExchangeRate, @IsPostingJournal, @OfficeCode, @ApplicationCode,
            @ApplicationID, @IsCompleted, @IsPrinted, @IsLocked, @CompanyCode,
            @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE())";

            var parameters = new
            {
                DebitHeaderID = debitHeaderId,
                DebitTypeCode = (byte)2, // Vadeli işlem kodu
                DocumentDate = DateTime.Now.Date,
                DocumentTime = DateTime.Now.TimeOfDay,
                DebitNumber = debitNumber,
                DocumentNumber = "0",
                RefNumber = refNumber, // sp_LastRefNumProcess'ten gelen değer
                CurrAccCode = request.CurrAccCode,
                CurrAccTypeCode = (byte)3, // Müşteri
                StoreCode = "",
                Description = $"Toptan Satış - Vadeli Fatura {request.InvoiceNumber}",
                GLTypeCode = "",
                DocCurrencyCode = request.DocCurrencyCode ?? "TRY",
                LocalCurrencyCode = "TRY",
                ExchangeRate = 1.0,
                IsPostingJournal = false,
                OfficeCode = "M",
                ApplicationCode = "Invoi",
                ApplicationID = !string.IsNullOrEmpty(request.InvoiceHeaderID) ? Guid.Parse(request.InvoiceHeaderID) : request.InvoiceId,
                IsCompleted = false,
                IsPrinted = false,
                IsLocked = false,
                CompanyCode = 1,
                CreatedUserName = "UZK  Uzak",
                LastUpdatedUserName = "UZK  Uzak"
            };

    await connection.ExecuteAsync(sql, parameters, transaction);
    _logger.LogInformation("Borç başlık kaydı oluşturuldu. DebitNumber: {DebitNumber}", debitNumber);
}

private async Task InsertDebitLineAsync(SqlConnection connection, SqlTransaction transaction, Guid debitLineId, Guid debitHeaderId, int lineNumber, PaymentRow paymentRow, CreditPaymentRequest request)
{
    _logger.LogInformation("InsertDebitLineAsync çağrıldı. DebitLineId: {DebitLineId}, LineNumber: {LineNumber}", 
        debitLineId, lineNumber);
    
    // Müşteri için özel fiyat/markup oranını al
    decimal markupRate = 1.0m;
    try
    {
        _logger.LogInformation("qry_GetItemCustomerMarkupRate sorgusu çağrılıyor. CurrAccTypeCode: {CurrAccTypeCode}, CurrAccCode: {CurrAccCode}", 
            3, request.CurrAccCode);
            
        var markupParams = new { 
            CurrAccTypeCode = (byte)3, // Müşteri
            CurrAccCode = request.CurrAccCode, 
            SubCurrAccID = (Guid?)null, // Müşteri alt hesabı (null olabilir)
            ITemTypeCode = (byte)1, // ItemTypeCode parametresi (stored procedure'de typo var - ITemTypeCode)
            ItemCode = "", // Boş ItemCode
            CompanyCode = (byte)1, // Şirket kodu
            ColorCode = "" // Opsiyonel renk kodu (varsayılan boş string)
        };
        
        var result = await connection.QueryFirstOrDefaultAsync<decimal?>(
            "qry_GetItemCustomerMarkupRate", 
            markupParams, 
            transaction, 
            commandType: CommandType.StoredProcedure
        );
        
        if (result.HasValue)
        {
            markupRate = result.Value;
            _logger.LogInformation("Markup oranı alındı: {MarkupRate}", markupRate);
        }
        else
        {
            _logger.LogInformation("Markup oranı alınamadı, varsayılan değer kullanılıyor: {MarkupRate}", markupRate);
        }
    }
    catch (Exception ex)
    {
        _logger.LogWarning(ex, "Markup oranı alınırken hata oluştu, varsayılan değer kullanılıyor: {Message}", ex.Message);
    }
        
    var sql = @"
    INSERT INTO [trDebitLine]
    ([DebitLineID], [DueDate], [DocCurrencyCode], [DebitHeaderID],
    [LineDescription], [SortOrder], [DebitReasonCode], [CreatedUserName],
    [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
    VALUES
    (@DebitLineID, @DueDate, @DocCurrencyCode, @DebitHeaderID,
    @LineDescription, @SortOrder, @DebitReasonCode, @CreatedUserName,
    GETDATE(), @LastUpdatedUserName, GETDATE())";

    var parameters = new
    {
        DebitLineID = debitLineId,
        DueDate = paymentRow.DueDate,
        DocCurrencyCode = paymentRow.CurrencyCode,
        DebitHeaderID = debitHeaderId,
        LineDescription = $"Vadeli Ödeme Satır {lineNumber} (Markup: {markupRate})",
        SortOrder = lineNumber,
        DebitReasonCode = "I", // Örnek sorgudaki değeri kullanıyoruz
        CreatedUserName = "UZK  Uzak",
        LastUpdatedUserName = "UZK  Uzak"
    };

    await connection.ExecuteAsync(sql, parameters, transaction);
    _logger.LogInformation("Borç satır kaydı oluşturuldu.");
}

private async Task InsertDebitLineCurrencyAsync(SqlConnection connection, SqlTransaction transaction, Guid debitLineId, PaymentRow paymentRow)
{
    // Tutar kontrolü - Sıfır veya negatif tutarları engelle
    if (paymentRow.Amount <= 0)
    {
        _logger.LogError("HATA: Geçersiz tutar: {Amount}. Sıfır veya negatif tutar olamaz.", paymentRow.Amount);
        _logger.LogWarning("Geçersiz tutar düzeltiliyor. Minimum 0.01 değeri kullanılacak.");
        paymentRow.Amount = 0.01m; // Minimum geçerli bir tutar atama
    }
    
    // Döviz kodu kontrolü
    if (string.IsNullOrEmpty(paymentRow.CurrencyCode))
    {
        _logger.LogWarning("DİKKAT: Döviz kodu boş. Varsayılan olarak TRY kullanılacak.");
        paymentRow.CurrencyCode = "TRY";
    }
    
    // Kur kontrolü
    if (paymentRow.CurrencyCode != "TRY" && paymentRow.ExchangeRate <= 0)
    {
        _logger.LogError("HATA: Geçersiz döviz kuru: {ExchangeRate}. Minimum 0.01 olmalı.", paymentRow.ExchangeRate);
        // Minimum geçerli bir değer ata
        paymentRow.ExchangeRate = 0.01m;
    }
    
    // TRY karşılığı hesapla
    decimal tryAmount = paymentRow.Amount * paymentRow.ExchangeRate;
    
    _logger.LogInformation("InsertDebitLineCurrencyAsync çağrıldı. DebitLineId: {DebitLineId}, CurrencyCode: {CurrencyCode}, Amount: {Amount}, ExchangeRate: {ExchangeRate}, TRY karşılığı: {TRYAmount}", 
        debitLineId, paymentRow.CurrencyCode, paymentRow.Amount, paymentRow.ExchangeRate, tryAmount);
    
    string currencyCode = paymentRow.CurrencyCode;
                
    var sql = @"
    MERGE [trDebitLineCurrency]
    USING(SELECT [DebitLineID] = @DebitLineID,
    [CurrencyCode] = @CurrencyCode
    ) AS TBNA
    ON (([trDebitLineCurrency].[DebitLineID] = @DebitLineID)
    AND ([trDebitLineCurrency].[CurrencyCode] = @CurrencyCode)
    AND ([trDebitLineCurrency].[DebitLineID] = [TBNA].[DebitLineID])
    AND ([trDebitLineCurrency].[CurrencyCode] = [TBNA].[CurrencyCode]))
    WHEN MATCHED THEN UPDATE SET
    [trDebitLineCurrency].[ExchangeRate] = @ExchangeRate
    ,[trDebitLineCurrency].[RelationCurrencyCode] = @RelationCurrencyCode
    ,[trDebitLineCurrency].[Debit] = @Debit
    ,[trDebitLineCurrency].[Credit] = @Credit
    ,[trDebitLineCurrency].[LastUpdatedUserName] = @LastUpdatedUserName
    ,[trDebitLineCurrency].[LastUpdatedDate] = GETDATE()
    
    WHEN NOT MATCHED THEN INSERT([DebitLineID], [CurrencyCode], [ExchangeRate], [RelationCurrencyCode], [Debit], [Credit], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
    VALUES(@DebitLineID, @CurrencyCode, @ExchangeRate, @RelationCurrencyCode, @Debit, @Credit, @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE());";

    // Döviz kaydı için
    var parameters = new
    {
        DebitLineID = debitLineId,
        CurrencyCode = currencyCode,
        ExchangeRate = currencyCode == "TRY" ? paymentRow.ExchangeRate : 1m, // Döviz kaydı için kur 1
        RelationCurrencyCode = currencyCode, // İşlem para birimi
        Debit = paymentRow.Amount, // Döviz tutarı - DÜZELTME: Debit değeri 0 değil, tutar olmalı
        Credit = (decimal)0,
        CreatedUserName = "UZK  Uzak",
        LastUpdatedUserName = "UZK  Uzak"
    };
    
    _logger.LogInformation("Döviz kaydı için parametreler: DebitLineID={DebitLineID}, CurrencyCode={CurrencyCode}, ExchangeRate={ExchangeRate}, RelationCurrencyCode={RelationCurrencyCode}, Debit={Debit}, Credit={Credit}", 
        debitLineId, currencyCode, currencyCode == "TRY" ? paymentRow.ExchangeRate : 1m, currencyCode, paymentRow.Amount, 0m);

    await connection.ExecuteAsync(sql, parameters, transaction);
    _logger.LogInformation("Borç satırı para birimi kaydı oluşturuldu. Para Birimi: {CurrencyCode}, Tutar: {Amount}", 
        currencyCode, paymentRow.Amount);
    
    // Eğer para birimi TRY değilse, TRY karşılık kaydı da oluştur
    if (currencyCode != "TRY")
    {
        _logger.LogInformation("TRY karşılık kaydı oluşturuluyor. DebitLineId: {DebitLineId}", debitLineId);
        
        var tryParameters = new
        {
            DebitLineID = debitLineId,
            CurrencyCode = "TRY",
            ExchangeRate = paymentRow.ExchangeRate, // TRY kaydı için gerçek kur değeri
            RelationCurrencyCode = currencyCode, // İşlem para birimi (USD, EUR, GBP vb.)
            Debit = paymentRow.Amount * paymentRow.ExchangeRate, // TL karşılığı
            Credit = (decimal)0,
            CreatedUserName = "UZK  Uzak",
            LastUpdatedUserName = "UZK  Uzak"
        };
        
        await connection.ExecuteAsync(sql, tryParameters, transaction);
        _logger.LogInformation("TRY karşılık kaydı oluşturuldu. Tutar: {Amount}, Kur: {ExchangeRate}", 
            paymentRow.Amount * paymentRow.ExchangeRate, paymentRow.ExchangeRate);
    }
}

        private async Task InsertCurrAccBookAsync(SqlConnection connection, SqlTransaction transaction, Guid currAccBookId, CreditPaymentRequest request, string applicationCode, Guid applicationId, int lineNumber)
        {
            _logger.LogInformation("InsertCurrAccBookAsync çağrıldı. CurrAccBookId: {CurrAccBookId}, ApplicationCode: {ApplicationCode}, LineNumber: {LineNumber}", 
                currAccBookId, applicationCode, lineNumber);
                
            var sql = @"
            INSERT INTO trCurrAccBook (
                CurrAccBookID, SortOrder, CurrAccTypeCode, CurrAccCode, DocumentDate, 
                DocumentTime, DocumentNumber, DueDate, LineDescription, Description, 
                InternalDescription, RefNumber, CompanyCode, OfficeCode, StoreTypeCode, 
                StoreCode, LocalCurrencyCode, DocCurrencyCode, ApplicationCode, ApplicationID, 
                BaseApplicationCode, BaseApplicationID, DebitTypeCode, CashTransTypeCode, 
                ImportFileNumber, ExportFileNumber, CreatedUserName, CreatedDate, 
                LastUpdatedUserName, LastUpdatedDate)
            VALUES (
                @CurrAccBookID, @SortOrder, @CurrAccTypeCode, @CurrAccCode, @DocumentDate, 
                @DocumentTime, @DocumentNumber, @DueDate, @LineDescription, @Description, 
                @InternalDescription, @RefNumber, @CompanyCode, @OfficeCode, @StoreTypeCode, 
                @StoreCode, @LocalCurrencyCode, @DocCurrencyCode, @ApplicationCode, @ApplicationID, 
                @BaseApplicationCode, @BaseApplicationID, @DebitTypeCode, @CashTransTypeCode, 
                @ImportFileNumber, @ExportFileNumber, @CreatedUserName, GETDATE(), 
                @LastUpdatedUserName, GETDATE())";

            var paymentRow = request.PaymentRows[lineNumber - 1];
    
    // Frontend'den gelen vade tarihini kullan
         var dueDate = paymentRow.DueDate;
    
    _logger.LogInformation("Frontend'den gelen vade tarihi kullanılıyor: {DueDate}", 
        dueDate.ToString("yyyy-MM-dd"));
    
    // LineDescription için qry_GetCurrAccBookLineDescription sorgusu çağrısı
    string lineDescription = "Toptan Satış - Fatura";
    try
    {
        _logger.LogInformation("qry_GetCurrAccBookLineDescription sorgusu çağrılıyor. ApplicationCode: {ApplicationCode}, ApplicationID: {ApplicationID}", 
            applicationCode, applicationId);
            
        var lineDescriptionQuery = "qry_GetCurrAccBookLineDescription";
        var lineDescriptionParams = new { 
            ApplicationCode = applicationCode, 
            ApplicationLineID = applicationId, 
            LangCode = "TR" 
        };
        
        var result = await connection.QueryFirstOrDefaultAsync<string>(
            lineDescriptionQuery, 
            lineDescriptionParams, 
            transaction, 
            commandType: CommandType.StoredProcedure
        );
        
        if (!string.IsNullOrEmpty(result))
        {
            lineDescription = result;
            _logger.LogInformation("LineDescription sorgudan alındı: {LineDescription}", lineDescription);
        }
        else
        {
            _logger.LogInformation("LineDescription sorgudan alınamadı, varsayılan değer kullanılıyor: {LineDescription}", lineDescription);
        }
    }
    catch (Exception ex)
    {
        _logger.LogWarning(ex, "LineDescription sorgusu çalıştırılırken hata oluştu, varsayılan değer kullanılıyor: {Message}", ex.Message);
    }

    var parameters = new
    {
        SortOrder = lineNumber,
        CurrAccBookID = currAccBookId,
        CompanyCode = 1,
        OfficeCode = "M",
        StoreTypeCode = (byte)5,
        StoreCode = "",
        CurrAccTypeCode = (byte)3, // Müşteri
        CurrAccCode = request.CurrAccCode,
        DocumentDate = DateTime.Now.Date,
        DocumentTime = DateTime.Now.TimeOfDay,
        DocumentNumber = "0",
        RefNumber = request.InvoiceNumber,
        DueDate = dueDate,
        LineDescription = lineDescription,
        LocalCurrencyCode = "TRY",
        DocCurrencyCode = paymentRow?.CurrencyCode ?? "TRY",
        ApplicationCode = "Debit",
        ApplicationID = applicationId,
        DebitTypeCode = (byte)2, // Vadeli işlem kodu
        BankTransTypeCode = (byte)0,
        CashTransTypeCode = (byte)0,
        CreditCardPaymentTypeCode = (byte)0,
        GiftCardPaymentTypeCode = (byte)0,
        ChequeTransTypeCode = (byte)0,
        ImportFileNumber = "",
        ExportFileNumber = "",
        Description = request.Description ?? "",
        InternalDescription = "",
        BaseApplicationCode = "Invoi",
        BaseApplicationID = !string.IsNullOrEmpty(request.InvoiceHeaderID) ? Guid.Parse(request.InvoiceHeaderID) : request.InvoiceId,
        CreatedUserName = "UZK  Uzak",
        LastUpdatedUserName = "UZK  Uzak"
    };

    await connection.ExecuteAsync(sql, parameters, transaction);
    _logger.LogInformation("Cari hesap kaydı oluşturuldu.");
}

private async Task InsertCurrAccBookCurrencyAsync(SqlConnection connection, SqlTransaction transaction, Guid currAccBookId, PaymentRow paymentRow, bool isDebit)
{
    // Tutar kontrolü - Sıfır veya negatif tutarları engelle
    if (paymentRow.Amount <= 0)
    {
        _logger.LogError("HATA: Muhasebe kaydında geçersiz tutar: {Amount}. Sıfır veya negatif tutar olamaz.", paymentRow.Amount);
        _logger.LogWarning("Muhasebe kaydında geçersiz tutar düzeltiliyor. Minimum 0.01 değeri kullanılacak.");
        paymentRow.Amount = 0.01m; // Minimum geçerli bir tutar atama
    }
    
    // Döviz kodu kontrolü
    if (string.IsNullOrEmpty(paymentRow.CurrencyCode))
    {
        _logger.LogWarning("DİKKAT: Muhasebe kaydında döviz kodu boş. Varsayılan olarak TRY kullanılacak.");
        paymentRow.CurrencyCode = "TRY";
    }
    
    // Kur kontrolü
    if (paymentRow.CurrencyCode != "TRY" && paymentRow.ExchangeRate <= 0)
    {
        _logger.LogError("HATA: Muhasebe kaydında geçersiz döviz kuru: {ExchangeRate}. Minimum 0.01 olmalı.", paymentRow.ExchangeRate);
        // Minimum geçerli bir değer ata
        paymentRow.ExchangeRate = 0.01m;
    }
    
    _logger.LogInformation("InsertCurrAccBookCurrencyAsync çağrıldı. CurrAccBookId: {CurrAccBookId}, IsDebit: {IsDebit}, CurrencyCode: {CurrencyCode}, Amount: {Amount}, ExchangeRate: {ExchangeRate}, TRY karşılığı: {TRYAmount}", 
        currAccBookId, isDebit, paymentRow.CurrencyCode, paymentRow.Amount, paymentRow.ExchangeRate, paymentRow.Amount * paymentRow.ExchangeRate);
    
    string currencyCode = paymentRow.CurrencyCode;
                
    var sql = @"
    INSERT INTO [trCurrAccBookCurrency]
    ([CurrAccBookID], [CurrencyCode], [ExchangeRate], [RelationCurrencyCode],
    [Debit], [Credit], [CreatedUserName], [CreatedDate],
    [LastUpdatedUserName], [LastUpdatedDate])
    VALUES
    (@CurrAccBookID, @CurrencyCode, @ExchangeRate, @RelationCurrencyCode,
    @Debit, @Credit, @CreatedUserName, GETDATE(),
    @LastUpdatedUserName, GETDATE())";

    // Döviz kaydı için
    var parameters = new
    {
        CurrAccBookID = currAccBookId,
        CurrencyCode = currencyCode,
        ExchangeRate = currencyCode == "TRY" ? paymentRow.ExchangeRate : 1m, // Döviz kaydı için kur 1
        RelationCurrencyCode = currencyCode, // İşlem para birimi
        Debit = isDebit ? paymentRow.Amount : 0m, // Döviz tutarı - DÜZELTME: Debit değeri doğru atanmalı
        Credit = isDebit ? 0m : paymentRow.Amount,
        CreatedUserName = "UZK  Uzak",
        LastUpdatedUserName = "UZK  Uzak"
    };
    
    _logger.LogInformation("Muhasebe para birimi kaydı için parametreler: CurrAccBookID={CurrAccBookID}, CurrencyCode={CurrencyCode}, ExchangeRate={ExchangeRate}, RelationCurrencyCode={RelationCurrencyCode}, Debit={Debit}, Credit={Credit}, IsDebit={IsDebit}", 
        currAccBookId, currencyCode, currencyCode == "TRY" ? paymentRow.ExchangeRate : 1m, currencyCode, isDebit ? paymentRow.Amount : 0m, isDebit ? 0m : paymentRow.Amount, isDebit);

    await connection.ExecuteAsync(sql, parameters, transaction);
    _logger.LogInformation("Muhasebe para birimi kaydı oluşturuldu. Para Birimi: {CurrencyCode}, Tutar: {Amount}, {IsDebitText}", 
        currencyCode, isDebit ? paymentRow.Amount : 0m, isDebit ? "Borç" : "Alacak");
    
    // Eğer para birimi TRY değilse, TRY karşılık kaydı da oluştur
    if (currencyCode != "TRY")
    {
        _logger.LogInformation("TRY karşılık kaydı oluşturuluyor. CurrAccBookId: {CurrAccBookId}", currAccBookId);
        
        // TRY karşılığı hesapla ve sıfırdan küçük olmamasını sağla
        decimal tryAmount = paymentRow.Amount * paymentRow.ExchangeRate;
        if (tryAmount <= 0 && paymentRow.Amount > 0)
        {
            _logger.LogWarning("DİKKAT: TRY karşılığı sıfır veya negatif hesaplandı. Orijinal tutar: {Amount}, Kur: {ExchangeRate}, Hesaplanan: {TryAmount}. Minimum değer kullanılacak.", 
                paymentRow.Amount, paymentRow.ExchangeRate, tryAmount);
            tryAmount = 0.01m; // Minimum değer
        }
        
        var tryParameters = new
        {
            CurrAccBookID = currAccBookId,
            CurrencyCode = "TRY",
            ExchangeRate = paymentRow.ExchangeRate, // TRY kaydı için gerçek kur değeri
            RelationCurrencyCode = currencyCode, // İşlem para birimi (USD, EUR, GBP vb.)
            Debit = isDebit ? tryAmount : 0m, // Güvenli TL karşılığı
            Credit = isDebit ? 0m : tryAmount,
            CreatedUserName = "UZK  Uzak",
            LastUpdatedUserName = "UZK  Uzak"
        };
        
        await connection.ExecuteAsync(sql, tryParameters, transaction);
        _logger.LogInformation("TRY karşılık kaydı oluşturuldu. Tutar: {Amount}, Kur: {ExchangeRate}", 
            isDebit ? paymentRow.Amount * paymentRow.ExchangeRate : 0m, paymentRow.ExchangeRate);
    }
}

        private async Task InsertDebitAttributeAsync(SqlConnection connection, SqlTransaction transaction, Guid debitHeaderId, CreditPaymentRequest request)
{
    if (string.IsNullOrEmpty(request.InvoiceHeaderID))
    {
        _logger.LogWarning("Fatura başlık ID'si boş olduğu için öznitelikler kopyalanamıyor.");
        return;
    }

    _logger.LogInformation("InsertDebitAttributeAsync çağrıldı. DebitHeaderId: {DebitHeaderId}, InvoiceHeaderID: {InvoiceHeaderID}", 
        debitHeaderId, request.InvoiceHeaderID);

            var sql = @"
            INSERT INTO tpDebitATAttribute (DebitHeaderID, tpDebitATAttribute.AttributeTypeCode, tpDebitATAttribute.AttributeCode, 
            tpDebitATAttribute.CreatedUserName, tpDebitATAttribute.CreatedDate, tpDebitATAttribute.LastUpdatedUserName, tpDebitATAttribute.LastUpdatedDate)
            SELECT @DebitHeaderID, tpInvoiceATAttribute.AttributeTypeCode, tpInvoiceATAttribute.AttributeCode, @UserName, GETDATE(), @UserName, GETDATE()
            FROM tpInvoiceATAttribute WITH(NOLOCK)
            WHERE InvoiceHeaderID = @InvoiceHeaderID";

            var parameters = new
            {
                DebitHeaderID = debitHeaderId,
                InvoiceHeaderID = Guid.Parse(request.InvoiceHeaderID),
                UserName = "UZK  Uzak"
            };

    var affectedRows = await connection.ExecuteAsync(sql, parameters, transaction);
    _logger.LogInformation("{AffectedRows} adet öznitelik faturadan borç kaydına kopyalandı.", affectedRows);
}

        private async Task MarkInvoiceAsCompletedAsync(string invoiceHeaderId)
        {
            _logger.LogInformation("MarkInvoiceAsCompletedAsync çağrıldı. InvoiceHeaderId: {InvoiceHeaderId}", invoiceHeaderId);
            
            using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
            await connection.OpenAsync();

            var sql = @"
            UPDATE trInvoiceHeader
            SET IsCompleted = 1
            WHERE InvoiceHeaderID = @InvoiceHeaderID";

            var parameters = new
            {
                InvoiceHeaderID = Guid.Parse(invoiceHeaderId)
            };

            await connection.ExecuteAsync(sql, parameters);
            _logger.LogInformation("Fatura tamamlandı olarak işaretlendi.");
        }
    }
}
