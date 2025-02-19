using AutoMapper;
using CustodialCryptoWallet.Dal.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace CustodialCryptoWallet.Dal.Repositories
{
    public class CurrencyAccountRepository : ICurrencyAccountRepository
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CurrencyAccountRepository(IMapper mapper, ILogger logger)
        {
            _mapper = mapper;
            _logger = logger;
        }
    }
}
