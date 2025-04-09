using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trTransferPlanProduct")]
    public partial class trTransferPlanProduct
    {
        public trTransferPlanProduct()
        {
            trTransferPlanProductQtys = new HashSet<trTransferPlanProductQty>();
        }

        [Key]
        [Required]
        public Guid TransferPlanProductID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public byte ProductTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

 

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim1Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim2Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim3Code { get; set; }

        [Required]
        public Guid TransferPlanID { get; set; }

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
        public virtual trTransferPlan trTransferPlan { get; set; }
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual cdColor cdColor { get; set; }

        public virtual ICollection<trTransferPlanProductQty> trTransferPlanProductQtys { get; set; }
    }
}
