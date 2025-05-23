using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Invoice
{
    public class InvoiceListRequest
    {
        public int Page { get; set; } = 1;
        
        // PageNumber parametresi frontend uyumluluğu için eklendi
        // Bu değer Page özelliğine aktarılacak
        public int PageNumber 
        { 
            get { return Page; }
            set { Page = value; }
        }
        
        public int PageSize { get; set; } = 10;
        
        private string _sortBy = "InvoiceDate";
        public string SortBy 
        { 
            get { return _sortBy; }
            set 
            { 
                // Büyük-küçük harf düzeltmesi
                if (string.Equals(value, "invoicedate", StringComparison.OrdinalIgnoreCase))
                    _sortBy = "InvoiceDate";
                else if (string.Equals(value, "customerdescription", StringComparison.OrdinalIgnoreCase))
                    _sortBy = "CustomerDescription";
                else if (string.Equals(value, "invoicenumber", StringComparison.OrdinalIgnoreCase))
                    _sortBy = "InvoiceNumber";
                else if (string.Equals(value, "invoicetypedescription", StringComparison.OrdinalIgnoreCase))
                    _sortBy = "InvoiceTypeDescription";
                else
                    _sortBy = value; 
            }
        }
        
        private string _sortDirection = "desc";
        public string SortDirection 
        { 
            get { return _sortDirection; }
            set 
            { 
                // Büyük-küçük harf düzeltmesi
                if (string.Equals(value, "desc", StringComparison.OrdinalIgnoreCase) || 
                    string.Equals(value, "descend", StringComparison.OrdinalIgnoreCase))
                    _sortDirection = "desc";
                else if (string.Equals(value, "asc", StringComparison.OrdinalIgnoreCase) || 
                         string.Equals(value, "ascend", StringComparison.OrdinalIgnoreCase))
                    _sortDirection = "asc";
                else
                    _sortDirection = value;
            }
        }
        
        // Filtreler
        public string InvoiceNumber { get; set; }

        
        // ProcessCode fatura işlem kodu (WS: Toptan Satış, BP: Toptan Alış, EP: Masraf Alış, EXS: Masraf Satış)
        public string ProcessCode { get; set; }
        
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
        public string LangCode { get; set; } = "TR";
    }
}
