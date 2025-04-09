using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_OutboxShipmentResponseDespatchSupplierParty")]
    public partial class e_OutboxShipmentResponseDespatchSupplierParty
    {
        public e_OutboxShipmentResponseDespatchSupplierParty()
        {
        }

        [Key]
        [Required]
        public Guid UUID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string CompanyName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TaxOfficeName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string TaxNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FirstName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LastName { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string IdentityNum { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string StreetName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BuildingNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CitySubdivisionName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CityName { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PostalZone { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CountryName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Telephone { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Telefax { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ElectronicMail { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string WebsiteURI { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SubCurrAccCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string URNAddress { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string RecordedCurrAccCode { get; set; }

        // Navigation Properties
        public virtual e_OutboxShipmentResponseHeader e_OutboxShipmentResponseHeader { get; set; }

    }
}
