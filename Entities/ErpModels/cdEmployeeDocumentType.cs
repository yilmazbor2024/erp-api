using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdEmployeeDocumentType")]
    public partial class cdEmployeeDocumentType
    {
        public cdEmployeeDocumentType()
        {
            cdEmployeeDocumentTypeDescs = new HashSet<cdEmployeeDocumentTypeDesc>();
            prEmployeeDocuments = new HashSet<prEmployeeDocument>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EmployeeDocumentTypeCode { get; set; }

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

        public virtual ICollection<cdEmployeeDocumentTypeDesc> cdEmployeeDocumentTypeDescs { get; set; }
        public virtual ICollection<prEmployeeDocument> prEmployeeDocuments { get; set; }
    }
}
