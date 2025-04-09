using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdProductPointType")]
    public partial class cdProductPointType
    {
        public cdProductPointType()
        {
            cdDiscountPointTypes = new HashSet<cdDiscountPointType>();
            cdProductPointTypeDescs = new HashSet<cdProductPointTypeDesc>();
            prProductPoints = new HashSet<prProductPoint>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ProductPointTypeCode { get; set; }

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

        public virtual ICollection<cdDiscountPointType> cdDiscountPointTypes { get; set; }
        public virtual ICollection<cdProductPointTypeDesc> cdProductPointTypeDescs { get; set; }
        public virtual ICollection<prProductPoint> prProductPoints { get; set; }
    }
}
