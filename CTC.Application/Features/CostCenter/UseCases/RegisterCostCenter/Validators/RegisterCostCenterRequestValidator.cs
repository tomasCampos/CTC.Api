using CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter.UseCase;
using CTC.Application.Shared.Request.Validator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter.Validators
{
    internal sealed class RegisterCostCenterRequestValidator : IRequestValidator<RegisterCostCenterInput>
    {
        public Task<RequestValidationModel> Validate(RegisterCostCenterInput request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("O nome do centro de custo deve ser informado.");
            if (!request.StartingDate.HasValue)
                errors.Add("A data de criação do centro de custo deve ser informada.");
            if (string.IsNullOrWhiteSpace(request.ClientId))
                errors.Add("O cliente relacionado ao centro de custo deve ser informado.");
            if (string.IsNullOrWhiteSpace(request.AddressStreetName))
                errors.Add("O nome da rua do endereço do centro de custo deve ser informado.");
            if (string.IsNullOrWhiteSpace(request.AddressNeighborhood))
                errors.Add("O nome do bairro do endereço do centro de custo deve ser informado.");
            if (string.IsNullOrWhiteSpace(request.AddressCity))
                errors.Add("O nome da cidade do endereço do centro de custo deve ser informado.");
            if (string.IsNullOrWhiteSpace(request.AddressState))
                errors.Add("O estado do endereço do centro de custo deve ser informado.");
            if (string.IsNullOrWhiteSpace(request.AddressNumber))
                errors.Add("O número do endereço do centro de custo deve ser informado.");

            var result = new RequestValidationModel(errors);
            return Task.FromResult(result);
        }
    }
}
