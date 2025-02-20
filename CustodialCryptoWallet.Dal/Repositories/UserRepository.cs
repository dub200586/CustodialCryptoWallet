using AutoMapper;
using CustodialCryptoWallet.Dal.DataModels;
using CustodialCryptoWallet.Dal.Models;
using CustodialCryptoWallet.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustodialCryptoWallet.Dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly CustodialCryptoWalletContext _context;

        public UserRepository(IMapper mapper, CustodialCryptoWalletContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserDataModel> GetUserByIdAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            var userDataModel = _mapper.Map<UserDataModel>(user);

            return userDataModel;
        }

        public async Task<UserDataModel> GetUserByEmailAsync(string userEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
            var userDataModel = _mapper.Map<UserDataModel>(user);

            return userDataModel;
        }

        public async Task<UserDataModel> CreateUserAsync(UserDataModel userDataModel)
        {
            var user = _mapper.Map<User>(userDataModel);
            var createdUser = await _context.Users.AddAsync(user);
            var createdUserDataModel = _mapper.Map<UserDataModel>(createdUser);

            return createdUserDataModel;
        }

        public async Task UpdateUserAsync(UserDataModel userDataModel)
        {
            var user = _mapper.Map<User>(userDataModel);
            _context.Users.Update(user);
        }
    }
}
