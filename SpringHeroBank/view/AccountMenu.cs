using System;
using SpringHeroBank.controller;
using SpringHeroBank.entity;

namespace SpringHeroBank.view
{
    public class AccountMenu
    {
        private TransactionController _transactionController = new TransactionController();
        
        public void MenuAccount(string usernameLogin, string accountNumber)
        {
            while (true)
            {
                Console.WriteLine("-------------");
                Console.WriteLine($"Hello {usernameLogin}"); 
                Console.WriteLine("1. Check Account");
                Console.WriteLine("2. DEPOSIT");
                Console.WriteLine("3. Withdrawal");
                Console.WriteLine("4. Transfers");
                Console.WriteLine("5. Transaction History");
                Console.WriteLine("6. Change password");
                Console.WriteLine("7. Log out");
                Console.WriteLine("--------------");
                Console.WriteLine("Please enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1: 
                        // _controller.CreateAccount();
                        _transactionController.CheckAccount(usernameLogin);
                        break;
                    case 2:
                        _transactionController.Deposit();
                        break;
                    case 3:
                        _transactionController.Withdrawal(usernameLogin, accountNumber);
                        break;
                    case 4:
                        _transactionController.Transfers(usernameLogin,accountNumber);
                        break;
                    case 5:
                        _transactionController.History(usernameLogin,accountNumber);
                        break;
                    case 6:
                        _transactionController.ChangePassword(usernameLogin);
                        break;
                    case 7:
                        Console.WriteLine("Logout");
                        break;
                    default:
                        Console.WriteLine("Please enter your choice: ");
                        break;
                    
                }
                if (choice == 7)
                {
                    break;
                }
            }
        }
    }
}