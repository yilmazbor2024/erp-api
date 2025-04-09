using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpOrdersViaInternetInfo")]
    public partial class tpOrdersViaInternetInfo
    {
        public tpOrdersViaInternetInfo()
        {
        }

        [Key]
        [Required]
        public Guid OrderHeaderID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SalesURL { get; set; }

        [Required]
        public byte PaymentTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaymentTypeDescription { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaymentAgent { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public DateTime SendDate { get; set; }

        [Required]
        public TimeSpan SendTime { get; set; }

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
        public virtual bsInternetPaymentType bsInternetPaymentType { get; set; }

    }
}
