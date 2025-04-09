using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpPickingFromSectionTransfer")]
    public partial class tpPickingFromSectionTransfer
    {
        public tpPickingFromSectionTransfer()
        {
        }

        [Key]
        [Required]
        public Guid PickingHeaderID { get; set; }

        [Required]
        public Guid InnerHeaderID { get; set; }

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
        public virtual trInnerHeader trInnerHeader { get; set; }
        public virtual trPickingHeader trPickingHeader { get; set; }

    }
}
