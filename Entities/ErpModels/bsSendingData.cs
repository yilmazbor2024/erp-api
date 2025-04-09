using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsSendingData")]
    public partial class bsSendingData
    {
        public bsSendingData()
        {
            prDataTransferTemplateQuerys = new HashSet<prDataTransferTemplateQuery>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TransferName { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ElementName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string WizardName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SpName { get; set; }

        [Required]
        public bool IsOptional { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FileName { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool UseLanguageFilter { get; set; }

        [Required]
        public bool UseElementNameFilter { get; set; }

        [Required]
        public byte TemplateType { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<prDataTransferTemplateQuery> prDataTransferTemplateQuerys { get; set; }
    }
}
