using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfBulkMailServiceProviderAccount")]
    public partial class dfBulkMailServiceProviderAccount
    {
        public dfBulkMailServiceProviderAccount()
        {
        }

        [Key]
        [Required]
        public Guid BulkMailServiceProviderAccountID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyBrandCode { get; set; }

        [Required]
        public byte BulkMailServiceProviderCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SFTPHost { get; set; }

        [Required]
        public int SFTPPort { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SFTPUserName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SFTPPassword { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SFTPOutgoingDirectory { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SFTPIncomingDirectory { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string WebServiceUsername { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string WebServicePassword { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string WebServiceAddress1 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string WebServiceAddress2 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string WebServiceAddress3 { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string WebServiceAddress4 { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string NotificationEmailAddress { get; set; }

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
        public virtual cdCompanyBrand cdCompanyBrand { get; set; }
        public virtual bsBulkMailServiceProvider bsBulkMailServiceProvider { get; set; }
        public virtual cdCompany cdCompany { get; set; }

    }
}
