using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdWarehouseChannelTemplate")]
    public partial class cdWarehouseChannelTemplate
    {
        public cdWarehouseChannelTemplate()
        {
            cdWarehouseChannelTemplateDescs = new HashSet<cdWarehouseChannelTemplateDesc>();
            prWarehouseChannelTemplateContents = new HashSet<prWarehouseChannelTemplateContent>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string WarehouseChannelTemplateCode { get; set; }

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

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }

        public virtual ICollection<cdWarehouseChannelTemplateDesc> cdWarehouseChannelTemplateDescs { get; set; }
        public virtual ICollection<prWarehouseChannelTemplateContent> prWarehouseChannelTemplateContents { get; set; }
    }
}
