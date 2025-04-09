using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdWorkPlaceGroup")]
    public partial class cdWorkPlaceGroup
    {
        public cdWorkPlaceGroup()
        {
            cdWorkPlaces = new HashSet<cdWorkPlace>();
            cdWorkPlaceGroupDescs = new HashSet<cdWorkPlaceGroupDesc>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WorkPlaceGroupCode { get; set; }

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

        public virtual ICollection<cdWorkPlace> cdWorkPlaces { get; set; }
        public virtual ICollection<cdWorkPlaceGroupDesc> cdWorkPlaceGroupDescs { get; set; }
    }
}
