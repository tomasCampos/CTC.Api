using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Client.UseCases.RegisterClient.UseCase
{
    public sealed class RegisterClientInput : IInput
    {
        public RegisterClientInput(string? name, string? email, string? phone, string? document)
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
