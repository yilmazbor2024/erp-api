using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Common;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ErpMobile.Api.Models.Invoice;
using ErpMobile.Api.Data;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/WholesalePurchase")]
    [Route("api/Invoices/WholesalePurchase")]
    [EnableCors]
    public class WholesalePurchaseInvoiceController : ControllerBase
    {
        private readonly ILogger<WholesalePurchaseInvoiceController> _logger;
        private readonly NanoServiceDbContext _context;

        public WholesalePurchaseInvoiceController(
            ILogger<WholesalePurchaseInvoiceController> logger,
            NanoServiceDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Gets a list of wholesale purchase invoices
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="sortBy">Field to sort by</param>
        /// <param name="sortDirection">Sort direction (asc or desc)</param>
        /// <param name="vendorCode">Filter by vendor code</param>
        /// <param name="invoiceNumber">Filter by invoice number</param>
        /// <param name="startDate">Filter by start date</param>
        /// <param name="endDate">Filter by end date</param>
        /// <param name="status">Filter by status</param>
        /// <returns>List of wholesale purchase invoices</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<object>>> GetWholesalePurchaseInvoices(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "invoiceDate",
            [FromQuery] string sortDirection = "desc",
            [FromQuery] string vendorCode = null,
            [FromQuery] string invoiceNumber = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] string status = null)
        {
            try
            {
                // Parametre koşullarını oluştur
                var conditions = new List<string>();
                var parameters = new List<object>();
                
                if (!string.IsNullOrEmpty(vendorCode))
                {
                    conditions.Add("VendorCode = @p" + parameters.Count);
                    parameters.Add(vendorCode);
                }
                
                if (!string.IsNullOrEmpty(invoiceNumber))
                {
                    conditions.Add("InvoiceNumber LIKE @p" + parameters.Count);
                    parameters.Add("%" + invoiceNumber + "%");
                }
                
                if (startDate.HasValue)
                {
                    conditions.Add("InvoiceDate >= @p" + parameters.Count);
                    parameters.Add(startDate.Value);
                }
                
                if (endDate.HasValue)
                {
                    conditions.Add("InvoiceDate <= @p" + parameters.Count);
                    parameters.Add(endDate.Value);
                }
                
                if (!string.IsNullOrEmpty(status))
                {
                    conditions.Add("Status = @p" + parameters.Count);
                    parameters.Add(status);
                }
                
                // WHERE koşulunu oluştur
                var whereClause = conditions.Count > 0 
                    ? "WHERE " + string.Join(" AND ", conditions) 
                    : string.Empty;
                
                // ORDER BY koşulunu oluştur
                var orderByClause = $"ORDER BY {sortBy} {sortDirection}";
                
                // Sayfalama için OFFSET ve FETCH koşullarını oluştur
                var offset = (page - 1) * pageSize;
                var fetchClause = $"OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";
                
                // Toplam kayıt sayısını al
                var countQuery = $@"
                    SELECT COUNT(*) 
                    FROM WholesalePurchaseInvoices 
                    {whereClause}";
                
                // Ana sorguyu oluştur
                var query = $@"
                    SELECT * 
                    FROM WholesalePurchaseInvoices 
                    {whereClause} 
                    {orderByClause} 
                    {fetchClause}";
                
                // Parametreleri oluştur
                var sqlParameters = new object[parameters.Count];
                for (int i = 0; i < parameters.Count; i++)
                {
                    sqlParameters[i] = parameters[i];
                }
                
                // Toplam kayıt sayısını al
                var totalCount = 0;
                if (parameters.Count > 0)
                {
                    totalCount = await _context.Database.ExecuteSqlRawAsync(countQuery, sqlParameters);
                }
                else
                {
                    totalCount = await _context.Database.ExecuteSqlRawAsync(countQuery);
                }
                
                // Verileri al
                List<WholesalePurchaseInvoiceModel> invoices;
                if (parameters.Count > 0)
                {
                    invoices = await _context.Set<WholesalePurchaseInvoiceModel>()
                        .FromSqlRaw(query, sqlParameters)
                        .ToListAsync();
                }
                else
                {
                    invoices = await _context.Set<WholesalePurchaseInvoiceModel>()
                        .FromSqlRaw(query)
                        .ToListAsync();
                }
                
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Wholesale purchase invoices retrieved successfully",
                    Data = new
                    {
                        items = invoices,
                        totalCount,
                        page,
                        pageSize
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving wholesale purchase invoices");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Error retrieving wholesale purchase invoices: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Gets a wholesale purchase invoice by ID
        /// </summary>
        /// <param name="id">Invoice ID</param>
        /// <returns>Wholesale purchase invoice</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<WholesalePurchaseInvoiceModel>>> GetWholesalePurchaseInvoice(int id)
        {
            try
            {
                var query = $"SELECT * FROM WholesalePurchaseInvoices WHERE InvoiceHeaderID = @id";
                var invoice = await _context.Set<WholesalePurchaseInvoiceModel>()
                    .FromSqlRaw(query, new object[] { id })
                    .FirstOrDefaultAsync();
                
                if (invoice == null)
                {
                    return NotFound(new ApiResponse<WholesalePurchaseInvoiceModel>
                    {
                        Success = false,
                        Message = $"Wholesale purchase invoice with ID {id} not found"
                    });
                }
                
                return Ok(new ApiResponse<WholesalePurchaseInvoiceModel>
                {
                    Success = true,
                    Message = "Wholesale purchase invoice retrieved successfully",
                    Data = invoice
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving wholesale purchase invoice with ID {id}");
                return StatusCode(500, new ApiResponse<WholesalePurchaseInvoiceModel>
                {
                    Success = false,
                    Message = $"Error retrieving wholesale purchase invoice with ID {id}: " + ex.Message
                });
            }
        }
    }
}
