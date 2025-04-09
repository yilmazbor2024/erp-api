using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDataTransferTableList")]
    public partial class bsDataTransferTableList
    {
        public bsDataTransferTableList()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TransferName { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TableName { get; set; }

        [Required]
        public bool IsMasterTable { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

    }
}
