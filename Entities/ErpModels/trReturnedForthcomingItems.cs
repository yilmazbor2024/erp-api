using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trReturnedForthcomingItems")]
    public partial class trReturnedForthcomingItems
    {
        public trReturnedForthcomingItems()
        {
        }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public byte ProcessFlowCode { get; set; }

        [Key]
        [Required]
        public Guid HeaderID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Required]
        public object FromCompanyCode { get; set; }

        [Required]
        public byte CustomerTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomerCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public TimeSpan TransactionTime { get; set; }

        public Guid? CreatedHeaderID { get; set; }

        [Required]
        public bool IsPosted { get; set; }

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
