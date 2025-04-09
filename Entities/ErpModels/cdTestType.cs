using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdTestType")]
    public partial class cdTestType
    {
        public cdTestType()
        {
            cdTestTypeDescs = new HashSet<cdTestTypeDesc>();
            trItemTestHeaders = new HashSet<trItemTestHeader>();
        }

        [Key]
        [Required]
        public byte TestTypeCode { get; set; }

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

        public virtual ICollection<cdTestTypeDesc> cdTestTypeDescs { get; set; }
        public virtual ICollection<trItemTestHeader> trItemTestHeaders { get; set; }
    }
}
