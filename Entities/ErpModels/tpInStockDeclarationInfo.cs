using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInStockDeclarationInfo")]
    public partial class tpInStockDeclarationInfo
    {
        public tpInStockDeclarationInfo()
        {
            tpOutStockDeclarationInfos = new HashSet<tpOutStockDeclarationInfo>();
        }

        [Key]
        [Required]
        public Guid InStockDeclarationInfoID { get; set; }

        [Required]
        public Guid InStockID { get; set; }

        [Required]
        public DateTime DeclarationDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DeclarationNumber { get; set; }

        [Required]
        public int DeclarationLineNumber { get; set; }

        [Required]
        public double Qty { get; set; }

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
        public virtual trStock trStock { get; set; }

        public virtual ICollection<tpOutStockDeclarationInfo> tpOutStockDeclarationInfos { get; set; }
    }
}
