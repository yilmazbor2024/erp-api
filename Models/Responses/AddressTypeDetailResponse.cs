namespace erp_api.Models.Responses
{
    /// <summary>
    /// Response model for detailed address type information.
    /// </summary>
    public class AddressTypeDetailResponse
    {
        /// <summary>
        /// Gets or sets the address type code.
        /// </summary>
        public string AddressTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the address type.
        /// </summary>
        public string AddressTypeDesc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the address type is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the address type.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the username of the user who created the address type.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the last update date of the address type.
        /// </summary>
        public DateTime? LastUpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the username of the user who last updated the address type.
        /// </summary>
        public string LastUpdatedBy { get; set; }
    }
} 