using MySql.Data.MySqlClient;

namespace DAL
{
    public class DbConfig
    {
        private static MySqlConnection? connection;
        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection
                {
                    ConnectionString = "server=localhost;userid=root;password=giahung2409;port=3306;database=cakesdata;"
                };
            }
            return connection;
        }
           public static MySqlConnection OpenConnection()
        {
            if (connection == null)
            {
                GetConnection();
            }
            connection!.Open();
            return connection;
        }
         public static void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}