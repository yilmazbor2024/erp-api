using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpCanceledUTSDeclaration")]
    public partial class tpCanceledUTSDeclaration
    {
        public tpCanceledUTSDeclaration()
        {
        }

        [Key]
        [Required]
        public Guid CanceledUTSDeclarationID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        [Required]
        public Guid ApplicationID { get; set; }

        [Required]
        public Guid ApplicationLineID { get; set; }

        [Required]
        public Guid StockID { get; set; }

        [Required]
        public object InnerProcessCode { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public bool IsReturn { get; set; }

        [Required]
        public byte TransTypeCode { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Required]
        public object FromOfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FromStoreCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string FromWarehouseCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DataMatrixCode { get; set; }

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
        public string Barcode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SerialNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ManufactureDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PartyCode { get; set; }

        [Required]
        public double Qty1 { get; set; }

        [Required]
        public double Qty2 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CancelledUserName { get; set; }

        [Required]
        public DateTime CancelledDate { get; set; }

        [Required]
        public bool IsDeclaredToUTS { get; set; }

    }
}
