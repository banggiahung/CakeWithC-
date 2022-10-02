using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class AdminDAL : IAdminDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();

        public AdminS LoginAdmin(AdminS admin)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"select * from adminS where admin_user = '{admin.admin_User}' and admin_pass = '{admin.admin_pass}';";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        admin = GetAdmin(reader);
                    }
                    reader.Close();
                }
            }
            finally
            {
                connection.Close();
            }
            return admin;
        }
        private AdminS GetAdmin(MySqlDataReader reader)
        {
            AdminS admin = new AdminS();
            admin.adminId = reader.GetInt32("admin_id");
            admin.admin_name = reader.GetString("admin_name");
            return admin;
        }

    }
}


