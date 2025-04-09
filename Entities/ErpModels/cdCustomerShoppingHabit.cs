using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCustomerShoppingHabit")]
    public partial class cdCustomerShoppingHabit
    {
        public cdCustomerShoppingHabit()
        {
            cdCustomerCRMGroups = new HashSet<cdCustomerCRMGroup>();
            cdCustomerShoppingHabitDescs = new HashSet<cdCustomerShoppingHabitDesc>();
        }

        [Key]
        [Required]
        public byte CustomerShoppingHabitCode { get; set; }

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

        public virtual ICollection<cdCustomerCRMGroup> cdCustomerCRMGroups { get; set; }
        public virtual ICollection<cdCustomerShoppingHabitDesc> cdCustomerShoppingHabitDescs { get; set; }
    }
}
