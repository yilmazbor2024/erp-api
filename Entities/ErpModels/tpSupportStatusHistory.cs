using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpSupportStatusHistory")]
    public partial class tpSupportStatusHistory
    {
        public tpSupportStatusHistory()
        {
        }

        [Key]
        [Required]
        public Guid SupportStatusHistoryID { get; set; }

        [Required]
        public Guid SupportRequestHeaderID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SupportStatusCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [Required]
        public int SortOrder { get; set; }

        public Guid? SupportResolveID { get; set; }

        public Guid? InnerHeaderID { get; set; }

        [Required]
        public bool IsUnchangeable { get; set; }

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
        public virtual trSupportRequestHeader trSupportRequestHeader { get; set; }
        public virtual cdSupportStatus cdSupportStatus { get; set; }
        public virtual tpSupportResolve tpSupportResolve { get; set; }
        public virtual trInnerHeader trInnerHeader { get; set; }

    }
}
