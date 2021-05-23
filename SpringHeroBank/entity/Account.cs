using System;

namespace SpringHeroBank.entity
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public double AccountBalance { get; set; }
        public DateTime CreateAt { get; set; }
        public int Status { get; set; }
    }
}