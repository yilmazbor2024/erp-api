using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trReserveHeader")]
    public partial class trReserveHeader
    {
        public trReserveHeader()
        {
            trReserveLines = new HashSet<trReserveLine>();
            trReserveTransfers = new HashSet<trReserveTransfer>();
        }

        [Key]
        [Required]
        public Guid ReserveHeaderID { get; set; }

        [Required]
        public byte ReserveTypeCode { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public object ReserveNumber { get; set; }

        [Required]
        public bool IsReturn { get; set; }

        [Required]
        public DateTime ReserveDate { get; set; }

        [Required]
        public TimeSpan ReserveTime { get; set; }

        public string Description { get; set; }

        public string InternalDescription { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

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
        public string WarehouseCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ToWarehouseCode { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsReserveType bsReserveType { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

        public virtual ICollection<trReserveLine> trReserveLines { get; set; }
        public virtual ICollection<trReserveTransfer> trReserveTransfers { get; set; }
    }
}
