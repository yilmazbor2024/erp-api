using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpDmsGetCustomerByCode")]
    public partial class zpDmsGetCustomerByCode
    {
        public zpDmsGetCustomerByCode()
        {
        }

        [Key]
        [Required]
        public Guid GetCustomerByCodeID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Code { get; set; }

        public string ResponseCode { get; set; }

        [Required]
        public int OperationStatus { get; set; }

        [Required]
        public int Segment { get; set; }

        [Required]
        public int Status { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DMSNo { get; set; }

        [Required]
        public int RecognitionId { get; set; }

        public string ApplicationName { get; set; }

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
