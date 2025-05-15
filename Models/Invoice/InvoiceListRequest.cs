using System;

namespace ErpMobile.Api.Models.Invoice
{
    public class InvoiceListRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "InvoiceDate";
        public string SortDirection { get; set; } = "desc";
        
        // Filtreler
        public string InvoiceNumber { get; set; }
        public string InvoiceTypeCode { get; set; }
        public string CustomerCode { get; set; }
        public string VendorCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsSuspended { get; set; }
        public bool? IsReturn { get; set; }
        public bool? IsEInvoice { get; set; }
        public string CompanyCode { get; set; }
        public string StoreCode { get; set; }
        public string WarehouseCode { get; set; }
        public string ExpenseTypeCode { get; set; }
        public string LangCode { get; set; } = "TR";
    }
}
