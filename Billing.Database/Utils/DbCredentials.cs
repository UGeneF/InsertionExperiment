using System.Configuration;

namespace Billing.Database.Utils
{
    class DbCredentials
    {
        public static string ConnectionString { get; }

        static DbCredentials()
        {
            ConnectionString = ConfigurationManager
                .ConnectionStrings["BillingContext"]
                .ConnectionString;
        }
    }
}