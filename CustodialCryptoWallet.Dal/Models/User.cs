﻿namespace CustodialCryptoWallet.Dal.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
    }
}
