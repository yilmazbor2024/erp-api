using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsRetailCustomerConditionalRequiredFields")]
    public partial class bsRetailCustomerConditionalRequiredFields
    {
        public bsRetailCustomerConditionalRequiredFields()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FieldName { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

    }
}
