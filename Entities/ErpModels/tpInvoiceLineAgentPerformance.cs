using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInvoiceLineAgentPerformance")]
    public partial class tpInvoiceLineAgentPerformance
    {
        public tpInvoiceLineAgentPerformance()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceLineAgentPerformanceID { get; set; }

        [Required]
        public Guid InvoiceLineID { get; set; }

        [Required]
        public Guid AgentReservationHeaderID { get; set; }

        [Required]
        public Guid AgentContractHeaderID { get; set; }

        [Required]
        public decimal AgentAmount { get; set; }

        [Required]
        public decimal AgentOfficeAmount { get; set; }

        [Required]
        public decimal TourGuideAmount { get; set; }

        [Required]
        public decimal TourDriverAmount { get; set; }

        [Required]
        public decimal TourLeaderAmount { get; set; }

        [Required]
        public decimal TourHelperGuideAmount { get; set; }

        [Required]
        public decimal HotelAmount { get; set; }

        [Required]
        public decimal HotelPersonnelAmount { get; set; }

        [Required]
        public decimal SpecialAmount { get; set; }

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
        public virtual trInvoiceLine trInvoiceLine { get; set; }
        public virtual trAgentContractHeader trAgentContractHeader { get; set; }
        public virtual trAgentReservationHeader trAgentReservationHeader { get; set; }

    }
}
