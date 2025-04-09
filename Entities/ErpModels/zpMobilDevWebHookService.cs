using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpMobilDevWebHookService")]
    public partial class zpMobilDevWebHookService
    {
        public zpMobilDevWebHookService()
        {
        }

        [Key]
        [Required]
        public Guid MobilDevWebHookServiceID { get; set; }

        public string JsonData { get; set; }

        [Required]
        public bool IsProcessed { get; set; }

        [Required]
        public DateTime ProcessedTime { get; set; }

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

    }
}
