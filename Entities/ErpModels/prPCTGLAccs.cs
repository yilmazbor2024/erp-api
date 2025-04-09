using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prPCTGLAccs")]
    public partial class prPCTGLAccs
    {
        public prPCTGLAccs()
        {
        }

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PCTCode { get; set; }

        [Key]
        [Required]
        public object ProcessCode { get; set; }

        [Key]
        [Required]
        public byte PostAccTypeCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GLAccCode { get; set; }

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
        public virtual bsPostAccType bsPostAccType { get; set; }
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdPCT cdPCT { get; set; }

    }
}
