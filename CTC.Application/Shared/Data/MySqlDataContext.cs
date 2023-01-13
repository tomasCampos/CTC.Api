using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace CTC.Application.Shared.Data
{
    internal sealed class MySqlDataContext : IDataContext
    {
        private readonly string _connectionString;

        public MySqlDataContext(IConfiguration configuration)
        {
            _connectionString = configuration["MySqlConnectionString"]!;
        }

        public IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
