using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auAirConnZInfo")]
    public partial class auAirConnZInfo
    {
        public auAirConnZInfo()
        {
        }

        [Key]
        [Required]
        public Guid AirConnZInfoID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string document_id { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string reportNumber { get; set; }

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

    }
}
