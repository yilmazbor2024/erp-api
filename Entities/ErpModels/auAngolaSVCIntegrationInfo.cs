using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auAngolaSVCIntegrationInfo")]
    public partial class auAngolaSVCIntegrationInfo
    {
        public auAngolaSVCIntegrationInfo()
        {
        }

        [Key]
        [Required]
        public Guid AngolaSVCIntegrationInfoID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationName { get; set; }

        [Required]
        public Guid ApplicationID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string InvoiceDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SystemEntryDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string InvoiceNo { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GrossTotal { get; set; }

        public string Hash { get; set; }

        public string Message { get; set; }

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
