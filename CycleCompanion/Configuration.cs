using System;
using MySqlConnector;

namespace CycleCompanion
{
    public class Configuration
    {
        public static string server = "185.182.56.245";
        public static string database = "nisatsv379_DeWielrenners";
        public static string uid = "nisatsv379_DeWielrenners";
        public static string password = "W7CRx5zGZ";

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
