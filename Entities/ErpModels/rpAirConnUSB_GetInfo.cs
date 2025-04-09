using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpAirConnUSB_GetInfo")]
    public partial class rpAirConnUSB_GetInfo
    {
        public rpAirConnUSB_GetInfo()
        {
        }

        [Key]
        [Required]
        public Guid TransactionID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string cashbox_factory_number { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string cashbox_tax_number { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string cashregister_factory_number { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string cashregister_model { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string company_name { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string company_tax_number { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string firmware_version { get; set; }

        [Required]
        public int last_doc_number { get; set; }

        [Required]
        public DateTime last_online_time { get; set; }

        [Required]
        public DateTime not_after { get; set; }

        [Required]
        public DateTime not_before { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object object_address { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string object_name { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string object_tax_number { get; set; }

        [Required]
        public DateTime oldest_document_time { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object qr_code_url { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string state { get; set; }

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
