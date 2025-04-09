using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPerceptionOfFashion")]
    public partial class cdPerceptionOfFashion
    {
        public cdPerceptionOfFashion()
        {
            cdItems = new HashSet<cdItem>();
            cdPerceptionOfFashionDescs = new HashSet<cdPerceptionOfFashionDesc>();
        }

        [Key]
        [Required]
        public byte PerceptionOfFashionCode { get; set; }

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

        public virtual ICollection<cdItem> cdItems { get; set; }
        public virtual ICollection<cdPerceptionOfFashionDesc> cdPerceptionOfFashionDescs { get; set; }
    }
}
