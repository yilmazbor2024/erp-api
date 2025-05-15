using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models
{
    public class TaxOfficeResponse
    {
        public string TaxOfficeCode { get; set; }
        public string TaxOfficeDescription { get; set; }
        public string CityCode { get; set; }
        public string CityDescription { get; set; }
        public bool IsBlocked { get; set; }
    }
}
