using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdContainerType")]
    public partial class cdContainerType
    {
        public cdContainerType()
        {
            cdContainerTypeDescs = new HashSet<cdContainerTypeDesc>();
            prExportFileShippingInfos = new HashSet<prExportFileShippingInfo>();
            prImportFileShippingInfos = new HashSet<prImportFileShippingInfo>();
            trOrderAsnHeaders = new HashSet<trOrderAsnHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ContainerTypeCode { get; set; }

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

        public virtual ICollection<cdContainerTypeDesc> cdContainerTypeDescs { get; set; }
        public virtual ICollection<prExportFileShippingInfo> prExportFileShippingInfos { get; set; }
        public virtual ICollection<prImportFileShippingInfo> prImportFileShippingInfos { get; set; }
        public virtual ICollection<trOrderAsnHeader> trOrderAsnHeaders { get; set; }
    }
}
