using CustodialCryptoWallet.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace CustodialCryptoWallet.Dal
{
    public class CustodialCryptoWalletContext : DbContext
    {
        public CustodialCryptoWalletContext(DbContextOptions<CustodialCryptoWalletContext> options)
            : base(options){}

        public DbSet<User> Users { get; set; }
    }
}
