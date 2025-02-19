using CustodialCryptoWallet.Common.Enums;

namespace CustodialCryptoWallet.Bll.Models
{
    public class CurrencyAccountModel
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public CurrencyType Type { get; set; }
        public decimal Balance { get; set; }
    }
}
