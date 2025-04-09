using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdJobTraining")]
    public partial class cdJobTraining
    {
        public cdJobTraining()
        {
            cdJobTrainingDescs = new HashSet<cdJobTrainingDesc>();
            prEmployeeJobTrainings = new HashSet<prEmployeeJobTraining>();
            prJobTrainingAttributes = new HashSet<prJobTrainingAttribute>();
            prJobTrainingNotess = new HashSet<prJobTrainingNotes>();
            prJobTrainingPlanneds = new HashSet<prJobTrainingPlanned>();
            prJobTrainingRealiseds = new HashSet<prJobTrainingRealised>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string JobTrainingCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Trainer { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Place { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public short TotalHour { get; set; }

        [Required]
        public bool IsInternal { get; set; }

        [Required]
        public short PersonCapacity { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal PricePerPerson { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }

        public virtual ICollection<cdJobTrainingDesc> cdJobTrainingDescs { get; set; }
        public virtual ICollection<prEmployeeJobTraining> prEmployeeJobTrainings { get; set; }
        public virtual ICollection<prJobTrainingAttribute> prJobTrainingAttributes { get; set; }
        public virtual ICollection<prJobTrainingNotes> prJobTrainingNotess { get; set; }
        public virtual ICollection<prJobTrainingPlanned> prJobTrainingPlanneds { get; set; }
        public virtual ICollection<prJobTrainingRealised> prJobTrainingRealiseds { get; set; }
    }
}
