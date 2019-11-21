using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PostgresCopy
{
    public class SqlFactory : ISqlFactory
    {
        private readonly Regex _wordInCamelCase = new Regex(@"([A-Z][a-z]*)", RegexOptions.Compiled);

        private readonly ConcurrentDictionary<Type, string> _sql =
            new ConcurrentDictionary<Type, string>();

        public string GetSql<T>()
        {
            var type = typeof(T);
            return _sql.GetOrAdd(type, BuildCommand);
        }

        private string BuildCommand(Type type)
        {
            var tableName = type.GetCustomAttribute<TableAttribute>().Name;
            var colNames = GetColNames(type);
            return $"COPY {tableName} ({string.Join(',', colNames)}) FROM STDIN (FORMAT BINARY)";
        }

        private string[] GetColNames(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(p => p.Name)
                .Select(ConvertToSnakeCase)
                .ToArray();
        }

        private string ConvertToSnakeCase(string camelCase)
        {
            var words = _wordInCamelCase.Matches(camelCase);
            return string.Join('_', words).ToLower();
        }
    }
}