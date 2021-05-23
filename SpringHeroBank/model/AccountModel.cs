using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using SpringHeroBank.entity;
using DbConnection = SpringHeroBank.util.DbConnection;

namespace SpringHeroBank.model
{
    public class AccountModel
    {
        public Boolean Save(Account account)
        {
            DbConnection.Instance().OpenConnection(); // đảm bảo rằng đã kết nối đến db thành công.
            var sqlQuery = "insert into `accounts` " +
                           "(`account_number`, `username`, `password`, `email`,`full_name`, `salt`,`createAt`,`status`) values" +
                           "(@account_number ,@username, @password, @email , @full_name, @salt  ,@createAt, @status)";
            var cmd = new MySqlCommand(sqlQuery, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@account_number", account.AccountNumber);
            cmd.Parameters.AddWithValue("@username", account.Username);
            cmd.Parameters.AddWithValue("@password", account.PasswordHash);
            cmd.Parameters.AddWithValue("@email", account.Email);
            cmd.Parameters.AddWithValue("@full_name", account.FullName);
            cmd.Parameters.AddWithValue("@salt", account.Salt);
            cmd.Parameters.AddWithValue("@createAt", account.CreateAt);
            cmd.Parameters.AddWithValue("@status", account.Status);
            var result = cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
            return result == 1;
        }

        public Account LoginModel(string user, string pwd)
        {
            Account accountClient = new Account();
            DbConnection.Instance().OpenConnection(); // đảm bảo rằng đã kết nối đến db thành công.
            var sqlQuery =
                "select `username`, `password`,`account_number`, `salt` from `accounts` where `username` = @username";
            var cmd = new MySqlCommand(sqlQuery, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@username", user);
            // Console.WriteLine("Lỗi ở đâu??");
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // mã hóa pwd nhập vào với muối cũ
                        int salt = reader.GetOrdinal("salt"); // lôi từ bảng ra
                        string saltString = reader.GetString(salt); // chuyển sang string
                        string pwdHash = Hash.GenerateSaltedSHA1(pwd, saltString);
                        accountClient.Username = user;
                        // accountClient.Email = emailString;
                        // accountClient.AccountBalance = balance;
                        // accountClient.FullName = nameString;
                        // lôi pwd mã hóa ra
                        int password = reader.GetOrdinal("password");
                        string stringPwd = reader.GetString(password);
                        int account = reader.GetOrdinal("account_number");
                        accountClient.AccountNumber = reader.GetString(account);
                        // Console.WriteLine(stringPwd);
                        // Console.WriteLine(pwdHash);
                        if (stringPwd == pwdHash)
                        {
                            Console.WriteLine("Login Success!!");
                            return accountClient;
                        }
                        else
                        {
                            Console.WriteLine("Err");
                            return null;
                        }
                    }
                }
            }

            return null;
        }
        
        public Account ChangePasswordModel()
        {
            
            return null;
        }
    }
}