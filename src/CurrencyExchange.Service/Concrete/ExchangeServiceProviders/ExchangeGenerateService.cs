using CurrencyExchange.Domain.Dto;
using CurrencyExchange.Domain.Exceptions;
using CurrencyExchange.Domain.Parameters;
using CurrencyExchange.Service.Abstract;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Service.Concrete.ExchangeServiceProviders
{
    public class ExchangeGenerateService : IExchangeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDistributedCache _cache;
        private readonly IOptions<CurrencyExchangeParameters> _parameters;
        public ExchangeGenerateService(IHttpClientFactory httpClientFactory, IDistributedCache cache, IOptions<CurrencyExchangeParameters> parameters)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
            _parameters = parameters;
        }
        public async Task<decimal> GetExchangeRateAsync(string currencyFrom, string currencyTo)
        {
            try
            {
                string cacheKey = $"ExchangeRate_{currencyFrom}_{currencyTo}";

                string cachedRate = await _cache.GetStringAsync(cacheKey);
                if (cachedRate != null && decimal.TryParse(cachedRate, out decimal exchangeRate))
                {
                    return exchangeRate;
                }

                string apiUrl = String.Format(_parameters.Value.ExchangeServiceEndpoint, _parameters.Value.ApiKey, currencyFrom, currencyTo);
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = System.Text.Json.JsonSerializer.Deserialize<ExchangeGenerateServiceDto>(json);

                    if (Decimal.TryParse(result.ConversionRate.ToString(), out decimal rate))
                    {
                        var cacheOptions = new DistributedCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_parameters.Value.CurrencyDurationMinute)
                        };
                        await _cache.SetStringAsync(cacheKey, rate.ToString(), cacheOptions);

                        return rate;
                    }
                }

                return -1;
            }
            catch (Exception ex)
            {
                throw new OperationException($"Exchange service throws error - {ex.Message}");
            }
        }
    }
}
