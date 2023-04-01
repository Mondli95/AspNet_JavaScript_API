using System.Configuration;

namespace DataApi
{
    public static class GlobalConfig
    {
        public static IDataConnection Connections { get; private set; }

        public static void InitializeConnection()
        {
            // TODO: set up the SQL connector
            SqlConnector connector = new SqlConnector();
            Connections = connector;
        }

        public static string GetConnectionString(string connectionString)
        {
            return ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
        }
    }
}
