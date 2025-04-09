using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpTuratelADMCommunicationReconciliation")]
    public partial class rpTuratelADMCommunicationReconciliation
    {
        public rpTuratelADMCommunicationReconciliation()
        {
        }

        [Key]
        [Required]
        public Guid TuratelADMCommunicationReconciliationID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Tckn { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Msisdn { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string EMailAddress { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Name { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Surname { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string AccountType { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Location { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SpecialField01 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SpecialField02 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SpecialField03 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SpecialField04 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SpecialField05 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SpecialField06 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SpecialField07 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SpecialField08 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SpecialField09 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SpecialField10 { get; set; }

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
