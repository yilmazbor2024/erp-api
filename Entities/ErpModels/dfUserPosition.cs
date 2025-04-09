using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfUserPosition")]
    public partial class dfUserPosition
    {
        public dfUserPosition()
        {
            dfMobilRevenueUsers = new HashSet<dfMobilRevenueUser>();
            dfUserAllowedOffices = new HashSet<dfUserAllowedOffice>();
            dfUserAllowedStores = new HashSet<dfUserAllowedStore>();
            dfUserAllowedWarehouses = new HashSet<dfUserAllowedWarehouse>();
            dfUserPosUISettingss = new HashSet<dfUserPosUISettings>();
            prRoleMembers = new HashSet<prRoleMember>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string UserGroupCode { get; set; }

        [Key]
        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string UserName { get; set; }

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

        [Required]
        public byte EmployeeTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EmployeeCode { get; set; }

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
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<dfMobilRevenueUser> dfMobilRevenueUsers { get; set; }
        public virtual ICollection<dfUserAllowedOffice> dfUserAllowedOffices { get; set; }
        public virtual ICollection<dfUserAllowedStore> dfUserAllowedStores { get; set; }
        public virtual ICollection<dfUserAllowedWarehouse> dfUserAllowedWarehouses { get; set; }
        public virtual ICollection<dfUserPosUISettings> dfUserPosUISettingss { get; set; }
        public virtual ICollection<prRoleMember> prRoleMembers { get; set; }
    }
}
