using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trReserveTransfer")]
    public partial class trReserveTransfer
    {
        public trReserveTransfer()
        {
        }

        [Key]
        [Required]
        public Guid ReserveTransferID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string FromWarehouseCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ToWarehouseCode { get; set; }

        [Required]
        public Guid BaseReserveHeaderID { get; set; }

        [Required]
        public Guid ReturnReserveHeaderID { get; set; }

        public Guid? InnerHeaderID { get; set; }

        [Required]
        public Guid ReserveHeaderID { get; set; }

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
        public virtual trReserveHeader trReserveHeader { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual trInnerHeader trInnerHeader { get; set; }

    }
}
