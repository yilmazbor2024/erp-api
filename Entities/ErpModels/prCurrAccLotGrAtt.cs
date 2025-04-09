using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccLotGrAtt")]
    public partial class prCurrAccLotGrAtt
    {
        public prCurrAccLotGrAtt()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrAccLotGrCode { get; set; }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevelCode01 { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevelCode02 { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevelCode03 { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevelCode04 { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevelCode05 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LotCode { get; set; }

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
        public virtual cdLot cdLot { get; set; }
        public virtual cdCurrAccLotGr cdCurrAccLotGr { get; set; }
        public virtual cdCompany cdCompany { get; set; }

    }
}
