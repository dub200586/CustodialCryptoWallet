namespace CustodialCryptoWallet.Web.Infrastructure.Configurations
{
    public static class MapperConfiguration
    {
        public static void InitMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
