using CurrencyExchange.Domain.Dto;
using CurrencyExchange.Domain.Entities;

namespace Customer.Service.Abstract
{
    public interface ITradeService
    {
        Task<Trade> InsertTradeAsync(Trade entity);
        Task<Trade> GetTradeByIdAsync(int id);
        Task<IEnumerable<Trade>> GetAllTradesAsync();
        Task<IEnumerable<Trade>> SearchTradeAsync(SearchTradeDto tradeFilter);
        Task<Trade> CreateTradeAsync(CreateTradeDto createTradeDto);
    }
}
