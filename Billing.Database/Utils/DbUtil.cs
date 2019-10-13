using System.Threading.Tasks;
using Npgsql;

namespace Billing.Database.Utils
{
    public class DbUtil
    {
        public async Task TruncateCallsAsync()
        {
            using var conn = new NpgsqlConnection(DbCredentials.ConnectionString);
            using var cmd=new NpgsqlCommand("truncate calls",conn);
            
            await conn.OpenAsync().ConfigureAwait(false);
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}