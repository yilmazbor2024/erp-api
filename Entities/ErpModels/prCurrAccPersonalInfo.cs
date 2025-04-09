using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccPersonalInfo")]
    public partial class prCurrAccPersonalInfo
    {
        public prCurrAccPersonalInfo()
        {
        }

        [Key]
        [Required]
        public Guid PersonalInfoID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? ContactID { get; set; }

        [Required]
        public byte GenderCode { get; set; }

        [Required]
        public bool IsMarried { get; set; }

        [Required]
        public DateTime MarriedDate { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BirthPlace { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Nationality { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PassportNum { get; set; }

        [Required]
        public DateTime PassportIssueDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityCardNum { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MotherName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FatherName { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RegisteredCityCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string RegisteredDistrictCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string RegisteredTown { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RegisteredFileNum { get; set; }

        [Required]
        public int RegisteredFamilyNum { get; set; }

        [Required]
        public int RegisteredNum { get; set; }

        [Required]
        public int RegisteredRecordNum { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MaidenName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SGKRecourseLastName { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DrivingLicenceType { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DrivingLicenceTypeNum { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal MonthlyIncome { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SocialInsuranceNumber { get; set; }

        [Required]
        public bool IsSmoker { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string HandicapTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RecidivistTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MaladyTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BloodTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MilitaryServiceStatusCode { get; set; }

        [Required]
        public DateTime MilitaryServiceFinishedDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MilitaryExcuseReason { get; set; }

        [Required]
        public short MilitaryServiceDelayYear { get; set; }

        [Required]
        public byte EducationStatusCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PrimarySchool { get; set; }

        [Required]
        public DateTime PrimarySchoolFinishedDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string HighSchool { get; set; }

        [Required]
        public DateTime HighSchoolFinishedDate { get; set; }

        [Required]
        public bool AvaibleToWorkOnAssignmentAbroad { get; set; }

        [Required]
        public bool AvaibleToTravelforTheBusiness { get; set; }

        [Required]
        public bool BenefitByAgi { get; set; }

        [Required]
        public bool IsEducationInProgress { get; set; }

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
        public virtual cdMilitaryServiceStatus cdMilitaryServiceStatus { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdCity cdCity { get; set; }
        public virtual bsGender bsGender { get; set; }
        public virtual cdRecidivistType cdRecidivistType { get; set; }
        public virtual cdHandicapType cdHandicapType { get; set; }
        public virtual cdMaladyType cdMaladyType { get; set; }
        public virtual cdEducationStatus cdEducationStatus { get; set; }
        public virtual cdDistrict cdDistrict { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdBloodType cdBloodType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
