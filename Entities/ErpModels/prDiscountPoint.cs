using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDiscountPoint")]
    public partial class prDiscountPoint
    {
        public prDiscountPoint()
        {
        }

        [Key]
        [Required]
        public Guid DiscountPointID { get; set; }

        [Required]
        public byte CustomerTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomerCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountPointTypeCode { get; set; }

        [Required]
        public byte PointRecordTypeCode { get; set; }

        [Required]
        public DateTime FirstValidDate { get; set; }

        [Required]
        public DateTime LastValidDate { get; set; }

        [Required]
        public decimal Point { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PointModifyReasonCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Description { get; set; }

        public Guid? InvoiceHeaderID { get; set; }

        public Guid? OrderHeaderID { get; set; }

        public Guid? OrderCancelDetailHeaderID { get; set; }

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
        public virtual cdPointModifyReason cdPointModifyReason { get; set; }
        public virtual cdDiscountPointType cdDiscountPointType { get; set; }
        public virtual bsPointRecordType bsPointRecordType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
