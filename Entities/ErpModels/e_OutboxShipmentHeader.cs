using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_OutboxShipmentHeader")]
    public partial class e_OutboxShipmentHeader
    {
        public e_OutboxShipmentHeader()
        {
            e_OutboxShipmentBuyerCustomerPartys = new HashSet<e_OutboxShipmentBuyerCustomerParty>();
            e_OutboxShipmentCarrierPartys = new HashSet<e_OutboxShipmentCarrierParty>();
            e_OutboxShipmentDeliveryCustomerPartys = new HashSet<e_OutboxShipmentDeliveryCustomerParty>();
            e_OutboxShipmentDespatchSupplierPartys = new HashSet<e_OutboxShipmentDespatchSupplierParty>();
            e_OutboxShipmentDriverss = new HashSet<e_OutboxShipmentDrivers>();
            e_OutboxShipmentLines = new HashSet<e_OutboxShipmentLine>();
            e_OutboxShipmentOrders = new HashSet<e_OutboxShipmentOrder>();
            e_OutboxShipmentOriginatorCustomerPartys = new HashSet<e_OutboxShipmentOriginatorCustomerParty>();
            e_OutboxShipmentSellerSupplierPartys = new HashSet<e_OutboxShipmentSellerSupplierParty>();
            e_OutboxShipmentTransportDetailss = new HashSet<e_OutboxShipmentTransportDetails>();
            e_OutboxShipmentUBLExtensionss = new HashSet<e_OutboxShipmentUBLExtensions>();
        }

        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public Guid UUID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TransactionNumber { get; set; }

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
        public byte OutboxShipmentStatusCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ErrorMessage { get; set; }

        // Navigation Properties
        public virtual e_OutboxShipmentStatus e_OutboxShipmentStatus { get; set; }

        public virtual ICollection<e_OutboxShipmentBuyerCustomerParty> e_OutboxShipmentBuyerCustomerPartys { get; set; }
        public virtual ICollection<e_OutboxShipmentCarrierParty> e_OutboxShipmentCarrierPartys { get; set; }
        public virtual ICollection<e_OutboxShipmentDeliveryCustomerParty> e_OutboxShipmentDeliveryCustomerPartys { get; set; }
        public virtual ICollection<e_OutboxShipmentDespatchSupplierParty> e_OutboxShipmentDespatchSupplierPartys { get; set; }
        public virtual ICollection<e_OutboxShipmentDrivers> e_OutboxShipmentDriverss { get; set; }
        public virtual ICollection<e_OutboxShipmentLine> e_OutboxShipmentLines { get; set; }
        public virtual ICollection<e_OutboxShipmentOrder> e_OutboxShipmentOrders { get; set; }
        public virtual ICollection<e_OutboxShipmentOriginatorCustomerParty> e_OutboxShipmentOriginatorCustomerPartys { get; set; }
        public virtual ICollection<e_OutboxShipmentSellerSupplierParty> e_OutboxShipmentSellerSupplierPartys { get; set; }
        public virtual ICollection<e_OutboxShipmentTransportDetails> e_OutboxShipmentTransportDetailss { get; set; }
        public virtual ICollection<e_OutboxShipmentUBLExtensions> e_OutboxShipmentUBLExtensionss { get; set; }
    }
}
