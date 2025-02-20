using CustodialCryptoWallet.Common.Constants;
using CustodialCryptoWallet.Dal;
using Microsoft.EntityFrameworkCore;

namespace CustodialCryptoWallet.Web.Infrastructure.Configurations
{
    public static class CustodialCryptoWalletContextConfiguration
    {
        public static void InitDbContext(this IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment env)
        {
            var connectionString = configuration.GetConnectionString(OptionConstant.ConnectionString);

            services.AddDbContext<CustodialCryptoWalletContext>(opt => {
                opt.UseNpgsql(connectionString);
            });
        }
    }
}
