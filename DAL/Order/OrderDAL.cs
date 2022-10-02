using MySql.Data.MySqlClient;
using Persistence;
using System.Text.RegularExpressions;

namespace DAL
{
    public class OrderDAL : IOrderDAL
    {
        private MySqlDataReader? reader;
        private MySqlConnection connection = DbConfig.GetConnection();


        // public List<Orders> GetOrById(int key)
        // {
        //     List<Orders> list = new List<Orders>();
        //     MySqlCommand cmd = new MySqlCommand("sp_getOrderById", connection);
        //     try
        //     {
        //         connection.Open();
        //         cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //         cmd.Parameters.AddWithValue("@orderId", key);
        //         using (MySqlDataReader reader = cmd.ExecuteReader())
        //         {
        //             while (reader.Read())
        //             {
        //                 Orders order = new Orders();
        //                 order = GetOrder(reader);
        //                 list.Add(order);
        //             }
        //             reader.Close();
        //         }
        //     }
        //     catch
        //     {
        //         Console.WriteLine("Disconnected database");
        //     }
        //     finally
        //     {
        //         connection.Close();
        //     }
        //     return list;

        // }


        public bool CreateOrder(Orders order)
        {
            if (order == null || order.cakesList == null || order.cakesList.Count == 0)
            {
                return false;
            }
            bool result = false;
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                //cmd.CommandText = $" SET  FOREIGN_KEY_CHECKS=0";
                cmd.CommandText = $"lock tables staff write, cake write, category write, customer write, order_details write, orders write";
                cmd.ExecuteNonQuery();
                MySqlTransaction trans = connection.BeginTransaction();
                cmd.Transaction = trans;
                bool check = false;
                if (order.orderCustomer == null || order.orderCustomer.customer_Name == null || order.orderCustomer.customer_Name == "")
                {
                    order.orderCustomer = new Customer() { customer_Id = 1 };
                }
                try
                {
                    Console.Write("Nhập số điện thoại khách hàng: ");
                    string cusPhone = Console.ReadLine() ?? "";
                    while (true)
                    {
                        if (Regex.Match(cusPhone, @"^(0|84)(2(0[3-9]|1[0-6|8|9]|2[0-2|5-9]|3[2-9]|4[0-9]|5[1|2|4-9]|6[0-3|9]|7[0-7]|8[0-9]|9[0-4|6|7|9])|3[2-9]|5[5|6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])([0-9]{7})$").Success)
                        {
                            break;

                        }
                        else
                        {
                            Console.WriteLine("Nhap du 10 so!");
                            Console.Write("Nhập số điện thoại khách hàng: ");
                            cusPhone = Console.ReadLine() ?? "";
                        }
                    }
                    cmd.CommandText = $"select * from customer where customer_phone = {cusPhone};";
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Console.WriteLine($"Tên khách hàng: {reader.GetString("customer_name")}");
                        order.orderCustomer.customer_Phone = reader.GetString("customer_phone");
                        order.orderCustomer.customer_Id = reader.GetInt32("customer_id");
                        check = true;
                    }
                    reader.Close();

                    if (!check)
                    {

                        Console.Write("Nhập tên khách hàng: ");
                        string cusName = Console.ReadLine() ?? "";
                        order.orderCustomer = new Customer { customer_Name = cusName, customer_Phone = cusPhone };
                        cmd.CommandText = $"insert into customer(customer_name, customer_phone) values ('{order.orderCustomer.customer_Name}', '{order.orderCustomer.customer_Phone}');";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "select LAST_INSERT_ID() as customer_id;";
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            order.orderCustomer.customer_Id = reader.GetInt32("customer_id");
                        }
                        reader.Close();
                    }

                    cmd.CommandText = $" SET  FOREIGN_KEY_CHECKS=0 ; insert into orders(customer_id, staff_id, order_status) values ('{order.orderCustomer!.customer_Id}', '{order.orderStaff!.staffId}', {OrderSTT.UNPAID});";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "select LAST_INSERT_ID() as order_id;";
                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        order.orderId = reader.GetInt32("order_id");
                    }
                    reader.Close();

                    cmd.CommandText = "SELECT order_date FROM orders ORDER BY order_id DESC LIMIT 1;";
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        order.orderDate = reader.GetDateTime("order_date");
                    }
                    reader.Close();

                    foreach (Cake cake in order.cakesList)
                    {
                        cmd.CommandText = $"select cake_price from cake where cake_id={cake.cakeId};";
                        reader = cmd.ExecuteReader();
                        if (!reader.Read())
                        {
                            throw new Exception("Không tồn tại");
                        }
                        cake.cakePrice = reader.GetDecimal("cake_price");
                        reader.Close();

                        cmd.CommandText = $"insert into order_details(order_id, cake_id, unit_price, quantity) values ({order.orderId}, {cake.cakeId}, {cake.cakePrice * cake.cakeQuantity}, {cake.cakeQuantity});";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = $"update cake set cake_quantity = cake_quantity-{cake.cakeQuantity} where cake_id={cake.cakeId};  SET FOREIGN_KEY_CHECKS=1;";
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    result = true;
                }
                catch
                {
                    Console.WriteLine("Khong ket noi dc!!!");
                    try
                    {
                        trans.Rollback();
                    }
                    catch
                    {
                        Console.WriteLine("Disconnected database");
                    }
                }
                finally
                {
                    //cmd.CommandText = $" SET FOREIGN_KEY_CHECKS=1";
                    cmd.CommandText = "unlock tables;";
                    cmd.ExecuteNonQuery();
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
            return result;
        }

        public List<Orders> GetAllPaidOrdersInDay()
        {
            List<Orders> list = new List<Orders>();
            Orders order = null!;
            MySqlCommand cmd = new MySqlCommand("sp_getPaidOrdersInDay", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        order = new Orders();
                        order = GetOrder(reader);
                        list.Add(order);
                    }
                    if (list == null || list.Count == 0)
                    {
                        reader.Close();
                        return null!;
                    }
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
            return list;
        }

        private Orders GetOrder(MySqlDataReader reader)
        {
            Orders order = new Orders();
            order.orderCustomer = new Customer();
            order.orderId = reader.GetInt32("order_id");
            order.orderDate = reader.GetDateTime("order_date");
            order.total = reader.GetDecimal("unit_price");
            order.orderCustomer.customer_Name = reader.GetString("customer_name");
            order.orderCustomer.customer_Phone = reader.GetString("customer_phone");
            return order;
        }
    }
}





