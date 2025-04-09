using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prWorkplaceSGKLogonInfo")]
    public partial class prWorkplaceSGKLogonInfo
    {
        public prWorkplaceSGKLogonInfo()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WorkPlaceCode { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string UserName { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SGKWorkPlaceCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SystemPassword { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string WorkPlacePassword { get; set; }

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
        public virtual cdWorkPlace cdWorkPlace { get; set; }

    }
}
