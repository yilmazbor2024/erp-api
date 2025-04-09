using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCustomerShoppingLevel")]
    public partial class cdCustomerShoppingLevel
    {
        public cdCustomerShoppingLevel()
        {
            cdCustomerCRMGroups = new HashSet<cdCustomerCRMGroup>();
            cdCustomerShoppingLevelDescs = new HashSet<cdCustomerShoppingLevelDesc>();
        }

        [Key]
        [Required]
        public byte CustomerShoppingLevelCode { get; set; }

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

        public virtual ICollection<cdCustomerCRMGroup> cdCustomerCRMGroups { get; set; }
        public virtual ICollection<cdCustomerShoppingLevelDesc> cdCustomerShoppingLevelDescs { get; set; }
    }
}
