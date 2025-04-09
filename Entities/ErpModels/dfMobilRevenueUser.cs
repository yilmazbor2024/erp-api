using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfMobilRevenueUser")]
    public partial class dfMobilRevenueUser
    {
        public dfMobilRevenueUser()
        {
            auMobilRevenueReportPermits = new HashSet<auMobilRevenueReportPermit>();
            dfMobilRevenueUserSalesPoints = new HashSet<dfMobilRevenueUserSalesPoint>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string UserGroupCode { get; set; }

        [Key]
        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string UserName { get; set; }

        [Required]
        public bool AllowedAllSalesPoints { get; set; }

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
        public virtual dfUserPosition dfUserPosition { get; set; }

        public virtual ICollection<auMobilRevenueReportPermit> auMobilRevenueReportPermits { get; set; }
        public virtual ICollection<dfMobilRevenueUserSalesPoint> dfMobilRevenueUserSalesPoints { get; set; }
    }
}
