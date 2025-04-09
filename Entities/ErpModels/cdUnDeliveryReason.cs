using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdUnDeliveryReason")]
    public partial class cdUnDeliveryReason
    {
        public cdUnDeliveryReason()
        {
            cdUnDeliveryReasonDescs = new HashSet<cdUnDeliveryReasonDesc>();
            tpVehicleLoadingLineDeliveryStatuss = new HashSet<tpVehicleLoadingLineDeliveryStatus>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnDeliveryReasonCode { get; set; }

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

        public virtual ICollection<cdUnDeliveryReasonDesc> cdUnDeliveryReasonDescs { get; set; }
        public virtual ICollection<tpVehicleLoadingLineDeliveryStatus> tpVehicleLoadingLineDeliveryStatuss { get; set; }
    }
}
