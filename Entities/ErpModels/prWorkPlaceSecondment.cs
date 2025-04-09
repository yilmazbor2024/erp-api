using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prWorkPlaceSecondment")]
    public partial class prWorkPlaceSecondment
    {
        public prWorkPlaceSecondment()
        {
        }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WorkPlaceCode { get; set; }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Key]
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool Canceled { get; set; }

        [Required]
        public DateTime CancelDate { get; set; }

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
        public virtual cdWorkPlace cdWorkPlace { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
