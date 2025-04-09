using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prItemProcessPermits")]
    public partial class prItemProcessPermits
    {
        public prItemProcessPermits()
        {
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Key]
        [Required]
        public byte ItemProcessPermitTypeCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ItemProcessPermitLevel { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Code { get; set; }

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
        public virtual bsItemProcessPermitType bsItemProcessPermitType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
