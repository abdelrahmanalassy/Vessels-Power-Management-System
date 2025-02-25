using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace VesselPowerManagement.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService()
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            _connectionString = config.GetConnectionString("VesselDB");

            if(string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Error: The ConnectionString property has not been initialized.");
            }
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}