using CustodialCryptoWallet.Bll.Services;
using CustodialCryptoWallet.Bll.Services.Interfaces;

namespace CustodialCryptoWallet.Web.Infrastructure.Configurations
{
    public static class ServicesConfiguration
    {
        public static void InitServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
