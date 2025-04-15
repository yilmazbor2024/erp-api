namespace erp_api.Models.Responses
{
    /// <summary>
    /// Response model for district information.
    /// </summary>
    public class DistrictResponse
    {
        /// <summary>
        /// Gets or sets the district code.
        /// </summary>
        public string DistrictCode { get; set; }

        /// <summary>
        /// Gets or sets the city code this district belongs to.
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the district.
        /// </summary>
        public string DistrictDescription { get; set; }

        /// <summary>
        /// Gets or sets whether the district is blocked.
        /// </summary>
        public bool IsBlocked { get; set; }
    }
} 