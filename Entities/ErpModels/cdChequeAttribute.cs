using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdChequeAttribute")]
    public partial class cdChequeAttribute
    {
        public cdChequeAttribute()
        {
            cdChequeAttributeDescs = new HashSet<cdChequeAttributeDesc>();
            prChequeAttributes = new HashSet<prChequeAttribute>();
        }

        [Key]
        [Required]
        public byte ChequeTypeCode { get; set; }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttributeCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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
        public virtual cdChequeAttributeType cdChequeAttributeType { get; set; }

        public virtual ICollection<cdChequeAttributeDesc> cdChequeAttributeDescs { get; set; }
        public virtual ICollection<prChequeAttribute> prChequeAttributes { get; set; }
    }
}
