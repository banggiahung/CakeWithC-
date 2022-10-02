using Persistence;
using DAL;

namespace BL
{
    public class AdminBL
    {
        private AdminDAL adminDal = new AdminDAL();

        public AdminS LoginAdmin(AdminS admin)
        {
            return adminDal.LoginAdmin(admin);
        }

    }
}