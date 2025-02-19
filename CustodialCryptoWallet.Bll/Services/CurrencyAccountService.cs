using AutoMapper;
using CustodialCryptoWallet.Bll.Services.Interfaces;
using CustodialCryptoWallet.Dal.Repositories.Interfaces;

namespace CustodialCryptoWallet.Bll.Services
{
    public class CurrencyAccountService : ICurrencyAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CurrencyAccountService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
    }
}
