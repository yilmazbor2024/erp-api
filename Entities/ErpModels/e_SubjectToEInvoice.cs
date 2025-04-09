using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_SubjectToEInvoice")]
    public partial class e_SubjectToEInvoice
    {
        public e_SubjectToEInvoice()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CompanyName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TitleName { get; set; }

        [Required]
        public byte Type { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

    }
}
