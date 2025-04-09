using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_InboxShipmentHeader")]
    public partial class e_InboxShipmentHeader
    {
        public e_InboxShipmentHeader()
        {
            e_InboxShipmentBuyerCustomerPartys = new HashSet<e_InboxShipmentBuyerCustomerParty>();
            e_InboxShipmentCarrierPartys = new HashSet<e_InboxShipmentCarrierParty>();
            e_InboxShipmentDeliveryCustomerPartys = new HashSet<e_InboxShipmentDeliveryCustomerParty>();
            e_InboxShipmentDespatchSupplierPartys = new HashSet<e_InboxShipmentDespatchSupplierParty>();
            e_InboxShipmentDriverss = new HashSet<e_InboxShipmentDrivers>();
            e_InboxShipmentLines = new HashSet<e_InboxShipmentLine>();
            e_InboxShipmentOriginatorCustomerPartys = new HashSet<e_InboxShipmentOriginatorCustomerParty>();
            e_InboxShipmentSellerSupplierPartys = new HashSet<e_InboxShipmentSellerSupplierParty>();
            e_InboxShipmentTransportDetailss = new HashSet<e_InboxShipmentTransportDetails>();
        }

        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public Guid UUID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string CopyIndicator { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ProfileID { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime IssueTime { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DespatchAdviceTypeCode { get; set; }

        [Required]
        public DateTime ActualDate { get; set; }

        [Required]
        public DateTime ActualTime { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Note { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string URNAddress { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        [Required]
        public byte InboxShipmentStatusCode { get; set; }

        [Required]
        public long EFinansDocumentSerialNumber { get; set; }

        public Guid? CreatedHeaderID { get; set; }

        // Navigation Properties
        public virtual e_InboxShipmentStatus e_InboxShipmentStatus { get; set; }

        public virtual ICollection<e_InboxShipmentBuyerCustomerParty> e_InboxShipmentBuyerCustomerPartys { get; set; }
        public virtual ICollection<e_InboxShipmentCarrierParty> e_InboxShipmentCarrierPartys { get; set; }
        public virtual ICollection<e_InboxShipmentDeliveryCustomerParty> e_InboxShipmentDeliveryCustomerPartys { get; set; }
        public virtual ICollection<e_InboxShipmentDespatchSupplierParty> e_InboxShipmentDespatchSupplierPartys { get; set; }
        public virtual ICollection<e_InboxShipmentDrivers> e_InboxShipmentDriverss { get; set; }
        public virtual ICollection<e_InboxShipmentLine> e_InboxShipmentLines { get; set; }
        public virtual ICollection<e_InboxShipmentOriginatorCustomerParty> e_InboxShipmentOriginatorCustomerPartys { get; set; }
        public virtual ICollection<e_InboxShipmentSellerSupplierParty> e_InboxShipmentSellerSupplierPartys { get; set; }
        public virtual ICollection<e_InboxShipmentTransportDetails> e_InboxShipmentTransportDetailss { get; set; }
    }
}
