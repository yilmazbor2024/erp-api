using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpHopiNotifyCheckOutSubTotalDetail")]
    public partial class zpHopiNotifyCheckOutSubTotalDetail
    {
        public zpHopiNotifyCheckOutSubTotalDetail()
        {
        }

        [Key]
        [Required]
        public Guid SubTotalDetailID { get; set; }

        [Required]
        public Guid NotifyCheckOutID { get; set; }

        [Required]
        public byte Percent { get; set; }

        [Required]
        public decimal Amount { get; set; }

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
