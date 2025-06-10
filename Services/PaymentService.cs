using System;
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
using Newtonsoft.Json;

namespace ErpMobile.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IConfiguration configuration, ILogger<PaymentService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Fatura için peşin ödeme işlemi yapar
        /// </summary>
        public async Task<CashPaymentResponse> CreateCashPaymentForInvoiceAsync(CashPaymentRequest request, string userName)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
            await connection.OpenAsync();
            
            using var transaction = connection.BeginTransaction();
            
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

                // Cash Header ekle
                await InsertCashHeaderAsync(connection, transaction, cashHeaderId, request, cashNumber);

                // Debit Header ekle
                await InsertDebitHeaderAsync(connection, transaction, debitHeaderId, request, debitNumber, cashHeaderId, "");

                // Payment Header ekle
                await InsertPaymentHeaderAsync(connection, transaction, paymentHeaderId, request, paymentNumber);

                int lineNumber = 0;
                foreach (var paymentRow in request.PaymentRows)
                {
                    lineNumber++;

                    // Line ID'leri oluştur
                    var cashLineId = Guid.NewGuid();
                    var debitLineId = Guid.NewGuid();
                    var paymentLineId = Guid.NewGuid();

                    // Cash Line ekle
                    await InsertCashLineAsync(connection, transaction, cashLineId, cashHeaderId, request, paymentRow, lineNumber);

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

        private async Task<string> GetCashNumberAsync(SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                var parameters = new DynamicParameters();
                // Stored procedure'un beklediği parametreleri ekle
                parameters.Add("CompanyCode", 1);
                parameters.Add("CashTransTypeCode", 1);
                
                // Bu stored procedure doğrudan sonuç döndürüyor, çıkış parametresi yok
                var result = await connection.QueryFirstOrDefaultAsync<string>("sp_LastRefNumCashTrans", parameters, transaction, commandType: CommandType.StoredProcedure);
                
                Console.WriteLine($"[SP CALL] sp_LastRefNumCashTrans - Generated CashTransNumber: {result}");
                
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to generate CashTransNumber: {ex.Message}");
                // Hata durumunda manuel bir numara oluştur
                var fallbackNumber = $"";
                
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

        private async Task InsertCashHeaderAsync(SqlConnection connection, SqlTransaction transaction, Guid cashHeaderId, CashPaymentRequest request, string cashTransNumber)
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
                CashCurrAccCode = request.CashCurrAccCode,
                CashCurrAccTypeCode = 7, // Kasa hesap tipi
                StoreCode = "",
                GLTypeCode = "",
                DocCurrencyCode = request.DocCurrencyCode,
                LocalCurrencyCode = "TRY",
                ExchangeRate = 1,
                ImportFileNumber = "", // SQL izlemesinde görülen alan
                ExportFileNumber = "", // SQL izlemesinde görülen alan
                IsPostingApproved = true, // SQL izlemesinde görülen alan
                JournalDate = DateTime.Today,
                IsPostingJournal = false,
                OfficeCode = "M",
                ApplicationCode = "Invoi",
                ApplicationID = request.InvoiceId,
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

            var parameters = new
            {
                CashLineID = cashLineId,
                CurrencyCode = paymentRow.CurrencyCode,
                RelationCurrencyCode = paymentRow.CurrencyCode, // İşlem para birimi
                Debit = paymentRow.Amount,
                Credit = 0,
                ExchangeRate = paymentRow.ExchangeRate,
                CreatedUserName = "UZK  Uzak",
                LastUpdatedUserName = "UZK  Uzak"
            };

            Console.WriteLine("\n[SQL MERGE] trCashLineCurrency:");
            Console.WriteLine(sql);
            Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parameters)}");

            await connection.ExecuteAsync(sql, parameters, transaction);
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
                ApplicationID = request.InvoiceId,
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
            var sql = @"
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

            var parameters = new
            {
                DebitLineID = debitLineId,
                CurrencyCode = paymentRow.CurrencyCode,
                ExchangeRate = paymentRow.ExchangeRate,
                RelationCurrencyCode = "TRY", // Yerel para birimi
                Debit = paymentRow.Amount, // Tahsilat işlemi olduğu için borç kısmına yazılır
                Credit = 0, // Tahsilat işlemi olduğu için alacak kısmı sıfır
                CreatedUserName = "UZK  Uzak",
                LastUpdatedUserName = "UZK  Uzak"
            };

            Console.WriteLine("\n[SQL MERGE] trDebitLineCurrency:");
            Console.WriteLine(sql);
            Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parameters)}");

            await connection.ExecuteAsync(sql, parameters, transaction);
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
                LineDescription = isDebit ? "Toptan Satış - Fatura" : "MERKEZ TL KASA(101)",
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
                BaseApplicationID = Guid.Empty,
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
                ApplicationID = request.InvoiceId,
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
                _logger.LogInformation("[PAYMENT] InsertPaymentLineCurrencyAsync başlıyor: PaymentLineID={PaymentLineID}, CurrencyCode={CurrencyCode}, DocCurrencyCode={DocCurrencyCode}", 
                    paymentLineId, paymentRow.CurrencyCode, docCurrencyCode);
                    
                var sql = @"
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

                var parameters = new
                {
                    PaymentLineID = paymentLineId,
                    CurrencyCode = paymentRow.CurrencyCode,
                    Payment = paymentRow.Amount,
                    ExchangeRate = paymentRow.ExchangeRate,
                    RelationCurrencyCode = docCurrencyCode, // Fatura para birimi (belge para birimi)
                    CreatedUserName = "UZK  Uzak",
                    LastUpdatedUserName = "UZK  Uzak"
                };

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[SQL MERGE] trPaymentLineCurrency:");
                Console.WriteLine(sql);
                Console.WriteLine($"Parameters: {JsonConvert.SerializeObject(parameters)}");
                Console.ResetColor();

                await connection.ExecuteAsync(sql, parameters, transaction);
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
    }
}