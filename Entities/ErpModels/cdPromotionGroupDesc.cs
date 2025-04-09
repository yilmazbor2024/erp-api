using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPromotionGroupDesc")]
    public class cdPromotionGroupDesc
    {
        [Key]
        [StringLength(50)]
        public string PromotionGroupCode { get; set; }

        [Key]
        [StringLength(5)]
        public string LangCode { get; set; }

        [StringLength(250)]
        public string PromotionGroupDesc { get; set; }

        [StringLength(50)]
        public string CreatedUserName { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string LastUpdatedUserName { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public Guid RowGuid { get; set; }

        // Navigation Properties
        [ForeignKey("PromotionGroupCode")]
        public virtual cdPromotionGroup cdPromotionGroup { get; set; }

        [ForeignKey("LangCode")]
        public virtual cdDataLanguage cdDataLanguage { get; set; }
    }
}
