using CTC.Application.Shared.Data;
using CTC.Application.Shared.Request;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.ListUsers.Data
{
    internal class ListUsersRepository : IListUsersRepository
    {
        private readonly ISqlService _sqlService;

        public ListUsersRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<PaginatedQueryResult<UserModel>> ListUsers(QueryRequest queryParams)
        {
            var result = await _sqlService.SelectPaginated<UserModel>(queryParams, ListUsersSqlScripts.ListUsersSelectStatement, ListUsersSqlScripts.ListUsersFromAndJoinsStatements,
                                    ListUsersSqlScripts.ListUsersWhereStatement, new { search_param = queryParams.SearchParam});

            return result;
        }
    }
}
