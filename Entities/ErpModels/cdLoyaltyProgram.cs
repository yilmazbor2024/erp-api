using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdLoyaltyProgram")]
    public partial class cdLoyaltyProgram
    {
        public cdLoyaltyProgram()
        {
            cdLoyaltyProgramDescs = new HashSet<cdLoyaltyProgramDesc>();
            dfCompanyLoyaltyPrograms = new HashSet<dfCompanyLoyaltyProgram>();
            prCustomerLoyaltyPrograms = new HashSet<prCustomerLoyaltyProgram>();
            prCustomerLoyaltyProgramHistorys = new HashSet<prCustomerLoyaltyProgramHistory>();
            prDiscountOfferRuless = new HashSet<prDiscountOfferRules>();
            prLoyaltyProgramLevels = new HashSet<prLoyaltyProgramLevel>();
            prLoyaltyProgramLevelHistorys = new HashSet<prLoyaltyProgramLevelHistory>();
            prLoyaltyProgramNotess = new HashSet<prLoyaltyProgramNotes>();
            prLoyaltyProgramProcessAvailableStatuss = new HashSet<prLoyaltyProgramProcessAvailableStatus>();
            prLoyaltyProgramProcessAvailableStatusHistorys = new HashSet<prLoyaltyProgramProcessAvailableStatusHistory>();
            prLoyaltyProgramProcessStatuss = new HashSet<prLoyaltyProgramProcessStatus>();
            prLoyaltyProgramProcessStatusHistorys = new HashSet<prLoyaltyProgramProcessStatusHistory>();
            tpInvoiceHeaderExtensions = new HashSet<tpInvoiceHeaderExtension>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramCode { get; set; }

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

        [Required]
        public byte[] LogoImage { get; set; }

        public virtual ICollection<cdLoyaltyProgramDesc> cdLoyaltyProgramDescs { get; set; }
        public virtual ICollection<dfCompanyLoyaltyProgram> dfCompanyLoyaltyPrograms { get; set; }
        public virtual ICollection<prCustomerLoyaltyProgram> prCustomerLoyaltyPrograms { get; set; }
        public virtual ICollection<prCustomerLoyaltyProgramHistory> prCustomerLoyaltyProgramHistorys { get; set; }
        public virtual ICollection<prDiscountOfferRules> prDiscountOfferRuless { get; set; }
        public virtual ICollection<prLoyaltyProgramLevel> prLoyaltyProgramLevels { get; set; }
        public virtual ICollection<prLoyaltyProgramLevelHistory> prLoyaltyProgramLevelHistorys { get; set; }
        public virtual ICollection<prLoyaltyProgramNotes> prLoyaltyProgramNotess { get; set; }
        public virtual ICollection<prLoyaltyProgramProcessAvailableStatus> prLoyaltyProgramProcessAvailableStatuss { get; set; }
        public virtual ICollection<prLoyaltyProgramProcessAvailableStatusHistory> prLoyaltyProgramProcessAvailableStatusHistorys { get; set; }
        public virtual ICollection<prLoyaltyProgramProcessStatus> prLoyaltyProgramProcessStatuss { get; set; }
        public virtual ICollection<prLoyaltyProgramProcessStatusHistory> prLoyaltyProgramProcessStatusHistorys { get; set; }
        public virtual ICollection<tpInvoiceHeaderExtension> tpInvoiceHeaderExtensions { get; set; }
    }
}
