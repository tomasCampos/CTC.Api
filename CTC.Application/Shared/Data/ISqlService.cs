using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Shared.Data
{
    internal interface ISqlService
    {
        Task<IEnumerable<T>> SelectAsync<T>(string sql, object @params);
        Task<int> CountAsync(string sql, object @params);
        Task<int> ExecuteAsync(string sql, object @params);
        Task<bool> ExecuteWithTransactionAsync(IDictionary<string, object?> commands);
    }
}
