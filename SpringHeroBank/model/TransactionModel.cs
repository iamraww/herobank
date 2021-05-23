using System;
using System.Data.Common;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.OpenSsl;
using SpringHeroBank.entity;
using DbConnection = SpringHeroBank.util.DbConnection;

namespace SpringHeroBank.model
{
    public class TransactionModel
    {
        public Account ShowInfo(string username)
        {
            Account account = new Account();
            DbConnection.Instance().OpenConnection(); // đảm bảo rằng đã kết nối đến db thành công.
            var sqlQuery = "select `username`,`account_number`, `email`, `full_name`, `balance` from `accounts` where `username` = @username";
            var cmd = new MySqlCommand(sqlQuery, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@username", username);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        // lôi thông tin trong bảng ra ngoài
                        int email = reader.GetOrdinal("email"); // lôi email từ bảng ra
                        string emailString = reader.GetString(email); 
                        
                        int accountNumber = reader.GetOrdinal("account_number"); // lôi account_number từ bảng ra
                        string accountNumberString = reader.GetString(accountNumber); 
                        
                        int name = reader.GetOrdinal("full_name"); // lôi full_name từ bảng ra
                        string nameString = reader.GetString(name); 
                        
                        int balance = reader.GetOrdinal("balance"); // lôi balance từ bảng ra
                        double balanceNumber = reader.GetDouble(balance);
                        Console.WriteLine($"Username : {username}, Email: {emailString}, Full Name: {nameString}, Balance: {balanceNumber}");
                        Console.WriteLine($"Account Number: {accountNumberString}");
                    }
                }
            }
            DbConnection.Instance().CloseConnection();
            return null;
        }

        public Account Withdrawal(string username, double money)
        {
            DbConnection.Instance().OpenConnection();
            Account account = new Account();
            var sqlQuerySelect = "select `balance` from `accounts` where `username` = @username";
            var cmdSelect = new MySqlCommand(sqlQuerySelect, DbConnection.Instance().Connection);
            cmdSelect.Parameters.AddWithValue("@username", username);
            using (DbDataReader reader = cmdSelect.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int balance = reader.GetOrdinal("balance"); // lôi balance từ bảng ra
                        account.AccountBalance = reader.GetDouble(balance);
                    }
                }
            }
            var sqlQuery = "update  `accounts` SET `balance` = @balance  where `username` = @username";
            var cmd = new MySqlCommand(sqlQuery, DbConnection.Instance().Connection);
            account.AccountBalance = account.AccountBalance - money;
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@balance", account.AccountBalance);
            cmd.ExecuteNonQuery();
            return null;
        }
        public Account Transfers(string username, string accountNumber, string accountTransfer,double money)
        {
            DbConnection.Instance().OpenConnection();

            // đoạn này là trừ tiền của người gửi
            Account account = new Account();
            var sqlQuerySelect = "select `balance` from `accounts` where `username` = @username";
            var cmdSelect = new MySqlCommand(sqlQuerySelect, DbConnection.Instance().Connection);
            cmdSelect.Parameters.AddWithValue("@username", username);
            using (DbDataReader reader = cmdSelect.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int balance = reader.GetOrdinal("balance"); // lôi balance từ bảng ra
                        account.AccountBalance = reader.GetDouble(balance);
                    }
                }
            }
            var sqlQuery = "update  `accounts` SET `balance` = @balance  where `username` = @username";
            var cmd = new MySqlCommand(sqlQuery, DbConnection.Instance().Connection);
            account.AccountBalance = account.AccountBalance - money;
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@balance", account.AccountBalance);
            Console.WriteLine(account.AccountBalance);
            cmd.ExecuteNonQuery();
            // đến đoạn này là hết trừ tiền của người gửi
            // đoạn này cộng tiền cho người nhận
            Account accountTo = new Account();
            var sqlQuerySelectTo = "select `balance` from `accounts` where `account_number` = @account_number";
            var cmdSelectTo = new MySqlCommand(sqlQuerySelectTo, DbConnection.Instance().Connection);
            cmdSelectTo.Parameters.AddWithValue("@account_number", accountTransfer);
            using (DbDataReader reader = cmdSelectTo.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int balance = reader.GetOrdinal("balance"); // lôi balance từ bảng ra
                        accountTo.AccountBalance = reader.GetDouble(balance);
                    }
                }
            }
            var sqlUpdateQueryTo = "update  `accounts` SET `balance` = @balance  where `account_number` = @account_number";
            var cmdUpdateTo = new MySqlCommand(sqlUpdateQueryTo, DbConnection.Instance().Connection);
            accountTo.AccountBalance = accountTo.AccountBalance + money;
            cmdUpdateTo.Parameters.AddWithValue("@account_number", accountTransfer);
            cmdUpdateTo.Parameters.AddWithValue("@balance", accountTo.AccountBalance);
            cmdUpdateTo.ExecuteNonQuery();
            // hết đoạn cộng tiền cho người nhận
            return null;
        }
        
        public Account ShowHistory(string accountNumber)
        {
            Account account = new Account();
            DbConnection.Instance().OpenConnection(); // đảm bảo rằng đã kết nối đến db thành công.
            var sqlQuery = "select `transaction_id`,`account_number`, `money`, `content`, `createAt` from `transaction` where `account_number` = @account_number or `receiving_account` = @account_number" ;
            var cmd = new MySqlCommand(sqlQuery, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@account_number", accountNumber);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        // lôi thông tin trong bảng ra ngoài
                        int transactionId = reader.GetOrdinal("transaction_id"); // lôi transactionId từ bảng ra
                        string transactionIdString = reader.GetString(transactionId); 
                        
                        int accountBank = reader.GetOrdinal("account_number"); // lôi account_number từ bảng ra
                        string accountNumberString = reader.GetString(accountBank);

                        int money = reader.GetOrdinal("money"); // lôi money từ bảng ra
                        double moneyDouble = reader.GetDouble(money); 
                        
                        int content = reader.GetOrdinal("content"); // lôi content từ bảng ra
                        string contentString = reader.GetString(content);
                        
                        int createAt = reader.GetOrdinal("createAt"); // lôi createAt từ bảng ra
                        string createAtString = reader.GetString(createAt);

                        if (accountNumber == accountNumberString)
                        {
                            Console.WriteLine($"Time: {createAtString} | Content: {contentString} | Status: -{moneyDouble}");
                        }
                        else
                        {
                            Console.WriteLine($"Time: {createAtString} | Content: {contentString} | Status: +{moneyDouble}");
                        }
                    }
                }
            }
            DbConnection.Instance().CloseConnection();
            return null;
        }

        public Account ChangePasswordModel(string username, string newPasswordHash, string newSalt)
        {
            Account account = new Account();
            var sqlQuery = "update  `accounts` SET `password` = @password, `salt` = @salt  where `username` = @username";
            var cmd = new MySqlCommand(sqlQuery, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", newPasswordHash);
            cmd.Parameters.AddWithValue("@salt", newSalt);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Success!!");
            return null;
        }
        public bool SaveHistoryModel(Transaction transaction)
        {
            DbConnection.Instance().OpenConnection(); // đảm bảo rằng đã kết nối đến db thành công.
            var sqlQuery = "insert into `transaction` " +
                           "(`transaction_id`,`account_number`, `money`, `content`, `createAt`,`receiving_account`) values" +
                           "(@transaction_id ,@account_number, @money, @content, @createAt , @receiving_account)";
            var cmd = new MySqlCommand(sqlQuery, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@transaction_id", transaction.TransactionId);
            cmd.Parameters.AddWithValue("@account_number", transaction.AccountNumber);
            cmd.Parameters.AddWithValue("@money", transaction.Money);
            cmd.Parameters.AddWithValue("@content", transaction.Content);
            cmd.Parameters.AddWithValue("@createAt", transaction.CreateAt);
            cmd.Parameters.AddWithValue("@receiving_account", transaction.ReceivingAccount);
            var result = cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
            return result == 1;
        }
    }
}