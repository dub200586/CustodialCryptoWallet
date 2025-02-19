namespace CustodialCryptoWallet.Bll.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<CurrencyAccountModel> CurrencyAccounts { get; set; }
    }
}
