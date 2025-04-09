using Microsoft.EntityFrameworkCore;

namespace Api.Models.Results
{
    [Keyless]
    public class CustomerTypeResult
    {
        public string CurrAccTypeCode { get; set; }
        public string CurrAccTypeDesc { get; set; }
    }
} 