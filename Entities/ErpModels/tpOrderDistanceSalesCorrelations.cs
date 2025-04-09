using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpOrderDistanceSalesCorrelations")]
    public partial class tpOrderDistanceSalesCorrelations
    {
        public tpOrderDistanceSalesCorrelations()
        {
        }

        [Key]
        [Required]
        public Guid CorrelationID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public Guid OrderHeaderID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentProviderCode { get; set; }

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
        public virtual cdPaymentProvider cdPaymentProvider { get; set; }

    }
}
