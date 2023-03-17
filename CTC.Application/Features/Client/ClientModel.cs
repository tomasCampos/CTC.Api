using CTC.Application.Shared.Person;
using System;

namespace CTC.Application.Features.Client
{
    internal sealed class ClientModel : PersonModel
    {
        public ClientModel(in string clientId, in string personId, in string name, in string email, in string phone, in string document) : base(personId, name, email, phone, document)
        {
            ClientId = clientId;
        }

        public ClientModel(in string name, in string email, in string phone, in string document) : base(Guid.NewGuid().ToString(), name, email, phone, document)
        {
            ClientId = Guid.NewGuid().ToString();
        }

        public ClientModel() : base()
        {

        }

        public string? ClientId { get; set; }
    }
}
