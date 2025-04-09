using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpDmsRegisterPaidCheckItem")]
    public partial class zpDmsRegisterPaidCheckItem
    {
        public zpDmsRegisterPaidCheckItem()
        {
        }

        [Key]
        [Required]
        public Guid RegisterPaidCheckItemID { get; set; }

        [Required]
        public Guid RegisterPaidCheckID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string InvoiceLineID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ProductName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductBarcode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColourCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Size { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public decimal TaxRate { get; set; }

        [Required]
        public decimal ProductUnitPrice { get; set; }

        [Required]
        public decimal ProductPaidTotal { get; set; }

        [Required]
        public int BenefitBoundary { get; set; }

        public string ApplicationName { get; set; }

        [Required]
        public decimal OriginalTotalPrice { get; set; }

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

    }
}
