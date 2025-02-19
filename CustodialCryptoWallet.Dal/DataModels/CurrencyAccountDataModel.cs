using CustodialCryptoWallet.Common.Enums;

namespace CustodialCryptoWallet.Dal.DataModels
{
    public class CurrencyAccountDataModel
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public CurrencyType Type { get; set; }
        public decimal Balance { get; set; }
    }
}
