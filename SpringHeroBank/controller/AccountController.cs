using System;
using SpringHeroBank.entity;
using SpringHeroBank.model;
using SpringHeroBank.service;

namespace SpringHeroBank.controller
{
    public class AccountController
    {
        private AccountService _accountService = new AccountService();
        private Account _account = new Account();
        public void CreateAccount()
        {
            Console.WriteLine("-------------");
            Console.WriteLine("REGISTER ACCOUNT");
            Console.WriteLine("Please enter your infomation");
            Console.WriteLine("Username: ");
            _account.Username = Console.ReadLine();
            Console.WriteLine("Password: ");
            _account.PasswordHash = Console.ReadLine();
            Console.WriteLine("Full Name: ");
            _account.FullName = Console.ReadLine();
            Console.WriteLine("Email: ");
            _account.Email = Console.ReadLine();
            _accountService.CreateAccount(_account);
        }

        // public void Login()
        // {
        //     Console.WriteLine("----------------");
        //     Console.WriteLine("LOGIN");
        //     Console.WriteLine("Username: ");
        //     string user = Console.ReadLine();
        //     Console.WriteLine("Password: ");
        //     string pwd = Console.ReadLine();
        //     _accountService.Login(user,pwd);
        // }
        
    }
}