using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpPaymentRegisterInfo")]
    public partial class tpPaymentRegisterInfo
    {
        public tpPaymentRegisterInfo()
        {
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        [Key]
        [Required]
        public Guid ApplicationID { get; set; }

        [Required]
        public Guid ApplicationLineID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CashRegisterSerialNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string zNo { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public TimeSpan DocumentTime { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EJNumber { get; set; }

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
        public virtual bsApplication bsApplication { get; set; }

    }
}
