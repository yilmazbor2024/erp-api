using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdWorkPlaceType")]
    public partial class cdWorkPlaceType
    {
        public cdWorkPlaceType()
        {
            cdWorkPlaces = new HashSet<cdWorkPlace>();
            cdWorkPlaceTypeDescs = new HashSet<cdWorkPlaceTypeDesc>();
        }

        [Key]
        [Required]
        public byte WorkPlaceTypeCode { get; set; }

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
        public virtual ICollection<cdWorkPlaceTypeDesc> cdWorkPlaceTypeDescs { get; set; }
    }
}
