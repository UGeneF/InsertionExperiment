using System;
using Npgsql;

namespace PostgresCopy
{
    public interface IDelegateFactory
    {
        Action<NpgsqlBinaryImporter, T> GetDelegate<T>();
    }
}