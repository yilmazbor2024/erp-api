using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfWeArePlanetTaxFreeStore")]
    public partial class dfWeArePlanetTaxFreeStore
    {
        public dfWeArePlanetTaxFreeStore()
        {
        }

        [Key]
        [Required]
        public byte StoreTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ClientID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ClientSecret { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Terminal { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MerchandiseGroup { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string VatCode { get; set; }

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
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
