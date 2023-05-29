using CurrencyExchange.Domain.Dto;
using CurrencyExchange.Domain.Entities;
using CurrencyExchange.Domain.Exceptions;
using CurrencyExchange.Repository.Abstract;
using CurrencyExchange.Service.Abstract;
using Customer.Service.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Customer.Service.Concrete
{
    public class TradeService : ITradeService
    {
        private readonly IRepository<Trade> _repository;
        private readonly IExchangeService _exchangeService;
        public TradeService(IRepository<Trade> repository, IExchangeService exchangeService)
        {
            _repository = repository;
            _exchangeService = exchangeService;
        }

        public async Task<Trade> CreateTradeAsync(CreateTradeDto createTradeDto)
        {
            var entity = new Trade();

            entity.CurrencyFrom = createTradeDto.CurrencyFrom;
            entity.CurrencyTo = createTradeDto.CurrencyTo;
            entity.TradeDate = DateTime.Now;
            entity.ExchangeRate = await _exchangeService.GetExchangeRateAsync(createTradeDto.CurrencyFrom, createTradeDto.CurrencyTo);

            if (entity.ExchangeRate == -1)
            {
                throw new OperationException("Exhange rate cannot be minus zero");
            }

            entity.AmountFrom = createTradeDto.AmountFrom;
            entity.AmountTo = createTradeDto.AmountFrom * entity.ExchangeRate;
            entity.ClientId = createTradeDto.ClientId;

            await _repository.InsertAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<Trade>> GetAllTradesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Trade> GetTradeByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("Trade id must be grater than 0");
            }
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Trade> InsertTradeAsync(Trade entity)
        {
            return await _repository.InsertAsync(entity);
        }

        public async Task<IEnumerable<Trade>> SearchTradeAsync(SearchTradeDto tradeFilter)
        {
            var entity = _repository.GetAllQueryable();
            if (tradeFilter.ClientId > 0)
            {
                entity = entity.Where(r => r.ClientId == tradeFilter.ClientId);
            }
            if (!String.IsNullOrEmpty(tradeFilter.CurrencyFrom))
            {
                entity = entity.Where(r => r.CurrencyFrom.Equals(tradeFilter.CurrencyFrom.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            if (!String.IsNullOrEmpty(tradeFilter.CurrencyFrom))
            {
                entity = entity.Where(r => r.CurrencyTo.Equals(tradeFilter.CurrencyTo.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            if (tradeFilter.AmountFrom > 0)
            {
                entity = entity.Where(r => r.AmountFrom == tradeFilter.AmountFrom);
            }
            if (tradeFilter.AmountTo > 0)
            {
                entity = entity.Where(r => r.AmountTo == tradeFilter.AmountTo);
            }
            if (tradeFilter.ExchangeRate > 0)
            {
                entity = entity.Where(r => r.ExchangeRate == tradeFilter.ExchangeRate);
            }
            if (tradeFilter.TradeDate > DateTime.MinValue)
            {
                entity = entity.Where(r => r.TradeDate == tradeFilter.TradeDate);
            }
            if (tradeFilter.CreatedAt > DateTime.MinValue)
            {
                entity = entity.Where(r => r.CreatedAt == tradeFilter.CreatedAt);
            }

            return await entity.ToListAsync();
        }
    }
}