using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpVehicleLoadingRoundsman")]
    public partial class tpVehicleLoadingRoundsman
    {
        public tpVehicleLoadingRoundsman()
        {
        }

        [Key]
        [Required]
        public Guid VehicleLoadingHeaderID { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RoundsmanCode { get; set; }

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
        public virtual cdRoundsman cdRoundsman { get; set; }
        public virtual trVehicleLoadingHeader trVehicleLoadingHeader { get; set; }

    }
}
