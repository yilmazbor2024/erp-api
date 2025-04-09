using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trSMSPoolMessage")]
    public partial class trSMSPoolMessage
    {
        public trSMSPoolMessage()
        {
            trSMSPoolLines = new HashSet<trSMSPoolLine>();
        }

        [Key]
        [Required]
        public Guid SMSPoolMessageID { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object MessageText { get; set; }

        [Required]
        public Guid SMSPoolHeaderID { get; set; }

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
        public virtual trSMSPoolHeader trSMSPoolHeader { get; set; }

        public virtual ICollection<trSMSPoolLine> trSMSPoolLines { get; set; }
    }
}
