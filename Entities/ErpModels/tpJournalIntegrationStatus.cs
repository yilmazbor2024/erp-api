using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpJournalIntegrationStatus")]
    public partial class tpJournalIntegrationStatus
    {
        public tpJournalIntegrationStatus()
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
        public byte IntegrationStatus { get; set; }

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
