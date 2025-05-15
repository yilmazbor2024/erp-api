namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Request model for filtering customers
    /// </summary>
    public class CustomerFilterRequest
    {
        /// <summary>
        /// Filter by customer code
        /// </summary>
        public string? CustomerCode { get; set; }
        
        /// <summary>
        /// Filter by customer name
        /// </summary>
        public string? CustomerName { get; set; }
        
        /// <summary>
        /// Filter by customer type code
        /// </summary>
        public int? CustomerTypeCode { get; set; }
        
        /// <summary>
        /// Filter by CurrAccTypeCode (1: Vendor, 3: Customer)
        /// </summary>
        public int? CurrAccTypeCode { get; set; }
        
        /// <summary>
        /// Filter by created date (from)
        /// </summary>
        public DateTime? CreatedDateFrom { get; set; }
        
        /// <summary>
        /// Filter by created date (to)
        /// </summary>
        public DateTime? CreatedDateTo { get; set; }
        
        /// <summary>
        /// Filter by created username
        /// </summary>
        public string? CreatedUsername { get; set; }
        
        /// <summary>
        /// Filter by currency code
        /// </summary>
        public string? CurrencyCode { get; set; }
        
        /// <summary>
        /// Filter by VIP status
        /// </summary>
        public bool? IsVIP { get; set; }
        
        /// <summary>
        /// Filter by company code
        /// </summary>
        public string? CompanyCode { get; set; }
        
        /// <summary>
        /// Filter by office code
        /// </summary>
        public string? OfficeCode { get; set; }

        /// <summary>
        /// Filter by city code (used for city description lookup)
        /// </summary>
        public string? CityCode { get; set; }
        
        /// <summary>
        /// Filter by district code (used for district description lookup)
        /// </summary>
        public string? DistrictCode { get; set; }
        
        /// <summary>
        /// Filter by identity number
        /// </summary>
        public string? IdentityNum { get; set; }
        
        /// <summary>
        /// Filter by tax number
        /// </summary>
        public string? TaxNumber { get; set; }
        
        /// <summary>
        /// Filter by vendor code
        /// </summary>
        public string? VendorCode { get; set; }
        
        /// <summary>
        /// Filter by e-invoice status
        /// </summary>
        public bool? IsSubjectToEInvoice { get; set; }
        
        /// <summary>
        /// Filter by DBS integration status
        /// </summary>
        public bool? UseDBSIntegration { get; set; }
        
        /// <summary>
        /// Filter by blocked status
        /// </summary>
        public bool? IsBlocked { get; set; }
        
        /// <summary>
        /// The page number to retrieve (1-based)
        /// </summary>
        public int PageNumber { get; set; } = 1;
        
        /// <summary>
        /// The number of items per page
        /// </summary>
        public int PageSize { get; set; } = 20;
        
        /// <summary>
        /// Sort column name
        /// </summary>
        public string? SortColumn { get; set; } = "CustomerCode";
        
        /// <summary>
        /// Sort direction (asc or desc)
        /// </summary>
        public string? SortDirection { get; set; } = "asc";
    }
} 