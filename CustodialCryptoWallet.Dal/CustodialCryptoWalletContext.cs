using CustodialCryptoWallet.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace CustodialCryptoWallet.Dal
{
    public class CustodialCryptoWalletContext : DbContext
    {
        private const string ConfigurationNamespace = "CustodialCryptoWallet.Dal.Infrastructure.ModelConfiguration";

        public CustodialCryptoWalletContext(DbContextOptions<CustodialCryptoWalletContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CurrencyAccount> CurrencyAccounts { get; set; }
    }
}
