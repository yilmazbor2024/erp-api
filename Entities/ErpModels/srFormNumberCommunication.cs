using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("srFormNumberCommunication")]
    public partial class srFormNumberCommunication
    {
        public srFormNumberCommunication()
        {
        }

        [Key]
        [Required]
        public decimal OfficeCodeNumber { get; set; }

        [Key]
        [Required]
        public decimal StoreCodeNumber { get; set; }

        [Key]
        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public decimal LastNumber { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

    }
}
