using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfBulutTahsilatVPosOffice")]
    public partial class dfBulutTahsilatVPosOffice
    {
        public dfBulutTahsilatVPosOffice()
        {
        }

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Password { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object WebServiceAddress { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ApiKey { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object MD5Key { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PaymentExpCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CreditCardTypeCode { get; set; }

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
        public virtual cdCreditCardType cdCreditCardType { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdOffice cdOffice { get; set; }

    }
}
