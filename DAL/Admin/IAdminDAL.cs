using MySql.Data.MySqlClient;
using Persistence;

namespace DAL { 
    public interface IAdminDAL{
        public AdminS LoginAdmin(AdminS admin);
    }
}