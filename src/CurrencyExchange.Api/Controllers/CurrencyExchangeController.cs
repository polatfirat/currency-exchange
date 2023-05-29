using CurrencyExchange.Domain.Dto;
using Customer.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly ILogger<CurrencyExchangeController> _logger;
        private readonly ITradeService _tradeService;

        public CurrencyExchangeController(ILogger<CurrencyExchangeController> logger, ITradeService tradeService)
        {
            _logger = logger;
            _tradeService = tradeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTradesAsync()
        {
            var entities = await _tradeService.GetAllTradesAsync();
            return entities.Any() ? Ok(entities) : NoContent();
        }

        [HttpGet("{tradeId}")]
        public async Task<IActionResult> GetAllTradesAsync(int tradeId)
        {
            var entity = await _tradeService.GetTradeByIdAsync(tradeId);
            return entity != null ? Ok(entity) : NoContent();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> SearchTradesAsync([FromQuery] SearchTradeDto searchFilter)
        {
            var entities = await _tradeService.SearchTradeAsync(searchFilter);
            return entities.Any() ? Ok(entities) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> ExchangeAmountAsync(CreateTradeDto createTradeDto)
        {
            var entity = await _tradeService.CreateTradeAsync(createTradeDto);
            return Ok(entity);
        }
    }
}