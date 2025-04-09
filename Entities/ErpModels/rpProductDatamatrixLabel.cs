using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpProductDatamatrixLabel")]
    public partial class rpProductDatamatrixLabel
    {
        public rpProductDatamatrixLabel()
        {
        }

        [Key]
        [Required]
        public Guid ProductLabelLineID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ListName { get; set; }

        [Required]
        public DateTime ListDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

        public Guid? ApplicationLineID { get; set; }

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

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DataMatrixCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Barcode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string SerialNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ManufactureDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PartyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ExpiryDate { get; set; }

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
