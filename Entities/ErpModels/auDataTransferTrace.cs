using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auDataTransferTrace")]
    public partial class auDataTransferTrace
    {
        public auDataTransferTrace()
        {
        }

        [Key]
        [Required]
        public Guid TraceID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SourceServerName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SourceDatabaseName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TargetServerName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TargetDatabaseName { get; set; }

        [Required]
        public byte TraceType { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ConfigName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TransferName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PackageName { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UserName { get; set; }

    }
}
