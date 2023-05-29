using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Domain.Entities
{
    public class Trade
    {
        [Key]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public decimal AmountFrom { get; set; }
        public decimal AmountTo { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime TradeDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
