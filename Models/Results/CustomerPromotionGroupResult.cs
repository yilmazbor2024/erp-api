using Microsoft.EntityFrameworkCore;

namespace Api.Models.Results
{
    [Keyless]
    public class CustomerPromotionGroupResult
    {
        public string PromotionGroupCode { get; set; }
        public string PromotionGroupDesc { get; set; }
    }
} 