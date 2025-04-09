using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCustomerCRMGroup")]
    public partial class cdCustomerCRMGroup
    {
        public cdCustomerCRMGroup()
        {
            cdCustomerCRMGroupDescs = new HashSet<cdCustomerCRMGroupDesc>();
            prCustomerPresentCards = new HashSet<prCustomerPresentCard>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerCRMGroupCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PresentCardTypeCode { get; set; }

        [Required]
        public byte CustomerShoppingLevelCode { get; set; }

        [Required]
        public byte CustomerShoppingHabitCode { get; set; }

        [Required]
        public byte CustomerAlertColorCode { get; set; }

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
        public virtual cdCustomerAlertColor cdCustomerAlertColor { get; set; }
        public virtual cdPresentCardType cdPresentCardType { get; set; }
        public virtual cdCustomerShoppingHabit cdCustomerShoppingHabit { get; set; }
        public virtual cdCustomerShoppingLevel cdCustomerShoppingLevel { get; set; }

        public virtual ICollection<cdCustomerCRMGroupDesc> cdCustomerCRMGroupDescs { get; set; }
        public virtual ICollection<prCustomerPresentCard> prCustomerPresentCards { get; set; }
    }
}
