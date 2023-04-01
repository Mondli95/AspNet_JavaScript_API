using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connections { get; private set; }

        public static void InitializeConnection()
        {
            // TODO: set up the SQL connector
            SqlConnector connector = new SqlConnector();
            Connections.Add(connector);
        }

        public static string ConnectionString(string connectionString)
        {
            ConfigurationManager.Connec
        }
    }
}
