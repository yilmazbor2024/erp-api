using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("srChequesSerialNumber")]
    public partial class srChequesSerialNumber
    {
        public srChequesSerialNumber()
        {
        }

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [Key]
        [Required]
        public byte ChequeTypeCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SeriesCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Series { get; set; }

        [Required]
        public decimal StartingNumber { get; set; }

        [Required]
        public decimal EndingNumber { get; set; }

        [Required]
        public decimal LastNumber { get; set; }

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
        public virtual cdOffice cdOffice { get; set; }
        public virtual bsChequeType bsChequeType { get; set; }
        public virtual cdBank cdBank { get; set; }

    }
}
