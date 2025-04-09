using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdEducationStatusDesc")]
    public partial class cdEducationStatusDesc
    {
        public cdEducationStatusDesc()
        {
        }

        [Key]
        [Required]
        public byte EducationStatusCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string EducationStatusDescription { get; set; }

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
        public virtual cdEducationStatus cdEducationStatus { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
