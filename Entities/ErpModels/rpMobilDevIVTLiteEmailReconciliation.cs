using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpMobilDevIVTLiteEmailReconciliation")]
    public partial class rpMobilDevIVTLiteEmailReconciliation
    {
        public rpMobilDevIVTLiteEmailReconciliation()
        {
        }

        [Key]
        [Required]
        public Guid MobilDevIVTLiteEmailReconciliationID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string email { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string recordId { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string firstName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string lastName { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        [Required]
        public DateTime updatedAt { get; set; }

        [Required]
        public int detail_brandId { get; set; }

        [Required]
        public int detail_recipientType { get; set; }

        [Required]
        public int detail_sendmail { get; set; }

        [Required]
        public int detail_source { get; set; }

        [Required]
        public DateTime detail_permissiondate { get; set; }

        [Required]
        public DateTime detail_createdAt { get; set; }

        [Required]
        public DateTime detail_updatedAt { get; set; }

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
