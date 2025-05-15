using System;

namespace ErpMobile.Api.Models.Office
{
    public class OfficeModel
    {
        public string OfficeCode { get; set; }
        public string OfficeDescription { get; set; }
        public string CompanyCode { get; set; }
        public bool IsExecutiveOffice { get; set; }
        public bool IsBlocked { get; set; }
    }
}
