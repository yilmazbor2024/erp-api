using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDiscountOfferMethodScript")]
    public partial class prDiscountOfferMethodScript
    {
        public prDiscountOfferMethodScript()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountOfferMethodCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BaseClassName { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string ScriptLanguage { get; set; }

        public string ScriptText { get; set; }

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

    }
}
