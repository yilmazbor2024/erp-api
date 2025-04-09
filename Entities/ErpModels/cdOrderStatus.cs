using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdOrderStatus")]
    public partial class cdOrderStatus
    {
        public cdOrderStatus()
        {
            cdOrderStatusDescs = new HashSet<cdOrderStatusDesc>();
            tpOrderLineExtensions = new HashSet<tpOrderLineExtension>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string OrderStatusCode { get; set; }

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

        public virtual ICollection<cdOrderStatusDesc> cdOrderStatusDescs { get; set; }
        public virtual ICollection<tpOrderLineExtension> tpOrderLineExtensions { get; set; }
    }
}
