using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("srSerialNumber")]
    public partial class srSerialNumber
    {
        public srSerialNumber()
        {
        }

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [Key]
        [Required]
        public byte StoreTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Key]
        [Required]
        public byte ProcessFlowCode { get; set; }

        [Key]
        [Required]
        public byte FormType { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SeriesCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Series { get; set; }

        [Required]
        public decimal StartingNumber { get; set; }

        [Required]
        public decimal EndingNumber { get; set; }

        [Required]
        public decimal LastNumber { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsProcessFlow bsProcessFlow { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }

    }
}
