using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CurrencyExchange.Domain.Dto
{
    public class ExchangeGenerateServiceDto
    {
        [JsonPropertyName("base_code")]
        public string BaseCode { get; set; }
        
        [JsonPropertyName("target_code")]
        public string TargetCode { get; set; }
        
        [JsonPropertyName("conversion_rate")]
        public decimal ConversionRate { get; set; }
    }
}
