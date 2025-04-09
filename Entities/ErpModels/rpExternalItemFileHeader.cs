using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpExternalItemFileHeader")]
    public partial class rpExternalItemFileHeader
    {
        public rpExternalItemFileHeader()
        {
            rpExternalItemFileLines = new HashSet<rpExternalItemFileLine>();
        }

        [Key]
        [Required]
        public Guid ExternalItemFileHeaderID { get; set; }

        [Required]
        public object ItemFileNumber { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public byte FileFormatTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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
        public virtual bsFileFormatType bsFileFormatType { get; set; }

        public virtual ICollection<rpExternalItemFileLine> rpExternalItemFileLines { get; set; }
    }
}
