using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trContractProduct")]
    public partial class trContractProduct
    {
        public trContractProduct()
        {
        }

        [Key]
        [Required]
        public Guid ContractID { get; set; }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim1Code { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim2Code { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim3Code { get; set; }

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
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual trContract trContract { get; set; }

    }
}
