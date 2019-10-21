using System.Configuration;
using Billing.Database.Utils;
using Billing.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace Billing.Database.EntityFramework
{
    internal class BillingContext : DbContext
    {
        public DbSet<Call> Calls { get; set; }
        
        static ILoggerFactory _loggerFactory=LoggerFactory.Create(builder => {
                builder
                    .AddFilter(level => true)
                    .AddConsole();
            }
        );
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder/*.UseLoggerFactory(_loggerFactory)*/
                .UseNpgsql(DbCredentials.ConnectionString)
                .UseSnakeCaseNamingConvention();
                
        }

     

    }
}