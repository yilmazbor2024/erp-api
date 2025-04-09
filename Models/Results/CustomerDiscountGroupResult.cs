using Microsoft.EntityFrameworkCore;

namespace Api.Models.Results
{
    [Keyless]
    public class CustomerDiscountGroupResult
    {
        public string DiscountGroupCode { get; set; }
        public string DiscountGroupDesc { get; set; }
    }
} 