using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAgentReservationHeader")]
    public partial class trAgentReservationHeader
    {
        public trAgentReservationHeader()
        {
            tpAgentContractDeservedDebits = new HashSet<tpAgentContractDeservedDebit>();
            tpAgentContractVehicleDebits = new HashSet<tpAgentContractVehicleDebit>();
            tpAgentContractVisitFrequencyDebits = new HashSet<tpAgentContractVisitFrequencyDebit>();
            tpAgentPerformanceDebits = new HashSet<tpAgentPerformanceDebit>();
            tpAgentReservationActualPaxs = new HashSet<tpAgentReservationActualPax>();
            tpAgentReservationReasonForNotShoppings = new HashSet<tpAgentReservationReasonForNotShopping>();
            tpInvoiceHeaderExtensions = new HashSet<tpInvoiceHeaderExtension>();
            tpInvoiceHeaderSalesPersons = new HashSet<tpInvoiceHeaderSalesPerson>();
            tpInvoiceLineAgentPerformances = new HashSet<tpInvoiceLineAgentPerformance>();
            trAgentReservationSalesPersons = new HashSet<trAgentReservationSalesPerson>();
            trAgentReservationVehicleDetails = new HashSet<trAgentReservationVehicleDetail>();
        }

        [Key]
        [Required]
        public Guid AgentReservationHeaderID { get; set; }

        [Required]
        public object AgentReservationNumber { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public byte CurrAccTypeCodeForAgent { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCodeForAgent { get; set; }

        [Required]
        public byte CurrAccTypeCodeForAgentOffice { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCodeForAgentOffice { get; set; }

        [Required]
        public byte CurrAccTypeCodeForTourGuide { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCodeForTourGuide { get; set; }

        [Required]
        public byte CurrAccTypeCodeForTourLeader { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCodeForTourLeader { get; set; }

        [Required]
        public byte CurrAccTypeCodeForTourHelperGuide { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCodeForTourHelperGuide { get; set; }

        [Required]
        public byte CurrAccTypeCodeForTourDriver { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCodeForTourDriver { get; set; }

        [Required]
        public byte CurrAccTypeCodeForHotel { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCodeForHotel { get; set; }

        [Required]
        public byte CurrAccTypeCodeForHotelPersonnel { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCodeForHotelPersonnel { get; set; }

        [Required]
        public byte CurrAccTypeCodeForAgentSpecialRate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCodeForAgentSpecialRate { get; set; }

        [Required]
        public short ExpectedPax { get; set; }

        [Required]
        public TimeSpan ExpectedCheckinTime { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceGroupCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        public Guid AgentContractHeaderID { get; set; }

        public Guid? AgentContractPeriodicalLineID { get; set; }

        [Required]
        public TimeSpan ActualCheckinTime { get; set; }

        [Required]
        public TimeSpan ActualCheckoutTime { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string NationalityCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VisitFrequencyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CardNumber { get; set; }

        [Required]
        public bool UseForReturnTransactions { get; set; }

        [Required]
        public bool IsAccrualsVehicle { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        public Guid? MainAgentReservationHeaderID { get; set; }

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
        public virtual trAgentContractHeader trAgentContractHeader { get; set; }
        public virtual trAgentContractPeriodicalLine trAgentContractPeriodicalLine { get; set; }
        public virtual cdVisitFrequency cdVisitFrequency { get; set; }
        public virtual cdNationality cdNationality { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdPriceGroup cdPriceGroup { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual ICollection<tpAgentContractDeservedDebit> tpAgentContractDeservedDebits { get; set; }
        public virtual ICollection<tpAgentContractVehicleDebit> tpAgentContractVehicleDebits { get; set; }
        public virtual ICollection<tpAgentContractVisitFrequencyDebit> tpAgentContractVisitFrequencyDebits { get; set; }
        public virtual ICollection<tpAgentPerformanceDebit> tpAgentPerformanceDebits { get; set; }
        public virtual ICollection<tpAgentReservationActualPax> tpAgentReservationActualPaxs { get; set; }
        public virtual ICollection<tpAgentReservationReasonForNotShopping> tpAgentReservationReasonForNotShoppings { get; set; }
        public virtual ICollection<tpInvoiceHeaderExtension> tpInvoiceHeaderExtensions { get; set; }
        public virtual ICollection<tpInvoiceHeaderSalesPerson> tpInvoiceHeaderSalesPersons { get; set; }
        public virtual ICollection<tpInvoiceLineAgentPerformance> tpInvoiceLineAgentPerformances { get; set; }
        public virtual ICollection<trAgentReservationSalesPerson> trAgentReservationSalesPersons { get; set; }
        public virtual ICollection<trAgentReservationVehicleDetail> trAgentReservationVehicleDetails { get; set; }
    }
}
