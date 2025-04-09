using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfPosCustomerScreenWidgetParameters")]
    public partial class dfPosCustomerScreenWidgetParameters
    {
        public dfPosCustomerScreenWidgetParameters()
        {
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
        public short PosTerminalID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ImageSlideFolderPath { get; set; }

        [Required]
        public short ImageSlideInterval { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ImageFilePath { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ProductRecommedationApiUrl { get; set; }

        [Required]
        public byte ProductRecommedationMaxRecordCount { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string TransactionInformationForeignCurrencyCode01 { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string TransactionInformationForeignCurrencyCode02 { get; set; }

        public bool? ShowProductPhotoOnTransactionInfo { get; set; }

        public bool? ShowCustomerFirstLastNameOnTransactionInfo { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string VideoFilePath { get; set; }

        [Required]
        public int IdleTimeOut { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object CurrencyCodesToShow { get; set; }

        [Required]
        public byte ExchangeTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string WidgetForIdle { get; set; }

        [Required]
        public byte FontSize { get; set; }

        [Required]
        public byte MaxFontSize { get; set; }

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
        public virtual cdPOSTerminal cdPOSTerminal { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
