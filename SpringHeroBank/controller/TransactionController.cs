using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Org.BouncyCastle.Asn1.X509;
using SpringHeroBank.entity;
using SpringHeroBank.model;
using SpringHeroBank.service;

namespace SpringHeroBank.controller
{
    public class TransactionController
    {
        private AccountService _accountService = new AccountService();
        private TransactionModel _transactionModel = new TransactionModel();
        private Account _currentLoggedIn = null;
        private Account _account = new Account();
        private TransactionService _transactionService = new TransactionService();
        public string User { get; set; }
        public string Pwd { get; set; }
        public Account Login()
        {
            Console.WriteLine("----------------");
            Console.WriteLine("LOGIN");
            Console.WriteLine("Username: ");
            User = Console.ReadLine();
            Console.WriteLine("Password: ");
            Pwd = Console.ReadLine();
            return _accountService.Login(User,Pwd);
        }

        public Account CheckAccount(string usernameLoggedIn)
        {
            _transactionModel.ShowInfo(usernameLoggedIn);
            return null;
        }

        public void Deposit()
        {
            Console.WriteLine("--------------");
            Console.WriteLine("DEPOSIT");
            Console.WriteLine("Momo, Airpay, Zalopay: 0946384029");
            Console.WriteLine("Vietinbank: 100868352899");
            Console.WriteLine("Vietcombank 1014824765");
            Console.WriteLine("MBBank 0010165158888");
            Console.WriteLine("TPBank 04036882401");
            Console.WriteLine("TRAN ANH DUC");
        }

        public void Withdrawal(string usernameLogin, string accountNumber)
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("WITHDRAWAL");
            Console.WriteLine("Please enter the amount to withdraw: ");
            double money = Convert.ToDouble(Console.ReadLine());
            _transactionService.WithdrawalService(usernameLogin,accountNumber,money);
        }

        public void Transfers(string username,string accountNumber)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("TRANSFER");
            Console.WriteLine("Enter the account number to transfer ");
            string accountTransfer = Console.ReadLine();
            Console.WriteLine("Please enter the amount to withdraw: ");
            double money = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter the transfer content ");
            string content = Console.ReadLine();
            _transactionService.TransfersService(username, accountNumber, accountTransfer, money, content);
        }

        public void History(string username, string accountNumber)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("HISTORY");
            _transactionModel.ShowHistory(accountNumber);
        }

        public void ChangePassword(string username)
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("CHANGE PASSWORD");
            Console.WriteLine("Please enter old password: ");
            string oldPassword = Console.ReadLine();
            Console.WriteLine("Enter new password: ");
            string newPassword = Console.ReadLine();
            Console.WriteLine("Confirm password: ");
            string confirmPwd = Console.ReadLine();
            if (newPassword == confirmPwd)
            {
                _transactionService.ChangePasswordService(username, oldPassword, newPassword);
            }
            else
            {
                Console.WriteLine("Confirm password is not correct");
            }

        }
    }
}