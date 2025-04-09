using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpMobildevIVTReconciliation")]
    public partial class rpMobildevIVTReconciliation
    {
        public rpMobildevIVTReconciliation()
        {
        }

        [Key]
        [Required]
        public Guid MobildevIVTReconciliationID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string FirstName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LastName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Msisdn { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Email { get; set; }

        [Required]
        public int EtkMsisdn { get; set; }

        [Required]
        public int EtkCall { get; set; }

        [Required]
        public int EtkEmail { get; set; }

        [Required]
        public int EtkShare { get; set; }

        [Required]
        public int KvkkShare { get; set; }

        [Required]
        public int KvkkProcess { get; set; }

        [Required]
        public int KvkkInternational { get; set; }

        [Required]
        public int ExtendedLoyalty { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public int AccountTypeId { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string AccountTypeText { get; set; }

        [Required]
        public int PermSourceId { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string PermSourceText { get; set; }

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
