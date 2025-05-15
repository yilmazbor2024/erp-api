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
                
                if (string.IsNullOrEmpty(request.CustomerCode))
                {
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = "Müşteri kodu boş olamaz"
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
                var invoice = await _invoiceRepository.CreateWholesaleInvoiceAsync(request);
                
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
                
                if (string.IsNullOrEmpty(request.ExpenseTypeCode))
                {
                    return new ApiResponse<InvoiceHeaderModel>
                    {
                        Success = false,
                        Message = "Masraf tipi kodu boş olamaz"
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
    }
}
