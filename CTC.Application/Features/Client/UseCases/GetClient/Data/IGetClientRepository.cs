using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.GetClient.Data
{
    internal interface IGetClientRepository
    {
        Task<ClientModel> GetClientById(string clientId);
    }
}
