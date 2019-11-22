﻿using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace PostgresCopy
{
    public class TypeMapping
    {
        private readonly Dictionary<string, NpgsqlDbType> _propertyMappings =
            new Dictionary<string, NpgsqlDbType>();

        public TypeMapping MapProperty(string name, NpgsqlDbType dbType)
        {
            if (_propertyMappings.ContainsKey(name))
                throw new Exception($"Property {name} is mapped already");
            _propertyMappings[name] = dbType;
            return this;
        }

        internal NpgsqlDbType? GetDbType(string propertyName)
        {
            _propertyMappings.TryGetValue(propertyName, out var dbType);
            if (dbType == default)
                return null;
            return dbType;
        }
    }
}