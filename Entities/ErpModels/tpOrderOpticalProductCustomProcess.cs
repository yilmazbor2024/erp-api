using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpOrderOpticalProductCustomProcess")]
    public partial class tpOrderOpticalProductCustomProcess
    {
        public tpOrderOpticalProductCustomProcess()
        {
        }

        [Key]
        [Required]
        public Guid OrderLineID { get; set; }

       

        [Required]
        public Guid ServiceOrderLineID { get; set; }

        [Key]
        [Required]
        public byte ServiceTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ServiceCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

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
        public virtual trOrderLine trOrderLine { get; set; }
        public virtual cdItem cdItem { get; set; }

    }
}
