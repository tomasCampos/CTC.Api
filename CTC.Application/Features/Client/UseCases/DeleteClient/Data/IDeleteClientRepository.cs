using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.DeleteClient.Data
{
    internal interface IDeleteClientRepository
    {
        Task<bool> DeleteClient(string clientId, string personId);

        Task<ClientModel> GetClientById(string clientId);
    }
}
