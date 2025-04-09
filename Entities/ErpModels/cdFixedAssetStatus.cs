using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdFixedAssetStatus")]
    public partial class cdFixedAssetStatus
    {
        public cdFixedAssetStatus()
        {
            cdFixedAssetStatusDescs = new HashSet<cdFixedAssetStatusDesc>();
            prFixedAssetStatusHistorys = new HashSet<prFixedAssetStatusHistory>();
        }

        [Key]
        [Required]
        public short FixedAssetStatusCode { get; set; }

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

        public virtual ICollection<cdFixedAssetStatusDesc> cdFixedAssetStatusDescs { get; set; }
        public virtual ICollection<prFixedAssetStatusHistory> prFixedAssetStatusHistorys { get; set; }
    }
}
