using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCustomsOffices")]
    public partial class cdCustomsOffices
    {
        public cdCustomsOffices()
        {
            cdCustomsOfficesDescs = new HashSet<cdCustomsOfficesDesc>();
            cdExportFiles = new HashSet<cdExportFile>();
            cdImportFiles = new HashSet<cdImportFile>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomsOfficesCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

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

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<cdCustomsOfficesDesc> cdCustomsOfficesDescs { get; set; }
        public virtual ICollection<cdExportFile> cdExportFiles { get; set; }
        public virtual ICollection<cdImportFile> cdImportFiles { get; set; }
    }
}
