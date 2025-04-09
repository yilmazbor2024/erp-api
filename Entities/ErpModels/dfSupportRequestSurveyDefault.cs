using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfSupportRequestSurveyDefault")]
    public partial class dfSupportRequestSurveyDefault
    {
        public dfSupportRequestSurveyDefault()
        {
        }

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SurveyCode { get; set; }

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
        public virtual cdSurvey cdSurvey { get; set; }
        public virtual bsCurrAccType bsCurrAccType { get; set; }

    }
}
