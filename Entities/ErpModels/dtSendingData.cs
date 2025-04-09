using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dtSendingData")]
    public partial class dtSendingData
    {
        public dtSendingData()
        {
        }

        [Key]
        [Required]
        public Guid SendingDataID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Suffix { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TransferName { get; set; }

        public int? CodeType { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Code { get; set; }

        public Guid? ID { get; set; }

    }
}
