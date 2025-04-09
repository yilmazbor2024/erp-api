using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsAirportIATADesc")]
    public partial class bsAirportIATADesc
    {
        public bsAirportIATADesc()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AirportIATACode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string AirportIATADescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsAirportIATA bsAirportIATA { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
