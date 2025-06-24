using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Invoice;
using ErpMobile.Api.Repositories.Invoice;
using ErpApi.Services;
using ErpApi.Services.Interfaces;
using ErpApi.Models.Payments;

namespace ErpMobile.Api.Services.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ILogger<InvoiceService> _logger;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICreditPaymentService _creditPaymentService;

        public InvoiceService(
            ILogger<InvoiceService> logger,
            IInvoiceRepository invoiceRepository,
            ICreditPaymentService creditPaymentService)
        {
            _logger = logger;
            _invoiceRepository = invoiceRepository;
            _creditPaymentService = creditPaymentService;
        }

             // Fatura numarası oluşturan, "1-{processCode}-7-" sabit ön ek kuralına göre çalışan metot
            public async Task<ApiResponse<string>> GenerateInvoiceNumberAsync(string processCode)
            {
                try
                {
                    if (string.IsNullOrEmpty(processCode))
                    {
                        return new ApiResponse<string> { Success = false, Message = "Process kodu boş olamaz" };
                    }

                    // YENİ KURAL: Ön ek her zaman "1-{processCode}-7" formatında oluşturulur.
                    string newPrefix = $"1-{processCode}-7";

                    var lastInvoiceNumber = await _invoiceRepository.GetLastInvoiceNumberByProcessCodeAsync(processCode);
                    
                    string newInvoiceNumber;

                    // EĞER HİÇ FATURA YOKSA
                    if (string.IsNullOrEmpty(lastInvoiceNumber))
                    {
                        // YENİ KURAL: İlk fatura bu sabit formatla ve 1 ile başlar.
                        newInvoiceNumber = $"{newPrefix}-1"; // Sonuç: "1-WS-7-1"
                    }
                    else // EĞER FATURA VARSA
                    {
                        var parts = lastInvoiceNumber.Split('-');
                        
                        // Sadece en sondaki sayıyı almamız yeterli.
                        if (parts.Length > 0 && int.TryParse(parts[parts.Length - 1], out int lastNumber))
                        {
                            // Sabit ön eki ve artırılmış son sayıyı birleştir.
                            newInvoiceNumber = $"{newPrefix}-{lastNumber + 1}"; // Sonuç: "1-WS-7-222"
                        }
                        else
                        {
                            // Format bozuksa, seriyi kurala göre yeniden başlat.
                            newInvoiceNumber = $"{newPrefix}-1"; 
                        }
                    }
                    // Gelen isteği logla
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n===== SERVICE: YENİ FATURA NUMRASI STOLUŞTUARTED =====\n");
                    Console.WriteLine($"Yeni lastInvoiceNumber: {lastInvoiceNumber}");
                    Console.WriteLine($"Yeni newPrefix: {newPrefix}");
                    Console.WriteLine($"Yeni Fatura Numarası: {newInvoiceNumber}");
                    Console.ResetColor();
                    return new ApiResponse<string>
                    {
                        Success = true,
                        Message = "Fatura numarası başarıyla oluşturuldu",
                        Data = newInvoiceNumber
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Fatura numarası oluşturulurken hata oluştu. Process Code: {ProcessCode}", processCode);
                    return new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Fatura numarası oluşturulurken bir hata oluştu: " + ex.Message
                    };
                }
            }
        // Toptan satış faturaları
        public async Task<ApiResponse<InvoiceListResult>> GetWholesaleInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                var (items, totalCount) = await _invoiceRepository.GetWholesaleInvoicesAsync(request);
                
                if (items == null || items.Count == 0)
                {
                    return new ApiResponse<InvoiceListResult>
                    {
                        Success = true,
                        Message = "Toptan satış faturası bulunamadı",
                        Data = new InvoiceListResult
                        {
                            Items = new List<InvoiceHeaderModel>(),
                            TotalCount = 0,
                            Page = request.Page,
                            PageSize = request.PageSize
                        }
                    };
                }
                
                return new ApiResponse<InvoiceListResult>
                {
                    Success = true,
                    Message = "Toptan satış faturaları başarıyla getirildi",
                    Data = new InvoiceListResult
                    {
                        Items = items,
                        TotalCount = totalCount,
                        Page = request.Page,
                        PageSize = request.PageSize
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan satış faturaları getirilirken hata oluştu");
                return new ApiResponse<InvoiceListResult>
                {
                    Success = false,
                    Message = "Toptan satış faturaları getirilirken bir hata oluştu: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<InvoiceHeaderModel>> GetWholesaleInvoiceByIdAsync(int invoiceHeaderId)
        {
            try
            {
                var invoice = await _invoiceRepository.GetWholesaleInvoiceByIdAsync(invoiceHeaderId);
                
                if (invoice == null)
                {
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = $"Fatura bulunamadı. ID: {invoiceHeaderId}"
                    };
                }
                
                return new ApiResponse<InvoiceHeaderModel>
                {
                    Success = true,
                    Message = "Fatura başarıyla getirildi",
                    Data = invoice
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatura getirilirken hata oluştu. ID: {invoiceHeaderId}");
                return new ApiResponse<InvoiceHeaderModel>
                {
                    Success = false,
                    Message = "Fatura getirilirken bir hata oluştu: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<InvoiceHeaderModel>> CreateWholesaleInvoiceAsync(CreateInvoiceRequest request)
        {
            try
            {
                // Gelen isteği logla
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n===== SERVICE: CreateWholesaleInvoiceAsync STARTED =====\n");
                Console.WriteLine($"Gelen İstek: {System.Text.Json.JsonSerializer.Serialize(request, new System.Text.Json.JsonSerializerOptions { WriteIndented = true })}");
                Console.ResetColor();
                
                // Fatura numarası kontrolü ve otomatik oluşturma
                if (string.IsNullOrEmpty(request.InvoiceNumber))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n===== SERVICE: Fatura numarası otomatik oluşturuluyor =====\n");
                    Console.ResetColor();
                    
                    // Fatura numarası otomatik olarak oluşturuluyor
                    var invoiceNumberResponse = await GenerateInvoiceNumberAsync(request.ProcessCode ?? "WS");
                    
                    if (!invoiceNumberResponse.Success)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"\n===== SERVICE: VALIDATION ERROR - Fatura numarası oluşturulamadı: {invoiceNumberResponse.Message} =====\n");
                        Console.ResetColor();
                        
                        return new ApiResponse<InvoiceHeaderModel>
                        {
                            Success = false,
                            Message = "Fatura numarası oluşturulamadı: " + invoiceNumberResponse.Message
                        };
                    }
                    
                    // Oluşturulan fatura numarasını request'e atama
                    request.InvoiceNumber = invoiceNumberResponse.Data;
                    
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"\n===== SERVICE: Fatura numarası otomatik oluşturuldu: {request.InvoiceNumber} =====\n");
                    Console.ResetColor();
                }
                
                // Validasyon kontrolü
                if (request.InvoiceDate == DateTime.MinValue)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n===== SERVICE: VALIDATION ERROR - Geçerli bir fatura tarihi girilmelidir =====\n");
                    Console.ResetColor();
                    
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = "Geçerli bir fatura tarihi girilmelidir"
                    };
                }
                
                if (string.IsNullOrEmpty(request.CustomerCode))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n===== SERVICE: VALIDATION ERROR - Müşteri kodu boş olamaz =====\n");
                    Console.ResetColor();
                    
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = "Müşteri kodu boş olamaz"
                    };
                }
                
                if (request.Details == null || request.Details.Count == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n===== SERVICE: VALIDATION ERROR - Fatura detayları boş olamaz =====\n");
                    Console.ResetColor();
                    
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = "Fatura detayları boş olamaz"
                    };
                }
                
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n===== SERVICE: Validasyon başarılı, fatura oluşturuluyor =====\n");
                Console.ResetColor();
                
                // Faturayı oluştur
                var invoice = await _invoiceRepository.CreateWholesaleInvoiceAsync(request);
                
                // Vadeli ödeme işlemi
                if (request.IsCreditSale && invoice != null)
                {
                    _logger.LogInformation("Vadeli ödeme işlemi başlatılıyor. Fatura ID: {InvoiceHeaderId}", invoice.InvoiceHeaderID);
                    
                    try
                    {
                        // Vadeli ödeme için request oluştur
                        var creditPaymentRequest = new CreditPaymentRequest
                        {
                            InvoiceHeaderID = invoice.InvoiceHeaderID.ToString(),
                            InvoiceNumber = invoice.InvoiceNumber,
                            CurrAccCode = invoice.CurrAccCode,
                            CurrAccTypeCode = (byte)(invoice.CurrAccTypeCode), // int'den byte'a dönüşüm
                            ProcessCode = invoice.ProcessCode,
                            DocCurrencyCode = invoice.DocCurrencyCode,
                            LocalCurrencyCode = invoice.LocalCurrencyCode,
                            ExchangeRate = invoice.ExchangeRate.HasValue ? invoice.ExchangeRate.Value : 1m, // decimal? -> decimal
                            PaymentRows = new List<PaymentRow>()
                        };
                        
                        // Fatura tutarını frontend'den gelen değerden al
                        decimal totalAmount = request.TotalAmount > 0 ? request.TotalAmount : invoice.TotalAmount;
                        
                        _logger.LogInformation("Fatura tutarları: Frontend TotalAmount={RequestTotalAmount}, Invoice.TotalAmount={InvoiceTotalAmount}, totalNetAmount={totalNetAmount}, totalGrossAmount={totalGrossAmount}", 
                            request.TotalAmount, invoice.TotalAmount, invoice.totalNetAmount, invoice.totalGrossAmount);
                        
                        _logger.LogInformation("Kullanılan tutar: {UsedAmount} (Frontend'den gelen değer kullanılıyor)", totalAmount);
                            
                        if (totalAmount <= 0)
                        {
                            _logger.LogWarning("DİKKAT: Fatura tutarı sıfır veya negatif: {Amount}. Faturanın toplam tutarını kontrol edin.", totalAmount);
                        }
                        
                        // Ödeme satırı ekle - Vadeli ödemede tek satır kullanıyoruz
                        // PaymentTerm 0 olsa bile vadeli ödeme için kayıt oluşturuyoruz
                        int paymentTerm = request.FormType.HasValue ? request.FormType.Value : 30;
                        if (paymentTerm <= 0) paymentTerm = 30; // Varsayılan vade 30 gün
                        
                        decimal exchangeRate = invoice.ExchangeRate.HasValue ? invoice.ExchangeRate.Value : 1m;
                        
                        _logger.LogInformation("PaymentRow oluşturuluyor. Amount: {Amount}, CurrencyCode: {CurrencyCode}, ExchangeRate: {ExchangeRate}, TRY karşılığı: {TRYAmount}", 
                            totalAmount, invoice.DocCurrencyCode, exchangeRate, totalAmount * exchangeRate);
                            
                        creditPaymentRequest.PaymentRows.Add(new PaymentRow
                        {
                            Amount = totalAmount,
                            DueDate = request.InvoiceDate.AddDays(paymentTerm),
                            CurrencyCode = invoice.DocCurrencyCode,
                            ExchangeRate = exchangeRate
                        });
                        
                        _logger.LogInformation("Vadeli ödeme isteği oluşturuldu. Tutar: {Amount}, Döviz: {Currency}, Kur: {ExchangeRate}, Vade Tarihi: {DueDate}", 
                            totalAmount, invoice.DocCurrencyCode, invoice.ExchangeRate.HasValue ? invoice.ExchangeRate.Value : 1m, request.InvoiceDate.AddDays(paymentTerm).ToString("yyyy-MM-dd"));
                        
                        // Döviz kuru kontrolü - Daha sıkı validasyon
                        if (invoice.DocCurrencyCode != "TRY")
                        {
                            if (invoice.ExchangeRate == null)
                            {
                                _logger.LogError("HATA: Döviz kuru null: {CurrencyCode} için kur değeri bulunamadı.", invoice.DocCurrencyCode);
                                throw new InvalidOperationException($"Döviz kuru bulunamadı: {invoice.DocCurrencyCode}");
                            }
                            else if (invoice.ExchangeRate <= 1)
                            {
                                _logger.LogWarning("DİKKAT: Döviz kuru 1 veya daha düşük: {ExchangeRate}. Kur kontrol edilmeli.", invoice.ExchangeRate);
                                // Burada kur 1'den düşükse uyarı veriyoruz ama işleme devam ediyoruz
                                // Eğer tamamen engellemek istersek, aşağıdaki satırı aktif edebiliriz:
                                // throw new InvalidOperationException($"Geçersiz döviz kuru: {invoice.ExchangeRate}");
                            }
                        }
                        
                        try
                        {
                            // Vadeli ödeme işlemini gerçekleştir
                            var creditPaymentResult = await _creditPaymentService.CreateCreditPaymentForInvoiceAsync(creditPaymentRequest);
                            
                            if (creditPaymentResult != null && !string.IsNullOrEmpty(creditPaymentResult.DebitHeaderId))
                            {
                                _logger.LogInformation("Vadeli ödeme işlemi başarıyla tamamlandı. DebitHeaderId: {DebitHeaderId}", 
                                    creditPaymentResult.DebitHeaderId);
                            }
                            else
                            {
                                _logger.LogWarning("Vadeli ödeme işlemi tamamlandı ancak sonuç boş veya geçersiz.");
                                // Sonuç boş olsa bile fatura oluşturma işlemine devam ediyoruz
                            }
                        }
                        catch (Exception ex)
                        {
                            // Vadeli ödeme oluşturma hatası fatura oluşturmayı engellemeyecek
                            // ama detaylı log tutacağız
                            _logger.LogError(ex, "Vadeli ödeme işlemi sırasında hata oluştu: {Message}. Fatura oluşturma işlemi devam ediyor.", ex.Message);
                        }
                    }
                    catch (Exception creditEx)
                    {
                        _logger.LogError(creditEx, "Vadeli ödeme işlemi sırasında hata oluştu. Fatura ID: {InvoiceHeaderId}", invoice.InvoiceHeaderID);
                        // Vadeli ödeme hatası faturanın oluşmasını engellemeyecek, sadece log'a kaydedilecek
                    }
                }
                else
                {
                    _logger.LogInformation("Fatura vadeli ödeme tipinde değil veya fatura oluşturulamadı. Vadeli ödeme işlemi atlanıyor.");
                }
                
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n===== SERVICE: CreateWholesaleInvoiceAsync COMPLETED =====\n");
                Console.WriteLine($"Oluşturulan Fatura: {System.Text.Json.JsonSerializer.Serialize(invoice, new System.Text.Json.JsonSerializerOptions { WriteIndented = true })}");
                Console.ResetColor();
                
                return new ApiResponse<InvoiceHeaderModel>
                {
                    Success = true,
                    Message = "Fatura başarıyla oluşturuldu",
                    Data = invoice
                };
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n===== SERVICE: CreateWholesaleInvoiceAsync ERROR =====\n");
                Console.WriteLine($"Hata: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                Console.WriteLine("\n===== SERVICE: CreateWholesaleInvoiceAsync ERROR END =====\n");
                Console.ResetColor();
                
                _logger.LogError(ex, "Fatura oluşturulurken hata oluştu");
                return new ApiResponse<InvoiceHeaderModel>
                {
                    Success = false,
                    Message = "Fatura oluşturulurken bir hata oluştu: " + ex.Message
                };
            }
        }

        // Toptan alış faturaları
        public async Task<ApiResponse<InvoiceListResult>> GetWholesalePurchaseInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                var (items, totalCount) = await _invoiceRepository.GetWholesalePurchaseInvoicesAsync(request);
                
                return new ApiResponse<InvoiceListResult>
                {
                    Success = true,
                    Message = "Toptan alış faturaları başarıyla getirildi",
                    Data = new InvoiceListResult
                    {
                        Items = items,
                        TotalCount = totalCount,
                        Page = request.Page,
                        PageSize = request.PageSize
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toptan alış faturaları getirilirken hata oluştu");
                return new ApiResponse<InvoiceListResult>
                {
                    Success = false,
                    Message = "Toptan alış faturaları getirilirken bir hata oluştu: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<InvoiceHeaderModel>> GetWholesalePurchaseInvoiceByIdAsync(int invoiceHeaderId)
        {
            try
            {
                var invoice = await _invoiceRepository.GetWholesalePurchaseInvoiceByIdAsync(invoiceHeaderId);
                
                if (invoice == null)
                {
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = $"Fatura bulunamadı. ID: {invoiceHeaderId}"
                    };
                }
                
                return new ApiResponse<InvoiceHeaderModel>
                {
                    Success = true,
                    Message = "Fatura başarıyla getirildi",
                    Data = invoice
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatura getirilirken hata oluştu. ID: {invoiceHeaderId}");
                return new ApiResponse<InvoiceHeaderModel>
                {
                    Success = false,
                    Message = "Fatura getirilirken bir hata oluştu: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<InvoiceHeaderModel>> CreateWholesalePurchaseInvoiceAsync(CreateInvoiceRequest request)
        {
            try
            {
                // Validasyon kontrolü
                if (string.IsNullOrEmpty(request.InvoiceNumber))
                {
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = "Fatura numarası boş olamaz"
                    };
                }
                
                if (request.InvoiceDate == DateTime.MinValue)
                {
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = "Geçerli bir fatura tarihi girilmelidir"
                    };
                }
                
                if (string.IsNullOrEmpty(request.VendorCode))
                {
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = "Tedarikçi kodu boş olamaz"
                    };
                }
                
                if (request.Details == null || request.Details.Count == 0)
                {
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = "Fatura detayları boş olamaz"
                    };
                }
                
                // Faturayı oluştur
                var invoice = await _invoiceRepository.CreateWholesalePurchaseInvoiceAsync(request);
                
                return new ApiResponse<InvoiceHeaderModel>
                {
                    Success = true,
                    Message = "Fatura başarıyla oluşturuldu",
                    Data = invoice
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatura oluşturulurken hata oluştu");
                return new ApiResponse<InvoiceHeaderModel>
                {
                    Success = false,
                    Message = "Fatura oluşturulurken bir hata oluştu: " + ex.Message
                };
            }
        }

        // Masraf faturaları
        public async Task<ApiResponse<InvoiceListResult>> GetExpenseInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                var (items, totalCount) = await _invoiceRepository.GetExpenseInvoicesAsync(request);
                
                return new ApiResponse<InvoiceListResult>
                {
                    Success = true,
                    Message = "Masraf faturaları başarıyla getirildi",
                    Data = new InvoiceListResult
                    {
                        Items = items,
                        TotalCount = totalCount,
                        Page = request.Page,
                        PageSize = request.PageSize
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Masraf faturaları getirilirken hata oluştu");
                return new ApiResponse<InvoiceListResult>
                {
                    Success = false,
                    Message = "Masraf faturaları getirilirken bir hata oluştu: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<InvoiceHeaderModel>> GetExpenseInvoiceByIdAsync(int invoiceHeaderId)
        {
            try
            {
                var invoice = await _invoiceRepository.GetExpenseInvoiceByIdAsync(invoiceHeaderId);
                
                if (invoice == null)
                {
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = $"Fatura bulunamadı. ID: {invoiceHeaderId}"
                    };
                }
                
                return new ApiResponse<InvoiceHeaderModel>
                {
                    Success = true,
                    Message = "Fatura başarıyla getirildi",
                    Data = invoice
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatura getirilirken hata oluştu. ID: {invoiceHeaderId}");
                return new ApiResponse<InvoiceHeaderModel>
                {
                    Success = false,
                    Message = "Fatura getirilirken bir hata oluştu: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<InvoiceHeaderModel>> CreateExpenseInvoiceAsync(CreateInvoiceRequest request)
        {
            try
            {
                // Validasyon kontrolü
                if (string.IsNullOrEmpty(request.InvoiceNumber))
                {
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = "Fatura numarası boş olamaz"
                    };
                }
                
                if (request.InvoiceDate == DateTime.MinValue)
                {
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = "Geçerli bir fatura tarihi girilmelidir"
                    };
                }
                
                if (string.IsNullOrEmpty(request.VendorCode))
                {
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = "Tedarikçi kodu boş olamaz"
                    };
                }
                
                if (string.IsNullOrEmpty(request.ProcessCode))
                {
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = "ProcessCode boş olamaz"
                    };
                }
                
                // Masraf faturaları için ProcessCode EP olmalıdır
                if (request.ProcessCode != "EP")
                {
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = "Masraf faturaları için ProcessCode 'EP' olmalıdır"
                    };
                }
                
                if (request.Details == null || request.Details.Count == 0)
                {
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = "Fatura detayları boş olamaz"
                    };
                }
                
                // Faturayı oluştur
                var invoice = await _invoiceRepository.CreateExpenseInvoiceAsync(request);
                
                return new ApiResponse<InvoiceHeaderModel>
                {
                    Success = true,
                    Message = "Fatura başarıyla oluşturuldu",
                    Data = invoice
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatura oluşturulurken hata oluştu");
                return new ApiResponse<InvoiceHeaderModel>
                {
                    Success = false,
                    Message = "Fatura oluşturulurken bir hata oluştu: " + ex.Message
                };
            }
        }

        // Sipariş bazlı faturaları getiren metot
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetOrderBasedInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                return await _invoiceRepository.GetOrderBasedInvoicesAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sipariş bazlı faturalar getirilirken hata oluştu");
                throw;
            }
        }

        // İrsaliye bazlı faturaları getiren metot
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetShipmentBasedInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                return await _invoiceRepository.GetShipmentBasedInvoicesAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İrsaliye bazlı faturalar getirilirken hata oluştu");
                throw;
            }
        }

        // Sipariş bazlı alış faturalarını getiren metot
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetOrderBasedPurchaseInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                return await _invoiceRepository.GetOrderBasedPurchaseInvoicesAsync(request);
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
                return await _invoiceRepository.GetShipmentBasedPurchaseInvoicesAsync(request);
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
                return await _invoiceRepository.GetDirectWholesalePurchaseInvoicesAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Direkt toptan alış faturaları getirilirken hata oluştu");
                throw;
            }
        }

        // Direkt satış faturalarını getiren metot
        public async Task<(List<InvoiceHeaderModel> items, int totalCount)> GetDirectSalesInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                return await _invoiceRepository.GetDirectSalesInvoicesAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Direkt satış faturaları getirilirken hata oluştu");
                throw;
            }
        }

        // Tüm fatura tiplerini getiren genel metot
        public async Task<ApiResponse<InvoiceListResult>> GetAllInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                var (items, totalCount) = await _invoiceRepository.GetAllInvoicesAsync(request);
                
                if (items == null || items.Count == 0)
                {
                    return new ApiResponse<InvoiceListResult>
                    {
                        Success = true,
                        Message = "Fatura bulunamadı",
                        Data = new InvoiceListResult
                        {
                            Items = new List<InvoiceHeaderModel>(),
                            TotalCount = 0,
                            Page = request.Page,
                            PageSize = request.PageSize
                        }
                    };
                }
                
                return new ApiResponse<InvoiceListResult>
                {
                    Success = true,
                    Message = "Faturalar başarıyla getirildi",
                    Data = new InvoiceListResult
                    {
                        Items = items,
                        TotalCount = totalCount,
                        Page = request.Page,
                        PageSize = request.PageSize
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Faturalar getirilirken hata oluştu");
                return new ApiResponse<InvoiceListResult>
                {
                    Success = false,
                    Message = "Faturalar getirilirken bir hata oluştu: " + ex.Message
                };
            }
        }

        // Fatura detayları
        public async Task<ApiResponse<List<InvoiceDetailModel>>> GetInvoiceDetailsAsync(int invoiceHeaderId)
        {
            try
            {
                var details = await _invoiceRepository.GetInvoiceDetailsAsync(invoiceHeaderId);
                
                if (details == null || details.Count == 0)
                {
                    return new ApiResponse<List<InvoiceDetailModel>>
                    {
                        Success = false,
                        Message = $"Fatura detayları bulunamadı. Fatura ID: {invoiceHeaderId}"
                    };
                }
                
                return new ApiResponse<List<InvoiceDetailModel>>
                {
                    Success = true,
                    Message = "Fatura detayları başarıyla getirildi",
                    Data = details
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatura detayları getirilirken hata oluştu. Fatura ID: {invoiceHeaderId}");
                return new ApiResponse<List<InvoiceDetailModel>>
                {
                    Success = false,
                    Message = "Fatura detayları getirilirken bir hata oluştu: " + ex.Message
                };
            }
        }

        // Fatura ödeme detayları
        public async Task<ApiResponse<List<InvoicePaymentDetailModel>>> GetInvoicePaymentDetailsAsync(string invoiceHeaderId)
        {
            try
            {
                var paymentDetails = await _invoiceRepository.GetInvoicePaymentDetailsAsync(invoiceHeaderId);
                
                if (paymentDetails == null || paymentDetails.Count == 0)
                {
                    return new ApiResponse<List<InvoicePaymentDetailModel>>
                    {
                        Success = false,
                        Message = $"Fatura ödeme detayları bulunamadı. Fatura ID: {invoiceHeaderId}"
                    };
                }
                
                return new ApiResponse<List<InvoicePaymentDetailModel>>
                {
                    Success = true,
                    Message = "Fatura ödeme detayları başarıyla getirildi",
                    Data = paymentDetails
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatura ödeme detayları getirilirken hata oluştu. Fatura ID: {invoiceHeaderId}");
                return new ApiResponse<List<InvoicePaymentDetailModel>>
                {
                    Success = false,
                    Message = "Fatura ödeme detayları getirilirken bir hata oluştu: " + ex.Message
                };
            }
        }
    }
}
