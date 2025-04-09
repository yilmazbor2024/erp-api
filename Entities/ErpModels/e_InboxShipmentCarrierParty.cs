using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_InboxShipmentCarrierParty")]
    public partial class e_InboxShipmentCarrierParty
    {
        public e_InboxShipmentCarrierParty()
        {
        }

        [Key]
        [Required]
        public Guid UUID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PartyIdentificationID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PartyName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CitySubdivisionName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CityName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CountryName { get; set; }

        // Navigation Properties
        public virtual e_InboxShipmentHeader e_InboxShipmentHeader { get; set; }

    }
}
