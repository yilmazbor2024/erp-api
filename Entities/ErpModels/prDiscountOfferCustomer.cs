using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDiscountOfferCustomer")]
    public partial class prDiscountOfferCustomer
    {
        public prDiscountOfferCustomer()
        {
        }

        [Key]
        [Required]
        public object ProcessCode { get; set; }

        [Key]
        [Required]
        public byte DiscountOfferStageCode { get; set; }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        public string CustomerQuery { get; set; }

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
