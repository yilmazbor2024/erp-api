using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Response model for creating a new customer
    /// </summary>
    public class CustomerCreateResponse
    {
        /// <summary>
        /// The customer code
        /// </summary>
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// The customer name
        /// </summary>
        public string CustomerName { get; set; }
        
        /// <summary>
        /// The customer tax number
        /// </summary>
        public string TaxNumber { get; set; }
        
        /// <summary>
        /// The customer tax office code
        /// </summary>
        public string TaxOfficeCode { get; set; }
        
        /// <summary>
        /// The customer type code
        /// </summary>
        public int CustomerTypeCode { get; set; }
        
        /// <summary>
        /// The customer type description
        /// </summary>
        public string CustomerTypeDescription { get; set; }
        
        /// <summary>
        /// The customer's addresses
        /// </summary>
        public List<CustomerAddressResponse> Addresses { get; set; } = new List<CustomerAddressResponse>();
        
        /// <summary>
        /// The customer's contacts
        /// </summary>
        public List<CustomerContactResponse> Contacts { get; set; } = new List<CustomerContactResponse>();
        
        /// <summary>
        /// Operation success status
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Operation message
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// The customer's communications
        /// </summary>
        public List<CustomerCommunicationResponse> Communications { get; set; } = new List<CustomerCommunicationResponse>();
        
        /// <summary>
        /// The date the customer was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// The username of the user who created the customer
        /// </summary>
        public string CreatedBy { get; set; }
    }
} 