using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpMobildevIVTContactReconciliation")]
    public partial class rpMobildevIVTContactReconciliation
    {
        public rpMobildevIVTContactReconciliation()
        {
        }

        [Key]
        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Email { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime updatedAt { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Reason { get; set; }

        public string Note { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StatusId { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string StatusText { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PermSourceId { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string PermSourceText { get; set; }

        [Required]
        public DateTime logAt { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

    }
}
