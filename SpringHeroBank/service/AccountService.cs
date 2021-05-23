using System;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Security.Policy;
using MySql.Data.MySqlClient;
using SpringHeroBank.entity;
using SpringHeroBank.model;
using DbConnection = SpringHeroBank.util.DbConnection;
using Hash = SpringHeroBank.entity.Hash;

namespace SpringHeroBank.service
{
    public class AccountService
    {
        private AccountModel _model = new AccountModel();
        private CheckDatabase _checkDatabase = new CheckDatabase();

        public void CreateAccount(Account account)
        {
            account.AccountNumber = Guid.NewGuid().ToString();
            var salt = Hash.RandomString(7); // sinh ra chuỗi muối random.
            account.Salt = salt; // đưa muối vào thuộc tính của account để lưu vào database.
            // mã hoá password của người dùng kèm theo muối, set thuộc tính password mới.
            account.PasswordHash = Hash.GenerateSaltedSHA1(account.PasswordHash, account.Salt);
            account.CreateAt = DateTime.Now;
            account.Status = 1;
            _model.Save(account);
        }

        public Account Login(string user, string pwd)
        {
            if (_checkDatabase.CheckUsername(user, pwd))
            {
                return _model.LoginModel(user, pwd);
            }
            else
            {
                Console.WriteLine("Account invalidate");
                return null;
            }
        }
        
    }
}