using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfCustomerOnlinePayment")]
    public partial class dfCustomerOnlinePayment
    {
        public dfCustomerOnlinePayment()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ApplicationWebAddress { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BalanceReportFile { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BalanceReportFileMobile { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string AbstractReportFile { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string AbstractReportFileMobile { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string AvailableCurrencyCodes { get; set; }

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
