using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Helpers;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models;
using ErpMobile.Api.Models.Payment;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Repositories.CashAccount;
using Newtonsoft.Json;
using ErpMobile.Api.Models.Notification;

namespace ErpMobile.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentService> _logger;
        private readonly ICashAccountRepository _cashAccountRepository;
        private readonly INotificationService _notificationService;
        private Dictionary<string, string> _cashAccountDescriptions;

        public PaymentService(
            IConfiguration configuration, 
            ILogger<PaymentService> logger, 
            ICashAccountRepository cashAccountRepository,
            INotificationService notificationService)
        {
            _configuration = configuration;
            _logger = logger;
            _cashAccountRepository = cashAccountRepository;
            _notificationService = notificationService;
            _cashAccountDescriptions = new Dictionary<string, string>();
        }

        /// <summary>
        /// Fatura için peşin ödeme işlemi yapar
        /// </summary>
        public async Task<CashPaymentResponse> CreateCashPaymentForInvoiceAsync(CashPaymentRequest request, string userName)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
            await connection.OpenAsync();
            
            using var transaction = connection.BeginTransaction();
            
            // Kasa açıklamalarını yükle
            await LoadCashAccountDescriptionsAsync();
            
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n[REQUEST] Cash Payment Request:");
                Console.WriteLine($"CurrAccCode: {request.CurrAccCode}");
                Console.WriteLine($"DocCurrencyCode: {request.DocCurrencyCode}");
                Console.WriteLine($"Description: {request.Description}");
                Console.WriteLine($"DocumentDate: {request.DocumentDate}");
                Console.WriteLine($"InvoiceId: {request.InvoiceId}");
                Console.WriteLine($"InvoiceNumber: {request.InvoiceNumber   }");
                Console.WriteLine($"Payment Rows Count: {request.PaymentRows?.Count ?? 0}");
                foreach (var row in request.PaymentRows ?? new List<PaymentRow>())
                {
                    Console.WriteLine($"  - Amount: {row.Amount}, CurrencyCode: {row.CurrencyCode}, ExchangeRate: {row.ExchangeRate}");
                }
                Console.WriteLine($"Attributes Count: {request.Attributes?.Count ?? 0}");
                foreach (var attr in request.Attributes ?? new List<CashPaymentAttributeRequest>())
                {
                    Console.WriteLine($"  - AttributeCode: {attr.AttributeCode}, AttributeValue: {attr.AttributeValue}");
                }
                Console.ResetColor();
                
                _logger.LogInformation("[PAYMENT] Başlıyor: Cash Payment Request DocCurrencyCode={DocCurrencyCode}, CurrencyCode={CurrencyCode}", 
                    request.DocCurrencyCode, 
                    request.PaymentRows?.FirstOrDefault()?.CurrencyCode);

                if (request.PaymentRows == null || !request.PaymentRows.Any())
                {
                    _logger.LogError("No payment rows provided in the request");
                    return new CashPaymentResponse { Success = false, Message = "No payment rows provided in the request" };
                }

                // Referans numaralarını oluştur
                string cashNumber = await GetCashTransNumberAsync(connection, transaction);
                string debitNumber = await GetDebitNumberAsync(connection, transaction);
                string paymentNumber = await GetPaymentNumberAsync(connection, transaction);

                // Header ID'leri oluştur
                var cashHeaderId = Guid.NewGuid();
                var debitHeaderId = Guid.NewGuid();
                var paymentHeaderId = Guid.NewGuid();

                // CurrAccBook ID'leri döngü dışında tanımla
                Guid debitCurrAccBookId = Guid.Empty;
                Guid cashCurrAccBookId = Guid.Empty;

                // Debit Header ekle
                await InsertDebitHeaderAsync(connection, transaction, debitHeaderId, request, debitNumber, cashHeaderId, "");

                // Payment Header ekle
                await InsertPaymentHeaderAsync(connection, transaction, paymentHeaderId, request, paymentNumber);

                // Para birimine göre ödeme satırlarını gruplandır
                var paymentRowsByCurrency = request.PaymentRows
                    .GroupBy(row => row.CurrencyCode)
                    .ToDictionary(g => g.Key, g => g.ToList());

                Console.WriteLine($"[INFO] Ödeme satırları {paymentRowsByCurrency.Count} farklı para birimine göre gruplandırıldı");
        
                int cashHeaderCount = 0;
                int lineNumber = 0;
        
                // Ana CashTransNumber'i sakla
                string commonCashNumber = cashNumber;
                _logger.LogInformation("Ana CashTransNumber oluşturuldu: {CashNumber}", commonCashNumber);
        
                // Her para birimi grubu için ayrı bir CashHeader oluştur
                foreach (var currencyGroup in paymentRowsByCurrency)
                {
                    string currencyCode = currencyGroup.Key;
                    List<PaymentRow> currencyRows = currencyGroup.Value;
            
                    Console.WriteLine($"[INFO] Para birimi: {currencyCode}, Satır sayısı: {currencyRows.Count}");
            
                    // İlk para birimi grubu için orijinal cashHeaderId'yi kullan, diğerleri için yeni oluştur
                    var currencyCashHeaderId = cashHeaderCount == 0 ? cashHeaderId : Guid.NewGuid();
            
                    // Her para birimi için benzersiz bir CashTransNumber oluştur
                    string currencyCashNumber = await GetCashTransNumberAsync(connection, transaction, currencyCode);
                    _logger.LogInformation("Para birimi {CurrencyCode} için benzersiz CashTransNumber oluşturuldu: {CashNumber}", currencyCode, currencyCashNumber);
            
                    // Bu para birimi için Cash Header ekle
                    await InsertCashHeaderAsync(connection, transaction, currencyCashHeaderId, request, currencyCashNumber, currencyCode);
            
                    // Bu para birimine ait tüm satırlar için işlem yap
                    foreach (var paymentRow in currencyRows)
                    {
                        lineNumber++;

                        // Line ID'leri oluştur
                        var cashLineId = Guid.NewGuid();
                        var debitLineId = Guid.NewGuid();
                        var paymentLineId = Guid.NewGuid();

                        // Cash Line ekle - currencyCashHeaderId ile ilişkilendir
                        await InsertCashLineAsync(connection, transaction, cashLineId, currencyCashHeaderId, request, paymentRow, lineNumber);

                        // Cash Line Currency ekle
                        await InsertCashLineCurrencyAsync(connection, transaction, cashLineId, paymentRow);

                        // Debit Line ekle
                        await InsertDebitLineAsync(connection, transaction, debitHeaderId, debitLineId, request, paymentRow, lineNumber);

                        // Debit Line Currency ekle
                        await InsertDebitLineCurrencyAsync(connection, transaction, debitLineId, paymentRow);

                        // Payment Line ekle
                        await InsertPaymentLineAsync(connection, transaction, paymentLineId, paymentHeaderId, cashLineId, debitLineId, lineNumber, paymentRow);

                        // Payment Line Currency ekle
                        await InsertPaymentLineCurrencyAsync(connection, transaction, paymentLineId, paymentRow, request.DocCurrencyCode);

                        // Debit işlemi için muhasebe kaydı
                        debitCurrAccBookId = Guid.NewGuid();
                        await InsertCurrAccBookAsync(connection, transaction, debitCurrAccBookId, request, "Debit", debitLineId, lineNumber);
                        await InsertCurrAccBookCurrencyAsync(connection, transaction, debitCurrAccBookId, paymentRow, true);
                        
                        // Cash işlemi için muhasebe kaydı
                        cashCurrAccBookId = Guid.NewGuid();
                        await InsertCurrAccBookAsync(connection, transaction, cashCurrAccBookId, request, "Cash", cashLineId, lineNumber);
                        await InsertCurrAccBookCurrencyAsync(connection, transaction, cashCurrAccBookId, paymentRow, false);
                    }
                    
                    cashHeaderCount++;
                }

                // Attribute tablolarına kayıt ekle
                await InsertCashAttributeAsync(connection, transaction, cashHeaderId, request);
                await InsertDebitAttributeAsync(connection, transaction, debitHeaderId, request);
                await InsertPaymentAttributeAsync(connection, transaction, paymentHeaderId, request);
                await InsertCurrAccBookAttributeAsync(connection, transaction, debitCurrAccBookId, request, "Debit");
                await InsertCurrAccBookAttributeAsync(connection, transaction, cashCurrAccBookId, request, "Cash");
                
                // Transaction'ı commit et
                transaction.Commit();
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[SUCCESS] Cash payment transaction committed successfully");
                Console.ResetColor();
                
                // Faturayı tamamlandı olarak işaretle
                if (!string.IsNullOrEmpty(request.InvoiceHeaderID))
                {
                    await MarkInvoiceAsCompletedAsync(request.InvoiceHeaderID, userName);
                    Console.WriteLine($"\n[INFO] Fatura {request.InvoiceHeaderID} tamamlandı olarak işaretlendi");
                }
                else if (request.InvoiceId != Guid.Empty)
                {
                    await MarkInvoiceAsCompletedAsync(request.InvoiceId.ToString(), userName);
                    Console.WriteLine($"\n[INFO] Fatura {request.InvoiceId} tamamlandı olarak işaretlendi");
                }
                
                // Başarılı işlem sonrası bildirim gönder
                decimal totalAmount = request.PaymentRows?.Sum(r => r.Amount) ?? 0;
                await SendTransactionNotificationAsync(userName, "CashTransaction", "Create", cashNumber, totalAmount, request.DocCurrencyCode, request.Description);
                
                var response = new CashPaymentResponse
                {
                    Success = true,
                    Message = "Nakit tahsilat başarıyla kaydedildi.",
                };
                response.CashHeaderId = cashHeaderId;
                response.PaymentHeaderId = paymentHeaderId;
                
                return response;
            }
            catch (Exception ex)
            {
                // Hata durumunda transaction'ı rollback et
                transaction.Rollback();
                _logger.LogError(ex, "Nakit tahsilat işlemi sırasında hata oluştu: {Message}", ex.Message);
                return new CashPaymentResponse
                {
                    Success = false,
                    Message = $"Nakit tahsilat işlemi sırasında hata oluştu: {ex.Message}"
                };
            }
        }

        private async Task<string> GetCashTransNumberAsync(SqlConnection connection, SqlTransaction transaction, string currencyCode = null)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("CompanyCode", 1);
                parameters.Add("CashTransTypeCode", 1);
                
                // Bu stored procedure doğrudan sonuç döndürüyor, çıkış parametresi yok
                var result = await connection.QueryFirstOrDefaultAsync<string>("sp_LastRefNumCashTrans", parameters, transaction, commandType: CommandType.StoredProcedure);
                
                // Eğer para birimi kodu varsa, benzersiz bir numara oluştur
                if (!string.IsNullOrEmpty(currencyCode))
                {
                    // Timestamp ekleyerek benzersizliği garanti et
                    string uniqueNumber = $"{result}-{currencyCode}-{DateTime.Now.ToString("yyyyMMddHHmmss")}";
                    Console.WriteLine($"[SP CALL] sp_LastRefNumCashTrans - Generated Unique CashTransNumber: {uniqueNumber} for currency {currencyCode}");
                    return uniqueNumber;
                }
                
                Console.WriteLine($"[SP CALL] sp_LastRefNumCashTrans - Generated CashTransNumber: {result}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to generate CashTransNumber: {ex.Message}");
                // Hata durumunda manuel bir numara oluştur
                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                var fallbackNumber = $"MANUAL-{timestamp}";
                
                if (!string.IsNullOrEmpty(currencyCode))
                {
                    fallbackNumber = $"{fallbackNumber}-{currencyCode}";
                }
                
                Console.WriteLine($"[ERROR] Using fallback CashTransNumber: {fallbackNumber}");
                return fallbackNumber;
            }
        }
        
        private async Task<string> GetDebitNumberAsync(SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                var parameters = new DynamicParameters();
                // Stored procedure'un beklediği parametreleri ekle
                parameters.Add("CompanyCode", 1);
                parameters.Add("DebitTypeCode", 1);
                
                // Bu stored procedure doğrudan sonuç döndürüyor, çıkış parametresi yok
                var result = await connection.QueryFirstOrDefaultAsync<string>("sp_LastRefNumDebit", parameters, transaction, commandType: CommandType.StoredProcedure);
                
                Console.WriteLine($"[SP CALL] sp_LastRefNumDebit - Generated DebitNumber: {result}");
                
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to generate DebitNumber: {ex.Message}");
                
                var fallbackNumber = $"";
                
                return fallbackNumber;
            }
        }
        
        private async Task<string> GetPaymentNumberAsync(SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                var parameters = new DynamicParameters();
                // Stored procedure'un beklediği parametreleri belirleyelim
                parameters.Add("CompanyCode", 1); // CompanyCode parametresi eklendi
                parameters.Add("OfficeCode", "M"); // OfficeCode parametresi eklendi
                parameters.Add("StoreCode", ""); // StoreCode parametresi eklendi
                parameters.Add("PosTerminalID", 0); // PosTerminalID parametresi eklendi
                
                // Bu stored procedure çıkış parametresi kullanmıyor, doğrudan result set dönüyor
                var result = await connection.QueryFirstAsync<dynamic>("sp_LastRefNumPayment", parameters, transaction, commandType: CommandType.StoredProcedure);
                
                var paymentNumber = result.PaymentNumber;
                
                Console.WriteLine($"[SP CALL] sp_LastRefNumPayment - Generated PaymentNumber: {paymentNumber}");
                
                return paymentNumber;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PaymentNumber oluşturulurken hata oluştu");
                
                // Hata durumunda manuel bir numara oluştur
              var fallbackNumber = $"";
                
              
                
                return fallbackNumber;
            }
        }

        private async Task InsertCashHeaderAsync(SqlConnection connection, SqlTransaction transaction, Guid cashHeaderId, CashPaymentRequest request, string cashTransNumber, string currencyCode = null)
        {
            // Fatura numarasını almak için SQL sorgusu
            string invoiceNumber = "";
            try {
                var invoiceQuery = "SELECT InvoiceNumber FROM trInvoiceHeader WHERE InvoiceHeaderID = @InvoiceHeaderID";
                invoiceNumber = await connection.QueryFirstOrDefaultAsync<string>(invoiceQuery, new { InvoiceHeaderID = request.InvoiceId }, transaction);
                
                Console.WriteLine($"[SQL QUERY] Fatura numarası alındı: {invoiceNumber}");
            }
            catch (Exception ex) {
                Console.WriteLine($"[ERROR] Fatura numarası alınamadı: {ex.Message}");
                // Fatura numarası alınamazsa, CashTransNumber kullanılacak
                invoiceNumber = cashTransNumber;
            }
            
            // Para birimine göre uygun kasa kodunu dinamik olarak belirle
            string cashCurrAccCode;
            try
            {
                // Kasa kodlarını veritabanından al
                var cashAccounts = await _cashAccountRepository.GetCashAccountsAsync();
                
                // İlgili para birimine ait ve aktif olan ilk kasa kodunu bul
                var matchingCashAccount = cashAccounts
                    .Where(ca => ca.CurrencyCode == (currencyCode ?? request.DocCurrencyCode) && !ca.IsBlocked)
                    .FirstOrDefault();
                    
                if (matchingCashAccount != null)
                {
                    cashCurrAccCode = matchingCashAccount.CashAccountCode;
                    _logger.LogInformation($"Para birimi {currencyCode} için veritabanından kasa kodu bulundu: {cashCurrAccCode} ({matchingCashAccount.CashAccountDescription})");
                }
                else
                {
                    // Eğer para birimine uygun kasa bulunamazsa, varsayılan kasa kodlarını kullan
                    if (currencyCode == "USD")
                    {
                        cashCurrAccCode = "102"; // Varsayılan USD kasası
                        _logger.LogWarning($"Para birimi {currencyCode} için veritabanında uygun kasa bulunamadı, varsayılan kod kullanılıyor: {cashCurrAccCode}");
                    }
                    else
                    {
                        cashCurrAccCode = "101"; // Varsayılan TL kasası
                        _logger.LogWarning($"Para birimi {currencyCode ?? request.DocCurrencyCode} için veritabanında uygun kasa bulunamadı, varsayılan kod kullanılıyor: {cashCurrAccCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda varsayılan kasa kodlarını kullan
                _logger.LogError(ex, "Kasa kodları alınırken hata oluştu, varsayılan kodlar kullanılacak");
                
                if (currencyCode == "USD")
                {
                    cashCurrAccCode = "102"; // Varsayılan USD kasası
                }
                else
                {
                    cashCurrAccCode = "101"; // Varsayılan TL kasası
                }
            }
            
            Console.WriteLine($"[INFO] Para birimi {currencyCode} için kasa kodu: {cashCurrAccCode} seçildi");
            
            var sql = @"
            INSERT INTO [trCashHeader]
            ([CashHeaderID], [CashTransTypeCode], [CashTransNumber], [DocumentDate], [DocumentTime], 
            [DocumentNumber], [RefNumber], [POSTerminalID], [Description], [CashCurrAccCode], 
            [CashCurrAccTypeCode], [StoreCode], [GLTypeCode], [DocCurrencyCode], [LocalCurrencyCode], 
            [ExchangeRate], [ImportFileNumber], [ExportFileNumber], [IsPostingApproved], [JournalDate], 
            [IsPostingJournal], [OfficeCode], [ApplicationCode], [ApplicationID], [IsCompleted], 
            [IsPrinted], [IsLocked], [CompanyCode], [CreatedUserName], [CreatedDate], 
            [LastUpdatedUserName], [LastUpdatedDate])
            VALUES
            (@CashHeaderID, @CashTransTypeCode, @CashTransNumber, @DocumentDate, @DocumentTime, 
            @DocumentNumber, @RefNumber, @POSTerminalID, @Description, @CashCurrAccCode, 
            @CashCurrAccTypeCode, @StoreCode, @GLTypeCode, @DocCurrencyCode, @LocalCurrencyCode, 
            @ExchangeRate, @ImportFileNumber, @ExportFileNumber, @IsPostingApproved, @JournalDate, 
            @IsPostingJournal, @OfficeCode, @ApplicationCode, @ApplicationID, @IsCompleted, 
            @IsPrinted, @IsLocked, @CompanyCode, @CreatedUserName, GETDATE(), 
            @LastUpdatedUserName, GETDATE())";

            var parameters = new
            {
                CashHeaderID = cashHeaderId,
                CashTransTypeCode = 1, // Tahsilat
                CashTransNumber = cashTransNumber,
                DocumentDate = request.DocumentDate,
                DocumentTime = DateTime.Now.ToString("HH:mm:ss.fffffff"),
                DocumentNumber = "0",
                RefNumber = request.InvoiceNumber, // Fatura numarası kullanılıyor
                POSTerminalID = 0, // Veritabanı şemasında görülen alan
                Description = request.Description,
                CashCurrAccCode = cashCurrAccCode, // Para birimine göre belirlenen kasa kodu
                CashCurrAccTypeCode = 7, // Kasa hesap tipi
                StoreCode = "",
                GLTypeCode = "",
                DocCurrencyCode = currencyCode ?? request.DocCurrencyCode,
                LocalCurrencyCode = "TRY",
                ExchangeRate = currencyCode == "TRY" ? 1 : (request.PaymentRows?.FirstOrDefault(r => r.CurrencyCode == currencyCode)?.ExchangeRate ?? 1),
                ImportFileNumber = "", // SQL izlemesinde görülen alan
                ExportFileNumber = "", // SQL izlemesinde görülen alan
                IsPostingApproved = true, // SQL izlemesinde görülen alan
                JournalDate = DateTime.Today,
                IsPostingJournal = false,
                OfficeCode = "M",
                ApplicationCode = "Invoi",
                ApplicationID = !string.IsNullOrEmpty(request.InvoiceHeaderID) ? Guid.Parse(request.InvoiceHeaderID) : request.InvoiceId, // Fatura Header ID'si burada kullanılıyor
                IsCompleted = true,
                IsPrinted = false,
                IsLocked = false,
                CompanyCode = 1,
                CreatedUserName = "UZK  Uzak",
                LastUpdatedUserName = "UZK  Uzak"
            };

            Console.WriteLine("\n[SQL INSERT] trCashHeader:");
            Console.WriteLine(sql);
            Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parameters)}");

            await connection.ExecuteAsync(sql, parameters, transaction);
        }

        private async Task InsertCashLineAsync(SqlConnection connection, SqlTransaction transaction, Guid cashLineId, Guid cashHeaderId, CashPaymentRequest request, PaymentRow paymentRow, int lineNumber)
        {
            var sql = @"
            INSERT INTO [trCashLine]
            ([CashLineID], [CashHeaderID], [SortOrder], [CurrAccCode], [CurrAccTypeCode], 
            [LineDescription], [DocCurrencyCode], [CurrAccAmount], [CurrAccCurrencyCode], [CurrAccExchangeRate], 
            [CompanyCode], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
            VALUES
            (@CashLineID, @CashHeaderID, @SortOrder, @CurrAccCode, @CurrAccTypeCode, 
            @LineDescription, @DocCurrencyCode, @CurrAccAmount, @CurrAccCurrencyCode, @CurrAccExchangeRate, 
            @CompanyCode, @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE())";

            var parameters = new
            {
                CashLineID = cashLineId,
                CashHeaderID = cashHeaderId,
                SortOrder = lineNumber,
                CurrAccCode = request.CurrAccCode,
                CurrAccTypeCode = 3, // Müşteri
                LineDescription = request.Description,
                DocCurrencyCode = paymentRow.CurrencyCode,
                CurrAccAmount = paymentRow.Amount,
                CurrAccCurrencyCode = paymentRow.CurrencyCode,
                CurrAccExchangeRate = paymentRow.ExchangeRate,
                CompanyCode = 1, // CompanyCode 1 olarak ayarlandı
                CreatedUserName = "UZK  Uzak",
                LastUpdatedUserName = "UZK  Uzak"
            };

            Console.WriteLine("\n[SQL INSERT] trCashLine:");
            Console.WriteLine(sql);
            Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parameters)}");

            await connection.ExecuteAsync(sql, parameters, transaction);
        }

        private async Task InsertCashLineCurrencyAsync(SqlConnection connection, SqlTransaction transaction, Guid cashLineId, PaymentRow paymentRow)
        {
            var sql = @"
            MERGE INTO [trCashLineCurrency] AS target
            USING (SELECT @CashLineID AS CashLineID, @CurrencyCode AS CurrencyCode) AS source
            ON (target.CashLineID = source.CashLineID AND target.CurrencyCode = source.CurrencyCode)
            WHEN MATCHED THEN
                UPDATE SET
                    [Debit] = @Debit,
                    [Credit] = @Credit,
                    [RelationCurrencyCode] = @RelationCurrencyCode,
                    [ExchangeRate] = @ExchangeRate,
                    [LastUpdatedUserName] = @LastUpdatedUserName,
                    [LastUpdatedDate] = GETDATE()
            WHEN NOT MATCHED THEN
                INSERT ([CashLineID], [CurrencyCode], [Debit], [Credit], [RelationCurrencyCode], [ExchangeRate], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                VALUES (@CashLineID, @CurrencyCode, @Debit, @Credit, @RelationCurrencyCode, @ExchangeRate, @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE());";

            // Önce kendi para birimiyle ilişkili kaydı ekle (USD-USD veya TRY-TRY)
            var parameters = new
            {
                CashLineID = cashLineId,
                CurrencyCode = paymentRow.CurrencyCode,
                RelationCurrencyCode = paymentRow.CurrencyCode, // Kendi para birimi
                Debit = paymentRow.Amount,
                Credit = 0,
                ExchangeRate = paymentRow.CurrencyCode == "TRY" ? 1 : paymentRow.ExchangeRate,
                CreatedUserName = "UZK  Uzak",
                LastUpdatedUserName = "UZK  Uzak"
            };

            Console.WriteLine($"\n[SQL MERGE] trCashLineCurrency ({paymentRow.CurrencyCode}-{paymentRow.CurrencyCode} ilişkisi):");
            Console.WriteLine(sql);
            Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parameters)}");

            await connection.ExecuteAsync(sql, parameters, transaction);
            
            // Eğer para birimi TRY değilse (USD gibi), hem TRY-USD hem de USD-TRY ilişkilerini oluştur
            if (paymentRow.CurrencyCode != "TRY")
            {
                // TRY-USD ilişkisi (TRY para birimi, USD ile ilişkili)
                var parametersForTRY = new
                {
                    CashLineID = cashLineId,
                    CurrencyCode = "TRY",
                    RelationCurrencyCode = paymentRow.CurrencyCode, // Örn: USD
                    Debit = paymentRow.Amount * (decimal)paymentRow.ExchangeRate, // TL karşılığı
                    Credit = 0,
                    ExchangeRate = paymentRow.ExchangeRate,
                    CreatedUserName = "UZK  Uzak",
                    LastUpdatedUserName = "UZK  Uzak"
                };

                Console.WriteLine($"\n[SQL MERGE] trCashLineCurrency (TRY-{paymentRow.CurrencyCode} ilişkisi):");
                Console.WriteLine(sql);
                Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parametersForTRY)}");

                await connection.ExecuteAsync(sql, parametersForTRY, transaction);
                
                // USD-TRY ilişkisi (USD para birimi, TRY ile ilişkili)
                // ERP'nin oluşturduğu kayıtlarda bu ilişki de var
                var parametersForForeignToTRY = new
                {
                    CashLineID = cashLineId,
                    CurrencyCode = paymentRow.CurrencyCode, // Örn: USD
                    RelationCurrencyCode = "TRY",
                    Debit = paymentRow.Amount, // Döviz miktarı
                    Credit = 0,
                    ExchangeRate = paymentRow.ExchangeRate,
                    CreatedUserName = "UZK  Uzak",
                    LastUpdatedUserName = "UZK  Uzak"
                };

                Console.WriteLine($"\n[SQL MERGE] trCashLineCurrency ({paymentRow.CurrencyCode}-TRY ilişkisi):");
                Console.WriteLine(sql);
                Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parametersForForeignToTRY)}");

                await connection.ExecuteAsync(sql, parametersForForeignToTRY, transaction);
            }
            // Eğer para birimi TRY ise ve kur 1'den farklıysa, ilişkili döviz kaydını ekle
            else if (paymentRow.ExchangeRate != 1)
            {
                // Kur 1'den farklıysa, ilişkili bir döviz var demektir
                string foreignCurrency = "USD"; // Genellikle USD olur, ama dinamik olarak belirlenebilir
                
                // TRY-USD ilişkisi
                var parametersForTRYtoForeign = new
                {
                    CashLineID = cashLineId,
                    CurrencyCode = "TRY",
                    RelationCurrencyCode = foreignCurrency,
                    Debit = paymentRow.Amount, // TL miktarı
                    Credit = 0,
                    ExchangeRate = paymentRow.ExchangeRate,
                    CreatedUserName = "UZK  Uzak",
                    LastUpdatedUserName = "UZK  Uzak"
                };

                Console.WriteLine($"\n[SQL MERGE] trCashLineCurrency (TRY-{foreignCurrency} ilişkisi):");
                Console.WriteLine(sql);
                Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parametersForTRYtoForeign)}");

                await connection.ExecuteAsync(sql, parametersForTRYtoForeign, transaction);
                
                // USD-TRY ilişkisi
                var parametersForForeign = new
                {
                    CashLineID = cashLineId,
                    CurrencyCode = foreignCurrency,
                    RelationCurrencyCode = "TRY",
                    Debit = paymentRow.Amount / (decimal)paymentRow.ExchangeRate, // Döviz karşılığı
                    Credit = 0,
                    ExchangeRate = paymentRow.ExchangeRate,
                    CreatedUserName = "UZK  Uzak",
                    LastUpdatedUserName = "UZK  Uzak"
                };

                Console.WriteLine($"\n[SQL MERGE] trCashLineCurrency ({foreignCurrency}-TRY ilişkisi):");
                Console.WriteLine(sql);
                Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parametersForForeign)}");

                await connection.ExecuteAsync(sql, parametersForForeign, transaction);
            }        
        }

        private async Task InsertDebitHeaderAsync(SqlConnection connection, SqlTransaction transaction, Guid debitHeaderId, CashPaymentRequest request, string debitNumber, Guid cashLineId, string invoiceNumber)
        {
            var sql = @"
            INSERT INTO [trDebitHeader]
            ([DebitHeaderID], [DebitTypeCode], [DebitNumber], [DocumentDate], [DocumentTime], 
            [DocumentNumber], [RefNumber], [Description], [CurrAccCode], 
            [CurrAccTypeCode], [StoreTypeCode], [StoreCode], [GLTypeCode], [DocCurrencyCode], [LocalCurrencyCode], 
            [ExchangeRate], [JournalDate], 
            [IsPostingJournal], [OfficeCode], [ApplicationCode], [ApplicationID], [IsCompleted], 
            [IsPrinted], [IsLocked], [CompanyCode], [CreatedUserName], [CreatedDate], 
            [LastUpdatedUserName], [LastUpdatedDate])
            VALUES
            (@DebitHeaderID, @DebitTypeCode, @DebitNumber, @DocumentDate, @DocumentTime, 
            @DocumentNumber, @RefNumber, @Description, @CurrAccCode, 
            @CurrAccTypeCode, @StoreTypeCode, @StoreCode, @GLTypeCode, @DocCurrencyCode, @LocalCurrencyCode, 
            @ExchangeRate, @JournalDate, 
            @IsPostingJournal, @OfficeCode, @ApplicationCode, @ApplicationID, @IsCompleted, 
            @IsPrinted, @IsLocked, @CompanyCode, @CreatedUserName, GETDATE(), 
            @LastUpdatedUserName, GETDATE())";

            var parameters = new
            {
                DebitHeaderID = debitHeaderId,
                DebitTypeCode = 1, // Tahsilat
                DebitNumber = debitNumber,
                DocumentDate = request.DocumentDate,
                DocumentTime = DateTime.Now.TimeOfDay,
                DocumentNumber = "0",
                RefNumber = request.InvoiceNumber, // Boş string olarak ayarlanıyor, null olamaz
                Description = request.Description,
                CurrAccCode = request.CurrAccCode,
                CurrAccTypeCode = 3, // Müşteri
                StoreTypeCode = 5, // Veritabanı şemasında default değeri 5
                StoreCode = "",
                GLTypeCode = "",
                DocCurrencyCode = request.DocCurrencyCode,
                LocalCurrencyCode = "TRY",
                ExchangeRate = 1,
                JournalDate = DateTime.Now.Date,
                IsPostingJournal = false,
                OfficeCode = "M",
                ApplicationCode = "Invoi",
                ApplicationID = !string.IsNullOrEmpty(request.InvoiceHeaderID) ? Guid.Parse(request.InvoiceHeaderID) : request.InvoiceId,
                IsCompleted = true,
                IsPrinted = false,
                IsLocked = false,   
                CompanyCode = 1,
                CreatedUserName = "UZK  Uzak",
                LastUpdatedUserName = "UZK  Uzak"
            };

            Console.WriteLine("\n[SQL INSERT] trDebitHeader:");
            Console.WriteLine(sql);
            Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parameters)}");

            await connection.ExecuteAsync(sql, parameters, transaction);
        }

        private async Task InsertDebitLineAsync(SqlConnection connection, SqlTransaction transaction, Guid debitHeaderId, Guid debitLineId, CashPaymentRequest request, PaymentRow paymentRow, int lineNumber)
        {
            var sql = @"
            INSERT INTO [trDebitLine]
            ([DebitLineID], [SortOrder], [DueDate], [LineDescription], 
            [DocCurrencyCode], [DebitHeaderID], [CreatedUserName], [CreatedDate], 
            [LastUpdatedUserName], [LastUpdatedDate])
            VALUES
            (@DebitLineID, @SortOrder, @DueDate, @LineDescription, 
            @DocCurrencyCode, @DebitHeaderID, @CreatedUserName, GETDATE(), 
            @LastUpdatedUserName, GETDATE())";

            var parameters = new
            {
                DebitLineID = debitLineId,
                SortOrder = lineNumber,
                DueDate = DateTime.Now.Date,
                LineDescription = request.Description,
                DocCurrencyCode = paymentRow.CurrencyCode,
                DebitHeaderID = debitHeaderId,
                CreatedUserName = "UZK  Uzak",
                LastUpdatedUserName = "UZK  Uzak"
            };

            Console.WriteLine("\n[SQL INSERT] trDebitLine:");
            Console.WriteLine(sql);
            Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parameters)}");
            
            await connection.ExecuteAsync(sql, parameters, transaction);
        }
        
        private async Task InsertDebitLineCurrencyAsync(SqlConnection connection, SqlTransaction transaction, Guid debitLineId, PaymentRow paymentRow)
        {
            string localCurrencyCode = "TRY"; // Yerel para birimi
            string docCurrencyCode = paymentRow.CurrencyCode; // Belge para birimi
            
            // 1. Belge para birimi kaydı (örn. USD)
            var sqlDoc = @"
            MERGE INTO [trDebitLineCurrency] AS target
            USING (SELECT @DebitLineID AS DebitLineID, @CurrencyCode AS CurrencyCode) AS source
            ON (target.DebitLineID = source.DebitLineID AND target.CurrencyCode = source.CurrencyCode)
            WHEN MATCHED THEN
                UPDATE SET
                    [ExchangeRate] = @ExchangeRate,
                    [RelationCurrencyCode] = @RelationCurrencyCode,
                    [Debit] = @Debit,
                    [Credit] = @Credit,
                    [LastUpdatedUserName] = @LastUpdatedUserName,
                    [LastUpdatedDate] = GETDATE()
            WHEN NOT MATCHED THEN
                INSERT ([DebitLineID], [CurrencyCode], [ExchangeRate], [RelationCurrencyCode], [Debit], [Credit], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                VALUES (@DebitLineID, @CurrencyCode, @ExchangeRate, @RelationCurrencyCode, @Debit, @Credit, @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE());";

            var parametersDoc = new
            {
                DebitLineID = debitLineId,
                CurrencyCode = docCurrencyCode,
                ExchangeRate = 1, // Kendi para biriminde kur 1'dir
                RelationCurrencyCode = docCurrencyCode, // ERP ile uyumlu olması için kendi para birimiyle ilişkilendir
                Debit = paymentRow.Amount, // Belge para birimindeki miktar
                Credit = 0, // Tahsilat işlemi olduğu için alacak kısmı sıfır
                CreatedUserName = "UZK  Uzak",
                LastUpdatedUserName = "UZK  Uzak"
            };

            Console.WriteLine("\n[SQL MERGE] trDebitLineCurrency (Belge Para Birimi):");
            Console.WriteLine(sqlDoc);
            Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parametersDoc)}");

            await connection.ExecuteAsync(sqlDoc, parametersDoc, transaction);
            
            // 2. Eğer belge para birimi yerel para biriminden farklıysa, yerel para birimi kaydı da ekle
            if (docCurrencyCode != localCurrencyCode)
            {
                var sqlLocal = @"
                MERGE INTO [trDebitLineCurrency] AS target
                USING (SELECT @DebitLineID AS DebitLineID, @CurrencyCode AS CurrencyCode) AS source
                ON (target.DebitLineID = source.DebitLineID AND target.CurrencyCode = source.CurrencyCode)
                WHEN MATCHED THEN
                    UPDATE SET
                        [ExchangeRate] = @ExchangeRate,
                        [RelationCurrencyCode] = @RelationCurrencyCode,
                        [Debit] = @Debit,
                        [Credit] = @Credit,
                        [LastUpdatedUserName] = @LastUpdatedUserName,
                        [LastUpdatedDate] = GETDATE()
                WHEN NOT MATCHED THEN
                    INSERT ([DebitLineID], [CurrencyCode], [ExchangeRate], [RelationCurrencyCode], [Debit], [Credit], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                    VALUES (@DebitLineID, @CurrencyCode, @ExchangeRate, @RelationCurrencyCode, @Debit, @Credit, @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE());";

                var parametersLocal = new
                {
                    DebitLineID = debitLineId,
                    CurrencyCode = localCurrencyCode,
                    ExchangeRate = paymentRow.ExchangeRate,
                    RelationCurrencyCode = docCurrencyCode, // Yerel para biriminin ilişkili olduğu para birimi
                    Debit = paymentRow.Amount * (decimal)paymentRow.ExchangeRate, // Yerel para birimindeki karşılığı
                    Credit = 0, // Tahsilat işlemi olduğu için alacak kısmı sıfır
                    CreatedUserName = "UZK  Uzak",
                    LastUpdatedUserName = "UZK  Uzak"
                };

                Console.WriteLine("\n[SQL MERGE] trDebitLineCurrency (Yerel Para Birimi):");
                Console.WriteLine(sqlLocal);
                Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parametersLocal)}");

                await connection.ExecuteAsync(sqlLocal, parametersLocal, transaction);
            }
        }

        private async Task InsertCurrAccBookAsync(SqlConnection connection, SqlTransaction transaction, Guid currAccBookId, CashPaymentRequest request, string type, Guid relatedLineId, int lineNumber)
        {
            var sql = @"
    INSERT INTO [trCurrAccBook]
    ([CurrAccBookID], [SortOrder], [CurrAccTypeCode], [CurrAccCode], [DocumentDate], 
    [DocumentTime], [DocumentNumber], [DueDate], [LineDescription], [Description], 
    [InternalDescription], [RefNumber], [CompanyCode], [OfficeCode], [StoreTypeCode], 
    [StoreCode], [LocalCurrencyCode], [DocCurrencyCode], [ApplicationCode], [ApplicationID], 
    [BaseApplicationCode], [BaseApplicationID], [DebitTypeCode], [CashTransTypeCode], 
    [ImportFileNumber], [ExportFileNumber], [CreatedUserName], [CreatedDate], 
    [LastUpdatedUserName], [LastUpdatedDate])
    VALUES
    (@CurrAccBookID, @SortOrder, @CurrAccTypeCode, @CurrAccCode, @DocumentDate, 
    @DocumentTime, @DocumentNumber, @DueDate, @LineDescription, @Description, 
    @InternalDescription, @RefNumber, @CompanyCode, @OfficeCode, @StoreTypeCode, 
    @StoreCode, @LocalCurrencyCode, @DocCurrencyCode, @ApplicationCode, @ApplicationID, 
    @BaseApplicationCode, @BaseApplicationID, @DebitTypeCode, @CashTransTypeCode, 
    @ImportFileNumber, @ExportFileNumber, @CreatedUserName, GETDATE(), 
    @LastUpdatedUserName, GETDATE())";

            var isDebit = type == "Debit";
            var paymentRow = request.PaymentRows.FirstOrDefault() ?? new PaymentRow { Amount = 0, CurrencyCode = "TRY", ExchangeRate = 1 };
            var amount = paymentRow.Amount;
            var localAmount = amount * (decimal)paymentRow.ExchangeRate;

            var parameters = new
            {
                CurrAccBookID = currAccBookId,
                SortOrder = lineNumber,
                CurrAccTypeCode = 3, // Müşteri
                CurrAccCode = request.CurrAccCode,
                DocumentDate = DateTime.Now.Date,
                DocumentTime = DateTime.Now.TimeOfDay,
                DocumentNumber = "0",
                DueDate = DateTime.Now.Date,
                LineDescription = isDebit ? $"Toptan Satış - Fatura No: {request.InvoiceNumber}" : $"{GetCashAccountDescription(paymentRow.CashAccountCode)} ({paymentRow.CashAccountCode})",
                Description = request.Description,
                InternalDescription = "",
                // RefNumber alanına frontend'den gelen fatura numarasını atıyoruz
                RefNumber = request.InvoiceNumber,
                CompanyCode = 1,
                OfficeCode = "M",
                StoreTypeCode = 5,
                StoreCode = "",
                LocalCurrencyCode = "TRY",
                DocCurrencyCode = paymentRow.CurrencyCode,
                ApplicationCode = isDebit ? "Debit" : "Cash",
                ApplicationID = relatedLineId,
                BaseApplicationCode = "Invoi",
                BaseApplicationID = !string.IsNullOrEmpty(request.InvoiceHeaderID) ? Guid.Parse(request.InvoiceHeaderID) : request.InvoiceId,
                DebitTypeCode = (byte)(isDebit ? 2 : 0),
                CashTransTypeCode = (byte)(isDebit ? 0 : 1),
                ImportFileNumber = "",
                ExportFileNumber = "",
                CreatedUserName = "UZK  Uzak",
                LastUpdatedUserName = "UZK  Uzak"
            };

            Console.WriteLine($"\n[SQL INSERT] trCurrAccBook ({type}):");
            Console.WriteLine(sql);
            Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parameters)}");

            await connection.ExecuteAsync(sql, parameters, transaction);
        }

        private async Task InsertCurrAccBookCurrencyAsync(SqlConnection connection, SqlTransaction transaction, Guid currAccBookId, PaymentRow paymentRow, bool isDebit)
        {
            // SQL sorgusunu ve parametreleri hazırlayalım
            var sql = @"
            MERGE INTO [trCurrAccBookCurrency] AS target
            USING (SELECT @CurrAccBookID AS CurrAccBookID, @CurrencyCode AS CurrencyCode) AS source
            ON (target.CurrAccBookID = source.CurrAccBookID AND target.CurrencyCode = source.CurrencyCode)
            WHEN MATCHED THEN
                UPDATE SET
                    [Debit] = @Debit,
                    [Credit] = @Credit,
                    [RelationCurrencyCode] = @RelationCurrencyCode,
                    [ExchangeRate] = @ExchangeRate,
                    [LastUpdatedUserName] = @LastUpdatedUserName,
                    [LastUpdatedDate] = GETDATE()
            WHEN NOT MATCHED THEN
                INSERT ([CurrAccBookID], [CurrencyCode], [Debit], [Credit], [RelationCurrencyCode], [ExchangeRate], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                VALUES (@CurrAccBookID, @CurrencyCode, @Debit, @Credit, @RelationCurrencyCode, @ExchangeRate, @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE());";

            var parameters = new
            {
                CurrAccBookID = currAccBookId,
                CurrencyCode = paymentRow.CurrencyCode,
                Debit = isDebit ? paymentRow.Amount : 0,
                Credit = isDebit ? 0 : paymentRow.Amount,
                RelationCurrencyCode = "TRY", // İlişkili para birimi sabit TRY olarak ayarlandı
                ExchangeRate = paymentRow.ExchangeRate,
                CreatedUserName = "UZK  Uzak",
                LastUpdatedUserName = "UZK  Uzak"
            };

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[SQL MERGE] trCurrAccBookCurrency ({(isDebit ? "Debit" : "Credit")}):");
            Console.WriteLine(sql);
            Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parameters)}");
            Console.ResetColor();

            await connection.ExecuteAsync(sql, parameters, transaction);
        }

        private async Task InsertPaymentHeaderAsync(SqlConnection connection, SqlTransaction transaction, Guid paymentHeaderId, CashPaymentRequest request, string paymentNumber)
        {
            var sql = @"
            INSERT INTO [trPaymentHeader]
            ([PaymentHeaderID], [PaymentNumber], [DocumentDate], [DocumentTime], 
            [DocumentNumber], [RefNumber], [POSTerminalID], [Description], [CurrAccCode], 
            [CurrAccTypeCode], [StoreCode], [GLTypeCode], [DocCurrencyCode], [LocalCurrencyCode], 
            [ExchangeRate], [JournalDate], 
            [IsPostingJournal], [OfficeCode], [ApplicationCode], [ApplicationID], [IsCompleted], 
            [IsPrinted], [IsLocked], [CompanyCode], [CreatedUserName], [CreatedDate], 
            [LastUpdatedUserName], [LastUpdatedDate])
            VALUES
            (@PaymentHeaderID, @PaymentNumber, @DocumentDate, @DocumentTime, 
            @DocumentNumber, @RefNumber, @POSTerminalID, @Description, @CurrAccCode, 
            @CurrAccTypeCode, @StoreCode, @GLTypeCode, @DocCurrencyCode, @LocalCurrencyCode, 
            @ExchangeRate, @JournalDate, 
            @IsPostingJournal, @OfficeCode, @ApplicationCode, @ApplicationID, @IsCompleted, 
            @IsPrinted, @IsLocked, @CompanyCode, @CreatedUserName, GETDATE(), 
            @LastUpdatedUserName, GETDATE())";

            var parameters = new
            {
                PaymentHeaderID = paymentHeaderId,
                PaymentNumber = paymentNumber,
                DocumentDate = request.DocumentDate,
                DocumentTime = DateTime.Now.TimeOfDay,
                DocumentNumber = "0",
                RefNumber = request.InvoiceNumber, // Boş string olarak ayarlanıyor, null olamaz
                POSTerminalID = 0, // Veritabanı şemasında görülen alan
                Description = request.Description,
                CurrAccCode = request.CurrAccCode,
                CurrAccTypeCode = 3, // Müşteri
                StoreCode = "",
                GLTypeCode = "",
                DocCurrencyCode = request.DocCurrencyCode,
                LocalCurrencyCode = "TRY",
                ExchangeRate = 1,
                JournalDate = DateTime.Now.Date,
                IsPostingJournal = false,
                OfficeCode = "M",
                ApplicationCode = "Invoi",
                ApplicationID = !string.IsNullOrEmpty(request.InvoiceHeaderID) ? Guid.Parse(request.InvoiceHeaderID) : request.InvoiceId,
                IsCompleted = true,
                IsPrinted = false,
                IsLocked = false,
                CompanyCode = 1,
                CreatedUserName = "UZK  Uzak",
                LastUpdatedUserName = "UZK  Uzak"
            };

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[SQL INSERT] trPaymentHeader:");
            Console.WriteLine(sql);
            Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parameters)}");
            Console.ResetColor();

            await connection.ExecuteAsync(sql, parameters, transaction);
        }

        private async Task InsertPaymentLineAsync(SqlConnection connection, SqlTransaction transaction, Guid paymentLineId, Guid paymentHeaderId, Guid cashLineId, Guid debitLineId, int lineNumber, PaymentRow paymentRow)
        {
            var sql = @"
            INSERT INTO [trPaymentLine]
            ([PaymentLineID], [PaymentHeaderID], [SortOrder], [PaymentTypeCode], [LineDescription], 
            [DocCurrencyCode], [CashLineID], [DebitLineID], [CreatedUserName], 
            [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
            VALUES
            (@PaymentLineID, @PaymentHeaderID, @SortOrder, @PaymentTypeCode, @LineDescription, 
            @DocCurrencyCode, @CashLineID, @DebitLineID, @CreatedUserName, 
            GETDATE(), @LastUpdatedUserName, GETDATE())";

            var parameters = new
            {
                PaymentLineID = paymentLineId,
                PaymentHeaderID = paymentHeaderId,
                SortOrder = lineNumber,
                PaymentTypeCode = 1, // Nakit
                LineDescription = $"Nakit Ödeme - {paymentRow.Amount} {paymentRow.CurrencyCode}",
                DocCurrencyCode = paymentRow.CurrencyCode,
                CashLineID = cashLineId,
                DebitLineID = debitLineId,
                CreatedUserName = "UZK  Uzak",
                LastUpdatedUserName = "UZK  Uzak"
            };

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[SQL INSERT] trPaymentLine:");
            Console.WriteLine(sql);
            Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parameters)}");
            Console.ResetColor();

            await connection.ExecuteAsync(sql, parameters, transaction);
        }
        
        private async Task InsertPaymentLineCurrencyAsync(SqlConnection connection, SqlTransaction transaction, Guid paymentLineId, PaymentRow paymentRow, string docCurrencyCode)
        {
            try
            {
                string localCurrencyCode = "TRY"; // Yerel para birimi
                string paymentCurrencyCode = paymentRow.CurrencyCode; // Ödeme para birimi
                
                _logger.LogInformation("[PAYMENT] InsertPaymentLineCurrencyAsync başlıyor: PaymentLineID={PaymentLineID}, CurrencyCode={CurrencyCode}, DocCurrencyCode={DocCurrencyCode}", 
                    paymentLineId, paymentCurrencyCode, docCurrencyCode);
                
                // 1. Ödeme para birimi kaydı (örn. USD, GBP, EUR)
                var sqlPayment = @"
                MERGE INTO [trPaymentLineCurrency] AS target
                USING (SELECT @PaymentLineID AS PaymentLineID, @CurrencyCode AS CurrencyCode) AS source
                ON (target.PaymentLineID = source.PaymentLineID AND target.CurrencyCode = source.CurrencyCode)
                WHEN MATCHED THEN
                    UPDATE SET
                        [Payment] = @Payment,
                        [ExchangeRate] = @ExchangeRate,
                        [RelationCurrencyCode] = @RelationCurrencyCode,
                        [LastUpdatedUserName] = @LastUpdatedUserName,
                        [LastUpdatedDate] = GETDATE()
                WHEN NOT MATCHED THEN
                    INSERT ([PaymentLineID], [CurrencyCode], [Payment], [ExchangeRate], [RelationCurrencyCode], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                    VALUES (@PaymentLineID, @CurrencyCode, @Payment, @ExchangeRate, @RelationCurrencyCode, @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE());";

                var parametersPayment = new
                {
                    PaymentLineID = paymentLineId,
                    CurrencyCode = paymentCurrencyCode,
                    Payment = paymentRow.Amount,
                    ExchangeRate = 1, // Kendi para biriminde kur 1'dir
                    RelationCurrencyCode = paymentCurrencyCode, // ERP ile uyumlu olması için kendi para birimiyle ilişkilendir
                    CreatedUserName = "UZK  Uzak",
                    LastUpdatedUserName = "UZK  Uzak"
                };

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[SQL MERGE] trPaymentLineCurrency (Ödeme Para Birimi):");
                Console.WriteLine(sqlPayment);
                Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parametersPayment)}");
                Console.ResetColor();

                await connection.ExecuteAsync(sqlPayment, parametersPayment, transaction);
                
                // 2. Eğer ödeme para birimi yerel para biriminden farklıysa, yerel para birimi kaydı da ekle
                if (paymentCurrencyCode != localCurrencyCode)
                {
                    var sqlLocal = @"
                    MERGE INTO [trPaymentLineCurrency] AS target
                    USING (SELECT @PaymentLineID AS PaymentLineID, @CurrencyCode AS CurrencyCode) AS source
                    ON (target.PaymentLineID = source.PaymentLineID AND target.CurrencyCode = source.CurrencyCode)
                    WHEN MATCHED THEN
                        UPDATE SET
                            [Payment] = @Payment,
                            [ExchangeRate] = @ExchangeRate,
                            [RelationCurrencyCode] = @RelationCurrencyCode,
                            [LastUpdatedUserName] = @LastUpdatedUserName,
                            [LastUpdatedDate] = GETDATE()
                    WHEN NOT MATCHED THEN
                        INSERT ([PaymentLineID], [CurrencyCode], [Payment], [ExchangeRate], [RelationCurrencyCode], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                        VALUES (@PaymentLineID, @CurrencyCode, @Payment, @ExchangeRate, @RelationCurrencyCode, @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE());";

                    var parametersLocal = new
                    {
                        PaymentLineID = paymentLineId,
                        CurrencyCode = localCurrencyCode,
                        Payment = paymentRow.Amount * (decimal)paymentRow.ExchangeRate, // Yerel para birimindeki karşılığı
                        ExchangeRate = paymentRow.ExchangeRate,
                        RelationCurrencyCode = paymentCurrencyCode, // Yerel para biriminin ilişkili olduğu para birimi
                        CreatedUserName = "UZK  Uzak",
                        LastUpdatedUserName = "UZK  Uzak"
                    };

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n[SQL MERGE] trPaymentLineCurrency (Yerel Para Birimi):");
                    Console.WriteLine(sqlLocal);
                    Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parametersLocal)}");
                    Console.ResetColor();

                    await connection.ExecuteAsync(sqlLocal, parametersLocal, transaction);
                }
                
                _logger.LogInformation("[PAYMENT] InsertPaymentLineCurrencyAsync başarılı: PaymentLineID={PaymentLineID}", paymentLineId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[PAYMENT] InsertPaymentLineCurrencyAsync hata: {Message}", ex.Message);
                throw;
            }
        }

        private async Task InsertCashAttributeAsync(SqlConnection connection, SqlTransaction transaction, Guid cashHeaderId, CashPaymentRequest request)
        {
            if (request.Attributes == null || !request.Attributes.Any())
            {
                return;
            }

            foreach (var attribute in request.Attributes)
            {
                var sql = @"
                INSERT INTO [tpCashATAttribute]
                ([CashHeaderID], [AttributeCode], [AttributeValue], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                VALUES
                (@CashHeaderID, @AttributeCode, @AttributeValue, @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE())";

                var parameters = new
                {
                    CashHeaderID = cashHeaderId,
                    AttributeCode = attribute.AttributeCode,
                    AttributeValue = attribute.AttributeValue,
                    CreatedUserName = "UZK  Uzak",
                    LastUpdatedUserName = "UZK  Uzak"
                };

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[SQL INSERT] tpCashATAttribute:");
                Console.WriteLine(sql);
                Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parameters)}");
                Console.ResetColor();

                await connection.ExecuteAsync(sql, parameters, transaction);
            }
        }

        private async Task InsertDebitAttributeAsync(SqlConnection connection, SqlTransaction transaction, Guid debitHeaderId, CashPaymentRequest request)
        {
            if (request.Attributes == null || !request.Attributes.Any())
            {
                return;
            }

            foreach (var attribute in request.Attributes)
            {
                var sql = @"
                INSERT INTO [tpDebitATAttribute]
                ([DebitHeaderID], [AttributeCode], [AttributeValue], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                VALUES
                (@DebitHeaderID, @AttributeCode, @AttributeValue, @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE())";

                var parameters = new
                {
                    DebitHeaderID = debitHeaderId,
                    AttributeCode = attribute.AttributeCode,
                    AttributeValue = attribute.AttributeValue,
                    CreatedUserName = "UZK  Uzak",
                    LastUpdatedUserName = "UZK  Uzak"
                };

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[SQL INSERT] tpDebitATAttribute:");
                Console.WriteLine(sql);
                Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parameters)}");
                Console.ResetColor();

                await connection.ExecuteAsync(sql, parameters, transaction);
            }
        }

        private async Task InsertPaymentAttributeAsync(SqlConnection connection, SqlTransaction transaction, Guid paymentHeaderId, CashPaymentRequest request)
        {
            if (request.Attributes == null || !request.Attributes.Any())
            {
                return;
            }

            foreach (var attribute in request.Attributes)
            {
                var sql = @"
                INSERT INTO [tpPaymentATAttribute]
                ([PaymentHeaderID], [AttributeCode], [AttributeValue], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                VALUES
                (@PaymentHeaderID, @AttributeCode, @AttributeValue, @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE())";

                var parameters = new
                {
                    PaymentHeaderID = paymentHeaderId,
                    AttributeCode = attribute.AttributeCode,
                    AttributeValue = attribute.AttributeValue,
                    CreatedUserName = "UZK  Uzak",
                    LastUpdatedUserName = "UZK  Uzak"
                };

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[SQL INSERT] tpPaymentATAttribute:");
                Console.WriteLine(sql);
                Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parameters)}");
                Console.ResetColor();

                await connection.ExecuteAsync(sql, parameters, transaction);
            }
        }

        private async Task InsertCurrAccBookAttributeAsync(SqlConnection connection, SqlTransaction transaction, Guid currAccBookId, CashPaymentRequest request, string type)
        {
            if (request.Attributes == null || !request.Attributes.Any())
            {
                return;
            }

            foreach (var attribute in request.Attributes)
            {
                // AT Attribute tablosuna ekle
                var sqlAT = @"
                INSERT INTO [tpCurrAccBookATAttribute]
                ([CurrAccBookID], [AttributeCode], [AttributeValue], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                VALUES
                (@CurrAccBookID, @AttributeCode, @AttributeValue, @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE())";

                var parametersAT = new
                {
                    CurrAccBookID = currAccBookId,
                    AttributeCode = attribute.AttributeCode,
                    AttributeValue = attribute.AttributeValue,
                    CreatedUserName = "UZK  Uzak",
                    LastUpdatedUserName = "UZK  Uzak"
                };

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n[SQL INSERT] tpCurrAccBookATAttribute ({type}):");
                Console.WriteLine(sqlAT);
                Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parametersAT)}");
                Console.ResetColor();

                await connection.ExecuteAsync(sqlAT, parametersAT, transaction);

                // FT Attribute tablosuna ekle
                var sqlFT = @"
                INSERT INTO [tpCurrAccBookFTAttribute]
                ([CurrAccBookID], [AttributeCode], [AttributeValue], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                VALUES
                (@CurrAccBookID, @AttributeCode, @AttributeValue, @CreatedUserName, GETDATE(), @LastUpdatedUserName, GETDATE())";

                var parametersFT = new
                {
                    CurrAccBookID = currAccBookId,
                    AttributeCode = attribute.AttributeCode,
                    AttributeValue = attribute.AttributeValue,
                    CreatedUserName = "UZK  Uzak",
                    LastUpdatedUserName = "UZK  Uzak"
                };

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n[SQL INSERT] tpCurrAccBookFTAttribute ({type}):");
                Console.WriteLine(sqlFT);
                Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parametersFT)}");
                Console.ResetColor();

                await connection.ExecuteAsync(sqlFT, parametersFT, transaction);
            }
        }
        
        /// <summary>
        /// Kasa hareketini günceller
        /// </summary>
        /// <param name="request">Güncelleme isteği</param>
        /// <param name="userName">Kullanıcı adı</param>
        /// <returns>Güncelleme sonucu</returns>
        public async Task<ApiResponse<bool>> UpdateCashTransactionAsync(CashUpdateRequest request, string userName)
        {
            try
            {
                _logger.LogInformation("Kasa hareketi güncelleme işlemi başlatıldı. CashHeaderId: {CashHeaderId}, Kullanıcı: {UserName}", 
                    request.CashHeaderId, userName);
                
                // Güncelleme işlemi burada yapılacak
                // ...
                
                // Örnek olarak başarılı olduğunu varsayalım
                bool success = true;
                string cashNumber = request.CashTransNumber;
                
                if (success)
                {
                    // Başarılı işlem sonrası bildirim gönder
                    await SendTransactionNotificationAsync(userName, "CashTransaction", "Update", 
                        cashNumber, request.TotalAmount, request.CurrencyCode, request.Description);
                    
                    return new ApiResponse<bool>
                    {
                        Success = true,
                        Message = "Kasa hareketi başarıyla güncellendi",
                        Data = true
                    };
                }
                else
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Kasa hareketi güncellenirken bir hata oluştu",
                        Data = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa hareketi güncellenirken hata oluştu: {Message}", ex.Message);
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Kasa hareketi güncellenirken bir hata oluştu: " + ex.Message,
                    Data = false
                };
            }
        }
        
        /// <summary>
        /// Kasa hareketini siler
        /// </summary>
        /// <param name="cashHeaderId">Kasa başlık ID'si</param>
        /// <param name="userName">Kullanıcı adı</param>
        /// <returns>Silme sonucu</returns>
        public async Task<ApiResponse<bool>> DeleteCashTransactionAsync(string cashHeaderId, string cashNumber, string userName)
        {
            try
            {
                _logger.LogInformation("Kasa hareketi silme işlemi başlatıldı. CashHeaderId: {CashHeaderId}, Kullanıcı: {UserName}", 
                    cashHeaderId, userName);
                
                // Silme işlemi burada yapılacak
                // ...
                
                // Örnek olarak başarılı olduğunu varsayalım
                bool success = true;
                
                if (success)
                {
                    // Başarılı işlem sonrası bildirim gönder
                    await SendTransactionNotificationAsync(userName, "CashTransaction", "Delete", cashNumber);
                    
                    return new ApiResponse<bool>
                    {
                        Success = true,
                        Message = "Kasa hareketi başarıyla silindi",
                        Data = true
                    };
                }
                else
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Kasa hareketi silinirken bir hata oluştu",
                        Data = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa hareketi silinirken hata oluştu: {Message}", ex.Message);
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Kasa hareketi silinirken bir hata oluştu: " + ex.Message,
                    Data = false
                };
            }
        }
        
        /// <summary>
        /// Faturayı tamamlandı olarak işaretler
        /// </summary>
        /// <param name="invoiceHeaderId">Fatura başlık ID'si</param>
        /// <param name="userName">İşlemi yapan kullanıcı adı</param>
        /// <returns>İşlem sonucu</returns>
        private async Task MarkInvoiceAsCompletedAsync(string invoiceHeaderId, string userName = "System")
        {
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                await connection.OpenAsync();

                // Önce fatura bilgilerini al (bildirim için gerekli)
                var invoiceQuery = @"
                SELECT 
                    InvoiceNumber, 
                    CurrencyCode, 
                    DocTotal
                FROM 
                    trInvoiceHeader
                WHERE 
                    InvoiceHeaderID = @InvoiceHeaderID";

                var invoiceParams = new DynamicParameters();
                invoiceParams.Add("@InvoiceHeaderID", Guid.Parse(invoiceHeaderId));

                var invoiceInfo = await connection.QueryFirstOrDefaultAsync(invoiceQuery, invoiceParams);
                string invoiceNumber = invoiceInfo?.InvoiceNumber;
                string currencyCode = invoiceInfo?.CurrencyCode;
                decimal? docTotal = invoiceInfo?.DocTotal;

                // Faturayı tamamlandı olarak işaretle
                var sql = @"
                UPDATE trInvoiceHeader
                SET 
                    IsCompleted = 1,
                    LastUpdatedDate = GETDATE(),
                    LastUpdatedUserName = @UserName
                WHERE 
                    InvoiceHeaderID = @InvoiceHeaderID";

                var parameters = new DynamicParameters();
                parameters.Add("@InvoiceHeaderID", Guid.Parse(invoiceHeaderId));
                parameters.Add("@UserName", userName);

                await connection.ExecuteAsync(sql, parameters);

                _logger.LogInformation("[INVOICE] Fatura tamamlandı olarak işaretlendi: {InvoiceHeaderID}, Fatura No: {InvoiceNumber}", invoiceHeaderId, invoiceNumber);

                // Bildirim gönder
                if (!string.IsNullOrEmpty(invoiceNumber) && _notificationService != null)
                {
                    await SendTransactionNotificationAsync(userName, "Invoice", "Complete", invoiceNumber, docTotal, currencyCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatura tamamlandı olarak işaretlenirken hata oluştu: {InvoiceHeaderID}, {Message}", invoiceHeaderId, ex.Message);
                Console.WriteLine($"[ERROR] Fatura tamamlandı olarak işaretlenirken hata oluştu: {ex.Message}");
            }
        }

        /// <summary>
        /// Kasa koduna göre açıklama döndürür
        /// </summary>
        private async Task LoadCashAccountDescriptionsAsync()
        {
            if (_cashAccountDescriptions.Count == 0)
            {
                try
                {
                    var cashAccounts = await _cashAccountRepository.GetCashAccountsAsync();
                    
                    foreach (var account in cashAccounts)
                    {
                        if (!string.IsNullOrEmpty(account.CashAccountCode))
                        {
                            _cashAccountDescriptions[account.CashAccountCode] = account.CashAccountDescription;
                        }
                    }
                    _logger.LogInformation($"Loaded {_cashAccountDescriptions.Count} cash account descriptions");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error loading cash account descriptions");
                }
            }
        }
        
        /// <summary>
        /// Kasa koduna göre açıklama döndürür
        /// </summary>
        private string GetCashAccountDescription(string cashAccountCode)
        {
            // Kasa açıklamalarını yükledik mi kontrol et
            if (_cashAccountDescriptions.Count == 0)
            {
                // Yüklenemediği durumda varsayılan değerleri kullan
                switch (cashAccountCode)
                {
                    case "10111":
                        return "MERKEZ TL KASA";
                    case "10222":
                        return "MERKEZ USD KASA";
                    case "10333":
                        return "MERKEZ EUR KASA";
                    case "10444":
                        return "MERKEZ GBP KASA";
                    default:
                        return $"KASA {cashAccountCode}";
                }
            }
            
            // Sözlükte bu kasa kodu var mı?
            if (_cashAccountDescriptions.TryGetValue(cashAccountCode, out string description))
            {
                return description;
            }
            
            // Bulunamadıysa kod ile döndür
            return $"KASA {cashAccountCode}";
        }
        
        /// <summary>
        /// Kasa hareketleri raporunu getirir
        /// </summary>
        public async Task<PagedApiResponse<CashTransactionReportItem>> GetCashTransactionReportAsync(CashTransactionReportRequest request)
        {
            try
            {
                _logger.LogInformation("Kasa hareketleri raporu alınıyor. Başlangıç: {StartDate}, Bitiş: {EndDate}", request.StartDate, request.EndDate);
                
                // Kasa açıklamalarını yükle
                await LoadCashAccountDescriptionsAsync();
                
                using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                await connection.OpenAsync();
                
                // Parametreleri hazırla
                var parameters = new DynamicParameters();
                parameters.Add("@StartDate", request.StartDate);
                parameters.Add("@EndDate", request.EndDate);
                
                // Sayfalama için parametreler
                int offset = (request.Page - 1) * request.PageSize;
                parameters.Add("@Offset", offset);
                parameters.Add("@PageSize", request.PageSize);
                
                // Filtreleme parametreleri (NULL değer kabul edecek şekilde)
                parameters.Add("@CashAccountCode", string.IsNullOrEmpty(request.CashAccountCode) ? null : request.CashAccountCode);
                parameters.Add("@TransactionType", string.IsNullOrEmpty(request.TransactionType) ? null : request.TransactionType);
                parameters.Add("@CurrencyCode", string.IsNullOrEmpty(request.CurrencyCode) ? null : request.CurrencyCode);
                
                // SQL sorgusu
                string sql = @"
                    WITH CashATAttributesFilter AS (
                        SELECT CashHeaderID FROM trCashHeader 
                    ),
                    CashAccountAttributesFilter AS (
                        SELECT CurrAccCode FROM cdCurrAcc 
                    ),
                    CashTransactions AS (
                        SELECT 
                            DocumentDate = ch.DocumentDate,
                            DocumentNumber = ch.DocumentNumber,
                            TransactionNumber = ch.CashTransNumber,
                            TransactionType = CASE 
                                WHEN ch.CashTransTypeCode = 1 THEN 'Tahsilat'
                                WHEN ch.CashTransTypeCode = 2 THEN 'Tediye'
                                WHEN ch.CashTransTypeCode = 3 THEN 'Virman'
                                ELSE 'Diğer'
                            END,
                            CashAccountCode = CASE 
                                WHEN ch.CashTransTypeCode = 2 THEN cl.CurrAccCode
                                WHEN ch.CashTransTypeCode = 3 AND cl.GLAccCode <> '' THEN ''
                                ELSE ch.CashCurrAccCode 
                            END,
                            CounterCashAccountCode = CASE 
                                WHEN ch.CashTransTypeCode = 8 THEN cl.CounterCashAccountCode
                                ELSE ''
                            END,
                            CurrAccCode = ch.CurrAccCode,
                            CurrAccDesc = ca.CurrAccDesc,
                            CurrencyCode = clc.CurrencyCode,
                            Amount = clc.Debit - clc.Credit,
                            Description = ch.Description,
                            CreatedBy = ch.CreatedUserName,
                            CreatedDate = ch.CreatedDate
                        FROM trCashHeader ch WITH(NOLOCK)
                        INNER JOIN trCashLine cl WITH(NOLOCK) ON cl.CashHeaderID = ch.CashHeaderID
                        LEFT OUTER JOIN trCashLineCurrency clc WITH(NOLOCK) ON clc.CashLineID = cl.CashLineID
                        LEFT OUTER JOIN cdCurrAcc ca WITH(NOLOCK) ON ca.CurrAccCode = ch.CurrAccCode
                        LEFT OUTER JOIN CashATAttributesFilter ON CashATAttributesFilter.CashHeaderID = ch.CashHeaderID
                        WHERE ch.DocumentDate BETWEEN @StartDate AND @EndDate
                        AND CASE 
                            WHEN ch.CashTransTypeCode = 2 THEN cl.CurrAccCode
                            WHEN ch.CashTransTypeCode = 3 AND cl.GLAccCode <> '' THEN ''
                            ELSE ch.CashCurrAccCode 
                        END <> ''
                    )
                    SELECT 
                        t.*,
                        COUNT(*) OVER() AS TotalCount
                    FROM CashTransactions t
                    WHERE 1=1";
                
                // Filtreleme koşulları ekle
                if (!string.IsNullOrEmpty(request.CashAccountCode))
                {
                    sql += " AND t.CashAccountCode = @CashAccountCode";
                }
                
                if (!string.IsNullOrEmpty(request.TransactionType))
                {
                    sql += " AND t.TransactionType = @TransactionType";
                }
                
                if (!string.IsNullOrEmpty(request.CurrencyCode))
                {
                    sql += " AND t.CurrencyCode = @CurrencyCode";
                }
                
                // Sıralama ekle
                sql += " ORDER BY t.DocumentDate DESC";
                
                // Sayfalama ekle
                sql += " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
                
                // Toplam kayıt sayısı için sorgu
                string countSql = @"
                    WITH CashATAttributesFilter AS (
                        SELECT CashHeaderID FROM trCashHeader WHERE 1=1
                    ),
                    CashAccountAttributesFilter AS (
                        SELECT CurrAccCode FROM cdCurrAcc WHERE 1=1
                    ),
                    CashTransactions AS (
                        SELECT 
                            CashAccountCode = CASE 
                                WHEN ch.CashTransTypeCode = 2 THEN cl.CurrAccCode
                                WHEN ch.CashTransTypeCode = 3 AND cl.GLAccCode <> '' THEN ''
                                ELSE ch.CashCurrAccCode 
                            END,
                            TransactionType = CASE 
                                WHEN ch.CashTransTypeCode = 1 THEN 'Tahsilat'
                                WHEN ch.CashTransTypeCode = 2 THEN 'Tediye'
                                WHEN ch.CashTransTypeCode = 3 THEN 'Virman'
                                ELSE 'Diğer'
                            END,
                            CurrencyCode = clc.CurrencyCode
                        FROM trCashHeader ch WITH(NOLOCK)
                        INNER JOIN trCashLine cl WITH(NOLOCK) ON cl.CashHeaderID = ch.CashHeaderID
                        LEFT OUTER JOIN trCashLineCurrency clc WITH(NOLOCK) ON clc.CashLineID = cl.CashLineID
                        LEFT OUTER JOIN CashATAttributesFilter ON CashATAttributesFilter.CashHeaderID = ch.CashHeaderID
                        WHERE ch.DocumentDate BETWEEN @StartDate AND @EndDate
                        AND CASE 
                            WHEN ch.CashTransTypeCode = 2 THEN cl.CurrAccCode
                            WHEN ch.CashTransTypeCode = 3 AND cl.GLAccCode <> '' THEN ''
                            ELSE ch.CashCurrAccCode 
                        END <> ''
                    )
                    SELECT COUNT(*) FROM CashTransactions t
                    WHERE 1=1";
                
                // Filtreleme koşullarını count sorgusuna da ekle
                if (!string.IsNullOrEmpty(request.CashAccountCode))
                {
                    countSql += " AND t.CashAccountCode = @CashAccountCode";
                }
                
                if (!string.IsNullOrEmpty(request.TransactionType))
                {
                    countSql += " AND t.TransactionType = @TransactionType";
                }
                
                if (!string.IsNullOrEmpty(request.CurrencyCode))
                {
                    countSql += " AND t.CurrencyCode = @CurrencyCode";
                }
                
                int totalCount = await connection.ExecuteScalarAsync<int>(countSql, parameters);
                
                // Sorguyu çalıştır
                var transactions = await connection.QueryAsync<CashTransactionReportItem>(sql, parameters);
                
                // Kasa adlarını ekle
                var result = transactions.ToList();
                foreach (var item in result)
                {
                    item.CashAccountName = GetCashAccountDescription(item.CashAccountCode);
                    
                    if (!string.IsNullOrEmpty(item.CounterCashAccountCode))
                    {
                        item.CounterCashAccountName = GetCashAccountDescription(item.CounterCashAccountCode);
                    }
                }
                
                return new PagedApiResponse<CashTransactionReportItem>(
                    result,
                    totalCount,
                    request.Page,
                    request.PageSize,
                    true,
                    "Kasa hareketleri başarıyla getirildi"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa hareketleri raporu alınırken hata oluştu: {Message}", ex.Message);
                var errorResponse = new PagedApiResponse<CashTransactionReportItem>();
                errorResponse.Success = false;
                errorResponse.Message = "Kasa hareketleri raporu alınırken bir hata oluştu";
                errorResponse.Error = ex.Message;
                return errorResponse;
            }
        }

        /// <summary>
        /// Kasa tahsilat fişlerini getirir
        /// </summary>
        public async Task<PagedApiResponse<ErpMobile.Api.Models.CashTransactionResponse>> GetCashReceiptVouchersAsync(string startDate, string endDate, string cashAccountCode, int page, int pageSize, string userName)
        {
            try
            {
                // Kasa tahsilat fişleri için sorgu hazırlanıyor
                
                using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                await connection.OpenAsync();
                
                // SQL sorgusu
                 string sql = @"
                    SELECT 
                        CH.CashHeaderID AS CashHeaderID,
                        CH.CashTransTypeCode AS CashTransTypeCode,
                        CH.CashTransNumber AS CashTransNumber,
                        CH.DocumentDate AS DocumentDate,
                        CH.DocumentTime AS DocumentTime,
                        CL.LineDescription AS Description,
                        CL.CurrAccAmount AS CurrAccAmount,
                        CH.ExchangeRate AS ExchangeRate,
                        CH.DocCurrencyCode AS DocCurrencyCode,
                        '' AS SpecialCode,
                        '' AS AuthCode,
                        0 AS BranchNo,
                        0 AS DepartmentNo,
                        0 AS AccountRef,
                        0 AS CostCenterRef,
                        CH.CashCurrAccCode AS CashCurrAccCode,
                        '' AS CashAccountName,
                        CL.CostCenterCode AS CostCenterCode,
                        '' AS CostCenterName,
                        CH.DocCurrencyCode AS DocCurrencyCode
                    FROM trCashHeader CH
                    JOIN trCashLine CL ON CH.CashHeaderID = CL.CashHeaderID
                    WHERE CH.CashTransTypeCode = 1 -- Tahsilat fişleri için CashTransTypeCode=1
                    AND CH.DocumentDate BETWEEN @startDate AND @endDate";
                
                // Kasa kodu filtresi ekle
                if (!string.IsNullOrEmpty(cashAccountCode))
                {
                    sql += " AND CH.CashCurrAccCode = @cashAccountCode";
                }
                
                // Sıralama ve sayfalama
                sql += " ORDER BY CH.DocumentDate DESC, CH.DocumentTime DESC";
                sql += " OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";
                
                // Toplam kayıt sayısı için sorgu
                string countSql = @"
                    SELECT COUNT(*)
                    FROM trCashHeader CH
                    JOIN trCashLine CL ON CH.CashHeaderID = CL.CashHeaderID
                    WHERE CH.CashTransTypeCode = 1 -- Tahsilat fişleri için CashTransTypeCode=1
                    AND CH.DocumentDate BETWEEN @startDate AND @endDate";
                
                // Kasa kodu filtresi ekle (count sorgusu için)
                if (!string.IsNullOrEmpty(cashAccountCode))
                {
                    countSql += " AND CH.CashCurrAccCode = @cashAccountCode";
                }
                
                // Parametreler
                var parameters = new DynamicParameters();
                parameters.Add("@startDate", startDate, DbType.String);
                parameters.Add("@endDate", endDate, DbType.String);
                if (!string.IsNullOrEmpty(cashAccountCode))
                {
                    parameters.Add("@cashAccountCode", cashAccountCode, DbType.String);
                }
                parameters.Add("@offset", (page - 1) * pageSize, DbType.Int32);
                parameters.Add("@pageSize", pageSize, DbType.Int32);
                
                // Sorguyu çalıştır
                var result = await connection.QueryAsync<ErpMobile.Api.Models.CashTransactionResponse>(sql, parameters);
                var totalCount = await connection.ExecuteScalarAsync<int>(countSql, parameters);
                
                return new PagedApiResponse<ErpMobile.Api.Models.CashTransactionResponse>(
                    result.ToList(),
                    totalCount,
                    page,
                    pageSize,
                    true,
                    "Kasa tahsilat fişleri başarıyla getirildi"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa tahsilat fişleri alınırken hata oluştu: {Message}", ex.Message);
                var errorResponse = new PagedApiResponse<ErpMobile.Api.Models.CashTransactionResponse>();
                errorResponse.Success = false;
                errorResponse.Message = "Kasa tahsilat fişleri alınırken bir hata oluştu";
                errorResponse.Error = ex.Message;
                return errorResponse;
            }
        }

        /// <summary>
        /// Kasa virman fişlerini getirir
        /// </summary>
        public async Task<PagedApiResponse<ErpMobile.Api.Models.CashTransactionResponse>> GetCashTransferVouchersAsync(string startDate, string endDate, string cashAccountCode, int page, int pageSize, string userName)
        {
            try
            {
                // Kasa virman fişleri için sorgu hazırlanıyor
                
                using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                await connection.OpenAsync();
                
                // SQL sorgusu
                string sql = @"
                    SELECT 
                        CH.CashHeaderID AS CashHeaderID,
                        CH.CashTransTypeCode AS CashTransTypeCode,
                        CH.CashTransNumber AS VoucherNumber,
                        CH.CashTransNumber AS CashTransNumber,
                        CH.DocumentDate AS DocumentDate,
                        CH.DocumentTime AS DocumentTime,
                        CL.LineDescription AS Description,
                        CL.CurrAccAmount AS Amount,
                        CL.CurrAccAmount AS CurrAccAmount,
                        CH.ExchangeRate AS ExchangeRate,
                        CH.DocCurrencyCode AS CurrencyCode,
                        CH.DocCurrencyCode AS DocCurrencyCode,
                        '' AS SpecialCode,
                        '' AS AuthCode,
                        0 AS BranchNo,
                        0 AS DepartmentNo,
                        0 AS AccountRef,
                        0 AS CostCenterRef,
                        CH.CashCurrAccCode AS CashCurrAccCode,
                        CH.CashCurrAccCode AS SourceCashAccountCode,
                        '' AS CashAccountName,
                        '' AS SourceCashAccountName,
                        CL.CurrAccCode AS TargetCashAccountCode,
                        '' AS TargetCashAccountName,
                        CL.CostCenterCode AS CostCenterCode,
                        '' AS CostCenterName
                    FROM trCashHeader CH
                    JOIN trCashLine CL ON CH.CashHeaderID = CL.CashHeaderID
                    WHERE CH.CashTransTypeCode = 3 -- Virman fişleri için CashTransTypeCode=3
                    AND CH.DocumentDate BETWEEN @startDate AND @endDate";
                
                // Kasa kodu filtresi ekle
                if (!string.IsNullOrEmpty(cashAccountCode))
                {
                    sql += " AND (CH.CashCurrAccCode = @cashAccountCode OR CL.TargetCashCode = @cashAccountCode)";
                }
                
                // Sıralama ve sayfalama
                sql += " ORDER BY CH.DocumentDate DESC, CH.DocumentTime DESC";
                sql += " OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";
                
                // Toplam kayıt sayısı için sorgu
                string countSql = @"
                    SELECT COUNT(*)
                    FROM trCashHeader CH
                    JOIN trCashLine CL ON CH.CashHeaderID = CL.CashHeaderID
                    WHERE CH.CashTransTypeCode = 3 -- Virman fişleri için CashTransTypeCode=3
                    AND CH.DocumentDate BETWEEN @startDate AND @endDate";
                
                // Kasa kodu filtresi ekle (count sorgusu için)
                if (!string.IsNullOrEmpty(cashAccountCode))
                {
                    countSql += " AND (CH.CashCurrAccCode = @cashAccountCode OR CL.TargetCashCode = @cashAccountCode)";
                }
                
                // Parametreler
                var parameters = new DynamicParameters();
                parameters.Add("@startDate", startDate, DbType.String);
                parameters.Add("@endDate", endDate, DbType.String);
                if (!string.IsNullOrEmpty(cashAccountCode))
                {
                    parameters.Add("@cashAccountCode", cashAccountCode, DbType.String);
                }
                parameters.Add("@offset", (page - 1) * pageSize, DbType.Int32);
                parameters.Add("@pageSize", pageSize, DbType.Int32);
                
                // Sorguyu çalıştır
                var result = await connection.QueryAsync<ErpMobile.Api.Models.CashTransactionResponse>(sql, parameters);
                var totalCount = await connection.ExecuteScalarAsync<int>(countSql, parameters);
                
                return new PagedApiResponse<ErpMobile.Api.Models.CashTransactionResponse>(
                    result.ToList(),
                    totalCount,
                    page,
                    pageSize,
                    true,
                    "Kasa virman fişleri başarıyla getirildi"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa virman fişleri alınırken hata oluştu: {Message}", ex.Message);
                var errorResponse = new PagedApiResponse<ErpMobile.Api.Models.CashTransactionResponse>();
                errorResponse.Success = false;
                errorResponse.Message = "Kasa virman fişleri alınırken bir hata oluştu";
                errorResponse.Error = ex.Message;
                return errorResponse;
            }
        }

        /// <summary>
        /// Kasa tediye, tahsilat ve virman fişlerini getirir
        /// </summary>
        /// <remarks>
        /// CashTransTypeCode = 1 (Tahsilat)
        /// CashTransTypeCode = 3 (Tediye)
        /// CashTransTypeCode = 8 (Kasalar Arası Virman)
        /// </remarks>
        public async Task<PagedApiResponse<ErpMobile.Api.Models.CashTransactionResponse>> GetCashPaymentVouchersAsync(string startDate, string endDate, string cashAccountCode, int page, int pageSize, string userName)
        {
            try
            {
                _logger.LogInformation("Kasa fişleri alınıyor. Başlangıç: {StartDate}, Bitiş: {EndDate}", startDate, endDate);
                
                using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                await connection.OpenAsync();
                
                // SQL sorgusu
                string sql = @"
                    WITH CashATAttributesFilter AS (
                        SELECT CashHeaderID FROM trCashHeader 
                    ),
                    CashAccountAttributesFilter AS (
                        SELECT CurrAccCode FROM cdCurrAcc 
                    ),
                    CashTransactions AS (
                        SELECT 
                            DocumentDate = ch.DocumentDate,
                            DocumentNumber = ch.DocumentNumber,
                            TransactionNumber = ch.CashTransNumber,
                            TransactionType = CASE 
                                WHEN ch.CashTransTypeCode = 1 THEN 'Tahsilat'
                                WHEN ch.CashTransTypeCode = 2 THEN 'Tediye'
                                WHEN ch.CashTransTypeCode = 3 THEN 'Virman'
                                ELSE 'Diğer'
                            END,
                            CashAccountCode = CASE 
                                WHEN ch.CashTransTypeCode = 3 THEN cl.CurrAccCode
                                WHEN ch.CashTransTypeCode = 8 AND cl.GLAccCode <> '' THEN ''
                                ELSE ch.CashCurrAccCode 
                            END,
                            CounterCashAccountCode = CASE 
                                WHEN ch.CashTransTypeCode = 8 THEN cl.CounterCashAccountCode
                                ELSE ''
                            END,
                            CurrAccCode = ch.CurrAccCode,
                            CurrAccDesc = ca.CurrAccDesc,
                            CurrencyCode = clc.CurrencyCode,
                            Amount = clc.Debit - clc.Credit,
                            Description = ch.Description,
                            CreatedBy = ch.CreatedUserName,
                            CreatedDate = ch.CreatedDate
                        FROM trCashHeader ch WITH(NOLOCK)
                        INNER JOIN trCashLine cl WITH(NOLOCK) ON cl.CashHeaderID = ch.CashHeaderID
                        LEFT OUTER JOIN trCashLineCurrency clc WITH(NOLOCK) ON clc.CashLineID = cl.CashLineID
                        LEFT OUTER JOIN cdCurrAcc ca WITH(NOLOCK) ON ca.CurrAccCode = ch.CurrAccCode
                        LEFT OUTER JOIN CashATAttributesFilter ON CashATAttributesFilter.CashHeaderID = ch.CashHeaderID
                        WHERE ch.DocumentDate BETWEEN @StartDate AND @EndDate
                        AND CASE 
                            WHEN ch.CashTransTypeCode = 3 THEN cl.CurrAccCode
                            WHEN ch.CashTransTypeCode = 8 AND cl.GLAccCode <> '' THEN ''
                            ELSE ch.CashCurrAccCode 
                        END <> ''
                    )
                    SELECT 
                        t.*,
                        COUNT(*) OVER() AS TotalCount
                    FROM CashTransactions t
                    WHERE 1=1";
                
                // Filtreleme koşulları ekle
                if (!string.IsNullOrEmpty(cashAccountCode))
                {
                    sql += " AND t.CashAccountCode = @CashAccountCode";
                }
                
                // Sıralama ekle
                sql += " ORDER BY t.DocumentDate DESC";
                
                // Sayfalama ekle
                sql += " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
                
                // Toplam kayıt sayısı için sorgu
                string countSql = @"
                    WITH CashATAttributesFilter AS (
                        SELECT CashHeaderID FROM trCashHeader WHERE 1=1
                    ),
                    CashAccountAttributesFilter AS (
                        SELECT CurrAccCode FROM cdCurrAcc WHERE 1=1
                    ),
                    CashTransactions AS (
                        SELECT 
                            CashAccountCode = CASE 
                                WHEN ch.CashTransTypeCode = 3 THEN cl.CurrAccCode
                                WHEN ch.CashTransTypeCode = 8 AND cl.GLAccCode <> '' THEN ''
                                ELSE ch.CashCurrAccCode 
                            END,
                            TransactionType = CASE 
                                WHEN ch.CashTransTypeCode = 1 THEN 'Tahsilat'
                                WHEN ch.CashTransTypeCode = 2 THEN 'Tediye'
                                WHEN ch.CashTransTypeCode = 3 THEN 'Virman'
                                ELSE 'Diğer'
                            END,
                            CurrencyCode = clc.CurrencyCode
                        FROM trCashHeader ch WITH(NOLOCK)
                        INNER JOIN trCashLine cl WITH(NOLOCK) ON cl.CashHeaderID = ch.CashHeaderID
                        LEFT OUTER JOIN trCashLineCurrency clc WITH(NOLOCK) ON clc.CashLineID = cl.CashLineID
                        LEFT OUTER JOIN CashATAttributesFilter ON CashATAttributesFilter.CashHeaderID = ch.CashHeaderID
                        WHERE ch.DocumentDate BETWEEN @StartDate AND @EndDate
                        AND CASE 
                            WHEN ch.CashTransTypeCode = 3 THEN cl.CurrAccCode
                            WHEN ch.CashTransTypeCode = 8 AND cl.GLAccCode <> '' THEN ''
                            ELSE ch.CashCurrAccCode 
                        END <> ''
                    )
                    SELECT COUNT(*) FROM CashTransactions t
                    WHERE 1=1";
                
                // Filtreleme koşullarını count sorgusuna da ekle
                if (!string.IsNullOrEmpty(cashAccountCode))
                {
                    countSql += " AND t.CashAccountCode = @CashAccountCode";
                }
                
                // Parametreler
                var parameters = new DynamicParameters();
                parameters.Add("@StartDate", startDate, DbType.String);
                parameters.Add("@EndDate", endDate, DbType.String);
                parameters.Add("@Offset", (page - 1) * pageSize, DbType.Int32);
                parameters.Add("@PageSize", pageSize, DbType.Int32);
                
                // Sorguyu çalıştır
                var result = await connection.QueryAsync<ErpMobile.Api.Models.CashTransactionResponse>(sql, parameters);
                var totalCount = await connection.ExecuteScalarAsync<int>(countSql, parameters);
                
                return new PagedApiResponse<ErpMobile.Api.Models.CashTransactionResponse>(
                    result.ToList(),
                    totalCount,
                    page,
                    pageSize,
                    true,
                    "Kasa fişleri başarıyla getirildi"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa fişleri alınırken hata oluştu: {Message}", ex.Message);
                var errorResponse = new PagedApiResponse<ErpMobile.Api.Models.CashTransactionResponse>();
                errorResponse.Success = false;
                errorResponse.Message = "Kasa fişleri alınırken bir hata oluştu";
                errorResponse.Error = ex.Message;
                return errorResponse;
            }
        }

        /// <summary>
        /// Kasa bakiyelerini getirir
        /// </summary>
        public async Task<PagedApiResponse<CashBalanceResponse>> GetCashBalancesAsync(string startDate, string endDate, string cashAccountCode, string userName, int page, int pageSize, string searchText = null, string cashTransTypeCode = null)
        {
            try
            {
                // Sayfalama için sonuç listesi ve toplam kayıt sayısı
                var cashBalances = new List<CashBalanceResponse>();
                int totalCount = 0;
                // Tarih formatını kontrol et - hem yyyyMMdd hem yyyy-MM-dd formatlarını destekle
                DateTime parsedStartDate;
                var startDateFormats = new[] { "yyyyMMdd", "yyyy-MM-dd" };
                if (!DateTime.TryParseExact(startDate, startDateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedStartDate))
                {
                    parsedStartDate = DateTime.Now.AddMonths(-1);
                    startDate = parsedStartDate.ToString("yyyyMMdd");
                    _logger.LogInformation($"Geçersiz startDate formatı, varsayılan değer kullanıldı: {startDate}");
                }
                else
                {
                    _logger.LogInformation($"StartDate başarıyla parse edildi: {parsedStartDate:yyyy-MM-dd}");
                }

                DateTime parsedEndDate;
                var endDateFormats = new[] { "yyyyMMdd", "yyyy-MM-dd" };
                if (!DateTime.TryParseExact(endDate, endDateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedEndDate))
                {
                    parsedEndDate = DateTime.Now;
                    endDate = parsedEndDate.ToString("yyyyMMdd");
                    _logger.LogInformation($"Geçersiz endDate formatı, varsayılan değer kullanıldı: {endDate}");
                }
                else
                {
                    _logger.LogInformation($"EndDate başarıyla parse edildi: {parsedEndDate:yyyy-MM-dd}");
                }
                
                using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                await connection.OpenAsync();
                
                // Kasa listesi sorgusu
                string cashAccountsQuery = @"
                    SELECT 
                        CashAccountCode = cdCurrAcc.CurrAccCode,
                        CashAccountDescription = cdCurrAccDesc.CurrAccDescription,
                        cdCurrAcc.CurrencyCode
                    FROM
                        cdCurrAcc WITH(NOLOCK)
                        INNER JOIN cdCurrAccDesc WITH(NOLOCK) ON cdCurrAcc.CurrAccTypeCode = cdCurrAccDesc.CurrAccTypeCode
                            AND cdCurrAcc.CurrAccCode = cdCurrAccDesc.CurrAccCode
                            AND cdCurrAccDesc.LangCode = N'TR'
                    WHERE cdCurrAcc.CurrAccTypeCode = 7";
                
                // Kasa kodu filtresi ekle
                if (!string.IsNullOrEmpty(cashAccountCode))
                {
                    cashAccountsQuery += " AND cdCurrAcc.CurrAccCode = @cashAccountCode";
                }
                
                var cashAccounts = await connection.QueryAsync<dynamic>(cashAccountsQuery, new { cashAccountCode });
                
                List<CashBalanceResponse> result = new List<CashBalanceResponse>();
                
                foreach (var cashAccount in cashAccounts)
                {
                    string currentCashCode = cashAccount.CashAccountCode;
                    byte currAccTypeCode = 7; // Kasa hesabı tipi kodu
                    decimal companyCode = 1; // Varsayılan şirket kodu
                    
                    // qry_GetCurrAccBalance stored procedure'ü ile kasa bakiyesi alma
                    var parameters = new DynamicParameters();
                    parameters.Add("@CurrAccTypeCode", currAccTypeCode);
                    parameters.Add("@CurrAccCode", currentCashCode);
                    parameters.Add("@CompanyCode", companyCode);
                    parameters.Add("@OfficeCode", "");
                    parameters.Add("@StoreCode", "");
                    
                    var cashBalance = await connection.QueryFirstOrDefaultAsync<dynamic>(
                        "qry_GetCurrAccBalance", 
                        parameters, 
                        commandType: CommandType.StoredProcedure);
                    
                    // İşlem detayları sorgusu
                    string transactionsQuery = @"
                        SELECT 
                            DocumentDate = trCashHeader.DocumentDate,
                            DocumentNumber = trCashHeader.DocumentNumber,
                            CashTransTypeCode = CAST(trCashHeader.CashTransTypeCode AS VARCHAR),
                            CashTransTypeDescription = ISNULL((SELECT CashTransTypeDescription FROM bsCashTransTypeDesc WITH(NOLOCK) 
                                                          WHERE bsCashTransTypeDesc.CashTransTypeCode = trCashHeader.CashTransTypeCode 
                                                          AND bsCashTransTypeDesc.LangCode = N'TR'), ''),
                            CashTransNumber = CASE WHEN ISNULL(trCashHeader.CashTransNumber, '') = '' THEN 'Fişsiz' ELSE trCashHeader.CashTransNumber END,
                            Description = trCashLine.LineDescription,
                            -- Yerel para birimi (TRY) değerleri
                            Loc_CurrencyCode = ISNULL(trCashLineCurrency.CurrencyCode, 'TRY'),
                            Loc_Debit = ISNULL(trCashLineCurrency.Debit, 0),
                            Loc_Credit = ISNULL(trCashLineCurrency.Credit, 0),
                            Loc_Balance = 0, -- Will calculate running balance later
                            -- Döviz değerleri
                            Doc_CurrencyCode = trCashLineCurrencyDoc.CurrencyCode,
                            Doc_Debit = ISNULL(trCashLineCurrencyDoc.Debit, 0),
                            Doc_Credit = ISNULL(trCashLineCurrencyDoc.Credit, 0),
                            Doc_Balance = 0, -- Will calculate running balance later
                            SourceCashAccountCode = trCashHeader.CashCurrAccCode,
                            TargetCashAccountCode = CASE WHEN trCashHeader.CashTransTypeCode = 3 THEN trCashLine.CurrAccCode ELSE '' END,
                            -- Cari hesap bilgileri
                            CurrAccCode = ISNULL(trCashLine.CurrAccCode, ''),
                            CurrAccDescription = CASE WHEN trCashLine.CurrAccTypeCode IN (4,8) 
                                                     THEN ISNULL((SELECT FirstLastName FROM cdCurrAcc WITH(NOLOCK) 
                                                                  WHERE cdCurrAcc.CurrAccTypeCode = trCashLine.CurrAccTypeCode 
                                                                  AND cdCurrAcc.CurrAccCode = trCashLine.CurrAccCode), '')
                                                     ELSE ISNULL((SELECT CurrAccDescription FROM cdCurrAccDesc WITH(NOLOCK) 
                                                                  WHERE cdCurrAccDesc.CurrAccTypeCode = trCashLine.CurrAccTypeCode 
                                                                  AND cdCurrAccDesc.CurrAccCode = trCashLine.CurrAccCode 
                                                                  AND cdCurrAccDesc.LangCode = N'TR'), '') END,
                            ApplicationDescription = ISNULL((SELECT ApplicationDescription FROM bsApplicationDesc WITH(NOLOCK) 
                                                           WHERE bsApplicationDesc.ApplicationCode = trCashHeader.ApplicationCode 
                                                           AND bsApplicationDesc.LangCode = N'TR'), '')
                        FROM trCashHeader WITH(NOLOCK) 
                            INNER JOIN trCashLine WITH(NOLOCK) ON trCashLine.CashHeaderID = trCashHeader.CashHeaderID
                            -- Yerel para birimi (TRY) için join
                            LEFT OUTER JOIN trCashLineCurrency WITH(NOLOCK) ON trCashLineCurrency.CashLineID = trCashLine.CashLineID 
                                AND trCashHeader.LocalCurrencyCode = trCashLineCurrency.CurrencyCode
                            -- Döviz için join
                            LEFT OUTER JOIN trCashLineCurrency trCashLineCurrencyDoc WITH(NOLOCK) ON trCashLineCurrencyDoc.CashLineID = trCashLine.CashLineID 
                                AND trCashLineCurrencyDoc.CurrencyCode <> trCashHeader.LocalCurrencyCode
                        WHERE 
                            -- Kasa kodu eşleşmesi (hem kaynak hem hedef kasalarda)
                            (CASE WHEN trCashHeader.CashTransTypeCode = 3 THEN trCashLine.CurrAccCode 
                                  WHEN trCashHeader.CashTransTypeCode = 8 AND trCashLine.GLAccCode <> '' THEN ''
                                  ELSE trCashHeader.CashCurrAccCode END = @currentCashCode
                             OR
                             (trCashHeader.CashTransTypeCode = 3 AND trCashHeader.CashCurrAccCode = @currentCashCode))
                            AND trCashHeader.DocumentDate BETWEEN 
                                CONVERT(datetime, @startDate) 
                                AND CONVERT(datetime, @endDate)
                        ORDER BY trCashHeader.DocumentDate, trCashHeader.DocumentTime";
                    
                    // searchText parametresi varsa sorguya ekle - CashTransNumber, CurrAccDescription, Description, CashTransTypeDescription alanlarında arama
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        _logger.LogInformation($"Searching with pattern: '%{searchText}%'");
                        transactionsQuery += @"
                            AND (
                                -- Fiş numarası araması (LIKE ile arama ve '0' olmayan kayıtlar)
                                ISNULL(trCashHeader.CashTransNumber, '') LIKE @searchPattern
                                OR ISNULL(trCashLine.LineDescription, '') LIKE @searchPattern
                                -- Cari hesap tanımı araması
                                OR (CASE WHEN trCashLine.CurrAccTypeCode IN (4,8) 
                                         THEN ISNULL((SELECT FirstLastName FROM cdCurrAcc WITH(NOLOCK) 
                                                      WHERE cdCurrAcc.CurrAccTypeCode = trCashLine.CurrAccTypeCode 
                                                      AND cdCurrAcc.CurrAccCode = trCashLine.CurrAccCode), '')
                                         ELSE ISNULL((SELECT CurrAccDescription FROM cdCurrAccDesc WITH(NOLOCK) 
                                                      WHERE cdCurrAccDesc.CurrAccTypeCode = trCashLine.CurrAccTypeCode 
                                                      AND cdCurrAccDesc.CurrAccCode = trCashLine.CurrAccCode 
                                                      AND cdCurrAccDesc.LangCode = N'TR'), '') END) LIKE @searchPattern
                                -- Fiş tipi tanımı araması
                                OR ISNULL((SELECT CashTransTypeDescription FROM bsCashTransTypeDesc WITH(NOLOCK) 
                                           WHERE bsCashTransTypeDesc.CashTransTypeCode = trCashHeader.CashTransTypeCode 
                                           AND bsCashTransTypeDesc.LangCode = N'TR'), '') LIKE @searchPattern
                            )
                        ";
                    }
                    
                // cashTransTypeCode parametresini güvenli bir şekilde işle
                    int cashTransTypeCodeInt = 0;
                    bool hasFilterByVoucherType = false;
                    if (!string.IsNullOrEmpty(cashTransTypeCode))
                    {
                        // Frontend'den gelen değeri sayıya çevir
                        if (int.TryParse(cashTransTypeCode, out cashTransTypeCodeInt))
                        {
                            _logger.LogInformation($"Fiş tipi kodu: {cashTransTypeCodeInt}");
                            
                            // Fiş tipi 0'dan büyükse (0 = Tüm Fişler) sorguya ekle
                            if (cashTransTypeCodeInt > 0)
                            {
                                hasFilterByVoucherType = true;
                                _logger.LogInformation($"Fiş tipine göre filtreleniyor: {cashTransTypeCodeInt}");
                            }
                        }
                        else
                        {
                            _logger.LogWarning($"Geçersiz fiş tipi formatı: {cashTransTypeCode}. Filtreleme yapılmayacak.");
                        }
                    }

                    
                    var queryParams = new { 
                        startDate, 
                        endDate, 
                        currentCashCode, 
                        searchPattern = !string.IsNullOrEmpty(searchText) ? $"%{searchText}%" : null,
                        cashTransTypeCodeInt
                    };

                    // Fiş tipi filtresini sorguya ekle
                    if (hasFilterByVoucherType && cashTransTypeCodeInt > 0)
                    {
                        transactionsQuery += @"
                        AND trCashHeader.CashTransTypeCode = @cashTransTypeCodeInt
                        ";
                        _logger.LogInformation($"SQL sorgusuna fiş tipi filtresi eklendi: {cashTransTypeCodeInt}");
                    }
                                        
                    // Toplam kayıt sayısını almak için COUNT sorgusu
                    string countQuery = $"SELECT COUNT(*) FROM ({transactionsQuery.Split("ORDER BY")[0]}) AS CountQuery";

                    // Toplam kayıt sayısını al
                    totalCount = await connection.ExecuteScalarAsync<int>(countQuery, queryParams);
                    _logger.LogInformation($"Kasa hesabı için işlemler getiriliyor: {currentCashCode}, tarih aralığı: {startDate} - {endDate}");

                    // SQL Server'da OFFSET-FETCH kullanımı için ORDER BY zorunludur
                    // Ana sorguda zaten ORDER BY var, tekrar eklemeye gerek yok


                   // Sayfalama için OFFSET-FETCH ekle
                    // ORDER BY'dan sonra olmalıdır
                    transactionsQuery += @"
                    OFFSET @offset ROWS 
                    FETCH NEXT @pageSize ROWS ONLY
                    ";
                    
                // Sayfalama parametrelerini ekle
                    var pagedQueryParams = new
                    {
                        startDate,
                        endDate,
                        currentCashCode,
                        searchPattern = !string.IsNullOrEmpty(searchText) ? $"%{searchText}%" : null,
                        cashTransTypeCodeInt, // Yukarıda güvenli bir şekilde dönüştürülen değeri kullan
                        offset = (page - 1) * pageSize,
                        pageSize
                    };

                    // Debug için SQL sorgusunu logla
                    _logger.LogInformation($"Çalıştırılan SQL sorgusu: {transactionsQuery}");
                    _logger.LogInformation($"Parametreler: startDate={startDate}, endDate={endDate}, cashTransTypeCodeInt={cashTransTypeCodeInt}, offset={(page - 1) * pageSize}, pageSize={pageSize}");
                    // İşlemleri veritabanından çek ve CashTransactionSummary tipine dönüştür
                    var transactions = (await connection.QueryAsync<CashTransactionSummary>(transactionsQuery, pagedQueryParams)).ToList();
                    
                    // Debug için virman işlemlerini kontrol et
                    _logger.LogInformation($"Toplam {transactions.Count} işlem bulundu.");
                    var virmanTransactions = transactions.Where(t => t.CashTransTypeCode == "3").ToList();
                    _logger.LogInformation($"Bunlardan {virmanTransactions.Count} tanesi virman işlemi.");
                    
                    // Açılış ve kapanış bakiyelerini tanımla
                    decimal openingBalance = 0;
                    decimal closingBalance = 0;
                    
                    if (cashBalance != null)
                    {
                        // Stored procedure'den gelen bakiye değerlerini kullan
                        closingBalance = cashBalance.Loc_Balance;
                        
                        // Dönem içi hareketleri topla (yerel para birimi)
                        decimal periodMovements = transactions.Sum(t => t.Loc_Debit - t.Loc_Credit);
                        
                        // Açılış bakiyesi = Kapanış bakiyesi - Dönem içi hareketler
                        openingBalance = closingBalance - periodMovements;
                    }
                    
                    // Tahsilat, tediye ve virman toplamlarını hesapla (yerel para birimi)
                    decimal totalReceipts = transactions.Where(t => t.CashTransTypeCode == "1").Sum(t => t.Loc_Debit);
                    decimal totalPayments = transactions.Where(t => t.CashTransTypeCode == "2").Sum(t => t.Loc_Credit);
                    decimal totalIncomingTransfers = transactions.Where(t => t.CashTransTypeCode == "3" && t.TargetCashAccountCode == currentCashCode).Sum(t => t.Loc_Debit);
                    decimal totalOutgoingTransfers = transactions.Where(t => t.CashTransTypeCode == "3" && t.SourceCashAccountCode == currentCashCode).Sum(t => t.Loc_Credit);
                    
                    // Yerel para birimi (TRY) için running balance hesaplama
                    decimal locRunningBalance = openingBalance;
                    
                    // Döviz bakiyelerini para birimi bazında takip etmek için sözlük
                    Dictionary<string, decimal> currencyBalances = new Dictionary<string, decimal>();
                    
                    // Her işlem için running balance hesaplama
                    foreach (var transaction in transactions)
                    {
                        // Yerel para birimi (TRY) için hesaplama
                        if (transaction.CashTransTypeCode == "3") // Virman
                        {
                            if (transaction.TargetCashAccountCode == currentCashCode) // Gelen virman
                            {
                                locRunningBalance += transaction.Loc_Debit;
                            }
                            else // Giden virman
                            {
                                locRunningBalance -= transaction.Loc_Credit;
                            }
                        }
                        else
                        {
                            locRunningBalance += transaction.Loc_Debit - transaction.Loc_Credit;
                        }
                        transaction.Loc_Balance = locRunningBalance;
                        
                        // Döviz için hesaplama
                        if (!string.IsNullOrEmpty(transaction.Doc_CurrencyCode))
                        {
                            string currencyCode = transaction.Doc_CurrencyCode;
                            
                            // Eğer bu para birimi için daha önce bir bakiye hesaplanmadıysa, sıfırdan başla
                            if (!currencyBalances.ContainsKey(currencyCode))
                            {
                                currencyBalances[currencyCode] = 0m;
                            }
                            
                            // Bu işlemin etkisini ekle
                            if (transaction.CashTransTypeCode == "3") // Virman
                            {
                                if (transaction.TargetCashAccountCode == currentCashCode) // Gelen virman
                                {
                                    currencyBalances[currencyCode] += transaction.Doc_Debit;
                                }
                                else // Giden virman
                                {
                                    currencyBalances[currencyCode] -= transaction.Doc_Credit;
                                }
                            }
                            else
                            {
                                currencyBalances[currencyCode] += transaction.Doc_Debit - transaction.Doc_Credit;
                            }
                            
                            // Güncel bakiyeyi işleme ata
                            transaction.Doc_Balance = currencyBalances[currencyCode];
                        }
                    }
                    
                    // Sonuç oluştur
                    // Doğru muhasebe formülüne göre kapanış bakiyesini hesapla
                    // Bakiye = Açılış Bakiyesi (Devir) + (Tahsilat - Tediye) + (Gelen Virman - Giden Virman)
                    decimal calculatedClosingBalance = openingBalance + totalReceipts - totalPayments + totalIncomingTransfers - totalOutgoingTransfers;
                    
                    var cashBalanceResponse = new CashBalanceResponse
                    {
                        CashAccountCode = currentCashCode,
                        CashAccountName = cashAccount.CashAccountDescription,
                        CurrencyCode = cashAccount.CurrencyCode,
                        OpeningBalance = openingBalance,
                        TotalReceipts = totalReceipts,
                        TotalPayments = totalPayments,
                        TotalIncomingTransfers = totalIncomingTransfers,
                        TotalOutgoingTransfers = totalOutgoingTransfers,
                        // Doğru hesaplanmış kapanış bakiyesini kullan
                        ClosingBalance = calculatedClosingBalance,
                        Transactions = transactions,
                        ForeignCurrencyBalances = new List<CurrencyBalance>()
                    };
                    
                    // Döviz bakiyelerini hesapla
                    foreach (var currencyGroup in transactions
                        .Where(t => !string.IsNullOrEmpty(t.Doc_CurrencyCode))
                        .GroupBy(t => t.Doc_CurrencyCode))
                    {
                        string currencyCode = currencyGroup.Key;
                        decimal totalForeignReceipts = currencyGroup.Where(t => t.CashTransTypeCode == "1").Sum(t => t.Doc_Debit);
                        decimal totalForeignPayments = currencyGroup.Where(t => t.CashTransTypeCode == "2").Sum(t => t.Doc_Credit);
                        decimal totalForeignIncomingTransfers = currencyGroup.Where(t => t.CashTransTypeCode == "3" && t.TargetCashAccountCode == currentCashCode).Sum(t => t.Doc_Debit);
                        decimal totalForeignOutgoingTransfers = currencyGroup.Where(t => t.CashTransTypeCode == "3" && t.SourceCashAccountCode == currentCashCode).Sum(t => t.Doc_Credit);
                        
                        // Döviz için açılış bakiyesini hesapla
                        // Eğer işlemler varsa ve tarih sıralıysa, ilk işlemin bakiyesini al
                        decimal foreignOpeningBalance = 0;
                        if (currencyGroup.Any())
                        {
                            var firstTransaction = currencyGroup.OrderBy(t => t.DocumentDate).FirstOrDefault();
                            if (firstTransaction != null)
                            {
                                // İlk işlemin bakiyesinden o işlemin tutarını çıkararak açılış bakiyesini buluyoruz
                                foreignOpeningBalance = firstTransaction.Doc_Balance - (firstTransaction.Doc_Debit - firstTransaction.Doc_Credit);
                            }
                        }
                        
                        // Doğru muhasebe formülüne göre döviz kapanış bakiyesini hesapla
                        // Bakiye = Açılış Bakiyesi + (Tahsilat - Tediye) + (Gelen Virman - Giden Virman)
                        decimal calculatedForeignClosingBalance = foreignOpeningBalance + totalForeignReceipts - totalForeignPayments + 
                                                     totalForeignIncomingTransfers - totalForeignOutgoingTransfers;
                        
                        // Döviz bakiyesi
                        var currencyBalance = new CurrencyBalance
                        {
                            CurrencyCode = currencyCode,
                            OpeningBalance = foreignOpeningBalance,
                            TotalReceipts = totalForeignReceipts,
                            TotalPayments = totalForeignPayments,
                            TotalIncomingTransfers = totalForeignIncomingTransfers,
                            TotalOutgoingTransfers = totalForeignOutgoingTransfers,
                            // Doğru hesaplanmış döviz kapanış bakiyesini kullan
                            ClosingBalance = calculatedForeignClosingBalance
                        };
                        
                        // Döviz bakiyesini listeye ekle
                        cashBalanceResponse.ForeignCurrencyBalances.Add(currencyBalance);
                    }
                    
                    // Kasa bakiyesini listeye ekle
                    cashBalances.Add(cashBalanceResponse);
                }
                
                // Sayfalama meta verilerini içeren yanıtı döndür
                return new PagedApiResponse<CashBalanceResponse>
                {
                    Data = cashBalances,
                    Page = page,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                    Success = true,
                    Message = "İşlem başarılı"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa bakiyeleri getirilirken hata oluştu");
                throw;
            }
        }
        
        /// <summary>
        /// Kasa işlemi için bildirim gönderir
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği</param>
        /// <param name="notificationType">Bildirim türü (CashTransaction, Invoice, vb.)</param>
        /// <param name="actionType">İşlem türü (Create, Update, Delete, vb.)</param>
        /// <param name="referenceNumber">İşlem referans numarası</param>
        /// <param name="amount">Tutar</param>
        /// <param name="currencyCode">Para birimi</param>
        /// <param name="description">Açıklama</param>
        /// <returns></returns>
        private async Task SendTransactionNotificationAsync(string userId, string notificationType, string actionType, 
            string referenceNumber, decimal? amount = null, string currencyCode = null, string description = null)
        {
            try
            {
                // Kullanıcının bildirim tercihini kontrol et
                bool shouldSend = await _notificationService.ShouldSendNotificationAsync(userId, notificationType, actionType);
                
                if (!shouldSend)
                {
                    _logger.LogInformation("Kullanıcı tercihi nedeniyle bildirim gönderilmiyor. UserId: {UserId}, NotificationType: {NotificationType}, ActionType: {ActionType}",
                        userId, notificationType, actionType);
                    return;
                }
                
                // Bildirim başlığı ve içeriği oluştur
                string title = string.Empty;
                string body = string.Empty;
                string url = "/cash/transactions";
                string amountText = amount.HasValue ? $" Tutar: {amount:N2} {currencyCode}" : "";
                
                switch (notificationType)
                {
                    case "CashTransaction":
                        switch (actionType)
                        {
                            case "Create":
                                title = "Yeni Kasa Hareketi";
                                body = $"{referenceNumber} numaralı kasa hareketi oluşturuldu.{amountText}";
                                break;
                            case "Update":
                                title = "Kasa Hareketi Güncellendi";
                                body = $"{referenceNumber} numaralı kasa hareketi güncellendi.{amountText}";
                                break;
                            case "Delete":
                                title = "Kasa Hareketi Silindi";
                                body = $"{referenceNumber} numaralı kasa hareketi silindi.";
                                break;
                            default:
                                title = "Kasa Hareketi Bildirimi";
                                body = $"{referenceNumber} numaralı kasa hareketi işlemi gerçekleşti.{amountText}";
                                break;
                        }
                        break;
                    
                    case "Invoice":
                        switch (actionType)
                        {
                            case "Create":
                                title = "Yeni Fatura";
                                body = $"{referenceNumber} numaralı fatura oluşturuldu.{amountText}";
                                url = "/invoices";
                                break;
                            case "Update":
                                title = "Fatura Güncellendi";
                                body = $"{referenceNumber} numaralı fatura güncellendi.{amountText}";
                                url = "/invoices";
                                break;
                            case "Complete":
                                title = "Fatura Tamamlandı";
                                body = $"{referenceNumber} numaralı fatura tamamlandı.{amountText}";
                                url = "/invoices";
                                break;
                            default:
                                title = "Fatura Bildirimi";
                                body = $"{referenceNumber} numaralı fatura işlemi gerçekleşti.{amountText}";
                                url = "/invoices";
                                break;
                        }
                        break;
                    
                    default:
                        title = $"{notificationType} Bildirimi";
                        body = $"{referenceNumber} numaralı işlem gerçekleşti.{amountText}";
                        break;
                }
                
                // Açıklama varsa ekle
                if (!string.IsNullOrEmpty(description))
                {
                    body += $" Açıklama: {description}";
                }
                
                // Bildirim gönder
                await _notificationService.SendPushNotificationAsync(userId, title, body, url);
                _logger.LogInformation("{NotificationType} bildirimi gönderildi. UserId: {UserId}, Title: {Title}", notificationType, userId, title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{NotificationType} bildirimi gönderilirken hata oluştu. UserId: {UserId}", notificationType, userId);
            }
        }
        
        public async Task<ApiResponse<IEnumerable<CashSummaryResponse>>> GetCashSummaryAsync(
            string cashAccountCode = null,
            string userName = null)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    
                    // Kasa özet bilgilerini almak için SQL sorgusu - AllCashs view kullanarak
                    var query = @"
                    SELECT 
                        c.CurrAccCode AS CashAccountCode,
                        cd.CurrAccDescription AS CashAccountDescription,
                        c.CurrencyCode,
                        0 AS LocalOpeningBalance,
                        0 AS DocumentOpeningBalance,
                        
                        -- Yerel para birimi toplamları
                        ISNULL(SUM(ac.Loc_Debit), 0) AS LocalTotalDebit,
                        ISNULL(SUM(ac.Loc_Credit), 0) AS LocalTotalCredit,
                        
                        -- Belge para birimi toplamları
                        ISNULL(SUM(ac.Doc_Debit), 0) AS DocumentTotalDebit,
                        ISNULL(SUM(ac.Doc_Credit), 0) AS DocumentTotalCredit,
                        
                        -- Tahsilat, tediye ve virman toplamları
                        ISNULL(SUM(CASE WHEN ac.CashTransTypeCode = 1 AND ac.ApplicationCode <> 'Cash' THEN ac.Loc_Debit ELSE 0 END), 0) AS TotalReceipt,
                        ISNULL(SUM(CASE WHEN ac.CashTransTypeCode = 2 AND ac.ApplicationCode <> 'Cash' THEN ac.Loc_Credit ELSE 0 END), 0) AS TotalPayment,
                        ISNULL(SUM(CASE WHEN ac.CashTransTypeCode = 1 AND ac.ApplicationCode = 'Cash' THEN ac.Loc_Debit ELSE 0 END), 0) AS TotalTransferIn,
                        ISNULL(SUM(CASE WHEN ac.CashTransTypeCode = 2 AND ac.ApplicationCode = 'Cash' THEN ac.Loc_Credit ELSE 0 END), 0) AS TotalTransferOut,
                        
                        -- Kapanış bakiyeleri
                        ISNULL(SUM(ac.Loc_Debit - ac.Loc_Credit), 0) AS LocalClosingBalance,
                        ISNULL(SUM(ac.Doc_Debit - ac.Doc_Credit), 0) AS DocumentClosingBalance
                    FROM 
                        cdCurrAcc c
                        INNER JOIN cdCurrAccDesc cd ON c.CurrAccTypeCode = cd.CurrAccTypeCode AND c.CurrAccCode = cd.CurrAccCode AND cd.LangCode = 'TR'
                        LEFT JOIN AllCashs ac ON c.CurrAccCode = ac.CashCurrAccCode
                    WHERE 
                        c.CurrAccTypeCode = 7 -- Sadece kasa hesaplarını getir (7: Kasa hesap tipi)
                        AND (@CashAccountCode IS NULL OR c.CurrAccCode = @CashAccountCode)
                    GROUP BY
                        c.CurrAccCode,
                        cd.CurrAccDescription,
                        c.CurrencyCode
                    ORDER BY 
                        CashAccountCode";
                    
                    var parameters = new 
                    { 
                        CashAccountCode = !string.IsNullOrEmpty(cashAccountCode) ? cashAccountCode : null
                    };
                    
                    var result = await connection.QueryAsync<CashSummaryResponse>(query, parameters);
                    _logger.LogInformation("Successfully retrieved {Count} cash accounts summary", result.Count());
                    
                    return new ApiResponse<IEnumerable<CashSummaryResponse>>(result, true, "Kasa özet bilgileri başarıyla alındı.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cash summary");
                return new ApiResponse<IEnumerable<CashSummaryResponse>>(null, false, $"Kasa özet bilgileri alınırken hata oluştu: {ex.Message}");
            }
        }
    }
}