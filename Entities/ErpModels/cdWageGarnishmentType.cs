using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdWageGarnishmentType")]
    public partial class cdWageGarnishmentType
    {
        public cdWageGarnishmentType()
        {
            cdWageGarnishmentTypeDescs = new HashSet<cdWageGarnishmentTypeDesc>();
            hrWageGarnishments = new HashSet<hrWageGarnishment>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string WageGarnishmentTypeCode { get; set; }

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

        public virtual ICollection<cdWageGarnishmentTypeDesc> cdWageGarnishmentTypeDescs { get; set; }
        public virtual ICollection<hrWageGarnishment> hrWageGarnishments { get; set; }
    }
}
