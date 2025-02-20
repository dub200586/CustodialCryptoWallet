namespace CustodialCryptoWallet.Dal.DataModels
{
    public class UserDataModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
    }
}
