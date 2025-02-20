using CustodialCryptoWallet.Dal;
using Microsoft.EntityFrameworkCore;

namespace CustodialCryptoWallet.Web.Infrastructure.Configurations
{
    public static class CustodialCryptoWalletContextConfiguration
    {
        private const string ConnectionString = "CustodialCryptoWalletDb";
        public static void InitDbContext(this IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment env)
        {
            var connectionString = configuration.GetConnectionString(ConnectionString);

            services.AddDbContext<CustodialCryptoWalletContext>(opt => {
                opt.UseNpgsql(connectionString);
            });
        }
    }
}
