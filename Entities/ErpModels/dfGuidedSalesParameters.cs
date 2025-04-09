using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfGuidedSalesParameters")]
    public partial class dfGuidedSalesParameters
    {
        public dfGuidedSalesParameters()
        {
        }

        [Key]
        [Required]
        public Guid GuidedSalesParametersID { get; set; }

        public bool? NotifyStoreWithEmail { get; set; }

        public bool? NotifyStoreWithSMS { get; set; }

        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object? HeadOfficeNotificaitonEmailAddresses { get; set; }

        public bool? CustomerCodeRequiredOnStoreRequest { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PhoneDefaultPrefix { get; set; }

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
