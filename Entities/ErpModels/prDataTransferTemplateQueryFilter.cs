using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDataTransferTemplateQueryFilter")]
    public partial class prDataTransferTemplateQueryFilter
    {
        public prDataTransferTemplateQueryFilter()
        {
            prDataTransferTemplateQuerys = new HashSet<prDataTransferTemplateQuery>();
        }

        [Key]
        [Required]
        public int DataTransferTemplateQueryFilterID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string UserGroupCode { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DatabaseName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ReportName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FilterName { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

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
        public virtual cdDataLanguage cdDataLanguage { get; set; }

        public virtual ICollection<prDataTransferTemplateQuery> prDataTransferTemplateQuerys { get; set; }
    }
}
