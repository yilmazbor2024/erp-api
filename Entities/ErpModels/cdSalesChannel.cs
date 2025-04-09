using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSalesChannel")]
    public partial class cdSalesChannel
    {
        public cdSalesChannel()
        {
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdSalesChannelDescs = new HashSet<cdSalesChannelDesc>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SalesChannelCode { get; set; }

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

        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdSalesChannelDesc> cdSalesChannelDescs { get; set; }
    }
}
