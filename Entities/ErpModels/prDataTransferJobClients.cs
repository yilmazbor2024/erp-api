using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDataTransferJobClients")]
    public partial class prDataTransferJobClients
    {
        public prDataTransferJobClients()
        {
        }

        [Key]
        [Required]
        public int DataTransferJobClientsID { get; set; }

        [Required]
        public int DataTransferJobID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DataTransferCompanyCode { get; set; }

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
        public virtual cdDataTransferCompany cdDataTransferCompany { get; set; }
        public virtual cdDataTransferJob cdDataTransferJob { get; set; }

    }
}
