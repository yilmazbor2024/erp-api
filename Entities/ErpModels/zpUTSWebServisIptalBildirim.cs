using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpUTSWebServisIptalBildirim")]
    public partial class zpUTSWebServisIptalBildirim
    {
        public zpUTSWebServisIptalBildirim()
        {
        }

        [Key]
        [Required]
        public Guid BildirimID { get; set; }

        [Required]
        public Guid IptalEdilenBildirimID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BildirimTuru { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        [Required]
        public Guid ApplicationID { get; set; }

        [Required]
        public Guid ApplicationLineID { get; set; }

        [Required]
        public Guid StockID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MesajTipi { get; set; }

        public string MesajMetni { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string MesajKodu { get; set; }

        public string MesajParametreleri { get; set; }

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

    }
}
