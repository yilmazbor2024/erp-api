using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPurchasingAgent")]
    public partial class cdPurchasingAgent
    {
        public cdPurchasingAgent()
        {
            tpPurchaseRequisitionClosedByInventorys = new HashSet<tpPurchaseRequisitionClosedByInventory>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PurchasingAgentCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FirstName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LastName { get; set; }

        public string FirstLastName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityNum { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string MobilePhoneNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EMail { get; set; }

        [Required]
        public bool IsManager { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string UserGroupCode { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string UserName { get; set; }

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

        public virtual ICollection<tpPurchaseRequisitionClosedByInventory> tpPurchaseRequisitionClosedByInventorys { get; set; }
    }
}
