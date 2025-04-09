using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpOrderHeaderExtension")]
    public partial class tpOrderHeaderExtension
    {
        public tpOrderHeaderExtension()
        {
        }

        [Key]
        [Required]
        public Guid OrderHeaderID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string InsuranceAgencyCode { get; set; }

        [Required]
        public bool IsInstantReserve { get; set; }

        [Required]
        public byte DiscountSubReasonCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DiscountReasonDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FastDeliveryCompanyCode { get; set; }

        [Required]
        public bool SendInvoiceByEMail { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EMailAddress { get; set; }

        [Required]
        public bool SendInvoiceBySMS { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string GSMNo { get; set; }

        [Required]
        public bool IsDistanceSale { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ContractandPaymentLink { get; set; }

        [Required]
        public bool IsDistanceSaleContractConfirmed { get; set; }

        [Required]
        public bool DistanceSalePaymentCompleted { get; set; }

        [Required]
        public bool IsEExport { get; set; }

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

        // Navigation Properties
        public virtual trOrderHeader trOrderHeader { get; set; }
        public virtual cdInsuranceAgency cdInsuranceAgency { get; set; }
        public virtual cdDiscountSubReason cdDiscountSubReason { get; set; }
        public virtual bsFastDeliveryCompany bsFastDeliveryCompany { get; set; }

    }
}
