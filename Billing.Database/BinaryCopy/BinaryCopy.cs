using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Billing.Database.Utils;
using Billing.Models;
using Npgsql;
using NpgsqlTypes;

namespace Billing.Database.BinaryCopy
{
    [SuppressMessage("ReSharper", "UseAwaitUsing")]
    [SuppressMessage("ReSharper", "ConvertToUsingDeclaration")]
    public class BinaryInsert : IInsert
    {
        public async Task InsertAsync(Call[] calls)
        {
            using (var conn = new NpgsqlConnection(DbCredentials.ConnectionString))
            {
                await conn.OpenAsync().ConfigureAwait(false);
                using (var writer = conn.BeginBinaryImport(
                    "COPY calls " +
                    "(start_time,end_time,calling_number," +
                    "called_number,duration,call_type,call_id) " +
                    "FROM STDIN (FORMAT BINARY)"))
                {
                    
                    
                    foreach (var call in calls)
                    {
                        writer.StartRow();
                        writer.Write(call.StartTime, NpgsqlDbType.Timestamp);
                        writer.Write(call.EndTime, NpgsqlDbType.Timestamp);
                        writer.Write(call.CallingNumber, NpgsqlDbType.Varchar);
                        writer.Write(call.CalledNumber, NpgsqlDbType.Varchar);
                        writer.Write(call.Duration, NpgsqlDbType.Integer);
                        writer.Write(call.CallType);
                        writer.Write(call.CallId, NpgsqlDbType.Varchar);
                    }

                    await writer.CompleteAsync().ConfigureAwait(false);
                }
            }
        }
    }
}