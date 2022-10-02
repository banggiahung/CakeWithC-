using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public interface ICakesDAL
    {
        public Cake GetCakeById(string searchKeyWord, Cake cake);
        public Cake GetByName(string commandText, Cake cake);
        public Cake GetByCT(string commandText, Cake cake);
        public List<Cake> GetByNameList(List<Cake> list, string commandText);
        public List<Cake> GetByCtList(List<Cake> list, string commandText);
        public List<Cake> GetAllC(Staff staff);
        public int Update(Cake cake);
        public int Delete(Cake cake);
        public int InS(Cake cake);
        public List<Cake> getAll();


    }
}