using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("bsCustomerTypeDesc")]
    public class bsCustomerTypeDesc
    {
        [Key]
        [Column(Order = 1)]
        public byte CustomerTypeCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string LangCode { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        [ForeignKey("CustomerTypeCode")]
        public virtual bsCustomerType CustomerType { get; set; }
    }
} 