using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsOfficialTaxType")]
    public partial class bsOfficialTaxType
    {
        public bsOfficialTaxType()
        {
            cdPCTs = new HashSet<cdPCT>();
            cdVats = new HashSet<cdVat>();
        }

        [Key]
        [Required]
        public int OfficialTaxTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string OfficialTaxTypeDescription { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ShortDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<cdPCT> cdPCTs { get; set; }
        public virtual ICollection<cdVat> cdVats { get; set; }
    }
}
