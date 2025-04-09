using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_LastShipmentAskDate")]
    public partial class e_LastShipmentAskDate
    {
        public e_LastShipmentAskDate()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public DateTime LastShipmentAskDate { get; set; }

        [Required]
        public DateTime LastShipmentResponseAskDate { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }

    }
}
