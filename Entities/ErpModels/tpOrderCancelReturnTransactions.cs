using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpOrderCancelReturnTransactions")]
    public partial class tpOrderCancelReturnTransactions
    {
        public tpOrderCancelReturnTransactions()
        {
        }

        [Key]
        [Required]
        public Guid OrderCancelDetailHeaderID { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ApplicationCode { get; set; }

        [Key]
        [Required]
        public Guid ApplicationID { get; set; }

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
        public virtual tpOrderCancelDetailHeader tpOrderCancelDetailHeader { get; set; }

    }
}
