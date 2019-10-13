using System.Data;
using System.Threading.Tasks;
using Billing.Database.Utils;
using Billing.Models;
using Npgsql;
using Npgsql.TypeMapping;

namespace Billing.Database.CompositeTypes
{
    public class CompositeTypesInsert : IInsert
    {


        public async Task InsertAsync(Call[] calls)
        {
            using var conn = new NpgsqlConnection(DbCredentials.ConnectionString);
            using var cmd=new NpgsqlCommand("call_ct_insert",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new NpgsqlParameter<Call[]>
            {
                ParameterName = "call_ct_array",
                TypedValue = calls
            });
            await conn.OpenAsync().ConfigureAwait(false);
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
        }
    }
}