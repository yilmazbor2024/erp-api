using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDataTransferTemplate")]
    public partial class cdDataTransferTemplate
    {
        public cdDataTransferTemplate()
        {
            cdDataTransferJobs = new HashSet<cdDataTransferJob>();
            prDataTransferTemplateQuerys = new HashSet<prDataTransferTemplateQuery>();
        }

        [Key]
        [Required]
        public int DataTransferTemplateID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DataTransferTemplateCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string DataTransferTemplateDescription { get; set; }

        [Required]
        public byte TemplateType { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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

        public virtual ICollection<cdDataTransferJob> cdDataTransferJobs { get; set; }
        public virtual ICollection<prDataTransferTemplateQuery> prDataTransferTemplateQuerys { get; set; }
    }
}
