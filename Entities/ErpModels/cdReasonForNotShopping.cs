using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdReasonForNotShopping")]
    public partial class cdReasonForNotShopping
    {
        public cdReasonForNotShopping()
        {
            cdReasonForNotShoppingDescs = new HashSet<cdReasonForNotShoppingDesc>();
            tpAgentReservationReasonForNotShoppings = new HashSet<tpAgentReservationReasonForNotShopping>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ReasonForNotShoppingCode { get; set; }

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

        public virtual ICollection<cdReasonForNotShoppingDesc> cdReasonForNotShoppingDescs { get; set; }
        public virtual ICollection<tpAgentReservationReasonForNotShopping> tpAgentReservationReasonForNotShoppings { get; set; }
    }
}
