using System;
using Npgsql;

namespace PostgresCopy
{
    public interface IWriteDelegateFactory
    {
        Action<NpgsqlBinaryImporter, T> GetDelegate<T>();
    }
}