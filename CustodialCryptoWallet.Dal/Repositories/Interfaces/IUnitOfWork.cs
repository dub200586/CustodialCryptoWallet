using Microsoft.EntityFrameworkCore.Storage;

namespace CustodialCryptoWallet.Dal.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        void Save();
        IDbContextTransaction BeginTransaction();
    }
}
