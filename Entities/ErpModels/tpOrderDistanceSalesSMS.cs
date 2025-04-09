using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpOrderDistanceSalesSMS")]
    public partial class tpOrderDistanceSalesSMS
    {
        public tpOrderDistanceSalesSMS()
        {
        }

        [Key]
        [Required]
        public Guid OrderHeaderID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string SMSCode { get; set; }

        [Required]
        public DateTime LinkExpireDate { get; set; }

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
