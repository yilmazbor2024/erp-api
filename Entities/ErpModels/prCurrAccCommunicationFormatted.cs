using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccCommunicationFormatted")]
    public partial class prCurrAccCommunicationFormatted
    {
        public prCurrAccCommunicationFormatted()
        {
        }

        [Key]
        [Required]
        public Guid CommunicationID { get; set; }

        [Required]
        public byte FormatTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FormattedCommAddress { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FormattedMobildevCommAddress { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FormattedTuratelCommAddress { get; set; }

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
        public virtual prCurrAccCommunication prCurrAccCommunication { get; set; }

    }
}
