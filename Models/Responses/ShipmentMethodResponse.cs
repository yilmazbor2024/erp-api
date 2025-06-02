using System;
using System.Collections.Generic;
using System.Text;

namespace ErpMobile.Api.Models.Responses
{
    public class ShipmentMethodResponse
    {
        public string ShipmentMethodCode { get; set; }
        public string ShipmentMethodDescription { get; set; }
        public string TransportModeDescription { get; set; }
        public string TransportModeCode { get; set; }
        public bool IsBlocked { get; set; }
    }
}
