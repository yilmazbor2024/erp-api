using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInvoiceSGKExtensions")]
    public partial class tpInvoiceSGKExtensions
    {
        public tpInvoiceSGKExtensions()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SubInvoiceType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        // Navigation Properties
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }

    }
}
