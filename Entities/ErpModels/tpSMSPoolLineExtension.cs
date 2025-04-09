using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpSMSPoolLineExtension")]
    public partial class tpSMSPoolLineExtension
    {
        public tpSMSPoolLineExtension()
        {
        }

        [Key]
        [Required]
        public Guid SmsPoolLineID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DiscountOfferPassword { get; set; }

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
        public virtual trSMSPoolLine trSMSPoolLine { get; set; }

    }
}
