using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDataTransferValidateColumnList")]
    public partial class bsDataTransferValidateColumnList
    {
        public bsDataTransferValidateColumnList()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TransferName { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SourceTableName { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SourceTableColumnName { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TargetTableName { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TargetTableColumnName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ErrorColumnCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ErrorMessage { get; set; }

        [Key]
        [Required]
        public short GrupNum { get; set; }

        [Required]
        public short SortOrder { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

    }
}
