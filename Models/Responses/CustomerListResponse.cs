namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Response model for customer list information.
    /// </summary>
    public class CustomerListResponse
    {
        /// <summary>
        /// Gets or sets the customer code.
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the customer type code.
        /// </summary>
        public int CustomerTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the customer type description.
        /// </summary>
        public string CustomerTypeDescription { get; set; }
        
        /// <summary>
        /// Gets or sets the date when the customer was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// Gets or sets the username that created the customer.
        /// </summary>
        public string CreatedUsername { get; set; }
        
        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        public string CurrencyCode { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the customer is VIP.
        /// </summary>
        public bool IsVIP { get; set; }
        
        /// <summary>
        /// Gets or sets the promotion group description.
        /// </summary>
        public string PromotionGroupDescription { get; set; }
        
        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        public string CompanyCode { get; set; }
        
        /// <summary>
        /// Gets or sets the office code.
        /// </summary>
        public string OfficeCode { get; set; }
        
        /// <summary>
        /// Gets or sets the office description.
        /// </summary>
        public string OfficeDescription { get; set; }
        
        /// <summary>
        /// Gets or sets the office country code.
        /// </summary>
        public string OfficeCountryCode { get; set; }
        
        /// <summary>
        /// Gets or sets the country description.
        /// </summary>
        public string CountryDescription { get; set; }
        
        /// <summary>
        /// Gets or sets the city description.
        /// </summary>
        public string CityDescription { get; set; }
        
        /// <summary>
        /// Gets or sets the district description.
        /// </summary>
        public string DistrictDescription { get; set; }
        
        /// <summary>
        /// Gets or sets the identity number.
        /// </summary>
        public string IdentityNum { get; set; }
        
        /// <summary>
        /// Gets or sets the tax number.
        /// </summary>
        public string TaxNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the vendor code.
        /// </summary>
        public string VendorCode { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the customer is subject to e-invoice.
        /// </summary>
        public bool IsSubjectToEInvoice { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether to use DBS integration.
        /// </summary>
        public bool UseDBSIntegration { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the customer is blocked.
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Gets or sets the customer's debt amount.
        /// </summary>
        public decimal? Debit { get; set; }

        /// <summary>
        /// Gets or sets the customer's credit amount.
        /// </summary>
        public decimal? Credit { get; set; }

        /// <summary>
        /// Gets or sets the customer's balance amount.
        /// </summary>
        public decimal? Balance { get; set; }
    }
} 