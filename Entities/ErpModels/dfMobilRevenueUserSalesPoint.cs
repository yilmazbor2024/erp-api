using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfMobilRevenueUserSalesPoint")]
    public partial class dfMobilRevenueUserSalesPoint
    {
        public dfMobilRevenueUserSalesPoint()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string UserGroupCode { get; set; }

        [Key]
        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string UserName { get; set; }

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [Key]
        [Required]
        public byte StoreTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

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
        public virtual dfMobilRevenueUser dfMobilRevenueUser { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
