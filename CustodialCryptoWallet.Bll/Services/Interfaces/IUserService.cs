using CustodialCryptoWallet.Bll.Models;
using CustodialCryptoWallet.Dal.DataModels;

namespace CustodialCryptoWallet.Bll.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> CreateUserAsync(UserModel user);
        Task<UserModel> GetUserByIdAsync(Guid userId);
        Task<UserDataModel> DepositMoneyToCurrencyAccountAsync(Guid userId, decimal amount);
        Task<UserDataModel> WithdrawMoneyFromCurrencyAccountAsync(Guid userId, decimal amount);
    }
}
