using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;

namespace PostgresCopy
{
    public class Copy
    {
        private readonly ISqlFactory _sqlFactory;
        private readonly IDelegateFactory _delegateFactory;

        public Copy()
        {
            _sqlFactory = new SqlFactory();
            _delegateFactory = new DelegateFactory();
        }

        public async Task InsertAsync<T>(IEnumerable<T> entities, string connectionString = null)
        {
            using var conn = new NpgsqlConnection(connectionString ?? CopyConfig.ConnectionString);
            await conn.OpenAsync().ConfigureAwait(false);
            using var writer = conn.BeginBinaryImport(_sqlFactory.GetSql<T>());

            var write = _delegateFactory.GetDelegate<T>();
            foreach (var entity in entities)
                write(writer, entity);

            await writer.CompleteAsync().ConfigureAwait(false);
        }
    }
}