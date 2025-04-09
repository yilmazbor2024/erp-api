using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdInsuranceType")]
    public partial class cdInsuranceType
    {
        public cdInsuranceType()
        {
            cdInsuranceTypeDescs = new HashSet<cdInsuranceTypeDesc>();
            prExportFileInsurances = new HashSet<prExportFileInsurance>();
            prFixedAssetInsurances = new HashSet<prFixedAssetInsurance>();
            prImportFileInsurances = new HashSet<prImportFileInsurance>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string InsuranceTypeCode { get; set; }

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

        public virtual ICollection<cdInsuranceTypeDesc> cdInsuranceTypeDescs { get; set; }
        public virtual ICollection<prExportFileInsurance> prExportFileInsurances { get; set; }
        public virtual ICollection<prFixedAssetInsurance> prFixedAssetInsurances { get; set; }
        public virtual ICollection<prImportFileInsurance> prImportFileInsurances { get; set; }
    }
}
