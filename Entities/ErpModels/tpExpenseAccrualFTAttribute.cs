using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpExpenseAccrualFTAttribute")]
    public partial class tpExpenseAccrualFTAttribute
    {
        public tpExpenseAccrualFTAttribute()
        {
        }

        [Key]
        [Required]
        public Guid ExpenseAccrualLineID { get; set; }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttributeCode { get; set; }

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
        public virtual trExpenseAccrualLine trExpenseAccrualLine { get; set; }
        public virtual cdFTAttribute cdFTAttribute { get; set; }

    }
}
