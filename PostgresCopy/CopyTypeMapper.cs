using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace PostgresCopy
{
    public static class CopyTypeMapper
    {
        private static Dictionary<Type, TypeMapping> _mappings
            = new Dictionary<Type, TypeMapping>();

        public static TypeMapping MapType(Type type)
        {
            if (_mappings.ContainsKey(type))
                throw new Exception($"Postgres type mapping for {type} is configured already");
            _mappings[type] = new TypeMapping();
            return _mappings[type];
        }

        public static NpgsqlDbType? GetDbType(Type type, string propertyName)
        {
            _mappings.TryGetValue(type, out var typeMapping);
            return typeMapping?.GetDbType(propertyName);
        }
    }
}