using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpBulutTahsilatRetailCustomerBankPaymentList")]
    public partial class zpBulutTahsilatRetailCustomerBankPaymentList
    {
        public zpBulutTahsilatRetailCustomerBankPaymentList()
        {
        }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaymentID { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public decimal PaymentAmount { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CustomField1 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CustomField2 { get; set; }

        public string JsonData { get; set; }

        [Required]
        public bool PaymentIntegrated { get; set; }

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
