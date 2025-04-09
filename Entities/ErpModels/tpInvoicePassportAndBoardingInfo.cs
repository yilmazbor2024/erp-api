using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInvoicePassportAndBoardingInfo")]
    public partial class tpInvoicePassportAndBoardingInfo
    {
        public tpInvoicePassportAndBoardingInfo()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CrewNo { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Nationality { get; set; }

        [Required]
        public byte PassportPassengerType { get; set; }

        [StringLength(5)]
        [Column(TypeName = "Char5")]
        public string GenderCode { get; set; }

        [Required]
        public byte PassengerStatus { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AirlineCompanyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DepartureAirportIATACode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ArrivalAirportIATACode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string FlightNumber { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SeatNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityNum { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FirstName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LastName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PassportNumber { get; set; }

        [Required]
        public DateTime FlightDate { get; set; }

        [Required]
        public TimeSpan FlightTime { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentID { get; set; }

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
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }
        public virtual bsAirportIATA bsAirportIATA { get; set; }

    }
}
