using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBankPOSImportType")]
    public partial class bsBankPOSImportType
    {
        public bsBankPOSImportType()
        {
            bsBankPOSImportTypeDescs = new HashSet<bsBankPOSImportTypeDesc>();
        }

        [Key]
        [Required]
        public byte BankPOSImportTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsBankPOSImportTypeDesc> bsBankPOSImportTypeDescs { get; set; }
    }
}
