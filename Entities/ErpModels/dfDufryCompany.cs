using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfDufryCompany")]
    public partial class dfDufryCompany
    {
        public dfDufryCompany()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FTPUserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FTPPassword { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object FTPFileDirectory { get; set; }

        [Required]
        public byte FTPExchangeTypeCode { get; set; }

        [Required]
        public byte FTPExchangeTypeCodeForDomesticStore { get; set; }

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
        public virtual cdExchangeType cdExchangeType { get; set; }

    }
}
