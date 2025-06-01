using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models.Entities
{
    [Table("ExchangeRates")]
    public class ExchangeRate
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        [StringLength(10)]
        public string CurrencyCode { get; set; }
        
        [Required]
        [StringLength(10)]
        public string RelationCurrencyCode { get; set; } = "TRY";
        
        [Required]
        public decimal Rate { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Source { get; set; } // "FreeMarket" veya "CentralBank"
        
        [Required]
        [StringLength(20)]
        public string Type { get; set; } // "Buying", "Selling", "CashBuying", "CashSelling", "BanknoteBuying", "BanknoteSelling", "BankForex"
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
