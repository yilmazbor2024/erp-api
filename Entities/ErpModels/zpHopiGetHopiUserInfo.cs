using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpHopiGetHopiUserInfo")]
    public partial class zpHopiGetHopiUserInfo
    {
        public zpHopiGetHopiUserInfo()
        {
        }

        [Key]
        [Required]
        public Guid GetHopiUserInfoID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MerchantCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string StoreCode { get; set; }

        public string Token { get; set; }

        public string ResponseCode { get; set; }

        [Required]
        public long BirdID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomerID { get; set; }

        [Required]
        public decimal CoinBalance { get; set; }

        [Required]
        public int OperationStatus { get; set; }

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
