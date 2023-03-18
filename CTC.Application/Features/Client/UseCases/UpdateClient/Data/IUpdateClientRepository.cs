using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.UpdateClient.Data
{
    internal interface IUpdateClientRepository
    {
        Task<int> UpdateClient(ClientModel model);

        Task<ClientModel> GetClientById(string id);

        Task<List<ClientModel>> GetClientsByEmail(string email);

        Task<List<ClientModel>> GetClientsByDocument(string document);

        Task<List<ClientModel>> GetClientsByPhone(string phone);
    }
}
