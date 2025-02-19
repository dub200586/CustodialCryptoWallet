using CustodialCryptoWallet.Common.Enums;

namespace CustodialCryptoWallet.Dal.Models
{
    public class CurrencyAccount
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public CurrencyType Type { get; set; }
        public decimal Balance { get; set; }

        public User User { get; set; }
    }
}
