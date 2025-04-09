using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPresentCardActivationStatus")]
    public partial class bsPresentCardActivationStatus
    {
        public bsPresentCardActivationStatus()
        {
            bsPresentCardActivationStatusDescs = new HashSet<bsPresentCardActivationStatusDesc>();
            prCustomerPresentCards = new HashSet<prCustomerPresentCard>();
        }

        [Key]
        [Required]
        public byte ActivationStatusCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPresentCardActivationStatusDesc> bsPresentCardActivationStatusDescs { get; set; }
        public virtual ICollection<prCustomerPresentCard> prCustomerPresentCards { get; set; }
    }
}
