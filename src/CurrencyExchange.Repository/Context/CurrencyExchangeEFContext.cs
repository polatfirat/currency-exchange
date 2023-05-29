using CurrencyExchange.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Repository.Context
{
    public class CurrencyExchangeEFContext : DbContext
    {
        public CurrencyExchangeEFContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Trade> Trades { get; set; }
    }
}
