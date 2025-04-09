using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfPosUISetting")]
    public partial class dfPosUISetting
    {
        public dfPosUISetting()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCurrAccCode { get; set; }

    

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PosUICode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PosActionGroup { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PosActionType { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ColorHex { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool Alt { get; set; }

        [Required]
        public bool Shift { get; set; }

        [Required]
        public bool Control { get; set; }

        [Required]
        public int KeyCode { get; set; }

        [Required]
        public byte ReportViewType { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string QueryID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ReportFileName { get; set; }

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
        public virtual dfStoreDefault dfStoreDefault { get; set; }
        public virtual dfPosUI dfPosUI { get; set; }

    }
}
