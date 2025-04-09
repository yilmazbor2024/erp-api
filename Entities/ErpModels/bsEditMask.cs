using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsEditMask")]
    public partial class bsEditMask
    {
        public bsEditMask()
        {
            bsEditMaskDescs = new HashSet<bsEditMaskDesc>();
            cdCommunicationTypes = new HashSet<cdCommunicationType>();
            cdCreditCardTypes = new HashSet<cdCreditCardType>();
        }

        [Key]
        [Required]
        public int EditMaskCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string EditMask { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Sample { get; set; }

        [Required]
        public byte CommunicationKindCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsCommunicationKind bsCommunicationKind { get; set; }

        public virtual ICollection<bsEditMaskDesc> bsEditMaskDescs { get; set; }
        public virtual ICollection<cdCommunicationType> cdCommunicationTypes { get; set; }
        public virtual ICollection<cdCreditCardType> cdCreditCardTypes { get; set; }
    }
}
