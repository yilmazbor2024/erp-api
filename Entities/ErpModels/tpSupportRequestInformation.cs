using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpSupportRequestInformation")]
    public partial class tpSupportRequestInformation
    {
        public tpSupportRequestInformation()
        {
        }

        [Key]
        [Required]
        public Guid SupportRequestInformationID { get; set; }

        [Required]
        public Guid SupportRequestHeaderID { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Information { get; set; }

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
        public virtual trSupportRequestHeader trSupportRequestHeader { get; set; }

    }
}
