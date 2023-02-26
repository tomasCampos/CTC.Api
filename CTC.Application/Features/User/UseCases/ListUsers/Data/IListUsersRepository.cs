using CTC.Application.Shared.Data.Pagination;
using CTC.Application.Shared.Request;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.ListUsers.Data
{
    internal interface IListUsersRepository
    {
        Task<PaginatedQueryResult<UserModel>> ListUsers(QueryRequest queryParams);
    }
}
