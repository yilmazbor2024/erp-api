using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfGlobalItemSize")]
    public partial class dfGlobalItemSize
    {
        public dfGlobalItemSize()
        {
        }

        [Key]
        [Required]
        public byte GlobalDefaultCode { get; set; }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Required]
        public byte MinCodeSize { get; set; }

        [Required]
        public byte MaxCodeSize { get; set; }

        [Required]
        public bool AutoGenItemCode { get; set; }

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
        public virtual dfGlobalDefault dfGlobalDefault { get; set; }
        public virtual bsItemType bsItemType { get; set; }

    }
}
