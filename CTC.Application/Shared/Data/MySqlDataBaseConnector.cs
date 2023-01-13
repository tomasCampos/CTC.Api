using Dapper;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Shared.Data
{
    internal sealed class MySqlDataBaseConnector : IMySqlDataBaseConnector
    {
        private readonly MySqlConnection _connection;

        public MySqlDataBaseConnector()
        {
            _connection = new MySqlConnection("Server=containers-us-west-45.railway.app;Port=7325;Database=railway;Uid=root;Pwd=JlkstkpJh6F05b6EuMUN;charset=utf8;");
        }

        public async Task<int> CountAsync(string sql, object @params)
        {
            await _connection.OpenAsync();

            var result = await _connection.QueryFirstAsync<int>(sql, @params);

            await _connection.CloseAsync();

            return result;
        }

        public async Task<int> ExecuteAsync(string sql)
        {
            await _connection.OpenAsync();

            var affectedRowsCount = await _connection.ExecuteAsync(sql);

            await _connection.CloseAsync();

            return affectedRowsCount;
        }

        public async Task<int> ExecuteAsync(string sql, object @params)
        {
            await _connection.OpenAsync();

            var affectedRowsCount = await _connection.ExecuteAsync(sql, @params);

            await _connection.CloseAsync();

            return affectedRowsCount;
        }

        public async Task<IEnumerable<T>> SelectAsync<T>(string sql)
        {
            await _connection.OpenAsync();

            var result = await _connection.QueryAsync<T>(sql);

            await _connection.CloseAsync();

            return result;
        }

        public async Task<IEnumerable<T>> SelectAsync<T>(string sql, object @params)
        {
            await _connection.OpenAsync();

            var result = await _connection.QueryAsync<T>(sql, @params);

            await _connection.CloseAsync();

            return result;
        }
    }
}
