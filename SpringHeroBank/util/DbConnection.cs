using System;
using System.Data;
using MySql.Data.MySqlClient;
namespace SpringHeroBank.util
{
    public class DbConnection
    {
        private DbConnection()
        {
        }

        private const string DatabaseName = "hero_bank";
        private const string ServerName = "localhost";
        private const string ServerPort = "3306";
        private const string Uid = "root";
        private const string Password = "";
        private const string SslMode = "none";
        private const string PersistSecurityInfo = "True";

        private MySqlConnection _connection = null;

        public MySqlConnection Connection
        {
            get { return _connection; }
        }

        private static DbConnection _instance = null;

        public static DbConnection Instance()
        {
            return _instance != null ? _instance : (_instance = new DbConnection());
        }

        public void OpenConnection()
        {
            if (_connection == null)
            {
                var connString =
                    $"Server={ServerName}; database={DatabaseName}; UID={Uid}; password={Password}; persistsecurityinfo={PersistSecurityInfo};port={ServerPort};SslMode={SslMode}";
                _connection = new MySqlConnection(connString);
                _connection.Open();
            }
            else if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();
        }


    }
}