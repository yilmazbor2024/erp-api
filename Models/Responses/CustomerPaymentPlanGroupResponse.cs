using System;

namespace erp_api.Models.Responses
{
    /// <summary>
    /// Response model for customer payment plan group information.
    /// </summary>
    public class CustomerPaymentPlanGroupResponse
    {
        /// <summary>
        /// Gets or sets the payment plan group code.
        /// </summary>
        public string CustomerPaymentPlanGrCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the payment plan group.
        /// </summary>
        public string PaymentPlanGroupDescription { get; set; }
    }
} 