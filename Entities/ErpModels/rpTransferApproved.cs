using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpTransferApproved")]
    public partial class rpTransferApproved
    {
        public rpTransferApproved()
        {
        }

        [Key]
        [Required]
        public Guid TransferApprovedID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        [Required]
        public Guid ApplicationID { get; set; }

        public Guid? ApplicationLineID { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public object InnerProcessCode { get; set; }

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

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SectionCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UsedBarcode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LotBarcode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SerialNumber { get; set; }

        [Required]
        public double MissingStock { get; set; }

        [Required]
        public double OverStock { get; set; }

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
