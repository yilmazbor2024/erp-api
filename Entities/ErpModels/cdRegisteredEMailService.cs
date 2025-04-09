using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdRegisteredEMailService")]
    public partial class cdRegisteredEMailService
    {
        public cdRegisteredEMailService()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string RegisteredEMailServiceCode { get; set; }

        [Key]
        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string RegisteredCompanyEmailAddress { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TransAuthorizedCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UserName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Password { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ReceiveServiceAddress { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object SendServiceAddress { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CommunicationTypeCode { get; set; }

        [Required]
        public bool IsTestAccount { get; set; }

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

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCommunicationType cdCommunicationType { get; set; }

    }
}
