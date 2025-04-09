using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCustomProcessGroupAtt")]
    public partial class prCustomProcessGroupAtt
    {
        public prCustomProcessGroupAtt()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomProcessGroupCode { get; set; }

        [Key]
        [Required]
        public byte ServiceTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ServiceCode { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool ChangePrice { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

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
        public virtual cdItem cdItem { get; set; }
        public virtual cdCustomProcessGroup cdCustomProcessGroup { get; set; }

    }
}
