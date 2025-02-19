using AutoMapper;
using CustodialCryptoWallet.Dal.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace CustodialCryptoWallet.Dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserRepository(IMapper mapper, ILogger logger)
        {
            _mapper = mapper;
            _logger = logger;
        }
    }
}
