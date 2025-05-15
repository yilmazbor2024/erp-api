namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Response model for city information.
    /// </summary>
    public class CityResponse
    {
        /// <summary>
        /// Gets or sets the city code.
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// Gets or sets the state code this city belongs to.
        /// </summary>
        public string StateCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the city.
        /// </summary>
        public string CityDescription { get; set; }

        /// <summary>
        /// Gets or sets whether the city is blocked.
        /// </summary>
        public bool IsBlocked { get; set; }
    }
} 