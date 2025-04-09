using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdUniFreeTenderType")]
    public partial class cdUniFreeTenderType
    {
        public cdUniFreeTenderType()
        {
            prUniFreeTenderTypeMappings = new HashSet<prUniFreeTenderTypeMapping>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TenderTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TenderTypeDescription { get; set; }

        [Required]
        public byte PaymentMethod { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public byte ExchangeTypeCode { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdExchangeType cdExchangeType { get; set; }

        public virtual ICollection<prUniFreeTenderTypeMapping> prUniFreeTenderTypeMappings { get; set; }
    }
}
