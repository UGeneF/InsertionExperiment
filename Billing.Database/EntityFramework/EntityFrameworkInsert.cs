using System.Threading.Tasks;
using Billing.Models;

namespace Billing.Database.EntityFramework
{
    public class EntityFrameworkInsert:IInsert
    {
        public async Task InsertAsync(Call[] calls)
        {
            using var context = new BillingContext();
            context.Calls.AddRange(calls);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}