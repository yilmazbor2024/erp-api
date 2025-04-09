using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpGetirCarsiOrderInfo")]
    public partial class zpGetirCarsiOrderInfo
    {
        public zpGetirCarsiOrderInfo()
        {
        }

        [Key]
        [Required]
        public Guid OrderHeaderID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ShopID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string OrderID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ConfirmationID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ApplicationName { get; set; }

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
