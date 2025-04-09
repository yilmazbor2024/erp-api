using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdStoreConcept")]
    public partial class cdStoreConcept
    {
        public cdStoreConcept()
        {
            cdStoreConceptDescs = new HashSet<cdStoreConceptDesc>();
            prStorePropertiess = new HashSet<prStoreProperties>();
        }

        [Key]
        [Required]
        public byte StoreConceptCode { get; set; }

        [Required]
        public double MinimumLimit { get; set; }

        [Required]
        public double MaximumLimit { get; set; }

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

        public virtual ICollection<cdStoreConceptDesc> cdStoreConceptDescs { get; set; }
        public virtual ICollection<prStoreProperties> prStorePropertiess { get; set; }
    }
}
