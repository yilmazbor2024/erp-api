using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trDispOrderHeader")]
    public partial class trDispOrderHeader
    {
        public trDispOrderHeader()
        {
            tpDispOrderHeaderExtensions = new HashSet<tpDispOrderHeaderExtension>();
            trDispOrderLines = new HashSet<trDispOrderLine>();
        }

        [Key]
        [Required]
        public Guid DispOrderHeaderID { get; set; }

        [Required]
        public byte DispOrderTypeCode { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public object DispOrderNumber { get; set; }

        [Required]
        public bool IsReturn { get; set; }

        [Required]
        public DateTime DispOrderDate { get; set; }

        [Required]
        public TimeSpan DispOrderTime { get; set; }

        [Required]
        public DateTime ShippingDate { get; set; }

        [Required]
        public TimeSpan ShippingTime { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RoundsmanCode { get; set; }

        public string Description { get; set; }

        public string InternalDescription { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomerASNNumber { get; set; }

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

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ConfirmedUser { get; set; }

        [Required]
        public DateTime ConfirmedDate { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

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
        public virtual cdRoundsman cdRoundsman { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual bsDispOrderType bsDispOrderType { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

        public virtual ICollection<tpDispOrderHeaderExtension> tpDispOrderHeaderExtensions { get; set; }
        public virtual ICollection<trDispOrderLine> trDispOrderLines { get; set; }
    }
}
