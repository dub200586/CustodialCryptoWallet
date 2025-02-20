namespace CustodialCryptoWallet.Bll.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
    }
}
