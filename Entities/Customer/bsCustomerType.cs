using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models
{
    [Table("bsCustomerType")]
    public class bsCustomerType
    {
        [Key]
        public byte CustomerTypeCode { get; set; }

        public bool IsBlocked { get; set; }

        // Navigation Properties
        public virtual bsCustomerTypeDesc CustomerTypeDesc { get; set; }
    }
} 