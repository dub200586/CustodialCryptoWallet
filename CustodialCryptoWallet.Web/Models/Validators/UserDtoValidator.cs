using CustodialCryptoWallet.Web.Models.Dto;
using FluentValidation;

namespace CustodialCryptoWallet.Web.Models.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("A valid email is required");
        }
    }
}
