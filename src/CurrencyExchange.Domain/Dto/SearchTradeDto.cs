using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Domain.Dto
{
    public class SearchTradeDto
    {
        public int ClientId { get; set; }
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public decimal AmountFrom { get; set; }
        public decimal AmountTo { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime TradeDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
