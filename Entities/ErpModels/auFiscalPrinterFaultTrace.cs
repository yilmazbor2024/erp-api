using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auFiscalPrinterFaultTrace")]
    public partial class auFiscalPrinterFaultTrace
    {
        public auFiscalPrinterFaultTrace()
        {
        }

        [Key]
        [Required]
        public Guid FaultTraceID { get; set; }

        public string JsonData { get; set; }

        public string ExceptionDetail { get; set; }

        public string UpdatedJson { get; set; }

    }
}
