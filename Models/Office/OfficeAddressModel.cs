using System;

namespace ErpMobile.Api.Models.Office
{
    public class OfficeAddressModel
    {
        public string OfficeCode { get; set; }
        public string Address { get; set; }
        public string SiteName { get; set; }
        public string BuildingName { get; set; }
        public string BuildingNum { get; set; }
        public string FloorNum { get; set; }
        public string DoorNum { get; set; }
        public string QuarterName { get; set; }
        public string Boulevard { get; set; }
        public string Street { get; set; }
        public string Road { get; set; }
        public string CountryCode { get; set; }
        public string CountryDescription { get; set; }
        public string StateCode { get; set; }
        public string StateDescription { get; set; }
        public string CityCode { get; set; }
        public string CityDescription { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictDescription { get; set; }
        public string ZipCode { get; set; }
        public string DrivingDirections { get; set; }
    }
}
