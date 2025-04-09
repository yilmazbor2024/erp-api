using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpAgentReservationReasonForNotShopping")]
    public partial class tpAgentReservationReasonForNotShopping
    {
        public tpAgentReservationReasonForNotShopping()
        {
        }

        [Key]
        [Required]
        public Guid AgentReservationReasonForNotShoppingID { get; set; }

        [Required]
        public Guid AgentReservationHeaderID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ReasonForNotShoppingCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

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
        public virtual cdReasonForNotShopping cdReasonForNotShopping { get; set; }

    }
}
