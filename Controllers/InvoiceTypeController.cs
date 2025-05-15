using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Common;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ErpMobile.Api.Models.InvoiceType;
using ErpMobile.Api.Data;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Invoices")]
    [EnableCors]
    public class InvoiceTypeController : ControllerBase
    {
        private readonly ILogger<InvoiceTypeController> _logger;
        private readonly NanoServiceDbContext _context;

        public InvoiceTypeController(
            ILogger<InvoiceTypeController> logger,
            NanoServiceDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Gets a list of invoice types
        /// </summary>
        /// <param name="langCode">Language code (default: TR)</param>
        /// <returns>List of invoice types</returns>
        [HttpGet("types")]
        public async Task<ActionResult<ApiResponse<List<InvoiceTypeModel>>>> GetInvoiceTypes(
            [FromQuery] string langCode = "TR")
        {
            try
            {
                var query = $"SELECT * FROM [dbo].[InvoiceType]('{langCode}')";
                var invoiceTypes = await _context.Set<InvoiceTypeModel>().FromSqlRaw(query).ToListAsync();

                return Ok(new ApiResponse<List<InvoiceTypeModel>>
                {
                    Success = true,
                    Message = "Invoice types retrieved successfully",
                    Data = invoiceTypes
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving invoice types");
                return StatusCode(500, new ApiResponse<List<InvoiceTypeModel>>
                {
                    Success = false,
                    Message = "Error retrieving invoice types: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Gets a list of invoices with optional filtering by invoice type
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="sortBy">Field to sort by</param>
        /// <param name="sortDirection">Sort direction (asc or desc)</param>
        /// <param name="invoiceTypeCode">Filter by invoice type code (e.g. WS for wholesale, WP for wholesale purchase)</param>
        /// <param name="transTypeCode">Filter by transaction type code</param>
        /// <param name="customerCode">Filter by customer code</param>
        /// <param name="vendorCode">Filter by vendor code</param>
        /// <param name="invoiceNumber">Filter by invoice number</param>
        /// <param name="startDate">Filter by start date</param>
        /// <param name="endDate">Filter by end date</param>
        /// <param name="status">Filter by status</param>
        /// <returns>List of invoices</returns>
        [HttpGet]
        public ActionResult<ApiResponse<object>> GetInvoices(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "invoiceDate",
            [FromQuery] string sortDirection = "desc",
            [FromQuery] string invoiceTypeCode = null,
            [FromQuery] string transTypeCode = null,
            [FromQuery] string customerCode = null,
            [FromQuery] string vendorCode = null,
            [FromQuery] string invoiceNumber = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] string status = null)
        {
            try
            {
                // TODO: Implement real database query for invoices based on parameters
                // For now, returning a message that this endpoint needs to be implemented with real data
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    Message = "This endpoint needs to be implemented with real database access. Mock data has been removed as requested.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving invoices");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Error retrieving invoices: " + ex.Message
                });
            }
        }
    }
}
