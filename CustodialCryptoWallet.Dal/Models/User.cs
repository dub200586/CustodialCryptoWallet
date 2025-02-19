namespace CustodialCryptoWallet.Dal.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<CurrencyAccount> CurrencyAccounts { get; set; }
    }
}
