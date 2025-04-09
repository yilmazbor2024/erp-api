using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_InboxShipmentResponseLine")]
    public partial class e_InboxShipmentResponseLine
    {
        public e_InboxShipmentResponseLine()
        {
        }

        [Key]
        [Required]
        public Guid InboxShipmentResponseLineID { get; set; }

        [Required]
        public Guid UUID { get; set; }

        [Required]
        public int LineID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnitCode { get; set; }

        [Required]
        public double ReceivedQty { get; set; }

        [Required]
        public double ShortQty { get; set; }

        [Required]
        public double OversupplyQty { get; set; }

        [Required]
        public double RejectedQty { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string RejectReasonCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string RejectReason { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TimingComplaintCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TimingComplaint { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ItemName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SellersItemIdentificationID { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim1Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim2Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim3Code { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CompanyBarcode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BuyersItemIdentificationID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ManufacturersItemIdentificationID { get; set; }

        // Navigation Properties
        public virtual e_InboxShipmentResponseHeader e_InboxShipmentResponseHeader { get; set; }

    }
}
