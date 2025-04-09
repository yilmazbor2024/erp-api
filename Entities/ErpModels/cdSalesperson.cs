using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSalesperson")]
    public partial class cdSalesperson
    {
        public cdSalesperson()
        {
            prCustomerSalespersons = new HashSet<prCustomerSalesperson>();
            prSubCurrAccSalespersons = new HashSet<prSubCurrAccSalesperson>();
            rpSelectedProducts = new HashSet<rpSelectedProduct>();
            tpInvoiceHeaderSalesPersons = new HashSet<tpInvoiceHeaderSalesPerson>();
            trAgentReservationSalesPersons = new HashSet<trAgentReservationSalesPerson>();
            trDepartmentReceiptLines = new HashSet<trDepartmentReceiptLine>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trOrderLines = new HashSet<trOrderLine>();
            trProposalLines = new HashSet<trProposalLine>();
            trShipmentLines = new HashSet<trShipmentLine>();
            trSupportRequestHeaders = new HashSet<trSupportRequestHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SalespersonCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FirstLastName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EMail { get; set; }

        [Required]
        public bool SignOff { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SalespersonTeamCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SalespersonTypeCode { get; set; }

        [Required]
        public bool IsSalespersonTeamLeader { get; set; }

        [Required]
        public bool IsWorkOnWholesale { get; set; }

        [Required]
        public bool IsWorkOnRetail { get; set; }

        [Required]
        public float DailySalesIncentiveRate { get; set; }

        [Required]
        public float WeekendSalesIncentiveRate { get; set; }

        [Required]
        public float ExtraSalesIncentiveRate1 { get; set; }

        [Required]
        public float ExtraSalesIncentiveRate2 { get; set; }

        [Required]
        public float ExtraSalesIncentiveRate3 { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public byte EmployeeTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EmployeeCode { get; set; }

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

        // Navigation Properties
        public virtual cdSalespersonType cdSalespersonType { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdSalespersonTeam cdSalespersonTeam { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<prCustomerSalesperson> prCustomerSalespersons { get; set; }
        public virtual ICollection<prSubCurrAccSalesperson> prSubCurrAccSalespersons { get; set; }
        public virtual ICollection<rpSelectedProduct> rpSelectedProducts { get; set; }
        public virtual ICollection<tpInvoiceHeaderSalesPerson> tpInvoiceHeaderSalesPersons { get; set; }
        public virtual ICollection<trAgentReservationSalesPerson> trAgentReservationSalesPersons { get; set; }
        public virtual ICollection<trDepartmentReceiptLine> trDepartmentReceiptLines { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trOrderLine> trOrderLines { get; set; }
        public virtual ICollection<trProposalLine> trProposalLines { get; set; }
        public virtual ICollection<trShipmentLine> trShipmentLines { get; set; }
        public virtual ICollection<trSupportRequestHeader> trSupportRequestHeaders { get; set; }
    }
}
