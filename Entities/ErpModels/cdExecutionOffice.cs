using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdExecutionOffice")]
    public partial class cdExecutionOffice
    {
        public cdExecutionOffice()
        {
            cdExecutionOfficeDescs = new HashSet<cdExecutionOfficeDesc>();
            hrWageGarnishments = new HashSet<hrWageGarnishment>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ExecutionOfficeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CityCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string IBAN { get; set; }

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

        // Navigation Properties
        public virtual cdCity cdCity { get; set; }

        public virtual ICollection<cdExecutionOfficeDesc> cdExecutionOfficeDescs { get; set; }
        public virtual ICollection<hrWageGarnishment> hrWageGarnishments { get; set; }
    }
}
