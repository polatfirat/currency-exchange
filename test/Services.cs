using Castle.Core.Resource;
using CurrencyExchange.Domain.Dto;
using CurrencyExchange.Domain.Entities;
using CurrencyExchange.Domain.Exceptions;
using CurrencyExchange.Repository.Abstract;
using CurrencyExchange.Service.Abstract;
using Customer.Service.Concrete;
using Moq;

namespace CurrencyExchange.Test
{
    public class Services
    {
        private readonly Mock<IRepository<Trade>> tradeRepositoryMock = new Mock<IRepository<Trade>>();
        private readonly Mock<IExchangeService> exchangeServiceMock = new Mock<IExchangeService>();


        [Fact]
        public void CurrencyExchange_GetTradeByIdAsync_ShouldThrowError_WhenTraceIdIsZero()
        {
            var tradeServie = new TradeService(tradeRepositoryMock.Object, exchangeServiceMock.Object);
            Assert.ThrowsAsync<ValidationException>(() => tradeServie.GetTradeByIdAsync(0));
        }


        [Fact]
        public void CurrencyExchange_CreateTradeAsync_ShouldThrowError_WhenRateIsZeroOrMinusZero()
        {
            var createTradeDto = new CreateTradeDto
            {
                CurrencyFrom = "USD",
                CurrencyTo = "TRY"
            };

            exchangeServiceMock.Setup(r => r.GetExchangeRateAsync(createTradeDto.CurrencyFrom, createTradeDto.CurrencyTo)).ReturnsAsync(-1);
            var tradeServie = new TradeService(tradeRepositoryMock.Object, exchangeServiceMock.Object);

            Assert.ThrowsAsync<OperationException>(() => tradeServie.CreateTradeAsync(createTradeDto));

        }
    }
}