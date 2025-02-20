using CustodialCryptoWallet.Bll.Models;
using CustodialCryptoWallet.Dal.DataModels;

namespace CustodialCryptoWallet.Bll.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> CreateUserAsync(UserModel user);
        Task<UserModel> GetUserByIdAsync(Guid userId);
        Task<UserModel> DepositMoneyToCurrencyAccountAsync(Guid userId, decimal amount);
        Task<UserModel> WithdrawMoneyFromCurrencyAccountAsync(Guid userId, decimal amount);
    }
}
