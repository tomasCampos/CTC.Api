using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Supplier.UseCases.RegisterSupplier.UseCase
{
    public sealed class RegisterSupplierInput : IInput
    {
        public RegisterSupplierInput(in string? name, in string? email, in string? phone, in string? document)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Document = document;
        }

        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Document { get; set; }
    }
}
