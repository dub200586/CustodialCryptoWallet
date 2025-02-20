using AutoMapper;
using CustodialCryptoWallet.Bll.Models;
using CustodialCryptoWallet.Bll.Services.Interfaces;
using CustodialCryptoWallet.Dal.DataModels;
using CustodialCryptoWallet.Dal.Repositories.Interfaces;

namespace CustodialCryptoWallet.Bll.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            var existUser = await _userRepository.GetUserByEmailAsync(user.Email);

            if (existUser != null) throw new Exception("User with this email already exists");

            var userDataModel = _mapper.Map<UserDataModel>(user);
            var createdUser = await _userRepository.CreateUserAsync(userDataModel);
            await _unitOfWork.SaveAsync();
            var createdUserModel = _mapper.Map<UserModel>(createdUser);

            return createdUserModel;
        }

        public async Task<UserModel> GetUserByIdAsync(Guid userId)
        {
            var userDataModel = await _userRepository.GetUserByIdAsync(userId);

            if (userDataModel == null) throw new Exception("Such user does not exist");

            var userModel = _mapper.Map<UserModel>(userDataModel);

            return userModel;
        }

        public async Task<UserDataModel> DepositMoneyToCurrencyAccountAsync(Guid userId, decimal amount)
        {
            var transaction = _unitOfWork.BeginTransaction();

            try
            {
                var userDataModel = await _userRepository.GetUserByIdAsync(userId);
                userDataModel.Balance += amount;
                await _userRepository.UpdateUserAsync(userDataModel);
                await _unitOfWork.SaveAsync();

                await transaction.CommitAsync();
                return userDataModel;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<UserDataModel> WithdrawMoneyFromCurrencyAccountAsync(Guid userId, decimal amount)
        {
            var transaction = _unitOfWork.BeginTransaction();

            try
            {
                var userDataModel = await _userRepository.GetUserByIdAsync(userId);

                if (userDataModel.Balance < amount) throw new Exception("Insufficient funds");

                userDataModel.Balance -= amount;
                await _userRepository.UpdateUserAsync(userDataModel);
                await _unitOfWork.SaveAsync();

                await transaction.CommitAsync();
                return userDataModel;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
