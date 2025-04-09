using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPOSTerminal")]
    public partial class cdPOSTerminal
    {
        public cdPOSTerminal()
        {
            dfAirportExchangeRateWidgetParameterss = new HashSet<dfAirportExchangeRateWidgetParameters>();
            dfChippinPOSTerminals = new HashSet<dfChippinPOSTerminal>();
            dfCOMOPOSTerminals = new HashSet<dfCOMOPOSTerminal>();
            dfDMSPOSTerminals = new HashSet<dfDMSPOSTerminal>();
            dffastPayPosTerminals = new HashSet<dffastPayPosTerminal>();
            dfIGAPosTerminals = new HashSet<dfIGAPosTerminal>();
            dfOfflinePosTerminalParameterss = new HashSet<dfOfflinePosTerminalParameters>();
            dfPAROPOSTerminals = new HashSet<dfPAROPOSTerminal>();
            dfPosCustomerScreenLayouts = new HashSet<dfPosCustomerScreenLayout>();
            dfPosCustomerScreenWidgetParameterss = new HashSet<dfPosCustomerScreenWidgetParameters>();
            dfPosDeviceParameterss = new HashSet<dfPosDeviceParameters>();
            dfUmicoPOSTerminals = new HashSet<dfUmicoPOSTerminal>();
            prPOSTerminalATAttributes = new HashSet<prPOSTerminalATAttribute>();
            prPosTerminalDevices = new HashSet<prPosTerminalDevice>();
            prPosTerminalFiscalPrinters = new HashSet<prPosTerminalFiscalPrinter>();
            prPOSTerminalPrinters = new HashSet<prPOSTerminalPrinter>();
            tpJournalZNums = new HashSet<tpJournalZNum>();
        }

        [Key]
        [Required]
        public byte StoreTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Key]
        [Required]
        public short POSTerminalID { get; set; }

        [Required]
        public byte POSModeCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string POSExternalFilesPath { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DisplayPort { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TillOpenPort { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SeriesCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string TaxFreeSeriesCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ExpenseVoucherSeriesCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CreditCardSecurityService { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CreditCardSecurityPort { get; set; }

        [Required]
        public bool DenyCreditCardPaymentWithoutConnection { get; set; }

        [Required]
        public bool ListSuspendedTransactionsOnStartUp { get; set; }

        [Required]
        public byte CashCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CashCurrAccCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Required]
        public byte DefaultDocumentTypeCode { get; set; }

        [Required]
        public bool IsOfflinePosTerminal { get; set; }

        [Required]
        public decimal OfflineTerminalNumber { get; set; }

        [Required]
        public bool IsEnabledCustomerScreen { get; set; }

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
        public virtual bsDocumentType bsDocumentType { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<dfAirportExchangeRateWidgetParameters> dfAirportExchangeRateWidgetParameterss { get; set; }
        public virtual ICollection<dfChippinPOSTerminal> dfChippinPOSTerminals { get; set; }
        public virtual ICollection<dfCOMOPOSTerminal> dfCOMOPOSTerminals { get; set; }
        public virtual ICollection<dfDMSPOSTerminal> dfDMSPOSTerminals { get; set; }
        public virtual ICollection<dffastPayPosTerminal> dffastPayPosTerminals { get; set; }
        public virtual ICollection<dfIGAPosTerminal> dfIGAPosTerminals { get; set; }
        public virtual ICollection<dfOfflinePosTerminalParameters> dfOfflinePosTerminalParameterss { get; set; }
        public virtual ICollection<dfPAROPOSTerminal> dfPAROPOSTerminals { get; set; }
        public virtual ICollection<dfPosCustomerScreenLayout> dfPosCustomerScreenLayouts { get; set; }
        public virtual ICollection<dfPosCustomerScreenWidgetParameters> dfPosCustomerScreenWidgetParameterss { get; set; }
        public virtual ICollection<dfPosDeviceParameters> dfPosDeviceParameterss { get; set; }
        public virtual ICollection<dfUmicoPOSTerminal> dfUmicoPOSTerminals { get; set; }
        public virtual ICollection<prPOSTerminalATAttribute> prPOSTerminalATAttributes { get; set; }
        public virtual ICollection<prPosTerminalDevice> prPosTerminalDevices { get; set; }
        public virtual ICollection<prPosTerminalFiscalPrinter> prPosTerminalFiscalPrinters { get; set; }
        public virtual ICollection<prPOSTerminalPrinter> prPOSTerminalPrinters { get; set; }
        public virtual ICollection<tpJournalZNum> tpJournalZNums { get; set; }
    }
}
