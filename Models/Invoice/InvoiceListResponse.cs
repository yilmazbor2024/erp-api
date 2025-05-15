using System.Collections.Generic;

namespace ErpMobile.Api.Models.Invoice
{
    public class InvoiceListResult
    {
        public List<InvoiceHeaderModel> Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
