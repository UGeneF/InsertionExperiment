using System.Configuration;
using Billing.Database.Utils;
using Billing.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Billing.Database.EntityFramework
{
    internal class BillingContext : DbContext
    {
        public DbSet<Call> Calls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(DbCredentials.ConnectionString)
                .UseSnakeCaseNamingConvention();
        }
    }
}