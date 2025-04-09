using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prDataTransferTemplateQuery")]
    public partial class prDataTransferTemplateQuery
    {
        public prDataTransferTemplateQuery()
        {
        }

        [Key]
        [Required]
        public int DataTransferTemplateQueryID { get; set; }

        public int? DataTransferTemplateQueryFilterID { get; set; }

        [Required]
        public int DataTransferTemplateID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TransferName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ElementName { get; set; }

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
        public virtual prDataTransferTemplateQueryFilter prDataTransferTemplateQueryFilter { get; set; }
        public virtual cdDataTransferTemplate cdDataTransferTemplate { get; set; }
        public virtual bsSendingData bsSendingData { get; set; }

    }
}
