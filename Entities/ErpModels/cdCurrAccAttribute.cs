using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCurrAccAttribute")]
    public partial class cdCurrAccAttribute
    {
        public cdCurrAccAttribute()
        {
            cdCurrAccAttributeDescs = new HashSet<cdCurrAccAttributeDesc>();
            prCurrAccAttributes = new HashSet<prCurrAccAttribute>();
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
        public virtual cdCurrAccAttributeType cdCurrAccAttributeType { get; set; }

        public virtual ICollection<cdCurrAccAttributeDesc> cdCurrAccAttributeDescs { get; set; }
        public virtual ICollection<prCurrAccAttribute> prCurrAccAttributes { get; set; }
    }
}
