using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prMDMCommunication")]
    public partial class prMDMCommunication
    {
        public prMDMCommunication()
        {
        }

        [Key]
        [Required]
        public Guid CommunicationID { get; set; }

        [Required]
        public int MDMCommunicationID { get; set; }

        [Required]
        public bool AdvertComfirmation_AC { get; set; }

        [Required]
        public bool AdvertComfirmation_BB { get; set; }

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
