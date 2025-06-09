using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Payment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // 1. Yeni CashHeader GUID oluştur
                            var cashHeaderId = Guid.NewGuid();
                            var cashLineId = Guid.NewGuid();
                            var paymentHeaderId = Guid.NewGuid();
                            var paymentLineId = Guid.NewGuid();
                            var currAccBookId = Guid.NewGuid();

                            // 2. Referans numarası al
                            var refNumberSql = "EXEC sp_LastRefNumCash @CompanyCode=1, @OfficeCode=@OfficeCode, @StoreCode=@StoreCode, @PosTerminalID=0";
                            var refNumber = await connection.QueryFirstOrDefaultAsync<string>(
                                refNumberSql, 
                                new { 
                                    OfficeCode = request.OfficeCode,
                                    StoreCode = request.StoreCode ?? ""
                                }, 
                                transaction);

                            // 3. Kasa başlık kaydı oluştur
                            var insertCashHeaderSql = @"
                            INSERT INTO [trCashHeader]
                            ([trCashHeader].[CashHeaderID]                                                   
                            ,[trCashHeader].[CashTransTypeCode]                                              
                            ,[trCashHeader].[CashTransNumber]                                                
                            ,[trCashHeader].[DocumentDate]                                                   
                            ,[trCashHeader].[DocumentTime]                                                   
                            ,[trCashHeader].[DocumentNumber]                                                 
                            ,[trCashHeader].[RefNumber]                                                      
                            ,[trCashHeader].[PosTerminalID]                                                  
                            ,[trCashHeader].[Description]                                                    
                            ,[trCashHeader].[CashCurrAccCode]                                                
                            ,[trCashHeader].[CashCurrAccTypeCode]                                            
                            ,[trCashHeader].[StoreCode]                                                      
                            ,[trCashHeader].[GLTypeCode]                                                     
                            ,[trCashHeader].[DocCurrencyCode]                                                
                            ,[trCashHeader].[LocalCurrencyCode]                                              
                            ,[trCashHeader].[ExchangeRate]                                                   
                            ,[trCashHeader].[ImportFileNumber]                                               
                            ,[trCashHeader].[ExportFileNumber]                                               
                            ,[trCashHeader].[IsPostingApproved]                                              
                            ,[trCashHeader].[JournalDate]                                                    
                            ,[trCashHeader].[IsPostingJournal]                                               
                            ,[trCashHeader].[OfficeCode]                                                     
                            ,[trCashHeader].[ApplicationCode]                                                
                            ,[trCashHeader].[ApplicationID]                                                  
                            ,[trCashHeader].[IsCompleted]                                                    
                            ,[trCashHeader].[IsPrinted]                                                      
                            ,[trCashHeader].[IsLocked]                                                       
                            ,[trCashHeader].[CompanyCode]                                                    
                            ,[trCashHeader].[CreatedUserName]                                                
                            ,[trCashHeader].[CreatedDate]                                                    
                            ,[trCashHeader].[LastUpdatedUserName]                                            
                            ,[trCashHeader].[LastUpdatedDate]                                                
                            )
                            VALUES (@CashHeaderID, @CashTransTypeCode, @CashTransNumber, @DocumentDate, @DocumentTime,
                            @DocumentNumber, @RefNumber, @PosTerminalID, @Description, @CashCurrAccCode,
                            @CashCurrAccTypeCode, @StoreCode, @GLTypeCode, @DocCurrencyCode, @LocalCurrencyCode,
                            @ExchangeRate, @ImportFileNumber, @ExportFileNumber, @IsPostingApproved, @JournalDate,
                            @IsPostingJournal, @OfficeCode, @ApplicationCode, @ApplicationID, @IsCompleted,
                            @IsPrinted, @IsLocked, @CompanyCode, @UserName, GETDATE(), @UserName, GETDATE())";

                            var today = DateTime.Now;
                            var documentTime = TimeSpan.FromHours(today.Hour) + TimeSpan.FromMinutes(today.Minute) + TimeSpan.FromSeconds(today.Second);

                            await connection.ExecuteAsync(insertCashHeaderSql, new
                            {
                                CashHeaderID = cashHeaderId,
                                DocumentDate = today.Date,
                                DocumentTime = documentTime,
                                DocumentNumber = "0",
                                CashTransNumber = "",
                                PosTerminalID = 0,
                                CashTransTypeCode = 1,
                                CashCurrAccCode = request.CurrAccCode,
                                CashCurrAccTypeCode = request.CurrAccTypeCode,
                                Description = request.Description ?? "",
                                RefNumber = request.InvoiceHeaderID,
                                DueDate = today.Date,
                                StoreCode = request.StoreCode ?? "",
                                GLTypeCode = request.GLTypeCode ?? "",
                                DocCurrencyCode = request.CurrencyCode,
                                LocalCurrencyCode = "TRY",
                                ExchangeRate = 1.0,
                                ImportFileNumber = "",
                                ExportFileNumber = "",
                                IsPostingApproved = false,
                                JournalDate = today.Date,
                                IsPostingJournal = false,
                                OfficeCode = request.OfficeCode,
                                ApplicationCode = "",
                                ApplicationID = 0,
                                IsCompleted = true,
                                IsPrinted = false,
                                IsLocked = false,
                                CompanyCode = 1,
                                CreatedUserName = userName,
                                CreatedDate = today,
                                LastUpdatedUserName = userName,
                                LastUpdatedDate = today,
                                UserName = userName
                            }, transaction);

                            // 4. Kasa satır kaydı oluştur
                            var insertCashLineSql = @"
                             INSERT INTO [trCashLine]
                            ([trCashLine].[CashLineID]                                                       
                            ,[trCashLine].[CurrAccCode]                                                      
                            ,[trCashLine].[CurrAccTypeCode]                                                  
                            ,[trCashLine].[SubCurrAccID]                                                     
                            ,[trCashLine].[ContactID]                                                        
                            ,[trCashLine].[LineDescription]                                                  
                            ,[trCashLine].[DocCurrencyCode]                                                  
                            ,[trCashLine].[CashHeaderID]                                                     
                            ,[trCashLine].[SortOrder]                                                        
                            ,[trCashLine].[EmployeePayTypeCode]                                              
                            ,[trCashLine].[GLAccCode]                                                        
                            ,[trCashLine].[CompanyCode]                                                      
                            ,[trCashLine].[CostCenterCode]                                                   
                            ,[trCashLine].[GLTypeCode]                                                       
                            ,[trCashLine].[ImportFileNumber]                                                 
                            ,[trCashLine].[ExportFileNumber]                                                 
                            ,[trCashLine].[CurrAccCurrencyCode]                                              
                            ,[trCashLine].[CurrAccExchangeRate]                                              
                            ,[trCashLine].[CurrAccAmount]                                                    
                            ,[trCashLine].[CreatedUserName]                                                  
                            ,[trCashLine].[CreatedDate]                                                      
                            ,[trCashLine].[LastUpdatedUserName]                                              
                            ,[trCashLine].[LastUpdatedDate]                                                  
                            )
                            VALUES (@CashLineID, @CurrAccCode, @CurrAccTypeCode, @SubCurrAccID, @ContactID,
                            @LineDescription, @DocCurrencyCode, @CashHeaderID, @SortOrder, @EmployeePayTypeCode,
                            @GLAccCode, @CompanyCode, @CostCenterCode, @GLTypeCode, @ImportFileNumber, @ExportFileNumber,
                            @CurrAccCurrencyCode, @CurrAccExchangeRate, @CurrAccAmount, @UserName, GETDATE(), @UserName, GETDATE())";

                            await connection.ExecuteAsync(insertCashLineSql, new
                            {
                                CashLineID = cashLineId,
                                CurrAccCode = request.CurrAccCode,
                                CurrAccTypeCode = request.CurrAccTypeCode,
                                SubCurrAccID = (Guid?)null,
                                ContactID = (Guid?)null,
                                LineDescription = request.Description ?? "",
                                DocCurrencyCode = request.CurrencyCode,
                                CashHeaderID = cashHeaderId,
                                SortOrder = 1,
                                EmployeePayTypeCode = (byte?)null,
                                GLAccCode = "",
                                CompanyCode = 1,
                                CostCenterCode = "",
                                GLTypeCode = request.GLTypeCode ?? "",
                                ImportFileNumber = "",
                                ExportFileNumber = "",
                                CurrAccCurrencyCode = request.CurrencyCode,
                                CurrAccExchangeRate = 1.0,
                                CurrAccAmount = request.Amount,
                                UserName = userName
                            }, transaction);

                            // 5. Kasa satır para birimi kaydı oluştur
                            var insertCashLineCurrencySql = @"
                            INSERT INTO [trCashLineCurrency]
                            ([CashLineID], [CurrencyCode], [ExchangeRate], [RelationCurrencyCode], [Payment],
                            [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                            VALUES (@CashLineID, @CurrencyCode, @ExchangeRate, @RelationCurrencyCode, @Payment,
                            @UserName, GETDATE(), @UserName, GETDATE())";

                            await connection.ExecuteAsync(insertCashLineCurrencySql, new
                            {
                                CashLineID = cashLineId,
                                CurrencyCode = request.CurrencyCode,
                                ExchangeRate = 1.0,
                                RelationCurrencyCode = request.CurrencyCode,
                                Payment = request.Amount,
                                UserName = userName
                            }, transaction);

                            // 6. Cari hesap kaydı oluştur
                            var insertCurrAccBookSql = @"
                            INSERT INTO [trCurrAccBook]
                            ([CurrAccBookID], [DocumentDate], [DocumentTime], [BookNumber], [DocumentNumber],
                            [RefNumber], [DueDate], [CurrAccCode], [CurrAccTypeCode], [Description],
                            [DocCurrencyCode], [LocalCurrencyCode], [ExchangeRate], [BaseApplicationCode], [BaseApplicationID],
                            [CompanyCode], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                            VALUES (@CurrAccBookID, @DocumentDate, @DocumentTime, @BookNumber, @DocumentNumber,
                            @RefNumber, @DueDate, @CurrAccCode, @CurrAccTypeCode, @Description,
                            @DocCurrencyCode, @LocalCurrencyCode, @ExchangeRate, @BaseApplicationCode, @BaseApplicationID,
                            @CompanyCode, @UserName, GETDATE(), @UserName, GETDATE())";

                            await connection.ExecuteAsync(insertCurrAccBookSql, new
                            {
                                CurrAccBookID = currAccBookId,
                                DocumentDate = today.Date,
                                DocumentTime = documentTime,
                                BookNumber = refNumber,
                                DocumentNumber = "0",
                                RefNumber = refNumber,
                                DueDate = today.Date,
                                CurrAccCode = request.CurrAccCode,
                                CurrAccTypeCode = request.CurrAccTypeCode,
                                Description = request.Description ?? "",
                                DocCurrencyCode = request.CurrencyCode,
                                LocalCurrencyCode = "TRY",
                                ExchangeRate = 1.0,
                                BaseApplicationCode = "Cash",
                                BaseApplicationID = cashHeaderId,
                                CompanyCode = 1,
                                UserName = userName
                            }, transaction);

                            // 7. Cari hesap para birimi kaydı oluştur
                            var insertCurrAccBookCurrencySql = @"
                            INSERT INTO [trCurrAccBookCurrency]
                            ([CurrAccBookID], [CurrencyCode], [ExchangeRate], [RelationCurrencyCode], [Amount],
                            [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                            VALUES (@CurrAccBookID, @CurrencyCode, @ExchangeRate, @RelationCurrencyCode, @Amount,
                            @UserName, GETDATE(), @UserName, GETDATE())";

                            await connection.ExecuteAsync(insertCurrAccBookCurrencySql, new
                            {
                                CurrAccBookID = currAccBookId,
                                CurrencyCode = request.CurrencyCode,
                                ExchangeRate = 1.0,
                                RelationCurrencyCode = request.CurrencyCode,
                                Amount = request.Amount,
                                UserName = userName
                            }, transaction);

                            // 8. Ödeme başlık kaydı oluştur
                            var paymentRefNumberSql = "EXEC sp_LastRefNumPayment @CompanyCode=1, @OfficeCode=@OfficeCode, @StoreCode=@StoreCode, @PosTerminalID=0";
                            var paymentRefNumber = await connection.QueryFirstOrDefaultAsync<string>(
                                paymentRefNumberSql, 
                                new { 
                                    OfficeCode = request.OfficeCode,
                                    StoreCode = request.StoreCode ?? ""
                                }, 
                                transaction);

                            var insertPaymentHeaderSql = @"
                            INSERT INTO [trPaymentHeader]
                            ([PaymentHeaderID], [DocumentDate], [DocumentTime], [PaymentNumber], [DocumentNumber],
                            [RefNumber], [DueDate], [StoreCode], [CurrAccCode], [CurrAccTypeCode],
                            [Description], [GLTypeCode], [DocCurrencyCode], [LocalCurrencyCode], [ExchangeRate],
                            [OfficeCode], [ApplicationCode], [ApplicationID], [IsCompleted], [IsPrinted],
                            [IsLocked], [CompanyCode], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                            VALUES (@PaymentHeaderID, @DocumentDate, @DocumentTime, @PaymentNumber, @DocumentNumber,
                            @RefNumber, @DueDate, @StoreCode, @CurrAccCode, @CurrAccTypeCode,
                            @Description, @GLTypeCode, @DocCurrencyCode, @LocalCurrencyCode, @ExchangeRate,
                            @OfficeCode, @ApplicationCode, @ApplicationID, @IsCompleted, @IsPrinted,
                            @IsLocked, @CompanyCode, @UserName, GETDATE(), @UserName, GETDATE())";

                            await connection.ExecuteAsync(insertPaymentHeaderSql, new
                            {
                                PaymentHeaderID = paymentHeaderId,
                                DocumentDate = today.Date,
                                DocumentTime = documentTime,
                                PaymentNumber = paymentRefNumber,
                                DocumentNumber = "0",
                                RefNumber = paymentRefNumber,
                                DueDate = today.Date,
                                StoreCode = request.StoreCode ?? "",
                                CurrAccCode = request.CurrAccCode,
                                CurrAccTypeCode = request.CurrAccTypeCode,
                                Description = request.Description ?? "",
                                GLTypeCode = request.GLTypeCode ?? "",
                                DocCurrencyCode = request.CurrencyCode,
                                LocalCurrencyCode = "TRY",
                                ExchangeRate = 1.0,
                                OfficeCode = request.OfficeCode,
                                ApplicationCode = "Invoi",
                                ApplicationID = request.InvoiceHeaderID,
                                IsCompleted = true,
                                IsPrinted = false,
                                IsLocked = false,
                                CompanyCode = 1,
                                UserName = userName
                            }, transaction);

                            // 9. Ödeme satır kaydı oluştur
                            var insertPaymentLineSql = @"
                            INSERT INTO [trPaymentLine]
                            ([PaymentLineID], [PaymentTypeCode], [LineDescription], [DocCurrencyCode],
                            [CashLineID], [PaymentHeaderID], [SortOrder], [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                            VALUES (@PaymentLineID, @PaymentTypeCode, @LineDescription, @DocCurrencyCode,
                            @CashLineID, @PaymentHeaderID, @SortOrder, @UserName, GETDATE(), @UserName, GETDATE())";

                            await connection.ExecuteAsync(insertPaymentLineSql, new
                            {
                                PaymentLineID = paymentLineId,
                                PaymentTypeCode = 1, // Nakit ödeme
                                LineDescription = request.Description ?? "",
                                DocCurrencyCode = request.CurrencyCode,
                                CashLineID = cashLineId,
                                PaymentHeaderID = paymentHeaderId,
                                SortOrder = 1,
                                UserName = userName
                            }, transaction);

                            // 10. Ödeme satır para birimi kaydı oluştur
                            var insertPaymentLineCurrencySql = @"
                            INSERT INTO [trPaymentLineCurrency]
                            ([PaymentLineID], [CurrencyCode], [ExchangeRate], [RelationCurrencyCode], [Payment],
                            [CreatedUserName], [CreatedDate], [LastUpdatedUserName], [LastUpdatedDate])
                            VALUES (@PaymentLineID, @CurrencyCode, @ExchangeRate, @RelationCurrencyCode, @Payment,
                            @UserName, GETDATE(), @UserName, GETDATE())";

                            await connection.ExecuteAsync(insertPaymentLineCurrencySql, new
                            {
                                PaymentLineID = paymentLineId,
                                CurrencyCode = request.CurrencyCode,
                                ExchangeRate = 1.0,
                                RelationCurrencyCode = request.CurrencyCode,
                                Payment = request.Amount,
                                UserName = userName
                            }, transaction);

                            // 11. Fatura özniteliklerini kasa özniteliklerine kopyala
                            if (request.Attributes != null && request.Attributes.Count > 0)
                            {
                                var insertCashAttributesSql = @"
                                INSERT INTO tpCashATAttribute 
                                (CashHeaderID, AttributeTypeCode, AttributeCode, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate)
                                VALUES (@CashHeaderID, @AttributeTypeCode, @AttributeCode, @UserName, GETDATE(), @UserName, GETDATE())";

                                foreach (var attr in request.Attributes)
                                {
                                    await connection.ExecuteAsync(insertCashAttributesSql, new
                                    {
                                        CashHeaderID = cashHeaderId,
                                        AttributeTypeCode = attr.AttributeTypeCode,
                                        AttributeCode = attr.AttributeCode,
                                        UserName = userName
                                    }, transaction);
                                }
                            }
                            else
                            {
                                // Fatura özniteliklerini kasa özniteliklerine kopyala
                                var copyInvoiceAttributesSql = @"
                                INSERT INTO tpCashATAttribute 
                                (CashHeaderID, AttributeTypeCode, AttributeCode, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate)
                                SELECT @CashHeaderID, AttributeTypeCode, AttributeCode, @UserName, GETDATE(), @UserName, GETDATE()
                                FROM tpInvoiceATAttribute WITH(NOLOCK)
                                WHERE InvoiceHeaderID = @InvoiceHeaderID";

                                await connection.ExecuteAsync(copyInvoiceAttributesSql, new
                                {
                                    CashHeaderID = cashHeaderId,
                                    InvoiceHeaderID = request.InvoiceHeaderID,
                                    UserName = userName
                                }, transaction);
                            }

                            // 12. Kasa özniteliklerini ödeme özniteliklerine kopyala
                            var copyPaymentAttributesSql = @"
                            INSERT INTO tpPaymentATAttribute 
                            (PaymentHeaderID, AttributeTypeCode, AttributeCode, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate)
                            SELECT @PaymentHeaderID, AttributeTypeCode, AttributeCode, @UserName, GETDATE(), @UserName, GETDATE()
                            FROM tpCashATAttribute WITH(NOLOCK)
                            WHERE CashHeaderID = @CashHeaderID";

                            await connection.ExecuteAsync(copyPaymentAttributesSql, new
                            {
                                PaymentHeaderID = paymentHeaderId,
                                CashHeaderID = cashHeaderId,
                                UserName = userName
                            }, transaction);

                            // 13. Kasa özniteliklerini cari hesap özniteliklerine kopyala
                            var copyCurrAccBookAttributesSql = @"
                            INSERT INTO tpCurrAccBookATAttribute 
                            (CurrAccBookID, AttributeTypeCode, AttributeCode, CreatedUserName, CreatedDate, LastUpdatedUserName, LastUpdatedDate)
                            SELECT @CurrAccBookID, AttributeTypeCode, AttributeCode, @UserName, GETDATE(), @UserName, GETDATE()
                            FROM tpCashATAttribute WITH(NOLOCK)
                            WHERE CashHeaderID = @CashHeaderID";

                            await connection.ExecuteAsync(copyCurrAccBookAttributesSql, new
                            {
                                CurrAccBookID = currAccBookId,
                                CashHeaderID = cashHeaderId,
                                UserName = userName
                            }, transaction);

                            transaction.Commit();

                            return new CashPaymentResponse
                            {
                                Success = true,
                                Message = "Peşin ödeme başarıyla kaydedildi",
                                CashHeaderID = cashHeaderId,
                                PaymentHeaderID = paymentHeaderId,
                                PaymentNumber = paymentRefNumber,
                                RefNumber = refNumber
                            };
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            _logger.LogError(ex, "Peşin ödeme oluşturulurken hata oluştu");
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Peşin ödeme oluşturulurken hata oluştu");
                return new CashPaymentResponse
                {
                    Success = false,
                    Message = $"Peşin ödeme oluşturulurken hata oluştu: {ex.Message}"
                };
            }
        }
    }
}