using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDataTransferCompanyParameter")]
    public partial class prDataTransferCompanyParameter
    {
        public prDataTransferCompanyParameter()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DataTransferCompanyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string FolderType { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string FolderName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ConnectionUser { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ConnectionPassword { get; set; }

        [Required]
        public int SFTPPort { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object SFTPHost { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ProcessNumberPrefix { get; set; }

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

    }
}
