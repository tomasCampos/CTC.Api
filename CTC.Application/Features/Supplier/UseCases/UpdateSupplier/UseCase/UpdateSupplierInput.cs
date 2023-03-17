using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Supplier.UseCases.UpdateSupplier.UseCase
{
    public sealed class UpdateSupplierInput : IInput
    {
        public UpdateSupplierInput(in string? id, in string? name, in string? email, in string? phone, in string? document)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Document = document;
        }

        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Document { get; set; }
    }
}
