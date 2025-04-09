using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfPAROProductProperty")]
    public partial class dfPAROProductProperty
    {
        public dfPAROProductProperty()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductProperty1 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductProperty2 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductProperty3 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductProperty4 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductProperty5 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductProperty6 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductProperty7 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductProperty8 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductProperty9 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProductProperty10 { get; set; }

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

    }
}
