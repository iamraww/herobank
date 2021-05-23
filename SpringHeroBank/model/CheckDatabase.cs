using System;
using System.Data.Common;
using MySql.Data.MySqlClient;
using SpringHeroBank.entity;
using DbConnection = SpringHeroBank.util.DbConnection;

namespace SpringHeroBank.model
{
    public class CheckDatabase
    {
        public bool CheckUsername(string username, string pwd)
        {
            DbConnection.Instance().OpenConnection(); // đảm bảo rằng đã kết nối đến db thành công.
            var sqlQuery = "select `username` from `accounts` where `username` = @username";
            var cmd = new MySqlCommand(sqlQuery, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@username", username);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckAccountTransfers(string accountTransfer)
        {
            DbConnection.Instance().OpenConnection(); // đảm bảo rằng đã kết nối đến db thành công.
            var sqlQuery = "select `account_number` from `accounts` where `account_number` = @account_number";
            var cmd = new MySqlCommand(sqlQuery, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@account_number", accountTransfer);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckBalance(string username, double money)
        {
            DbConnection.Instance().OpenConnection(); // đảm bảo rằng đã kết nối đến db thành công.
            var sqlQuery = "select `balance` from `accounts` where `username` = @username";
            var cmd = new MySqlCommand(sqlQuery, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@username", username);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int balance = reader.GetOrdinal("balance"); // lôi balance từ bảng ra
                        double accountBalance = reader.GetDouble(balance);
                        if (money > accountBalance)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool CheckOldPassword(string username, string oldPassword)
        {
            DbConnection.Instance().OpenConnection(); // đảm bảo rằng đã kết nối đến db thành công.
            var sqlQuery = "select `password`, `salt` from `accounts` where `username` = @username";
            var cmd = new MySqlCommand(sqlQuery, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@username", username);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int password = reader.GetOrdinal("password"); // lôi pw từ bảng ra
                        string passwordString = reader.GetString(password);
                        int salt = reader.GetOrdinal("salt");
                        string saltString = reader.GetString(salt);
                        string pwdHash = Hash.GenerateSaltedSHA1(oldPassword, saltString);
                        if (passwordString == pwdHash)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            return false;
        }
    }
}