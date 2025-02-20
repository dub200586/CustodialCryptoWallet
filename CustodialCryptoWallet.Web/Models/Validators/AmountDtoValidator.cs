using CustodialCryptoWallet.Web.Models.Dto;
using FluentValidation;

namespace CustodialCryptoWallet.Web.Models.Validators
{
    public class AmountDtoValidator : AbstractValidator<AmountDto>
    {
        public AmountDtoValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero");
        }
    }
}
