using System.Threading.Tasks;
using Billing.Models;

namespace Billing.Database
{
    public interface IInsert
    {
        Task InsertAsync(Call[] calls);
    }
}