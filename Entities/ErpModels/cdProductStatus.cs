using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdProductStatus")]
    public partial class cdProductStatus
    {
        public cdProductStatus()
        {
            cdProductStatusDescs = new HashSet<cdProductStatusDesc>();
            prProductStatusHistorys = new HashSet<prProductStatusHistory>();
        }

        [Key]
        [Required]
        public short ProductStatusCode { get; set; }

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

        public virtual ICollection<cdProductStatusDesc> cdProductStatusDescs { get; set; }
        public virtual ICollection<prProductStatusHistory> prProductStatusHistorys { get; set; }
    }
}
