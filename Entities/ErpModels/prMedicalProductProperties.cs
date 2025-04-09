using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prMedicalProductProperties")]
    public partial class prMedicalProductProperties
    {
        public prMedicalProductProperties()
        {
        }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BarcodeCompanyCode1 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BarcodeTypeCode1 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BarcodeCompanyCode2 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BarcodeTypeCode2 { get; set; }

        [Required]
        public byte ManufacturerCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ManufacturerCurrAccCode { get; set; }

        [Required]
        public byte ImporterCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImporterCurrAccCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UTSAttributeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UTSMRGInfoCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GDMNCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UTSBranchCode { get; set; }

        [Required]
        public bool IsManufacturedProduct { get; set; }

        [Required]
        public bool ContainsLatexOnLabel { get; set; }

        [Required]
        public bool ContainsIonizedRadiation { get; set; }

        [Required]
        public bool ContainsDEHPOnLabel { get; set; }

        [Required]
        public bool ContainsNanoMaterial { get; set; }

        [Required]
        public bool IsDisposable { get; set; }

        [Required]
        public int LimitedUseCount { get; set; }

        [Required]
        public bool IsSterilePacked { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SterilizationMethods { get; set; }

        [Required]
        public short CalibrationPeriod { get; set; }

        [Required]
        public short MaintenancePeriod { get; set; }

        [Required]
        public bool CanSellApartFromSalesCenter { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string UTSDocumentNumbers { get; set; }

        [Required]
        public bool RequiresStorageCondition { get; set; }

        [Required]
        public bool AvailableForSinglePatient { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SutConvertStatus { get; set; }

        [Required]
        public bool CanSellApartFromHealthMarkets { get; set; }

        [Required]
        public bool HasUserGuide { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BrandCode { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ConfirmedInstitutionNumber { get; set; }

        [Required]
        public byte ProductTypeCode { get; set; }

        [Required]
        public short ContentCount { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ExtraInfoLink { get; set; }

        [Required]
        public bool IsImplantable { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string PackageSterilizationMethods { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string MainProductBrachCodes { get; set; }

        [Required]
        public short ProductSurfaceArea { get; set; }

        [Required]
        public bool ReadyToApply { get; set; }

        [Required]
        public bool UTSSent { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BasicUDIDI { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdItem cdItem { get; set; }
        public virtual cdUTSAttribute cdUTSAttribute { get; set; }
        public virtual cdBarcodeCompany cdBarcodeCompany { get; set; }
        public virtual cdBrand cdBrand { get; set; }
        public virtual cdUTSMRGInfo cdUTSMRGInfo { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
