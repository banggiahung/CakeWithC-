using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using DAL;
using BL;

namespace Persistence
{
    public class RanToken
    {
        public string? Token { get; set; }
        public string? find { get; set; }

        public RanToken()
        {

        }
        public RanToken(
            string? Token,
            string? find
        )
        {
            this.Token = Token;
            this.find = find;
        }


        public int RanDom(MySqlConnection connection)
        {
            int result = 1;
            MySqlCommand cmd = new MySqlCommand("TokenSend", connection);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@find", this.find);
                cmd.Parameters.AddWithValue("@token", this.Token);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Lỗi");
                result = 0;
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return result;

        }

    }
}