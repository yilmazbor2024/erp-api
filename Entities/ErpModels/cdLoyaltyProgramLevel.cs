using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdLoyaltyProgramLevel")]
    public partial class cdLoyaltyProgramLevel
    {
        public cdLoyaltyProgramLevel()
        {
            cdLoyaltyProgramLevelDescs = new HashSet<cdLoyaltyProgramLevelDesc>();
            prCustomerLoyaltyPrograms = new HashSet<prCustomerLoyaltyProgram>();
            prCustomerLoyaltyProgramHistorys = new HashSet<prCustomerLoyaltyProgramHistory>();
            prLoyaltyProgramLevels = new HashSet<prLoyaltyProgramLevel>();
            prLoyaltyProgramLevelHistorys = new HashSet<prLoyaltyProgramLevelHistory>();
            tpInvoiceHeaderExtensions = new HashSet<tpInvoiceHeaderExtension>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramLevelCode { get; set; }

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

        public virtual ICollection<cdLoyaltyProgramLevelDesc> cdLoyaltyProgramLevelDescs { get; set; }
        public virtual ICollection<prCustomerLoyaltyProgram> prCustomerLoyaltyPrograms { get; set; }
        public virtual ICollection<prCustomerLoyaltyProgramHistory> prCustomerLoyaltyProgramHistorys { get; set; }
        public virtual ICollection<prLoyaltyProgramLevel> prLoyaltyProgramLevels { get; set; }
        public virtual ICollection<prLoyaltyProgramLevelHistory> prLoyaltyProgramLevelHistorys { get; set; }
        public virtual ICollection<tpInvoiceHeaderExtension> tpInvoiceHeaderExtensions { get; set; }
    }
}
