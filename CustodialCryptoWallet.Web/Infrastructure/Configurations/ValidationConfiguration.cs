using CustodialCryptoWallet.Web.Models.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace CustodialCryptoWallet.Web.Infrastructure.Configurations
{
    public static class ValidationConfiguration
    {
        public static void InitModelValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(v => { v.DisableDataAnnotationsValidation = true; });
            services.AddValidatorsFromAssemblyContaining<UserDtoValidator>();
        }
    }
}
