using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAgentContractStandartLine")]
    public partial class trAgentContractStandartLine
    {
        public trAgentContractStandartLine()
        {
        }

        [Key]
        [Required]
        public Guid AgentContractStandartLineID { get; set; }

        [Required]
        public Guid AgentContractHeaderID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevelCode01 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevelCode02 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevelCode03 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevelCode04 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ProductGroupLevelCode05 { get; set; }

        [Required]
        public double AgentRate { get; set; }

        [Required]
        public double AgentOfficeRate { get; set; }

        [Required]
        public double TourGuideRate { get; set; }

        [Required]
        public double TourDriverRate { get; set; }

        [Required]
        public double TourLeaderRate { get; set; }

        [Required]
        public double TourHelperGuideRate { get; set; }

        [Required]
        public double HotelRate { get; set; }

        [Required]
        public double HotelPersonnelRate { get; set; }

        [Required]
        public double SpecialRate { get; set; }

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
        public virtual trAgentContractHeader trAgentContractHeader { get; set; }

    }
}
