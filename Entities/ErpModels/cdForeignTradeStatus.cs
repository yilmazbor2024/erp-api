using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdForeignTradeStatus")]
    public partial class cdForeignTradeStatus
    {
        public cdForeignTradeStatus()
        {
            cdForeignTradeStatusDescs = new HashSet<cdForeignTradeStatusDesc>();
            prExportFileStatusHistorys = new HashSet<prExportFileStatusHistory>();
            prImportFileStatusHistorys = new HashSet<prImportFileStatusHistory>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ForeignTradeStatusCode { get; set; }

        [Required]
        public bool UseOnImportFile { get; set; }

        [Required]
        public bool UseOnExportFile { get; set; }

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

        public virtual ICollection<cdForeignTradeStatusDesc> cdForeignTradeStatusDescs { get; set; }
        public virtual ICollection<prExportFileStatusHistory> prExportFileStatusHistorys { get; set; }
        public virtual ICollection<prImportFileStatusHistory> prImportFileStatusHistorys { get; set; }
    }
}
