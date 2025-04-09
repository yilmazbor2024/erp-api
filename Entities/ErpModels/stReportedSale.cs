using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("stReportedSale")]
    public partial class stReportedSale
    {
        public stReportedSale()
        {
        }

        [Key]
        [Required]
        public Guid ReportedSaleLineID { get; set; }

        [Required]
        public double Qty1 { get; set; }

    }
}
