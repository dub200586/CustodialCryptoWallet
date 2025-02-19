using CustodialCryptoWallet.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CustodialCryptoWallet.Dal.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustodialCryptoWalletContext _custodialCryptoWalletContext;

        public UnitOfWork(CustodialCryptoWalletContext custodialCryptoWalletContext)
        {
            _custodialCryptoWalletContext = custodialCryptoWalletContext;
        }

        public async Task SaveAsync()
        {
            await _custodialCryptoWalletContext.SaveChangesAsync();
        }

        public void Save()
        {
            _custodialCryptoWalletContext.SaveChanges();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _custodialCryptoWalletContext.Database.BeginTransaction();
        }
    }
}
