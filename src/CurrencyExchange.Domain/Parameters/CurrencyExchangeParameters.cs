using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Domain.Parameters
{
    public class CurrencyExchangeParameters
    {
        public string ExchangeServiceEndpoint { get; set; }
        public string ApiKey { get; set; }
        public int CurrencyDurationMinute { get; set; }
    }
}
