using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpOnlineBankServiceFinrotaLog")]
    public partial class zpOnlineBankServiceFinrotaLog
    {
        public zpOnlineBankServiceFinrotaLog()
        {
        }

        [Key]
        [Required]
        public Guid ServiceLogID { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }

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
