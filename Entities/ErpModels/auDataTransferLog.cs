using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auDataTransferLog")]
    public partial class auDataTransferLog
    {
        public auDataTransferLog()
        {
        }

        [Key]
        [Required]
        public int ID { get; set; }

        public DateTime? EventDateTime { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EventLevel { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MachineName { get; set; }

        public string EventMessage { get; set; }

        public string ErrorSource { get; set; }

        public string ErrorMethod { get; set; }

        public string ErrorMessage { get; set; }

        public string InnerErrorMessage { get; set; }

    }
}
