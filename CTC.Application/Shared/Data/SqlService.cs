using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Shared.Data
{
    internal sealed class SqlService : ISqlService
    {
        private readonly IDataContext _context;

        public SqlService(IDataContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync(string sql, object? param = default)
        {
            using var connection = _context.GetConnection();
            return await connection.QueryFirstAsync<int>(sql, param);
        }

        public async Task<int> ExecuteAsync(string sql, object? param = default)
        {
            using var connection = _context.GetConnection();
            return await connection.ExecuteAsync(sql, param);
        }

        public async Task<IEnumerable<T>> SelectAsync<T>(string sql, object? param = default)
        {
            using var connection = _context.GetConnection();
            return await connection.QueryAsync<T>(sql, param);
        }
    }
}
