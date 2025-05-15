using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services
{
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly IDbConnection _erpConnection;
        private readonly ILogger<SalesInvoiceService> _logger;
        private readonly IConfiguration _configuration;

        public SalesInvoiceService(IDbConnection erpConnection, ILogger<SalesInvoiceService> logger, IConfiguration configuration)
        {
            _erpConnection = erpConnection;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<InvoiceResponseList> GetInvoicesAsync(InvoiceListRequest request)
        {
            try
            {
                _logger.LogInformation("Fetching sales invoices with request: {@Request}", request);

                // Convert InvoiceListRequest to InvoiceFilterRequest for internal use
                var filter = new InvoiceFilterRequest
                {
                    Page = request.PageNumber,
                    PageSize = request.PageSize,
                    OrderBy = string.IsNullOrEmpty(request.SortBy) ? "InvoiceDate DESC" : 
                              $"{request.SortBy} {request.SortDirection}",
                    CustomerCode = request.CustomerCode,
                    SearchText = request.InvoiceNumber,
                    FromDate = request.StartDate,
                    ToDate = request.EndDate
                };

                string whereClause = BuildWhereClause(filter);

                string countQuery = $@"
                    SELECT COUNT(*) 
                    FROM dbo.trInvoiceHeader 
                    INNER JOIN dbo.cdCurrAcc ON cdCurrAcc.CurrAccCode = trInvoiceHeader.CurrAccCode
                    {whereClause}";

                string query = $@"
                    SELECT 
                        trInvoiceHeader.InvoiceHeaderId,
                        trInvoiceHeader.InvoiceNumber,
                        trInvoiceHeader.InvoiceDate,
                        trInvoiceHeader.InvoiceTime,
                        trInvoiceHeader.CurrAccCode,
                        cdCurrAcc.CurrAccDesc,
                        trInvoiceHeader.NetTotal,
                        trInvoiceHeader.GrandTotal,
                        trInvoiceHeader.IsCompleted,
                        trInvoiceHeader.IsCancelled
                    FROM dbo.trInvoiceHeader 
                    INNER JOIN dbo.cdCurrAcc ON cdCurrAcc.CurrAccCode = trInvoiceHeader.CurrAccCode
                    {whereClause}
                    ORDER BY {filter.OrderBy}
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY";

                var parameters = new DynamicParameters();
                parameters.Add("@Offset", (filter.Page - 1) * filter.PageSize);
                parameters.Add("@PageSize", filter.PageSize);
                
                if (!string.IsNullOrEmpty(filter.CustomerCode))
                {
                    parameters.Add("@CustomerCode", filter.CustomerCode);
                }
                
                if (!string.IsNullOrEmpty(filter.SearchText))
                {
                    parameters.Add("@SearchText", $"%{filter.SearchText}%");
                }
                
                if (filter.FromDate.HasValue)
                {
                    parameters.Add("@FromDate", filter.FromDate.Value);
                }
                
                if (filter.ToDate.HasValue)
                {
                    parameters.Add("@ToDate", filter.ToDate.Value);
                }

                int totalCount = await _erpConnection.ExecuteScalarAsync<int>(countQuery, parameters);
                var invoices = await _erpConnection.QueryAsync<InvoiceResponse>(query, parameters);

                return new InvoiceResponseList
                {
                    Invoices = invoices.ToList(),
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize),
                    CurrentPage = filter.Page,
                    PageSize = filter.PageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching sales invoices");
                throw;
            }
        }

        public async Task<InvoiceDetailResponse> GetInvoiceByIdAsync(Guid invoiceId)
        {
            try
            {
                _logger.LogInformation("Fetching sales invoice details for ID: {InvoiceId}", invoiceId);

                string invoiceIdStr = invoiceId.ToString();

                string headerQuery = @"
                    SELECT 
                        h.InvoiceHeaderId,
                        h.InvoiceNumber,
                        h.InvoiceDate,
                        h.InvoiceTime,
                        h.CurrAccCode,
                        c.CurrAccDesc,
                        h.OfficeCode,
                        h.Description1,
                        h.Description2,
                        h.Description3,
                        h.Description4,
                        h.NetTotal,
                        h.TaxTotal,
                        h.GrandTotal,
                        h.PaymentPlanCode,
                        h.IsCompleted,
                        h.IsCancelled,
                        h.DocTrackingNumber
                    FROM dbo.trInvoiceHeader h
                    INNER JOIN dbo.cdCurrAcc c ON c.CurrAccCode = h.CurrAccCode
                    WHERE h.InvoiceHeaderId = @InvoiceId";

                string linesQuery = @"
                    SELECT 
                        l.InvoiceLineId,
                        l.InvoiceHeaderId,
                        l.LineNumber,
                        l.ItemCode,
                        i.ItemDescription,
                        l.Quantity,
                        l.UnitCode,
                        l.UnitPrice,
                        l.NetAmount,
                        l.TaxRate,
                        l.TaxAmount,
                        l.TotalAmount,
                        l.DiscountRate,
                        l.DiscountAmount,
                        l.Description
                    FROM dbo.trInvoiceLine l
                    LEFT JOIN dbo.cdItem i ON i.ItemCode = l.ItemCode
                    WHERE l.InvoiceHeaderId = @InvoiceId
                    ORDER BY l.LineNumber";

                var parameters = new { InvoiceId = invoiceIdStr };

                var invoice = await _erpConnection.QuerySingleOrDefaultAsync<InvoiceDetailResponse>(headerQuery, parameters);
                
                if (invoice == null)
                {
                    return null;
                }

                var lines = await _erpConnection.QueryAsync<InvoiceLineResponse>(linesQuery, parameters);
                invoice.Lines = lines.AsList();

                return invoice;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching sales invoice details for ID: {InvoiceId}", invoiceId);
                throw;
            }
        }

        public async Task<InvoiceCreateResponse> CreateInvoiceAsync(InvoiceCreateRequest request)
        {
            _logger.LogInformation("Creating new sales invoice for customer: {CustomerCode}", request.CustomerCode);

            IDbTransaction transaction = null;
            try
            {
                if (_erpConnection.State != ConnectionState.Open)
                {
                    _erpConnection.Open();
                }

                transaction = _erpConnection.BeginTransaction();

                // Generate invoice header ID
                var invoiceHeaderId = Guid.NewGuid().ToString();
                
                // Get next invoice number
                string nextInvoiceNumberQuery = "SELECT MAX(CAST(InvoiceNumber AS INT)) + 1 FROM dbo.trInvoiceHeader";
                var nextInvoiceNumber = await _erpConnection.ExecuteScalarAsync<int?>(nextInvoiceNumberQuery, transaction: transaction) ?? 10001;

                // Insert invoice header
                string insertHeaderQuery = @"
                    INSERT INTO dbo.trInvoiceHeader (
                        InvoiceHeaderId, 
                        InvoiceNumber,
                        InvoiceDate,
                        InvoiceTime,
                        CurrAccCode,
                        OfficeCode,
                        Description1,
                        Description2,
                        Description3,
                        Description4,
                        PaymentPlanCode,
                        NetTotal,
                        TaxTotal,
                        GrandTotal,
                        IsCompleted,
                        IsCancelled,
                        CreatedBy,
                        CreatedDate
                    )
                    VALUES (
                        @InvoiceHeaderId,
                        @InvoiceNumber,
                        @InvoiceDate,
                        @InvoiceTime,
                        @CustomerCode,
                        @OfficeCode,
                        @Description1,
                        @Description2,
                        @Description3,
                        @Description4,
                        @PaymentPlanCode,
                        @NetTotal,
                        @TaxTotal,
                        @GrandTotal,
                        1, -- IsCompleted
                        0, -- IsCancelled
                        'SYSTEM',
                        GETDATE()
                    )";

                // Calculate totals
                decimal netTotal = 0;
                decimal taxTotal = 0;
                decimal grandTotal = 0;

                foreach (var line in request.Lines)
                {
                    decimal lineNetAmount = line.Quantity * line.UnitPrice * (1 - line.DiscountRate / 100);
                    decimal lineTaxAmount = lineNetAmount * (line.TaxRate / 100);
                    decimal lineTotalAmount = lineNetAmount + lineTaxAmount;

                    netTotal += lineNetAmount;
                    taxTotal += lineTaxAmount;
                    grandTotal += lineTotalAmount;
                }

                var headerParameters = new
                {
                    InvoiceHeaderId = invoiceHeaderId,
                    InvoiceNumber = nextInvoiceNumber.ToString(),
                    InvoiceDate = request.InvoiceDate,
                    InvoiceTime = request.InvoiceTime ?? DateTime.Now.TimeOfDay,
                    CustomerCode = request.CustomerCode,
                    OfficeCode = request.OfficeCode,
                    Description1 = request.Description1,
                    Description2 = request.Description2,
                    Description3 = request.Description3,
                    Description4 = request.Description4,
                    PaymentPlanCode = request.PaymentPlanCode,
                    NetTotal = netTotal,
                    TaxTotal = taxTotal,
                    GrandTotal = grandTotal
                };

                await _erpConnection.ExecuteAsync(insertHeaderQuery, headerParameters, transaction);

                // Insert invoice lines
                string insertLineQuery = @"
                    INSERT INTO dbo.trInvoiceLine (
                        InvoiceLineId,
                        InvoiceHeaderId,
                        LineNumber,
                        ItemCode,
                        Quantity,
                        UnitCode,
                        UnitPrice,
                        NetAmount,
                        TaxRate,
                        TaxAmount,
                        TotalAmount,
                        DiscountRate,
                        DiscountAmount,
                        Description,
                        CreatedBy,
                        CreatedDate
                    )
                    VALUES (
                        @InvoiceLineId,
                        @InvoiceHeaderId,
                        @LineNumber,
                        @ItemCode,
                        @Quantity,
                        @UnitCode,
                        @UnitPrice,
                        @NetAmount,
                        @TaxRate,
                        @TaxAmount,
                        @TotalAmount,
                        @DiscountRate,
                        @DiscountAmount,
                        @Description,
                        'SYSTEM',
                        GETDATE()
                    )";

                int lineNumber = 10;
                foreach (var line in request.Lines)
                {
                    decimal netAmount = line.Quantity * line.UnitPrice * (1 - line.DiscountRate / 100);
                    decimal discountAmount = line.Quantity * line.UnitPrice * (line.DiscountRate / 100);
                    decimal taxAmount = netAmount * (line.TaxRate / 100);
                    decimal totalAmount = netAmount + taxAmount;

                    var lineParameters = new
                    {
                        InvoiceLineId = Guid.NewGuid().ToString(),
                        InvoiceHeaderId = invoiceHeaderId,
                        LineNumber = lineNumber,
                        ItemCode = line.ItemCode,
                        Quantity = line.Quantity,
                        UnitCode = line.UnitCode,
                        UnitPrice = line.UnitPrice,
                        NetAmount = netAmount,
                        TaxRate = line.TaxRate,
                        TaxAmount = taxAmount,
                        TotalAmount = totalAmount,
                        DiscountRate = line.DiscountRate,
                        DiscountAmount = discountAmount,
                        Description = line.Description
                    };

                    await _erpConnection.ExecuteAsync(insertLineQuery, lineParameters, transaction);
                    lineNumber += 10;
                }

                transaction.Commit();

                // Get customer name
                string customerQuery = "SELECT CurrAccDesc FROM dbo.cdCurrAcc WHERE CurrAccCode = @CustomerCode";
                string customerName = await _erpConnection.ExecuteScalarAsync<string>(customerQuery, new { CustomerCode = request.CustomerCode });

                // Return the created invoice
                return new InvoiceCreateResponse
                {
                    InvoiceId = Guid.Parse(invoiceHeaderId),
                    InvoiceNumber = nextInvoiceNumber.ToString(),
                    InvoiceDate = request.InvoiceDate,
                    CustomerCode = request.CustomerCode,
                    CustomerName = customerName,
                    TotalAmount = grandTotal,
                    IsSuccess = true,
                    Message = "Fatura başarıyla oluşturuldu."
                };
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                _logger.LogError(ex, "Error occurred while creating sales invoice for customer: {CustomerCode}", request.CustomerCode);
                throw;
            }
            finally
            {
                if (_erpConnection.State == ConnectionState.Open)
                {
                    _erpConnection.Close();
                }
            }
        }

        public async Task<bool> CancelInvoiceAsync(Guid invoiceId)
        {
            try
            {
                _logger.LogInformation("Cancelling sales invoice: {InvoiceId}", invoiceId);

                string invoiceIdStr = invoiceId.ToString();

                string query = @"
                    UPDATE dbo.trInvoiceHeader
                    SET 
                        IsCancelled = 1,
                        ModifiedBy = 'SYSTEM',
                        ModifiedDate = GETDATE()
                    WHERE InvoiceHeaderId = @InvoiceId";

                var parameters = new { InvoiceId = invoiceIdStr };
                
                int rowsAffected = await _erpConnection.ExecuteAsync(query, parameters);
                
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while cancelling sales invoice: {InvoiceId}", invoiceId);
                throw;
            }
        }

        public async Task<PaymentPlanListResponse> GetPaymentPlansAsync(bool? forCreditCardPlan, bool isBlocked)
        {
            try
            {
                _logger.LogInformation("Fetching payment plans: ForCreditCardPlan={ForCreditCardPlan}, IsBlocked={IsBlocked}", forCreditCardPlan, isBlocked);

                string query = @"
                    SELECT 
                        PaymentPlanCode,
                        PaymentPlanDesc,
                        IsCreditCardPlan,
                        IsBlocked
                    FROM dbo.cdPaymentPlan
                    WHERE (@ForCreditCardPlan IS NULL OR IsCreditCardPlan = @ForCreditCardPlan)
                    AND IsBlocked = @IsBlocked
                    ORDER BY PaymentPlanDesc";

                var parameters = new { ForCreditCardPlan = forCreditCardPlan, IsBlocked = isBlocked };
                
                var paymentPlans = await _erpConnection.QueryAsync<PaymentPlanResponse>(query, parameters);
                
                return new PaymentPlanListResponse
                {
                    PaymentPlans = paymentPlans.ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching payment plans");
                throw;
            }
        }

        public async Task<AttributeTypeListResponse> GetAttributeTypesAsync(bool isBlocked)
        {
            try
            {
                _logger.LogInformation("Fetching attribute types: IsBlocked={IsBlocked}", isBlocked);

                string query = @"
                    SELECT 
                        AttributeTypeCode,
                        AttributeTypeDesc,
                        IsBlocked
                    FROM dbo.cdAttributeType
                    WHERE IsBlocked = @IsBlocked
                    ORDER BY AttributeTypeDesc";

                var parameters = new { IsBlocked = isBlocked };
                
                var attributeTypes = await _erpConnection.QueryAsync<AttributeTypeResponse>(query, parameters);
                
                return new AttributeTypeListResponse
                {
                    AttributeTypes = attributeTypes.ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching attribute types");
                throw;
            }
        }

        public async Task<AttributeListResponse> GetAttributesAsync(string attributeTypeCode, bool isBlocked)
        {
            try
            {
                _logger.LogInformation("Fetching attributes: AttributeTypeCode={AttributeTypeCode}, IsBlocked={IsBlocked}", attributeTypeCode, isBlocked);

                string query = @"
                    SELECT 
                        AttributeCode,
                        AttributeDesc,
                        AttributeTypeCode,
                        IsBlocked
                    FROM dbo.cdAttribute
                    WHERE AttributeTypeCode = @AttributeTypeCode
                    AND IsBlocked = @IsBlocked
                    ORDER BY AttributeDesc";

                var parameters = new { AttributeTypeCode = attributeTypeCode, IsBlocked = isBlocked };
                
                var attributes = await _erpConnection.QueryAsync<AttributeResponse>(query, parameters);
                
                return new AttributeListResponse
                {
                    Attributes = attributes.ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching attributes for type: {AttributeTypeCode}", attributeTypeCode);
                throw;
            }
        }

        public async Task<OfficeListResponse> GetOfficesAsync(bool isBlocked)
        {
            try
            {
                _logger.LogInformation("Fetching offices: IsBlocked={IsBlocked}", isBlocked);

                string query = @"
                    SELECT 
                        OfficeCode,
                        OfficeDesc
                    FROM dbo.cdOffice
                    WHERE IsBlocked = @IsBlocked
                    ORDER BY OfficeDesc";

                var parameters = new { IsBlocked = isBlocked };
                
                var offices = await _erpConnection.QueryAsync<OfficeResponse>(query, parameters);
                
                return new OfficeListResponse
                {
                    Offices = offices.ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching offices");
                throw;
            }
        }

        public async Task<ItemDimensionTypeListResponse> GetItemDimensionTypesAsync(bool isBlocked)
        {
            try
            {
                _logger.LogInformation("Fetching item dimension types: IsBlocked={IsBlocked}", isBlocked);

                string query = @"
                    SELECT 
                        DimensionTypeCode,
                        DimensionTypeDesc,
                        IsBlocked
                    FROM dbo.cdDimensionType
                    WHERE IsBlocked = @IsBlocked
                    ORDER BY DimensionTypeDesc";

                var parameters = new { IsBlocked = isBlocked };
                
                var dimensionTypes = await _erpConnection.QueryAsync<ItemDimensionTypeResponse>(query, parameters);
                
                return new ItemDimensionTypeListResponse
                {
                    DimensionTypes = dimensionTypes.ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching item dimension types");
                throw;
            }
        }

        public async Task<DebitListResponse> GetInvoiceDebitsAsync(Guid invoiceId)
        {
            try
            {
                _logger.LogInformation("Fetching debits for invoice: {InvoiceId}", invoiceId);

                string invoiceIdStr = invoiceId.ToString();

                string query = @"
                    SELECT 
                        DebitId,
                        InvoiceHeaderId,
                        DebitAmount,
                        DebitDate,
                        IsProcessed,
                        ProcessDate
                    FROM dbo.trInvoiceDebit
                    WHERE InvoiceHeaderId = @InvoiceId
                    ORDER BY DebitDate";

                var parameters = new { InvoiceId = invoiceIdStr };
                
                var debits = await _erpConnection.QueryAsync<InvoiceDebitResponse>(query, parameters);
                
                return new DebitListResponse
                {
                    Debits = debits.ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching debits for invoice: {InvoiceId}", invoiceId);
                throw;
            }
        }

        private string BuildWhereClause(InvoiceFilterRequest filter)
        {
            var conditions = new List<string> { "1=1" }; // Always true condition to simplify concatenation

            if (!string.IsNullOrEmpty(filter.CustomerCode))
            {
                conditions.Add("trInvoiceHeader.CurrAccCode = @CustomerCode");
            }

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                conditions.Add("(trInvoiceHeader.InvoiceNumber LIKE @SearchText OR cdCurrAcc.CurrAccDesc LIKE @SearchText)");
            }

            if (filter.FromDate.HasValue)
            {
                conditions.Add("trInvoiceHeader.InvoiceDate >= @FromDate");
            }

            if (filter.ToDate.HasValue)
            {
                conditions.Add("trInvoiceHeader.InvoiceDate <= @ToDate");
            }

            return $"WHERE {string.Join(" AND ", conditions)}";
        }
    }
} 