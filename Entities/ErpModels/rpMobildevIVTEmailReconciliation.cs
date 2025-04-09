using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpMobildevIVTEmailReconciliation")]
    public partial class rpMobildevIVTEmailReconciliation
    {
        public rpMobildevIVTEmailReconciliation()
        {
        }

        [Key]
        [Required]
        public Guid MobildevIVTEmailReconciliationID { get; set; }

        [Required]
        public int LogId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Email { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string FirstName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LastName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Msisdn { get; set; }

        [Required]
        public bool HasIVTForm { get; set; }

        [Required]
        public int StatusId { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string StatusText { get; set; }

        [Required]
        public int TypeId { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string TypeText { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Note { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Reasons { get; set; }

        [Required]
        public int PermSourceId { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string PermSourceText { get; set; }

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
