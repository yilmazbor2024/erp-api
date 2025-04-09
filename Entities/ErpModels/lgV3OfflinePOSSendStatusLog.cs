using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("lgV3OfflinePOSSendStatusLog")]
    public partial class lgV3OfflinePOSSendStatusLog
    {
        public lgV3OfflinePOSSendStatusLog()
        {
        }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TransferName { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        [Key]
        [Required]
        public Guid ApplicationID { get; set; }

        [Required]
        public bool SendStatus { get; set; }

        public string LogText { get; set; }

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
