using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Service.Abstract
{
    public interface IExchangeService
    {
        Task<decimal> GetExchangeRateAsync(string currencyFrom, string currencyTo);
    }
}
