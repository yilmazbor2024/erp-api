using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpCompareTransactionSourceFiles")]
    public partial class rpCompareTransactionSourceFiles
    {
        public rpCompareTransactionSourceFiles()
        {
        }

        [Key]
        [Required]
        public Guid CompareTransactionSourceFileID { get; set; }

        [Required]
        public byte FileFormatTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FileName { get; set; }

        [Required]
        public object ItemFileNumber { get; set; }

        [Required]
        public byte ProcessFlowCode { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public object RefNumber { get; set; }

        [Required]
        public Guid CompareTransactionHeaderID { get; set; }

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
        public virtual rpCompareTransactionHeader rpCompareTransactionHeader { get; set; }

    }
}
