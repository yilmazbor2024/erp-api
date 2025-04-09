using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPromotionGroup")]
    public partial class cdPromotionGroup
    {
        public cdPromotionGroup()
        {
           
            cdPromotionGroupDescs = new HashSet<cdPromotionGroupDesc>();
        }

        [Key]
        [StringLength(50)]
        public string PromotionGroupCode { get; set; }

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
       
        public virtual ICollection<cdPromotionGroupDesc> cdPromotionGroupDescs { get; set; }
    }
}
