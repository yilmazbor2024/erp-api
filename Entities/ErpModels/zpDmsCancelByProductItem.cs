using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpDmsCancelByProductItem")]
    public partial class zpDmsCancelByProductItem
    {
        public zpDmsCancelByProductItem()
        {
        }

        [Key]
        [Required]
        public Guid CancelByProductItemID { get; set; }

        [Required]
        public Guid CancelByProductID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductCode { get; set; }

        [Required]
        public decimal CanceledAmount { get; set; }

        [Required]
        public int Count { get; set; }

        public string ApplicationName { get; set; }

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

    }
}
