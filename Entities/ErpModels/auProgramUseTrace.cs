using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auProgramUseTrace")]
    public partial class auProgramUseTrace
    {
        public auProgramUseTrace()
        {
        }

        [Key]
        [Required]
        public Guid ProgramUseTraceID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string UserGroupCode { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string UserName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ApplicationName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ParentProgramName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ProgramName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ParentFormName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string FormName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string MenuName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string SubMenuName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ParentFormCaption { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string FormCaption { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool IsDialog { get; set; }

    }
}
