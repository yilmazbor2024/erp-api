using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfCustomsStore")]
    public partial class dfCustomsStore
    {
        public dfCustomsStore()
        {
        }

        [Key]
        [Required]
        public byte StoreTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SubcontractorCompanyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SubcontractorStoreCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SubcontractorCashTillCode { get; set; }

        [Required]
        public bool IsDepartureStore { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ServiceUrl { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomsWarehouseNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Password { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AreaCode { get; set; }

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
