using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.ListUsers
{
    internal sealed class ListUsersUseCase : IUseCase<QueryInput, Output>
    {
        public Task<Output> Execute(QueryInput input)
        {
            //TODO: Chamar repositório para buscar usuarios paginados
            throw new System.NotImplementedException();
        }
    }
}