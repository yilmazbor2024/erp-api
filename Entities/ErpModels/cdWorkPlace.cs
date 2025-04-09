using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdWorkPlace")]
    public partial class cdWorkPlace
    {
        public cdWorkPlace()
        {
            cdWorkPlaceDescs = new HashSet<cdWorkPlaceDesc>();
            hrEmployeeWorkPlaces = new HashSet<hrEmployeeWorkPlace>();
            hrSGKMonthlyDocuments = new HashSet<hrSGKMonthlyDocument>();
            hrSGKMonthlyDocumentDeclarations = new HashSet<hrSGKMonthlyDocumentDeclaration>();
            prCompanyExpenseInvoiceConfirmationRules = new HashSet<prCompanyExpenseInvoiceConfirmationRule>();
            prConfirmationRuleStepUsers = new HashSet<prConfirmationRuleStepUser>();
            prEmployeeRemoteWorkDayss = new HashSet<prEmployeeRemoteWorkDays>();
            prEmployeeWorkplaceInformations = new HashSet<prEmployeeWorkplaceInformation>();
            prExpenseInvoiceConfirmationRules = new HashSet<prExpenseInvoiceConfirmationRule>();
            prWorkPlaceATAttributes = new HashSet<prWorkPlaceATAttribute>();
            prWorkPlaceFTAttributes = new HashSet<prWorkPlaceFTAttribute>();
            prWorkPlaceGLAccss = new HashSet<prWorkPlaceGLAccs>();
            prWorkPlaceOptimalEmployments = new HashSet<prWorkPlaceOptimalEmployment>();
            prWorkPlaceSecondments = new HashSet<prWorkPlaceSecondment>();
            prWorkplaceSGKLogonInfos = new HashSet<prWorkplaceSGKLogonInfo>();
            trIncentiveHeaders = new HashSet<trIncentiveHeader>();
            trPayrollHeaders = new HashSet<trPayrollHeader>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WorkPlaceCode { get; set; }

        [Required]
        public byte WorkPlaceTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WorkPlaceGroupCode { get; set; }

        [Required]
        public TimeSpan WorkStartTime { get; set; }

        [Required]
        public TimeSpan WorkEndTime { get; set; }

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

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SGKWorkPlaceSectorCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SGKRegisterNum { get; set; }

        [Required]
        public double ShortTermInsuranceRateLevel { get; set; }

        [Required]
        public byte WorkDangerClass { get; set; }

        [Required]
        public byte WorkDangerLevelCode { get; set; }

        [Required]
        public DateTime WorkplaceOpeningDate { get; set; }

        [Required]
        public DateTime WorkplaceClosingDate { get; set; }

        [Required]
        public bool IsWorkOnSaturday { get; set; }

        [Required]
        public bool IsWorkOnSunday { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string NACECode { get; set; }

        [Required]
        public short TradeRegistryOfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TradeRegistryNo { get; set; }

        [Required]
        public decimal WorkplaceNumber { get; set; }

        [Required]
        public byte WorkplaceKindCode { get; set; }

        [Required]
        public byte WorkplacePropertyStatusCode { get; set; }

        [Required]
        public long AddressID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Address { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string SiteName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BuildingName { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BuildingNum { get; set; }

        [Required]
        public short FloorNum { get; set; }

        [Required]
        public short DoorNum { get; set; }

        [Required]
        public int QuarterCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string QuarterName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Boulevard { get; set; }

        [Required]
        public int StreetCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Street { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Road { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string StateCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CityCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DistrictCode { get; set; }

    

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ZipCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DrivingDirections { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EMailAddress { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string GSMNo { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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

        [Required]
        public double ReductionRate_05510 { get; set; }

        // Navigation Properties
        public virtual bsWorkplacePropertyStatus bsWorkplacePropertyStatus { get; set; }
        public virtual bsWorkplaceKind bsWorkplaceKind { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdState cdState { get; set; }
        public virtual cdCity cdCity { get; set; }
        public virtual cdTradeRegistryOffice cdTradeRegistryOffice { get; set; }
        public virtual bsSGKWorkPlaceSector bsSGKWorkPlaceSector { get; set; }
        public virtual cdCountry cdCountry { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdDistrict cdDistrict { get; set; }
        public virtual cdWorkPlaceType cdWorkPlaceType { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdStreet cdStreet { get; set; }
        public virtual cdWorkPlaceGroup cdWorkPlaceGroup { get; set; }
        public virtual bsWorkDangerLevel bsWorkDangerLevel { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<cdWorkPlaceDesc> cdWorkPlaceDescs { get; set; }
        public virtual ICollection<hrEmployeeWorkPlace> hrEmployeeWorkPlaces { get; set; }
        public virtual ICollection<hrSGKMonthlyDocument> hrSGKMonthlyDocuments { get; set; }
        public virtual ICollection<hrSGKMonthlyDocumentDeclaration> hrSGKMonthlyDocumentDeclarations { get; set; }
        public virtual ICollection<prCompanyExpenseInvoiceConfirmationRule> prCompanyExpenseInvoiceConfirmationRules { get; set; }
        public virtual ICollection<prConfirmationRuleStepUser> prConfirmationRuleStepUsers { get; set; }
        public virtual ICollection<prEmployeeRemoteWorkDays> prEmployeeRemoteWorkDayss { get; set; }
        public virtual ICollection<prEmployeeWorkplaceInformation> prEmployeeWorkplaceInformations { get; set; }
        public virtual ICollection<prExpenseInvoiceConfirmationRule> prExpenseInvoiceConfirmationRules { get; set; }
        public virtual ICollection<prWorkPlaceATAttribute> prWorkPlaceATAttributes { get; set; }
        public virtual ICollection<prWorkPlaceFTAttribute> prWorkPlaceFTAttributes { get; set; }
        public virtual ICollection<prWorkPlaceGLAccs> prWorkPlaceGLAccss { get; set; }
        public virtual ICollection<prWorkPlaceOptimalEmployment> prWorkPlaceOptimalEmployments { get; set; }
        public virtual ICollection<prWorkPlaceSecondment> prWorkPlaceSecondments { get; set; }
        public virtual ICollection<prWorkplaceSGKLogonInfo> prWorkplaceSGKLogonInfos { get; set; }
        public virtual ICollection<trIncentiveHeader> trIncentiveHeaders { get; set; }
        public virtual ICollection<trPayrollHeader> trPayrollHeaders { get; set; }
    }
}
