using System;

namespace ErpMobile.Api.Models.Shipment
{
    public class ShipmentAddressModel
    {
        public string ShipmentAddressCode { get; set; }
        public string CurrAccCode { get; set; }
        public string AddressCode { get; set; }
        public string AddressDescription { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public bool IsDefault { get; set; }
        public bool IsBlocked { get; set; }
    }
}
