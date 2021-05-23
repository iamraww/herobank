using System;
using SpringHeroBank.controller;
using SpringHeroBank.entity;
using SpringHeroBank.service;

namespace SpringHeroBank.view
{
    public class Menu
    {
        private AccountController _accountControllercontroller = new AccountController();
        private TransactionController _transactionController = new TransactionController();
        private AccountMenu _accountMenu = new AccountMenu();
        private Account _currentLoggedIn = null;
        public void ApplicationMenu()
        {
            
            while (true)
            {
                Console.WriteLine("-------------");
                Console.WriteLine("Account Menu");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.WriteLine("--------------");
                Console.WriteLine("Please enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1: 
                        _accountControllercontroller.CreateAccount();
                        break;
                    case 2:
                        _currentLoggedIn = _transactionController.Login();
                        break;
                    case 3:
                        Console.WriteLine("GoodBye!!!");
                        break;
                    default:
                        break;
                }

                if (_currentLoggedIn != null)
                {
                    _accountMenu.MenuAccount(_currentLoggedIn.Username, _currentLoggedIn.AccountNumber);
                }
                if (choice == 3)
                {
                    break;
                }
            }
        }
    }
}