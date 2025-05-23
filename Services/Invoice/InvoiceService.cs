using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Invoice;
using ErpMobile.Api.Repositories.Invoice;

namespace ErpMobile.Api.Services.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ILogger<InvoiceService> _logger;
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(
            ILogger<InvoiceService> logger,
            IInvoiceRepository invoiceRepository)
        {
            _logger = logger;
            _invoiceRepository = invoiceRepository;
        }

        // Fatura numarası oluşturan metot
        public async Task<ApiResponse<string>> GenerateInvoiceNumberAsync(string processCode)
        {
            try
            {
                // Process kodu kontrolü
                if (string.IsNullOrEmpty(processCode))
                {
                    return new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Process kodu boş olamaz"
                    };
                }

                // Veritabanından son fatura numarasını al
                var lastInvoiceNumber = await _invoiceRepository.GetLastInvoiceNumberByProcessCodeAsync(processCode);
                
                // Yeni fatura numarası oluştur
                string newInvoiceNumber;
                if (string.IsNullOrEmpty(lastInvoiceNumber))
                {
                    // İlk fatura numarası
                    newInvoiceNumber = $"{processCode}-1";
                }
                else
                {
                    // Son fatura numarasından yeni numara oluştur
                    var parts = lastInvoiceNumber.Split('-');
                    if (parts.Length >= 2 && int.TryParse(parts[1], out int lastNumber))
                    {
                        newInvoiceNumber = $"{processCode}-{lastNumber + 1}";
                    }
                    else
                    {
                        // Format uygun değilse yeni format oluştur
                        newInvoiceNumber = $"{processCode}-1";
                    }
                }

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Fatura numarası başarıyla oluşturuldu",
                    Data = newInvoiceNumber
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatura numarası oluşturulurken hata oluştu");
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
