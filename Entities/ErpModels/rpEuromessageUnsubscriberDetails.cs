using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpEuromessageUnsubscriberDetails")]
    public partial class rpEuromessageUnsubscriberDetails
    {
        public rpEuromessageUnsubscriberDetails()
        {
        }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Email { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string KeyId { get; set; }

        [Required]
        public DateTime UnsubscribeTime { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Reason { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GsmNo { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string EmailPermit { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string GsmPermit { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Status { get; set; }

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

    }
}
