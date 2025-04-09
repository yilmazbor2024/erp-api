using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpItemLastDimensions")]
    public partial class rpItemLastDimensions
    {
        public rpItemLastDimensions()
        {
        }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Required]
        public byte ItemDimTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim1 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim2 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim3 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim4 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim5 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim6 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim7 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim8 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim9 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim10 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim11 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim12 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim13 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim14 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim15 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim16 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim17 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim18 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim19 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim20 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim21 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim22 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim23 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim24 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim25 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim26 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim27 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim28 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim29 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Dim30 { get; set; }

    }
}
