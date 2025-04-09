using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsAirportIATA")]
    public partial class bsAirportIATA
    {
        public bsAirportIATA()
        {
            bsAirportIATADescs = new HashSet<bsAirportIATADesc>();
            tpInvoicePassportAndBoardingInfos = new HashSet<tpInvoicePassportAndBoardingInfo>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AirportIATACode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsAirportIATADesc> bsAirportIATADescs { get; set; }
        public virtual ICollection<tpInvoicePassportAndBoardingInfo> tpInvoicePassportAndBoardingInfos { get; set; }
    }
}
