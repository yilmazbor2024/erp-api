using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfRomaniaGoosfrabaeShipmentOffice")]
    public partial class dfRomaniaGoosfrabaeShipmentOffice
    {
        public dfRomaniaGoosfrabaeShipmentOffice()
        {
        }

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public bool IsSubjectToRomaniaEShipment { get; set; }

        [Required]
        public DateTime RomaniaEShipmentStartDate { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object FilePath { get; set; }

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

    }
}
