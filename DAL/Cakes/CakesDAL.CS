using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class CakesDAL : ICakesDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();

        public Cake GetCakeByIdTest(int keySearch)
        {
            Cake cake = null!;
            MySqlCommand cmd = new MySqlCommand("sp_getCakeById", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cakeId", keySearch);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cake = GetCake(reader);
                    }
                    reader.Close();
                }
            }
            catch
            {
                Console.WriteLine("Disconnected database");
            }
            finally
            {
                connection.Close();
            }
            return cake;
        }
        private Cake GetCake(MySqlDataReader reader)
        {
            Cake cake = new Cake();
            cake.cakeCate = new Category();
            cake.cakeId = reader.GetInt32("cake_id");
            cake.cakeName = reader.GetString("cake_name");
            cake.cakePrice = reader.GetDecimal("cake_price");
            cake.cakeDescription = reader.GetString("cake_description");
            cake.cakeQuantity = reader.GetInt32("cake_quantity");
            cake.cakeCate.categoryName = reader.GetString("category_name");
            return cake;
        }
        public Cake GetCakeById(string searchKeyWord, Cake cake)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"select cake.cake_id, cake.cake_name, cake.cake_price, cake.cake_description, cake.cake_quantity, category.category_name from cake inner join category on cake.category_id = category.category_id where cake.cake_id = '{searchKeyWord}';";
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cake.cakeId = reader.GetInt32("cake_id");
                    cake.cakeName = reader.GetString("cake_name");
                    cake.cakePrice = reader.GetDecimal("cake_price");
                    cake.cakeDescription = reader.GetString("cake_description");
                    cake.cakeQuantity = reader.GetInt32("cake_quantity");
                    cake.cakeCategory = reader.GetString("category_name");
                }
                else
                {
                    cake.cakeId = -1;
                }
                reader.Close();
            }
            catch
            {
                cake.cakeId = -1;
            }
            finally
            {
                connection.Close();
            }
            return cake;
        }
        
        public Cake GetByName(string key, Cake cake)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"select cake.cake_id, cake.cake_name, cake.cake_price, cake.cake_description, cake.cake_quantity, category.category_name from cake inner join category on cake.category_id = category.category_id where category.category_name = '{key}' ;";
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cake.cakeId = reader.GetInt32("cake_id");
                    cake.cakeName = reader.GetString("cake_name");
                    cake.cakePrice = reader.GetDecimal("cake_price");
                    cake.cakeDescription = reader.GetString("cake_description");
                    cake.cakeQuantity = reader.GetInt32("cake_quantity");
                    cake.cakeCategory = reader.GetString("category_name");

                }
                reader.Close();

            }
            catch

            {
                Console.WriteLine("Ko kn dc dtb!");
            }
            finally
            {
                connection.Close();
            }
            return cake;
        }
        public Cake GetByCT(string key, Cake cake)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"select cake.cake_id, cake.cake_name, cake.cake_price, cake.cake_description, cake.cake_quantity, category.category_name from cake inner join category on cake.category_id = category.category_id where cake.cake_name = '{key}';";
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cake.cakeId = reader.GetInt32("cake_id");
                    cake.cakeName = reader.GetString("cake_name");
                    cake.cakePrice = reader.GetDecimal("cake_price");
                    cake.cakeDescription = reader.GetString("cake_description");
                    cake.cakeQuantity = reader.GetInt32("cake_quantity");
                    cake.cakeCategory = reader.GetString("category_name");

                }
                reader.Close();

            }
            catch

            {
                Console.WriteLine("Ko kn dc dtb!");
            }
            finally
            {
                connection.Close();
            }
            return cake;
        }


        


        public List<Cake> GetByCtList(List<Cake> list, string commandText)
        {
            lock (connection)
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    // cmd.CommandText = commandText;
                    cmd.CommandText = $"select cake.cake_id, cake.cake_name, cake.cake_price, cake.cake_description, cake.cake_quantity, category.category_name from cake inner join category on cake.category_id = category.category_id where category.category_name = '{commandText}' ;";

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Cake cake = new Cake();
                        cake.cakeId = reader.GetInt32("cake_id");
                        cake.cakeName = reader.GetString("cake_name");
                        cake.cakePrice = reader.GetDecimal("cake_price");
                        cake.cakeDescription = reader.GetString("cake_description");
                        cake.cakeQuantity = reader.GetInt32("cake_quantity");
                        cake.cakeCategory = reader.GetString("category_name");
                        list.Add(cake);
                    }
                    reader.Close();
                }
                catch
                {
                    Console.WriteLine("Disconnected database");
                }
                finally
                {
                    connection.Close();
                }
            return list;
        }

        public List<Cake> GetByNameList(List<Cake> list, string commandText)
        {
            lock (connection)
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    // cmd.CommandText = commandText;
                    cmd.CommandText = $"select cake.cake_id, cake.cake_name, cake.cake_price, cake.cake_description, cake.cake_quantity, category.category_name from cake inner join category on cake.category_id = category.category_id where cake.cake_name = '{commandText}';";

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Cake cake = new Cake();
                        cake.cakeId = reader.GetInt32("cake_id");
                        cake.cakeName = reader.GetString("cake_name");
                        cake.cakePrice = reader.GetDecimal("cake_price");
                        cake.cakeDescription = reader.GetString("cake_description");
                        cake.cakeQuantity = reader.GetInt32("cake_quantity");
                        cake.cakeCategory = reader.GetString("category_name");
                        list.Add(cake);
                    }
                    reader.Close();
                }
                catch
                {
                    Console.WriteLine("Disconnected database");
                }
                finally
                {
                    connection.Close();
                }
            return list;
        }


        public int Update(Cake cake)
        {
            MySqlCommand cmd = new MySqlCommand("updateProduct", connection);
            // Cake cake = new Cake();
            int rs = 1;
            connection.Open();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productId", cake.cakeId);
                cmd.Parameters.AddWithValue("@productName", cake.cakeName);
                cmd.Parameters.AddWithValue("@productCt", cake.cakeCategory);
                cmd.Parameters.AddWithValue("@productD", cake.cakeDescription);
                cmd.Parameters.AddWithValue("@productPrice", cake.cakePrice);
                cmd.Parameters.AddWithValue("@productQty", cake.cakeAmount);
                cmd.ExecuteNonQuery();
                // return true;
            }
            catch
            {
                rs = 0;
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return rs;
        }

        public int Delete(Cake cake)
        {
            int rs = 1;
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("deletePr", connection);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", cake.cakeId);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                rs = 0;
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return rs;
        }

        public int InS(Cake cake)
        {
            int rs = 1;
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("In_Cake", connection);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@category_idNew", cake.cakeCategory);
                cmd.Parameters.AddWithValue("@cake_nameNew", cake.cakeName);
                cmd.Parameters.AddWithValue("@cake_desNew", cake.cakeDescription);
                cmd.Parameters.AddWithValue("@cake_priceNew", cake.cakePrice);
                cmd.Parameters.AddWithValue("@cake_quanNew", cake.cakeAmount);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                rs = 0;
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return rs;

        }
        public List<Cake> getAll()
        {

            List<Cake> list = new List<Cake>();
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("getAll", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            MySqlDataReader reader = cmd.ExecuteReader();
            // reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Cake cake = new Cake();
                cake.cakeId = reader.GetInt32("cake_id");
                cake.cakeName = reader.GetString("cake_name");
                cake.cakePrice = reader.GetDecimal("cake_price");
                cake.cakeDescription = reader.GetString("cake_description");
                cake.cakeQuantity = reader.GetInt32("cake_quantity");
                cake.cakeCategory = reader.GetString("category_name");
                list.Add(cake);
            }
            connection.Close();
            return list;
        }

        public List<Cake> GetAllC(Staff staff)
        {
            List<Cake> list = new List<Cake>();
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("sp_getAllBook", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Cake cake = new Cake();
                cake.cakeCate = new Category();
                cake.cakeId = reader.GetInt32("cake_id");
                cake.cakeName = reader.GetString("cake_name");
                cake.cakePrice = reader.GetDecimal("cake_price");
                cake.cakeDescription = reader.GetString("cake_description");
                cake.cakeQuantity = reader.GetInt32("cake_quantity");
                cake.cakeCate.categoryName = reader.GetString("category_name");

                list.Add(cake);
            }
            connection.Close();
            return list;
        }


    }
}







