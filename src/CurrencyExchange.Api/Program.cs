using CurrencyExchange.Domain.Parameters;
using CurrencyExchange.Repository.Abstract;
using CurrencyExchange.Repository.Concrete;
using CurrencyExchange.Service.Abstract;
using CurrencyExchange.Service.Concrete.ExchangeServiceProviders;
using Customer.Repository.Context;
using Customer.Service.Abstract;
using Customer.Service.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

builder.Services.AddDbContext<CurrencyExchangeEFContext>(options => options.UseInMemoryDatabase("TradeDatabase"));
builder.Services.AddScoped<ITradeService, TradeService>();
builder.Services.AddScoped<IExchangeService, ExchangeGenerateService>();
builder.Services.AddHttpClient();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


//INFO - This is using for assessment. I didn't create user secret file.
builder.Services.Configure<CurrencyExchangeParameters>(options =>
{
    options.ApiKey = "1a51d69ad19deefd76ddbc1f";
    options.CurrencyDurationMinute = 30;
    options.ExchangeServiceEndpoint = "https://v6.exchangerate-api.com/v6/{0}/pair/{1}/{2}";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
