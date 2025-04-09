using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfOfflinePosServiceParameters")]
    public partial class dfOfflinePosServiceParameters
    {
        public dfOfflinePosServiceParameters()
        {
        }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ImportFolder { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ServerName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string DatabaseName { get; set; }

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

    }
}
