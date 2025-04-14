using System;
using System.Collections.Generic;
using erp_api.Models.Contact;
using System.ComponentModel.DataAnnotations;

namespace erp_api.Models.Requests
{
    /// <summary>
    /// Request model for creating a new customer.
    /// </summary>
    public class CustomerCreateRequest
    {
        /// <summary>
        /// Gets or sets the customer code.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        [Required]
        [StringLength(250)]
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the customer surname.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string CustomerSurname { get; set; }

        /// <summary>
        /// Gets or sets the customer title.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string CustomerTitle { get; set; }

        /// <summary>
        /// Gets or sets the tax number.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string TaxNumber { get; set; }

        /// <summary>
        /// Gets or sets the customer identity number.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string CustomerIdentityNumber { get; set; }

        /// <summary>
        /// Gets or sets the customer type code.
        /// </summary>
        [Required]
        public int CustomerTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the discount group code.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string DiscountGroupCode { get; set; }

        /// <summary>
        /// Gets or sets the payment plan group code.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string PaymentPlanGroupCode { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        [Required]
        [StringLength(3)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the office code.
        /// </summary>
        [Required]
        [StringLength(10)]
        public string OfficeCode { get; set; }

        /// <summary>
        /// Gets or sets the salesman code.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string SalesmanCode { get; set; }

        /// <summary>
        /// Gets or sets the credit limit.
        /// </summary>
        [Required]
        public decimal CreditLimit { get; set; }

        /// <summary>
        /// Gets or sets the risk limit.
        /// </summary>
        [Required]
        public decimal RiskLimit { get; set; }

        /// <summary>
        /// Gets or sets the list of customer contacts.
        /// </summary>
        [Required]
        public List<ContactCreateRequest> Contacts { get; set; }

        /// <summary>
        /// Gets or sets the list of customer addresses.
        /// </summary>
        [Required]
        public List<CustomerAddressCreateRequest> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the tax office.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string TaxOffice { get; set; }

        /// <summary>
        /// Gets or sets the region code.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string RegionCode { get; set; }

        /// <summary>
        /// Gets or sets the city code.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string CityCode { get; set; }

        /// <summary>
        /// Gets or sets the district code.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string DistrictCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is blocked.
        /// </summary>
        [Required]
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Gets or sets the list of customer communications.
        /// </summary>
        [Required]
        public List<CustomerCommunicationCreateRequest> Communications { get; set; }
    }
}