using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpBankMT940")]
    public partial class tpBankMT940
    {
        public tpBankMT940()
        {
        }

        [Key]
        [Required]
        public Guid BankHeaderID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MT940FileName { get; set; }

        public Guid? MT940ProcessRulesID { get; set; }

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
        public virtual trBankHeader trBankHeader { get; set; }
        public virtual prMT940ProcessRules prMT940ProcessRules { get; set; }

    }
}
