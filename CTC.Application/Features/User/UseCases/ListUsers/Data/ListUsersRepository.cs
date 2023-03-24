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
            var result = await _sqlService.SelectPaginated<UserModel>(queryParams, UserSqlScripts.LIST_USERS_SELECT_STATEMENT, UserSqlScripts.LIST_USERS_FROM_AND_JOIN_STATEMENT,
                                    UserSqlScripts.LIST_USERS_WHERE_STATEMENT);

            return result;
        }
    }
}
