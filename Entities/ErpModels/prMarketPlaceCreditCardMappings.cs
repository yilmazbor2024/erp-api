using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prMarketPlaceCreditCardMappings")]
    public partial class prMarketPlaceCreditCardMappings
    {
        public prMarketPlaceCreditCardMappings()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MarketPlaceCode { get; set; }

        [Key]
        [Required]
        public byte MarketPlacePaymentType { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CreditCardTypeCode { get; set; }

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
        public virtual cdCreditCardType cdCreditCardType { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsMarketPlace bsMarketPlace { get; set; }

    }
}
