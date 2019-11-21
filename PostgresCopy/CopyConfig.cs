using System;

namespace PostgresCopy
{
    public static class CopyConfig
    {
        private static string _connectionString;

        public static string ConnectionString
        {
            get => _connectionString;
            set
            {
                if (_connectionString == null)
                    _connectionString = value;
                else
                    throw new Exception("Connection string is setted already");
            }
        }
    }
}