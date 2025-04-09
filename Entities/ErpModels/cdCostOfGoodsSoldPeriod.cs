using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCostOfGoodsSoldPeriod")]
    public partial class cdCostOfGoodsSoldPeriod
    {
        public cdCostOfGoodsSoldPeriod()
        {
            cdCostOfGoodsSoldPeriodDescs = new HashSet<cdCostOfGoodsSoldPeriodDesc>();
            trCostOfGoodsSoldHeaders = new HashSet<trCostOfGoodsSoldHeader>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CostOfGoodsSoldPeriodCode { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

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

        public virtual ICollection<cdCostOfGoodsSoldPeriodDesc> cdCostOfGoodsSoldPeriodDescs { get; set; }
        public virtual ICollection<trCostOfGoodsSoldHeader> trCostOfGoodsSoldHeaders { get; set; }
    }
}
