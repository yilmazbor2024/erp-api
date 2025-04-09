using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prPaymentNumberGroup")]
    public partial class prPaymentNumberGroup
    {
        public prPaymentNumberGroup()
        {
        }

        [Key]
        [Required]
        public int ID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GroupPaymentNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PaymentNumber { get; set; }

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
