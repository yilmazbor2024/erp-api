using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auFastPayServiceLog")]
    public partial class auFastPayServiceLog
    {
        public auFastPayServiceLog()
        {
        }

        [Key]
        [Required]
        public Guid LogID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaymentID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string OperationType { get; set; }

        public string Content { get; set; }

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
