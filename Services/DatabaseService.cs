using System;
using System.Data.SqlClient;

namespace VesselPowerManagement.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection Getconnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}