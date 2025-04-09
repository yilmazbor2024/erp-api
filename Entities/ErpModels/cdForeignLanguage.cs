using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdForeignLanguage")]
    public partial class cdForeignLanguage
    {
        public cdForeignLanguage()
        {
            cdForeignLanguageDescs = new HashSet<cdForeignLanguageDesc>();
            prEmployeeForeignLanguages = new HashSet<prEmployeeForeignLanguage>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ForeignLanguageCode { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<cdForeignLanguageDesc> cdForeignLanguageDescs { get; set; }
        public virtual ICollection<prEmployeeForeignLanguage> prEmployeeForeignLanguages { get; set; }
    }
}
