using System;
using SpringHeroBank.entity;
using SpringHeroBank.model;

namespace SpringHeroBank.service
{
    public class TransactionService
    {
        private CheckDatabase _checkDatabase = new CheckDatabase();
        private TransactionModel _transactionModel = new TransactionModel();

        public Account WithdrawalService(string username,string accountNumber, double money)
        {
            Transaction thisTransaction = new Transaction();
            if (_checkDatabase.CheckBalance(username, money))
            {
                thisTransaction.AccountNumber = accountNumber;
                thisTransaction.TransactionId = Convert.ToString(DateTime.Now.Ticks);
                thisTransaction.Money = money;
                thisTransaction.Content = "Withdrawal";
                thisTransaction.CreateAt = DateTime.Now;
                thisTransaction.ReceivingAccount = "88888888888";
                _transactionModel.Withdrawal(username, money);
                _transactionModel.SaveHistoryModel(thisTransaction);
            }
            else
            {
                Console.WriteLine("Insufficient balance");
            }
            return null;
        }

        public Account TransfersService(string username, string accountNumber, string accountTransfer,double money, string content)
        {
            Transaction thisTransaction = new Transaction();
            if (_checkDatabase.CheckAccountTransfers(accountTransfer))  // kiểm tra tài khoản nhận có tồn tại ko
            {
                if (_checkDatabase.CheckBalance(username, money))  // kiểm tra số dư có đủ hay ko
                {
                    thisTransaction.AccountNumber = accountNumber;
                    thisTransaction.TransactionId = Convert.ToString(DateTime.Now.Ticks);
                    thisTransaction.Money = money;
                    thisTransaction.Content = content;
                    thisTransaction.CreateAt = DateTime.Now;
                    thisTransaction.ReceivingAccount = accountTransfer;
                    _transactionModel.Transfers(username, accountNumber, accountTransfer,money);
                    _transactionModel.SaveHistoryModel(thisTransaction);
                }
                else
                {
                    Console.WriteLine("Insufficient balance");
                }
            } 
            else
            {
                Console.WriteLine("Account number does not exist");
            }
            return null;
        }
        
        // public Transaction SaveHistory(Transaction transaction)
        // {
        //     transaction.TransactionId = Convert.ToString(DateTime.Now.Ticks);
        //     _transactionModel.SaveHistoryModel(transaction);
        //     return null;
        // }
        public Account ChangePasswordService(string username, string oldPassword, string newPassword)
        {
            if (_checkDatabase.CheckOldPassword(username,oldPassword))
            {
                string newSalt = Hash.RandomString(7);
                string newPwdHash = Hash.GenerateSaltedSHA1(newPassword, newSalt);
                _transactionModel.ChangePasswordModel(username, newPwdHash, newSalt);
            }
            else
            {
                Console.WriteLine("Old password is not correct");
            }
            return null;
        }
    }
}