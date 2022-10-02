using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace Persistence
{
    public class NewPassword
    {
        public string? newPass { get; set; }
        public string? phoneN { get; set; }

        public string? tokenNew { get; set; }


        public NewPassword()
        {

        }
        public NewPassword(
            string? newPass,
            string? phoneN,
            string? tokenNew
        )
        {
            this.newPass = newPass;
            this.phoneN = phoneN;
            this.tokenNew = tokenNew;
        }

        public int ChangePw(MySqlConnection connection)
        {
            int result = 1;
            MySqlCommand cmd = new MySqlCommand("upPw", connection);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PhoneOld", this.phoneN);
                cmd.Parameters.AddWithValue("@newPass", this.newPass);
                cmd.Parameters.AddWithValue("@tokenOld", this.tokenNew);
                cmd.ExecuteNonQuery();

            }
            catch
            {
                Console.WriteLine("k kn dc dtb");
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return result;

        }
         public bool isValidate(NewPassword newP, string re_P)
        {
            if (newP.tokenNew == re_P)
            {
                return true;
            }
            return false;
        }
    }
}

