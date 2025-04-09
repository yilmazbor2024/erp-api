using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpTaxFreePointServiceLog")]
    public partial class zpTaxFreePointServiceLog
    {
        public zpTaxFreePointServiceLog()
        {
        }

        [Key]
        [Required]
        public Guid ServiceLogID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ServiceMethodName { get; set; }

        [Required]
        public object Request { get; set; }

        public object? Response { get; set; }

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
