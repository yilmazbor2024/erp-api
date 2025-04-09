using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPrivateInsurance")]
    public partial class cdPrivateInsurance
    {
        public cdPrivateInsurance()
        {
            cdPrivateInsuranceDescs = new HashSet<cdPrivateInsuranceDesc>();
            hrEmployeePrivateInsurances = new HashSet<hrEmployeePrivateInsurance>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PrivateInsuranceCode { get; set; }

        [Required]
        public bool IsHealthInsurance { get; set; }

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

        public virtual ICollection<cdPrivateInsuranceDesc> cdPrivateInsuranceDescs { get; set; }
        public virtual ICollection<hrEmployeePrivateInsurance> hrEmployeePrivateInsurances { get; set; }
    }
}
