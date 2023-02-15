using MySqlConnector;
using System;
using System.Configuration;
using System.Data;

namespace CTC.Application.Shared.Data
{
    internal sealed class MySqlDataContext : IDataContext
    {
        private readonly string _connectionString;

        private const string MySqlConnectionStringEnvironmentVariableName = "MY_SQL_CONNECTION_STRING";

        public MySqlDataContext()
        {
            _connectionString = Environment.GetEnvironmentVariable(MySqlConnectionStringEnvironmentVariableName) 
                ?? throw new ConfigurationErrorsException($"Missing environment variable named: {MySqlConnectionStringEnvironmentVariableName}");
        }

        public IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
