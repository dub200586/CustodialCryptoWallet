using CustodialCryptoWallet.Web.Middleware;

namespace CustodialCryptoWallet.Web.Infrastructure.Configurations
{
    public static class MiddlewareConfiguration
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
