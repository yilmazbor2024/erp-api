using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDataTransferCompany")]
    public partial class cdDataTransferCompany
    {
        public cdDataTransferCompany()
        {
            cdDataTransferCompanyDescs = new HashSet<cdDataTransferCompanyDesc>();
            prDataTransferCompanyParameters = new HashSet<prDataTransferCompanyParameter>();
            prDataTransferJobClientss = new HashSet<prDataTransferJobClients>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DataTransferCompanyCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public bool IsPartnerCompany { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<cdDataTransferCompanyDesc> cdDataTransferCompanyDescs { get; set; }
        public virtual ICollection<prDataTransferCompanyParameter> prDataTransferCompanyParameters { get; set; }
        public virtual ICollection<prDataTransferJobClients> prDataTransferJobClientss { get; set; }
    }
}
