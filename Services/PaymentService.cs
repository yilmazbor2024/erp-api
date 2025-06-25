using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models;
using ErpMobile.Api.Models.Payment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Repositories.CashAccount;
using Newtonsoft.Json;

namespace ErpMobile.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentService> _logger;
        private readonly ICashAccountRepository _cashAccountRepository;
        private Dictionary<string, string> _cashAccountDescriptions;

        public PaymentService(IConfiguration configuration, ILogger<PaymentService> logger, ICashAccountRepository cashAccountRepository)
        {
            _configuration = configuration;
            _logger = logger;
            _cashAccountRepository = cashAccountRepository;
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
                string cashNumber = await GetCashNumberAsync(connection, transaction);
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
                    string currencyCashNumber = await GetCashNumberAsync(connection, transaction, currencyCode);
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
                    await MarkInvoiceAsCompletedAsync(request.InvoiceHeaderID);
                    Console.WriteLine($"\n[INFO] Fatura {request.InvoiceHeaderID} tamamlandı olarak işaretlendi");
                }
                else if (request.InvoiceId != Guid.Empty)
                {
                    await MarkInvoiceAsCompletedAsync(request.InvoiceId.ToString());
                    Console.WriteLine($"\n[INFO] Fatura {request.InvoiceId} tamamlandı olarak işaretlendi");
                }
                
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
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[ERROR] Exception occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                Console.ResetColor();
                
                _logger.LogError(ex, "Nakit tahsilat işlemi sırasında hata oluştu: {Message}", ex.Message);
                
                return new CashPaymentResponse
                {
                    Success = false,
                    Message = $"Hata: {ex.Message}"
                };
            }
        }

        private async Task<string> GetCashNumberAsync(SqlConnection connection, SqlTransaction transaction, string currencyCode = null)
        {
            try
            {
                var parameters = new DynamicParameters();
                // Stored procedure'un beklediği parametreleri ekle
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
        /// Faturayı tamamlandı olarak işaretler
        /// </summary>
        /// <param name="invoiceHeaderId">Fatura başlık ID'si</param>
        /// <returns>İşlem sonucu</returns>
        private async Task MarkInvoiceAsCompletedAsync(string invoiceHeaderId)
        {
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                await connection.OpenAsync();

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
                parameters.Add("@UserName", "System");

                await connection.ExecuteAsync(sql, parameters);

                _logger.LogInformation("[INVOICE] Fatura tamamlandı olarak işaretlendi: {InvoiceHeaderID}", invoiceHeaderId);
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
                    case "101":
                        return "MERKEZ TL KASA";
                    case "102":
                        return "MERKEZ USD KASA";
                    case "103":
                        return "MERKEZ EUR KASA";
                    case "104":
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
    }
}