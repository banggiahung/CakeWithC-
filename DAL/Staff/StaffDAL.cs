using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class StaffDAL : IStaffDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();

        public Staff Login(Staff staff)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"select * from staff where staff_username = '{staff.userName}' and staff_password = '{staff.password}';";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        staff = GetStaff(reader);
                    }
                    reader.Close();
                }
            }
            finally
            {
                connection.Close();
            }
            return staff;
        }

        private Staff GetStaff(MySqlDataReader reader)
        {
            Staff staff = new Staff();
            staff.staffId = reader.GetInt32("staff_id");
            staff.staffName = reader.GetString("staff_name");
            staff.staffPhone = reader.GetString("staff_phone");
            staff.staffAddress = reader.GetString("staff_address");
            staff.staffRole = reader.GetInt32("staff_role");
            return staff;
        }
        public int RegUser(Staff staff)
        {
            int result = 1;
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("Register", connection);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name_user", staff.staffName);
                cmd.Parameters.AddWithValue("@phone", staff.staffPhone);
                cmd.Parameters.AddWithValue("@address", staff.staffAddress);
                cmd.Parameters.AddWithValue("@account_user", staff.userName);
                cmd.Parameters.AddWithValue("@password_user", staff.password);
                cmd.ExecuteNonQuery();
            }
            catch
            {
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
