using CTC.Application.Features.Revenue.UseCases.DeleteRevenue.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.DeleteRevenue.UseCase
{
    internal sealed class DeleteRevenueUseCase : IUseCase<DeleteRevenueInput, Output>
    {
        private readonly IDeleteRevenueRepository _repository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;

        public DeleteRevenueUseCase(IDeleteRevenueRepository repository, IUseCaseAuthorizationService useCaseAuthorizationService)
        {
            _repository = repository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
        }

        public async Task<Output> Execute(DeleteRevenueInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(DeleteRevenueUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var transactionId = await _repository.GetTransactionIdByRevenueId(input.RevenueId!);
            if (string.IsNullOrEmpty(transactionId))
                return Output.CreateInvalidParametersResult("A receita a ser alterada não existe.");

            var result = await _repository.DeleteRevenue(input.RevenueId!, transactionId);
            if (!result)
                return Output.CreateInternalErrorResult("Ocorreu um erro e não foi possível excluir a receita. Tente novamente mais tarde.");

            return Output.CreateOkResult();
        }
    }
}
