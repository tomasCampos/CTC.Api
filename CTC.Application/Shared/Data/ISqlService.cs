using CTC.Application.Shared.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Shared.Data
{
    internal interface ISqlService
    {
        Task<IEnumerable<T>> SelectAsync<T>(string sql, object @params);
        Task<T> SelectSingleAsync<T>(string sql, object? param = default);
        Task<int> CountAsync(string sql, object @params);
        Task<int> ExecuteAsync(string sql, object @params);
        Task<(bool Success, int RowsAffected)> ExecuteWithTransactionAsync(IDictionary<string, object?> commands);
        Task<PaginatedQueryResult<T>> SelectPaginated<T>(QueryRequest queryRequest, string selectStatement, string fromAndJoinsStatements, string whereStatement = "") where T : class;
    }
}
