using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSubCurrAccAttributeType")]
    public partial class cdSubCurrAccAttributeType
    {
        public cdSubCurrAccAttributeType()
        {
            cdSubCurrAccAttributes = new HashSet<cdSubCurrAccAttribute>();
            cdSubCurrAccAttributeTypeDescs = new HashSet<cdSubCurrAccAttributeTypeDesc>();
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        [Required]
        public bool UseReports { get; set; }

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
        public virtual bsCurrAccType bsCurrAccType { get; set; }

        public virtual ICollection<cdSubCurrAccAttribute> cdSubCurrAccAttributes { get; set; }
        public virtual ICollection<cdSubCurrAccAttributeTypeDesc> cdSubCurrAccAttributeTypeDescs { get; set; }
    }
}
