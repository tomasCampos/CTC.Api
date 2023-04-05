using CTC.Application.Features.CostCenter.UseCases.UpdateCostCenter.UseCase;
using CTC.Application.Shared.Request.Validator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.UpdateCostCenter.Validators
{
    internal sealed class UpdateCostCenterRequestValidator : IRequestValidator<UpdateCostCenterInput>
    {
        public Task<RequestValidationModel> Validate(UpdateCostCenterInput request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Id))
                errors.Add("O ID do centro de custo deve ser informado.");
            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("O nome do centro de custo deve ser informado.");
            if (!request.StartingDate.HasValue)
                errors.Add("A data de criação do centro de custo deve ser informada.");
            if (string.IsNullOrWhiteSpace(request.ClientId))
                errors.Add("O cliente relacionado ao centro de custo deve ser informado.");

            var result = new RequestValidationModel(errors);
            return Task.FromResult(result);
        }
    }
}
