using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdBarcodeCompany")]
    public partial class cdBarcodeCompany
    {
        public cdBarcodeCompany()
        {
            prMedicalProductPropertiess = new HashSet<prMedicalProductProperties>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BarcodeCompanyCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Description { get; set; }

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

        public virtual ICollection<prMedicalProductProperties> prMedicalProductPropertiess { get; set; }
    }
}
