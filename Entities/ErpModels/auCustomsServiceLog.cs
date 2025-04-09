using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auCustomsServiceLog")]
    public partial class auCustomsServiceLog
    {
        public auCustomsServiceLog()
        {
        }

        [Key]
        [Required]
        public Guid ServiceLogID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ServiceMethodName { get; set; }

        public object? Request { get; set; }

        public object? Response { get; set; }

        public Guid? InvoiceHeaderID { get; set; }

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

    }
}
