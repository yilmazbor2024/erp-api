using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auCustomizedDiscountEngineServiceLog")]
    public partial class auCustomizedDiscountEngineServiceLog
    {
        public auCustomizedDiscountEngineServiceLog()
        {
        }

        [Key]
        [Required]
        public Guid ServiceLogID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ServiceMethodName { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }

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
