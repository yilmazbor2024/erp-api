using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpDispOrderHeaderExtension")]
    public partial class tpDispOrderHeaderExtension
    {
        public tpDispOrderHeaderExtension()
        {
        }

        [Key]
        [Required]
        public Guid DispOrderHeaderID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DBSBankCode { get; set; }

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

        // Navigation Properties
        public virtual cdBank cdBank { get; set; }
        public virtual trDispOrderHeader trDispOrderHeader { get; set; }

    }
}
