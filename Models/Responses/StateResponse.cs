using System;

namespace erp_api.Models.Responses
{
    /// <summary>
    /// Response model for state/province information.
    /// </summary>
    public class StateResponse
    {
        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        public string StateCode { get; set; }

        /// <summary>
        /// Gets or sets the country code this state belongs to.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the state.
        /// </summary>
        public string StateDescription { get; set; }

        /// <summary>
        /// Gets or sets whether the state is blocked.
        /// </summary>
        public bool IsBlocked { get; set; }
    }
} 