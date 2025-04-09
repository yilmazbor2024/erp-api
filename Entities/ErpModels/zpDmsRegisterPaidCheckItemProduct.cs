using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpDmsRegisterPaidCheckItemProduct")]
    public partial class zpDmsRegisterPaidCheckItemProduct
    {
        public zpDmsRegisterPaidCheckItemProduct()
        {
        }

        [Key]
        [Required]
        public Guid RegisterPaidCheckItemProductID { get; set; }

        [Required]
        public Guid RegisterPaidCheckItemID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ProductName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductBarcode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColourCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Size { get; set; }

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
