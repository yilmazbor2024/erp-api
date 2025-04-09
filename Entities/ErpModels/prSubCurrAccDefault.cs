using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prSubCurrAccDefault")]
    public partial class prSubCurrAccDefault
    {
        public prSubCurrAccDefault()
        {
        }

        [Key]
        [Required]
        public Guid SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

        public Guid? PostalAddressID { get; set; }

        public Guid? CommunicationID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CardNumber { get; set; }

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
        public virtual prCurrAccPostalAddress prCurrAccPostalAddress { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual prCurrAccCommunication prCurrAccCommunication { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

    }
}
