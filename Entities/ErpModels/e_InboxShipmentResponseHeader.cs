using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_InboxShipmentResponseHeader")]
    public partial class e_InboxShipmentResponseHeader
    {
        public e_InboxShipmentResponseHeader()
        {
            e_InboxShipmentResponseDeliveryCustomerPartys = new HashSet<e_InboxShipmentResponseDeliveryCustomerParty>();
            e_InboxShipmentResponseDespatchSupplierPartys = new HashSet<e_InboxShipmentResponseDespatchSupplierParty>();
            e_InboxShipmentResponseLines = new HashSet<e_InboxShipmentResponseLine>();
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
        public string ReceiptAdviceTypeCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Note { get; set; }

        [Required]
        public Guid DespatchDocumentReferenceID { get; set; }

        [Required]
        public DateTime DespatchDocumentReferenceIssueDate { get; set; }

        [Required]
        public byte InboxShipmentStatusCode { get; set; }

        // Navigation Properties
        public virtual e_InboxShipmentStatus e_InboxShipmentStatus { get; set; }

        public virtual ICollection<e_InboxShipmentResponseDeliveryCustomerParty> e_InboxShipmentResponseDeliveryCustomerPartys { get; set; }
        public virtual ICollection<e_InboxShipmentResponseDespatchSupplierParty> e_InboxShipmentResponseDespatchSupplierPartys { get; set; }
        public virtual ICollection<e_InboxShipmentResponseLine> e_InboxShipmentResponseLines { get; set; }
    }
}
