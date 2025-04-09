using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_InboxShipmentDrivers")]
    public partial class e_InboxShipmentDrivers
    {
        public e_InboxShipmentDrivers()
        {
        }

        [Key]
        [Required]
        public Guid InboxShipmentDriversID { get; set; }

        [Required]
        public Guid UUID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FirstName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FamilyName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Title { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string NationalityID { get; set; }

        // Navigation Properties
        public virtual e_InboxShipmentHeader e_InboxShipmentHeader { get; set; }

    }
}
