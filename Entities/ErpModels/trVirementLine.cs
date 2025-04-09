using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trVirementLine")]
    public partial class trVirementLine
    {
        public trVirementLine()
        {
            tpVirementFTAttributes = new HashSet<tpVirementFTAttribute>();
        }

        [Key]
        [Required]
        public Guid VirementLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string DebitReasonCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [Required]
        public Guid CreditDebitHeaderID { get; set; }

        [Required]
        public Guid DebitDebitHeaderID { get; set; }

        [Required]
        public Guid VirementHeaderID { get; set; }

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
        public virtual trVirementHeader trVirementHeader { get; set; }
        public virtual cdDebitReason cdDebitReason { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual trDebitHeader trDebitHeader { get; set; }

        public virtual ICollection<tpVirementFTAttribute> tpVirementFTAttributes { get; set; }
    }
}
