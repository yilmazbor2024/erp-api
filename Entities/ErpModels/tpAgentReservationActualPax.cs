using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpAgentReservationActualPax")]
    public partial class tpAgentReservationActualPax
    {
        public tpAgentReservationActualPax()
        {
        }

        [Key]
        [Required]
        public Guid AgentReservationActualPaxID { get; set; }

        [Required]
        public Guid AgentReservationHeaderID { get; set; }

        [Required]
        public byte GenderCode { get; set; }

        [Required]
        public byte MinAge { get; set; }

        [Required]
        public byte MaxAge { get; set; }

        [Required]
        public short ActualPax { get; set; }

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

        // Navigation Properties
        public virtual trAgentReservationHeader trAgentReservationHeader { get; set; }
        public virtual bsGender bsGender { get; set; }

    }
}
