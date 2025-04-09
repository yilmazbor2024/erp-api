using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdMMSBusinessPartnerService")]
    public partial class cdMMSBusinessPartnerService
    {
        public cdMMSBusinessPartnerService()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MMSBusinessPartnerCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Gateway { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PlatformID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ChannelCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Password { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ApiKey { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Secret { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string CollectorID { get; set; }

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
        public virtual bsMMSBusinessPartner bsMMSBusinessPartner { get; set; }
        public virtual cdCompany cdCompany { get; set; }

    }
}
