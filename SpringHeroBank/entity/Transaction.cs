using System;

namespace SpringHeroBank.entity
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public string AccountNumber { get; set; }
        public double Money { get; set; }
        public string ReceivingAccount { get; set; }
        public string Content { get; set; }
        public int Status { get; set; } // 1. + tiền , 2 . trừ tiền
        public DateTime CreateAt { get; set; }
    }
}
