using CustodialCryptoWallet.Dal.DataModels;

namespace CustodialCryptoWallet.Dal.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDataModel> GetUserByIdAsync(Guid userId);
        Task<UserDataModel> GetUserByEmailAsync(string userEmail);
        Task<UserDataModel> CreateUserAsync(UserDataModel userDataModel);
        Task UpdateUserAsync(UserDataModel userDataModel);
    }
}
