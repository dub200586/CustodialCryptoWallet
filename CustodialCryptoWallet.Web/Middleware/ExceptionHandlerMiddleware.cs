using CustodialCryptoWallet.Common.Constants;
using Newtonsoft.Json;

namespace CustodialCryptoWallet.Web.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }

        private async Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception.Message);

            context.Response.ContentType = OptionConstant.ContentTypeJson;

            var result = JsonConvert.SerializeObject(new
            {
                Error = exception.Message
            });

            await context.Response.WriteAsync(result);
        }
    }
}
