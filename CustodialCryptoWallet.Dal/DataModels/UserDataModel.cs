namespace CustodialCryptoWallet.Dal.DataModels
{
    public class UserDataModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<CurrencyAccountDataModel> CurrencyAccounts { get; set; }
    }
}
