using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;

namespace PostgresCopy
{
    public class PostgresCopy
    {
        private readonly ISqlCommandFactory _sqlCommandFactory;
        private readonly IWriteDelegateFactory _writeDelegateFactory;

        public PostgresCopy()
        {
            _sqlCommandFactory = new SqlCommandFactory();
            _writeDelegateFactory = new WriteDelegateFactory();
        }

        public async Task InsertAsync<T>(IEnumerable<T> entities, string connectionString)
        {
            using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync().ConfigureAwait(false);
            using var writer = conn.BeginBinaryImport(_sqlCommandFactory.GetSql<T>());

            var write = _writeDelegateFactory.GetDelegate<T>();
            foreach (var entity in entities)
                write(writer, entity);

            await writer.CompleteAsync().ConfigureAwait(false);
        }
    }
}