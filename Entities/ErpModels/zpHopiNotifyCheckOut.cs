using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpHopiNotifyCheckOut")]
    public partial class zpHopiNotifyCheckOut
    {
        public zpHopiNotifyCheckOut()
        {
        }

        [Key]
        [Required]
        public Guid NotifyCheckOutID { get; set; }

        [Required]
        public Guid GetHopiUserInfoID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MerchantCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string StoreCode { get; set; }

        [Required]
        public long BirdID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CashDeskTag { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TransactionID { get; set; }

        public string ResponseCode { get; set; }

        [Required]
        public int OperationStatus { get; set; }

        [Required]
        public int TransactionStatus { get; set; }

        public string ApplicationName { get; set; }

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
