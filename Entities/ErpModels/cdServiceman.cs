using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdServiceman")]
    public partial class cdServiceman
    {
        public cdServiceman()
        {
            tpSupportResolves = new HashSet<tpSupportResolve>();
            trInnerHeaders = new HashSet<trInnerHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ServicemanCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FirstLastName { get; set; }

        [Required]
        public bool SignOff { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string VehiclePlateNum { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EMailAddress { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string MobilePhone { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public byte EmployeeTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EmployeeCode { get; set; }

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
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpSupportResolve> tpSupportResolves { get; set; }
        public virtual ICollection<trInnerHeader> trInnerHeaders { get; set; }
    }
}
