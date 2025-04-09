using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfGlobalDataMatrix")]
    public partial class dfGlobalDataMatrix
    {
        public dfGlobalDataMatrix()
        {
        }

        [Key]
        [Required]
        public byte GlobalDefaultCode { get; set; }

        [Key]
        [Required]
        public byte TypeCode { get; set; }

        [Required]
        public byte SerialNumberSize { get; set; }

        [Required]
        public decimal LastNumber { get; set; }

        [Required]
        public byte PartyITAttributeNumber { get; set; }

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

        // Navigation Properties
        public virtual dfGlobalDefault dfGlobalDefault { get; set; }

    }
}
