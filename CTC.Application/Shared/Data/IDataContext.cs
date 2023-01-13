using System.Data;

namespace CTC.Application.Shared.Data
{
    internal interface IDataContext
    {
        IDbConnection GetConnection();
    }
}
