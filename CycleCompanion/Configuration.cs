using System;
using MySqlConnector;

namespace CycleCompanion
{
    public class Configuration
    {
        public static string server = "34.70.112.36";
        public static string database = "wielrenners";
        public static string uid = "db-user";
        public static string password = "SABu9eb";

        public Configuration()
        {
        }

        public static string getConnectionString()
        {
            var stringBuilder = new MySqlConnectionStringBuilder
            {
                Server = server,
                UserID = uid,
                Password=password,
                Database=database,
            };


            return stringBuilder.ConnectionString;
        }
    }
}
