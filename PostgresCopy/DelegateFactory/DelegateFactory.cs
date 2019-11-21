using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using GrEmit;
using Npgsql;
using NpgsqlTypes;

namespace PostgresCopy
{
    public class DelegateFactory : IDelegateFactory
    {
        private readonly ConcurrentDictionary<Type, object> _delegates =
            new ConcurrentDictionary<Type, object>();

        public Action<NpgsqlBinaryImporter, T> GetDelegate<T>()
        {
            var type = typeof(T);
            return (Action<NpgsqlBinaryImporter, T>) _delegates.GetOrAdd(type, BuildDelegate<T>);
        }

        private object BuildDelegate<T>(Type type)
        {
            var dynamicMethod = new DynamicMethod($"WriteProperties{type}",
                typeof(void),
                new[] {typeof(NpgsqlBinaryImporter), typeof(T)});
            var props = ExtractInfo<T>();
            using (var il = new GroboIL(dynamicMethod))
            {
                il.Ldarg(0);
                il.Callnonvirt(typeof(NpgsqlBinaryImporter).GetMethod("StartRow",
                    BindingFlags.Instance | BindingFlags.Public));
                foreach (var prop in props)
                {
                    il.Ldarg(0);
                    il.Ldarg(1);
                    il.Callnonvirt(prop.Getter);
                    if (prop.PostgresType != null)
                        il.Ldc_I4((int) prop.PostgresType.Value);
                    il.Callnonvirt(prop.WriteMethod);
                }

                il.Ret();
            }

            return dynamicMethod.CreateDelegate(typeof(Action<NpgsqlBinaryImporter, T>));
        }


        private PropInfo[] ExtractInfo<T>()
        {
            var type = typeof(T);
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return props.Select(p =>
                {
                    var dbType = GetDbType(p);
                    return new PropInfo
                    {
                        Getter = p.GetMethod,
                        PostgresType = dbType,
                        WriteMethod = GetMatchedGenericMethod(p.PropertyType, dbType)
                    };
                })
                .ToArray();
        }

        private NpgsqlDbType? GetDbType(PropertyInfo propertyInfo)
        {
            return CopyTypeMapper.GetDbType(propertyInfo.DeclaringType, propertyInfo.Name);
        }

        private MethodInfo GetMatchedGenericMethod(Type paramType, NpgsqlDbType? dbType)
        {
            var paramTypes = dbType == null ? new[] {paramType} : new[] {paramType, typeof(NpgsqlDbType)};
            if (paramType.IsClass)
            {
                return typeof(DelegateFactory)
                    .GetMethods(BindingFlags.Static|BindingFlags.NonPublic)
                    .First(m => m.Name == nameof(WriteRefType) && m.GetParameters().Length == paramTypes.Length+1)
                    .MakeGenericMethod(paramTypes);
            }

            if (Nullable.GetUnderlyingType(paramType) != null)
            {
                return typeof(DelegateFactory)
                    .GetMethods(BindingFlags.Static|BindingFlags.NonPublic)
                    .First(m => m.Name == nameof(WriteNullableStruct) && m.GetParameters().Length == paramTypes.Length+1)
                    .MakeGenericMethod(paramTypes);
            }

            return typeof(NpgsqlBinaryImporter)
                .GetMethods()
                .Where(m => m.Name == "Write")
                .Select(m => new
                {
                    Method = m,
                    Params = m.GetParameters(),
                    Args = m.GetGenericArguments()
                })
                .Where(x => x.Params.Length == paramTypes.Length
                            && x.Args.Length == 1
                            && x.Params[0].ParameterType == x.Args[0])
                .Select(x => x.Method.MakeGenericMethod(paramTypes[0]))
                .First();
        }

        private static void WriteRefType<T>(NpgsqlBinaryImporter writer, T value)
            where T : class
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.Write(value);
        }

        private static void WriteRefType<T>(NpgsqlBinaryImporter writer, T value, NpgsqlDbType dbType)
            where T : class
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.Write(value, dbType);
        }

        private static void WriteNullableStruct<T>(NpgsqlBinaryImporter writer, T? value)
            where T : struct
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.Write(value.Value);
        }

        private static void WriteNullableStruct<T>(NpgsqlBinaryImporter writer, T? value, NpgsqlDbType dbType)
            where T : struct
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.Write(value.Value, dbType);
        }

        private class PropInfo
        {
            public MethodInfo Getter { get; set; }
            public NpgsqlDbType? PostgresType { get; set; }
            public MethodInfo WriteMethod { get; set; }
        }
    }
}