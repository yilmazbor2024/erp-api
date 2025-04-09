using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdLegalResignation")]
    public partial class cdLegalResignation
    {
        public cdLegalResignation()
        {
            cdLegalResignationDescs = new HashSet<cdLegalResignationDesc>();
            hrEmployeePayrollProfiles = new HashSet<hrEmployeePayrollProfile>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LegalResignationCode { get; set; }

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

        public virtual ICollection<cdLegalResignationDesc> cdLegalResignationDescs { get; set; }
        public virtual ICollection<hrEmployeePayrollProfile> hrEmployeePayrollProfiles { get; set; }
    }
}
