using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trMessageBox")]
    public partial class trMessageBox
    {
        public trMessageBox()
        {
        }

        [Key]
        [Required]
        public Guid MessageID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string FromUserGroupCode { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string FromUserName { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ToUserGroupCode { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string ToUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public TimeSpan CreatedTime { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Subject { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [Required]
        public DateTime DeadLine { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MessageTypeCode { get; set; }

        [Required]
        public byte MessageImportanceCode { get; set; }

        public string MessageText { get; set; }

        [Required]
        public bool IsRead { get; set; }

        [Required]
        public DateTime ReadDate { get; set; }

        [Required]
        public TimeSpan ReadTime { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        // Navigation Properties
        public virtual bsMessageImportance bsMessageImportance { get; set; }
        public virtual cdMessageType cdMessageType { get; set; }

    }
}
