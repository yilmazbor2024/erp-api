using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpWeArePlanetTaxFreeServiceLog")]
    public partial class zpWeArePlanetTaxFreeServiceLog
    {
        public zpWeArePlanetTaxFreeServiceLog()
        {
        }

        [Key]
        [Required]
        public Guid ServiceLogID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ServiceMethodName { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ApplicationName { get; set; }

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
