using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsGettingData")]
    public partial class bsGettingData
    {
        public bsGettingData()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TransferName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string WizardName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CreateTableSpName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BulkInsertSpName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CustomSPName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ConvertSpName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ValidateSpName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MergeSpName { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

    }
}
