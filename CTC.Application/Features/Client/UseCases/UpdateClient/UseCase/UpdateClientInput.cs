using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Client.UseCases.UpdateClient.UseCase
{
    internal class UpdateClientInput : IInput
    {
        public UpdateClientInput(in string? id, in string? name, in string? email, in string? phone, in string? document)
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
