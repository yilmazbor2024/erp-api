using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trProposalLineSum")]
    public partial class trProposalLineSum
    {
        public trProposalLineSum()
        {
            trProposalLineSumDetails = new HashSet<trProposalLineSumDetail>();
        }

        [Key]
        [Required]
        public Guid ProposalHeaderID { get; set; }

        [Key]
        [Required]
        public int ProposalLineSumID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LotCode { get; set; }

        [Required]
        public int LotQty { get; set; }

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
        public virtual trProposalHeader trProposalHeader { get; set; }
        public virtual cdLot cdLot { get; set; }

        public virtual ICollection<trProposalLineSumDetail> trProposalLineSumDetails { get; set; }
    }
}
