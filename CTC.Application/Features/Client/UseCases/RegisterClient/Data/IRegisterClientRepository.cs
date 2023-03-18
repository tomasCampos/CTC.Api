using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.RegisterClient.Data
{
    internal interface IRegisterClientRepository
    {
        Task<bool> InsertClient(ClientModel model);
        Task<int> VerifyIfClientAlreadyExists(string email, string phone, string document);
    }
}
