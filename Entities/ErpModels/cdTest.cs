using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdTest")]
    public partial class cdTest
    {
        public cdTest()
        {
            cdTestDescs = new HashSet<cdTestDesc>();
            hrTestResults = new HashSet<hrTestResult>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string TestCode { get; set; }

        [Required]
        public int QuestionCount { get; set; }

        [Required]
        public double HighestScore { get; set; }

        [Required]
        public double PassScore { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }

        public virtual ICollection<cdTestDesc> cdTestDescs { get; set; }
        public virtual ICollection<hrTestResult> hrTestResults { get; set; }
    }
}
