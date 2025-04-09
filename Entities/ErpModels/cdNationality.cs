using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdNationality")]
    public partial class cdNationality
    {
        public cdNationality()
        {
            cdNationalityDescs = new HashSet<cdNationalityDesc>();
            trAgentContractDeservedLines = new HashSet<trAgentContractDeservedLine>();
            trAgentContractSpecialLines = new HashSet<trAgentContractSpecialLine>();
            trAgentContractVehicles = new HashSet<trAgentContractVehicle>();
            trAgentContractVisitFrequencyLines = new HashSet<trAgentContractVisitFrequencyLine>();
            trAgentReservationHeaders = new HashSet<trAgentReservationHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string NationalityCode { get; set; }

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

        public virtual ICollection<cdNationalityDesc> cdNationalityDescs { get; set; }
        public virtual ICollection<trAgentContractDeservedLine> trAgentContractDeservedLines { get; set; }
        public virtual ICollection<trAgentContractSpecialLine> trAgentContractSpecialLines { get; set; }
        public virtual ICollection<trAgentContractVehicle> trAgentContractVehicles { get; set; }
        public virtual ICollection<trAgentContractVisitFrequencyLine> trAgentContractVisitFrequencyLines { get; set; }
        public virtual ICollection<trAgentReservationHeader> trAgentReservationHeaders { get; set; }
    }
}
