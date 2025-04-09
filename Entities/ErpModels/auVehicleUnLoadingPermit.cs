using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auVehicleUnLoadingPermit")]
    public partial class auVehicleUnLoadingPermit
    {
        public auVehicleUnLoadingPermit()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }
 

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RoleCode { get; set; }

        [Required]
        public bool CanSelect { get; set; }

        [Required]
        public bool CanLocked { get; set; }

        [Required]
        public bool CanUnLocked { get; set; }

        [Required]
        public bool CanEditAmount { get; set; }

        [Required]
        public bool CanInsert { get; set; }

        [Required]
        public short InsertLastNDay { get; set; }

        [Required]
        public DateTime InsertMinDate { get; set; }

        [Required]
        public DateTime InsertMaxDate { get; set; }

        [Required]
        public bool CanUpdate { get; set; }

        [Required]
        public short UpdateLastNDay { get; set; }

        [Required]
        public DateTime UpdateMinDate { get; set; }

        [Required]
        public DateTime UpdateMaxDate { get; set; }

        [Required]
        public bool CanDelete { get; set; }

        [Required]
        public short DeleteLastNDay { get; set; }

        [Required]
        public DateTime DeleteMinDate { get; set; }

        [Required]
        public DateTime DeleteMaxDate { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdRole cdRole { get; set; }

    }
}
