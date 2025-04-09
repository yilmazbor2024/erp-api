using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpChippinProcessedPayment")]
    public partial class zpChippinProcessedPayment
    {
        public zpChippinProcessedPayment()
        {
        }

        [Key]
        [Required]
        public Guid ChippinProcessedPaymentID { get; set; }

        [Required]
        public Guid OtherPaymentLineID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FirmID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BranchID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PersonnelID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SocialRebateID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TransactionID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TransactionGuid { get; set; }

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

    }
}
