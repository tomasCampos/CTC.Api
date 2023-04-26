using CTC.Application.Features.Revenue.UseCases.RegisterRevenue.UseCase;
using CTC.Application.Shared.Request.Validator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.RegisterRevenue.Validators
{
    internal sealed class RegisterRevenueRequestValidator : IRequestValidator<RegisterRevenueInput>
    {
        public Task<RequestValidationModel> Validate(RegisterRevenueInput request)
        {
            var errors = new List<string>();

            if (!request.Value.HasValue)
                errors.Add("O valor da transação deve ser informado");
            if (string.IsNullOrWhiteSpace(request.CostCenterId))
                errors.Add("O Centro de Custo deve ser informado");

            var result = new RequestValidationModel(errors);
            return Task.FromResult(result);
        }
    }
}
