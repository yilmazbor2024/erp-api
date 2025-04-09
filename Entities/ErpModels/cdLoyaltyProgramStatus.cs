using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdLoyaltyProgramStatus")]
    public partial class cdLoyaltyProgramStatus
    {
        public cdLoyaltyProgramStatus()
        {
            cdLoyaltyProgramStatusDescs = new HashSet<cdLoyaltyProgramStatusDesc>();
            prCustomerLoyaltyPrograms = new HashSet<prCustomerLoyaltyProgram>();
            prCustomerLoyaltyProgramHistorys = new HashSet<prCustomerLoyaltyProgramHistory>();
            prLoyaltyProgramProcessAvailableStatuss = new HashSet<prLoyaltyProgramProcessAvailableStatus>();
            prLoyaltyProgramProcessAvailableStatusHistorys = new HashSet<prLoyaltyProgramProcessAvailableStatusHistory>();
            prLoyaltyProgramProcessStatuss = new HashSet<prLoyaltyProgramProcessStatus>();
            prLoyaltyProgramProcessStatusHistorys = new HashSet<prLoyaltyProgramProcessStatusHistory>();
            tpInvoiceHeaderExtensions = new HashSet<tpInvoiceHeaderExtension>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LoyaltyProgramStatusCode { get; set; }

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

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string UserWarning { get; set; }

        [Required]
        public bool IsUnchangeable { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ColorHex { get; set; }

        public virtual ICollection<cdLoyaltyProgramStatusDesc> cdLoyaltyProgramStatusDescs { get; set; }
        public virtual ICollection<prCustomerLoyaltyProgram> prCustomerLoyaltyPrograms { get; set; }
        public virtual ICollection<prCustomerLoyaltyProgramHistory> prCustomerLoyaltyProgramHistorys { get; set; }
        public virtual ICollection<prLoyaltyProgramProcessAvailableStatus> prLoyaltyProgramProcessAvailableStatuss { get; set; }
        public virtual ICollection<prLoyaltyProgramProcessAvailableStatusHistory> prLoyaltyProgramProcessAvailableStatusHistorys { get; set; }
        public virtual ICollection<prLoyaltyProgramProcessStatus> prLoyaltyProgramProcessStatuss { get; set; }
        public virtual ICollection<prLoyaltyProgramProcessStatusHistory> prLoyaltyProgramProcessStatusHistorys { get; set; }
        public virtual ICollection<tpInvoiceHeaderExtension> tpInvoiceHeaderExtensions { get; set; }
    }
}
