using CustodialCryptoWallet.Dal.Repositories;
using CustodialCryptoWallet.Dal.Repositories.Interfaces;

namespace CustodialCryptoWallet.Web.Infrastructure.Configurations
{
    public static class RepositoriesConfiguration
    {
        public static void InitRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
