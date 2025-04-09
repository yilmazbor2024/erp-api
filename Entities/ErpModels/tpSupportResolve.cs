using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpSupportResolve")]
    public partial class tpSupportResolve
    {
        public tpSupportResolve()
        {
            tpSupportResolveMaterials = new HashSet<tpSupportResolveMaterial>();
            tpSupportStatusHistorys = new HashSet<tpSupportStatusHistory>();
        }

        [Key]
        [Required]
        public Guid SupportResolveID { get; set; }

        [Required]
        public Guid SupportRequestHeaderID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SupportResolveTypeCode { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ServicemanCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RoundsmanCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

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
        public virtual trSupportRequestHeader trSupportRequestHeader { get; set; }
        public virtual cdServiceman cdServiceman { get; set; }
        public virtual cdRoundsman cdRoundsman { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual cdSupportResolveType cdSupportResolveType { get; set; }

        public virtual ICollection<tpSupportResolveMaterial> tpSupportResolveMaterials { get; set; }
        public virtual ICollection<tpSupportStatusHistory> tpSupportStatusHistorys { get; set; }
    }
}
