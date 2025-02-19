using AutoMapper;
using CustodialCryptoWallet.Bll.Services.Interfaces;
using CustodialCryptoWallet.Dal.Repositories.Interfaces;

namespace CustodialCryptoWallet.Bll.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
    }
}
