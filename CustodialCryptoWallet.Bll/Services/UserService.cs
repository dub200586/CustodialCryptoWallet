using AutoMapper;
using CustodialCryptoWallet.Bll.Models;
using CustodialCryptoWallet.Bll.Services.Interfaces;
using CustodialCryptoWallet.Dal.DataModels;
using CustodialCryptoWallet.Dal.Repositories.Interfaces;

namespace CustodialCryptoWallet.Bll.Services
{
    public class UserService : IUserService
    {
        private const string ExistingUserMessage = "User with this email already exists";
        private const string NonExistingUserMessage = "Such user does not exist";
        private const string InsufficientFundsMessage = "Insufficient funds";

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

            if (existUser != null) throw new Exception(ExistingUserMessage);

            var userDataModel = _mapper.Map<UserDataModel>(user);
            var createdUser = await _userRepository.CreateUserAsync(userDataModel);
            await _unitOfWork.SaveAsync();
            var createdUserModel = _mapper.Map<UserModel>(createdUser);

            return createdUserModel;
        }

        public async Task<UserModel> GetUserByIdAsync(Guid userId)
        {
            var userDataModel = await _userRepository.GetUserByIdAsync(userId);

            if (userDataModel == null) throw new Exception(NonExistingUserMessage);

            var userModel = _mapper.Map<UserModel>(userDataModel);

            return userModel;
        }

        public async Task<UserModel> DepositMoneyToCurrencyAccountAsync(Guid userId, decimal amount)
        {
            var transaction = _unitOfWork.BeginTransaction();

            try
            {
                var userDataModel = await _userRepository.GetUserByIdAsync(userId);
                userDataModel.Balance += amount;
                await _userRepository.UpdateUserAsync(userDataModel);
                await _unitOfWork.SaveAsync();

                await transaction.CommitAsync();
                var userModel = _mapper.Map<UserModel>(userDataModel);

                return userModel;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<UserModel> WithdrawMoneyFromCurrencyAccountAsync(Guid userId, decimal amount)
        {
            var transaction = _unitOfWork.BeginTransaction();

            try
            {
                var userDataModel = await _userRepository.GetUserByIdAsync(userId);

                if (userDataModel.Balance < amount) throw new Exception(InsufficientFundsMessage);

                userDataModel.Balance -= amount;
                await _userRepository.UpdateUserAsync(userDataModel);
                await _unitOfWork.SaveAsync();

                await transaction.CommitAsync();
                var userModel = _mapper.Map<UserModel>(userDataModel);

                return userModel;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
