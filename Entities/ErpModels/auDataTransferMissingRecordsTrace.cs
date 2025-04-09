using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auDataTransferMissingRecordsTrace")]
    public partial class auDataTransferMissingRecordsTrace
    {
        public auDataTransferMissingRecordsTrace()
        {
        }

        [Key]
        [Required]
        public int LogID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ServerName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DBName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PackageName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PackageType { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SequenceName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TaskName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string HeaderIDName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string HeaderID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string IDName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ColumnName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Record { get; set; }

    }
}
