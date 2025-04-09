using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSubCurrAccAttribute")]
    public partial class cdSubCurrAccAttribute
    {
        public cdSubCurrAccAttribute()
        {
            cdSubCurrAccAttributeDescs = new HashSet<cdSubCurrAccAttributeDesc>();
            prSubCurrAccAttributes = new HashSet<prSubCurrAccAttribute>();
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttributeCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

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
        public virtual cdSubCurrAccAttributeType cdSubCurrAccAttributeType { get; set; }

        public virtual ICollection<cdSubCurrAccAttributeDesc> cdSubCurrAccAttributeDescs { get; set; }
        public virtual ICollection<prSubCurrAccAttribute> prSubCurrAccAttributes { get; set; }
    }
}
