using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsJournalType")]
    public partial class bsJournalType
    {
        public bsJournalType()
        {
            bsJournalTypeDescs = new HashSet<bsJournalTypeDesc>();
            cdJournalTypeSubs = new HashSet<cdJournalTypeSub>();
            trJournalHeaders = new HashSet<trJournalHeader>();
        }

        [Key]
        [Required]
        public byte JournalTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsJournalTypeDesc> bsJournalTypeDescs { get; set; }
        public virtual ICollection<cdJournalTypeSub> cdJournalTypeSubs { get; set; }
        public virtual ICollection<trJournalHeader> trJournalHeaders { get; set; }
    }
}
