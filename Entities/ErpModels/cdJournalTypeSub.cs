using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdJournalTypeSub")]
    public partial class cdJournalTypeSub
    {
        public cdJournalTypeSub()
        {
            auJournalPermits = new HashSet<auJournalPermit>();
            cdJournalTypeSubDescs = new HashSet<cdJournalTypeSubDesc>();
            dfJournalDefATAttributes = new HashSet<dfJournalDefATAttribute>();
            dfJournalOfficialForms = new HashSet<dfJournalOfficialForm>();
            prGLAccAvailableJournalTypeSubs = new HashSet<prGLAccAvailableJournalTypeSub>();
            trJournalHeaders = new HashSet<trJournalHeader>();
        }

        [Key]
        [Required]
        public short JournalTypeSubCode { get; set; }

        [Required]
        public byte JournalTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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
        public virtual bsJournalType bsJournalType { get; set; }

        public virtual ICollection<auJournalPermit> auJournalPermits { get; set; }
        public virtual ICollection<cdJournalTypeSubDesc> cdJournalTypeSubDescs { get; set; }
        public virtual ICollection<dfJournalDefATAttribute> dfJournalDefATAttributes { get; set; }
        public virtual ICollection<dfJournalOfficialForm> dfJournalOfficialForms { get; set; }
        public virtual ICollection<prGLAccAvailableJournalTypeSub> prGLAccAvailableJournalTypeSubs { get; set; }
        public virtual ICollection<trJournalHeader> trJournalHeaders { get; set; }
    }
}
