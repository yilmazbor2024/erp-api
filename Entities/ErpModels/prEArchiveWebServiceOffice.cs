using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prEArchiveWebServiceOffice")]
    public partial class prEArchiveWebServiceOffice
    {
        public prEArchiveWebServiceOffice()
        {
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string EArchiveWebServiceCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

 

        [Key]
        [Required]
        public object OfficeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string EArchiveBranchCode { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdEArchiveWebService cdEArchiveWebService { get; set; }

    }
}
