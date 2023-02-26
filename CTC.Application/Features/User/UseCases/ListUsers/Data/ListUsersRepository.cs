using CTC.Application.Shared.Data;
using CTC.Application.Shared.Data.Pagination;
using CTC.Application.Shared.Request;
using System;
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

        public Task<PaginatedQueryResult<UserModel>> ListUsers(QueryRequest queryParams)
        {
            //TODO: Fazer consulta paginada no banco de dados
            var paginationFilters = PaginationSqlScript.GetLimitParameters(queryParams.PageNumber, queryParams.PageSize);

            throw new NotImplementedException();
        }
    }
}
