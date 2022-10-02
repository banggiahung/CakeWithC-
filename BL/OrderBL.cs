using Persistence;
using DAL;
using System.Text.RegularExpressions;
using LineAdd;

namespace BL
{
    public class OrderBL
    {
        private OrderDAL orderDal = new OrderDAL();
        private CakesDAL cakesDal = new CakesDAL();
        private CakesBL cakesBl = new CakesBL();

        public bool CreateOrder(Orders order)
        {
            return orderDal.CreateOrder(order);
        }

        public void CreateOrderTest(Staff staff)
        {
            List<Cake> list = new List<Cake>();
            Orders order = new Orders();
            list = cakesBl.GetAllCakeTest(staff);
            order.orderStaff = staff;
            if (list.Count == 0)
            {
                Console.WriteLine(" Cake khong co!");
                Line.WaitForButton("Enter any key to continue...");
                return;
            }
            else
            {
                int size = 10;
                int page = 1;
                int pages = (int)Math.Ceiling((double)list.Count / size);
                int k = 0;
                string choose, price;
                while (true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    LineAdd.Line.LineA(150);
                    Console.WriteLine(@"                                                           _____  _      _     
                                                          (  _  )( )    ( )    
                                                          | (_) || |    | |    
                                                          |  _  || |  _ | |  _ 
                                                          | | | || |_( )| |_( )
                                                          (_) (_)(____/'(____/'
                     
                                                                                 ");
                    Console.WriteLine($"                                                                                                                     Page {page}/{pages} ");
                    LineAdd.Line.LineA(150);
                    Console.WriteLine("| Cake id   | Cake name                                                  | Price              | Type                   ");
                    if (list.Count < size)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            price = Line.FormatCurrency(list[i].cakePrice.ToString());
                            LineAdd.Line.LineB(150);
                            Console.WriteLine($"| {list[i].cakeId,-9}  | {list[i].cakeName,-69} | {price,-15} | {list[i].cakeCate!.categoryName,-22} ");
                        }
                    }
                    else
                    {
                        for (int i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            if (i == list.Count) break;
                            price = Line.FormatCurrency(list[i].cakePrice.ToString());
                            LineAdd.Line.LineB(150);

                            Console.WriteLine($"| {list[i].cakeId,-7}  | {list[i].cakeName,-60} | {price,-18} | {list[i].cakeCate!.categoryName,-30} ");
                        }
                    }
                    LineAdd.Line.LineA(150);
                    Console.WriteLine("Enter P to view the previous page.");
                    Console.WriteLine("Enter N to view the next page.");
                    Console.WriteLine("Enter cake id to add to order.");
                    Console.WriteLine("Enter 0 to go back.");
                    LineAdd.Line.LineB(50);
                    Console.Write("Choose: ");
                    choose = Console.ReadLine() ?? "";
                    while (true)
                    {
                        if (Regex.Match(choose, @"([PpNn]|[1-9]|^0$)").Success)
                        {
                            break;
                        }
                        else
                        {
                            Console.Write("Invalid selection! re-select: ");
                            choose = Console.ReadLine() ?? "";
                        }
                    }
                    choose = choose.Trim();
                    choose = choose.ToLower();
                    string number = Regex.Match(choose, @"\d+").Value;
                    if (choose == "n")
                    {
                        if (page == pages)
                        {
                            Line.WaitForButton("No next page! Enter any key to continue...");
                        }
                        else
                        {
                            page = page + 1;
                            k = k + 10;
                        }
                    }
                    else if (choose == "p")
                    {
                        if (page == 1)
                        {
                            Line.WaitForButton("No previous page! Enter any key to continue...");
                        }
                        else
                        {
                            page = page - 1;
                            k = k - 10;
                        }
                    }
                    else if (choose == "0")
                    {
                        return;
                    }
                    else
                    {
                        bool found = false;
                        string search = '"' + choose + '"';
                        for (int i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            try
                            {
                                if (int.Parse(choose) == list[i].cakeId)
                                {
                                    cakesBl.ShowCakeDetailTest(list[i], search);
                                    Cake cake = cakesBl.GetCakeByIdT(list[i].cakeId);
                                    found = true;
                                    if (cake.cakeId < 0)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        string strQuantity;
                                        bool isSuccess;
                                        int quantity;
                                        while (true)
                                        {
                                            Console.Write("Nhap so luong: ");
                                            strQuantity = Console.ReadLine() ?? "";
                                            isSuccess = int.TryParse(strQuantity, out quantity);
                                            while (!isSuccess)
                                            {
                                                Console.Write("!Nhap so de mua: ");
                                                strQuantity = Console.ReadLine() ?? "";
                                                isSuccess = int.TryParse(strQuantity, out quantity);
                                            }
                                            if (cake.cakeQuantity <= 0)
                                            {
                                                Console.WriteLine("Da het!");
                                                continue;
                                            }
                                            else if (quantity <= 0)
                                            {
                                                Console.WriteLine("Nhap so luong sai!");
                                                continue;
                                            }
                                            else if (quantity > cake.cakeQuantity)
                                            {
                                                Console.WriteLine("Qua so luong!");
                                                continue;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        decimal amount = (decimal)quantity * cake.cakePrice;
                                        cake.cakeQuantity = quantity;
                                        cake.cakeAmount = amount;
                                        bool add = true;
                                        if (order.cakesList == null || order.cakesList.Count == 0)
                                        {

                                            order.cakesList!.Add(cake);
                                        }
                                        else
                                        {
                                            for (i = 0; i < order.cakesList.Count; i++)
                                            {
                                                if (int.Parse(choose) == order.cakesList[i].cakeId)
                                                {
                                                    order.cakesList[i].cakeQuantity += quantity;
                                                    order.cakesList[i].cakeAmount += amount;
                                                    add = false;
                                                }
                                            }
                                            if (add)
                                            {
                                                order.cakesList.Add(cake);
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                            catch (System.FormatException) { }
                            catch (System.ArgumentOutOfRangeException) { }
                        }
                        if (!found)
                        {
                            Console.WriteLine("Khong tim thay san pham!");
                            Line.WaitForButton("Enter any key to continue...");
                        }

                        if (Line.IsContinue("Co muon thanh toan luon khong? (Y/N): "))
                        {
                            break;
                        }
                    }
                }
            }
            if (CreateOrder(order))
            {
                Console.WriteLine("Don hang da tao thanh cong!");
                Line.WaitForButton("Nhập phím bất kỳ để xem hoá đơn...");
                Console.Clear();
                string prices, amount;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                LineAdd.Line.LineA(100);
                Console.WriteLine(@" __   _____  _   _ ____    ____ ___ _     _     
 \ \ / / _ \| | | |  _ \  | __ )_ _| |   | |    
  \ V / | | | | | | |_) | |  _ \| || |   | |    
   | || |_| | |_| |  _ <  | |_) | || |___| |___ 
   |_| \___/ \___/|_| \_\ |____/___|_____|_____|
                                                                                                                              ");
                LineAdd.Line.LineA(100);
                Console.ResetColor();
                Console.WriteLine($"| Thời gian: {order.orderDate,-61}   Mã hoá đơn: {order.orderId,6} |");
                Console.WriteLine($"| Địa chỉ: Đảo Phú Quốc ");
                LineAdd.Line.LineB(100);
                Console.WriteLine("| Mặt hàng                                                            Đơn giá    SL      T.Tiền |");
                LineAdd.Line.LineB(100);
                foreach (Cake cake in order.cakesList!)
                {
                    prices = Line.FormatCurrency(cake.cakePrice.ToString());
                    amount = Line.FormatCurrency(cake.cakeAmount.ToString());
                    Console.WriteLine($"| {cake.cakeName,-65} {prices,9} {cake.cakeQuantity,5} {amount,11} |");
                    order.total += cake.cakeAmount;
                }
                string total = Line.FormatCurrency(order.total.ToString());
                Console.ForegroundColor = ConsoleColor.Yellow;
                LineAdd.Line.LineB(100);
                Console.WriteLine($"| TỔNG TIỀN PHẢI THANH TOÁN {total,63} VND |");
                LineAdd.Line.LineB(100);
                Console.WriteLine($"| Số điện thoại khách hàng: {order.orderCustomer!.customer_Phone,12} ");
                LineAdd.Line.LineB(100);
                Payment payment = new Payment();
                string paymentAmount;
                string refund;
                while (true)
                {
                    Console.Write("Nhập số tiền khách thanh toán: ");
                    paymentAmount = Line.GetMoney();
                    if (Convert.ToDecimal(paymentAmount) >= order.total)
                    {
                        payment.paymentAmount = Convert.ToDecimal(paymentAmount);
                        paymentAmount = Line.FormatCurrency(payment.paymentAmount.ToString());

                        payment.refund = payment.paymentAmount - order.total;
                        refund = Line.FormatCurrency(payment.refund.ToString());
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Số tiền bạn nhập không đúng! Vui lòng nhập lại!");
                    }
                }
                LineAdd.Line.LineB(100);
                Console.WriteLine($"| + Tổng tiền      : {total,15} VND ");
                Console.WriteLine($"| + Tiền thanh toán: {paymentAmount,15} VND ");
                Console.WriteLine($"| + Hoàn tiền      : {refund,15} VND ");
                LineAdd.Line.LineB(100);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("           CẢM ƠN QUÝ KHÁCH VÀ HẸN GẶP LẠI   ");
                Console.WriteLine("             Website: www.hungtest.cf        ");
            }

            else
            {
                Console.WriteLine("Khong co order!");
                Console.WriteLine("Tao that bai!");
            }
            Line.WaitForButton("Nhap any key de tiep tuc!");
        }
        public void ShowTran()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            OrderDAL orderDAL = new OrderDAL();
            List<Orders> ordersList = new List<Orders>();
            ordersList = orderDAL.GetAllPaidOrdersInDay();
            if (ordersList == null || ordersList.Count == 0)
            {
                Line.WaitForButton("Không có order nào cả!!");
                return;
            }
            else
            {
                int size = 10;
                int page = 1;
                int pages = (int)Math.Ceiling((double)ordersList.Count / size);
                int k = 0;
                string choose;
                while (true)
                {
                    {
                        Line.LineA(150);
                        Console.Clear();

                        Console.WriteLine(@"                                           ____              _           __ ___     __               
                                          /  _/__ _  _____  (_)______   / // (_)__ / /____  ______ __
                                         _/ // _ \ |/ / _ \/ / __/ -_) / _  / (_-</ __/ _ \/ __/ // /
                                        /___/_//_/___/\___/_/\__/\__/ /_//_/_/___/\__/\___/_/  \_, / 
                                                                                              /___/       ");
                        Console.WriteLine($"                                                                                                                     Page {page}/{pages}      ");
                        Line.LineA(150);
                        Console.WriteLine("| Order id           Tên khách hàng               Cus Phone                  Thời gian tạo              Tổng tiền             Trạng thái       ");
                        Console.WriteLine("| ----------         --------------              -------------               -------------            ------------            ------------       ");
                        if (ordersList.Count < size)
                        {
                            for (int i = 0; i < ordersList.Count; i++)
                            {
                                Console.WriteLine($"| {ordersList[i].orderId,-17} {ordersList[i].orderCustomer!.customer_Name,-30}{ordersList[i].orderCustomer!.customer_Phone,-25} {ordersList[i].orderDate,-27} {Line.FormatCurrency(ordersList[i].total.ToString()),-22} {"Đã thanh toán",-12} ");
                                Line.LineB(150);
                            }
                        }
                        else
                        {
                            for (int i = ((page - 1)) * size; i < k + 10; i++)
                            {
                                if (i == ordersList.Count) break;
                                Console.WriteLine($"| {ordersList[i].orderId,-17} {ordersList[i].orderCustomer!.customer_Name,-30}{ordersList[i].orderCustomer!.customer_Phone,-25} {ordersList[i].orderDate,-27} {Line.FormatCurrency(ordersList[i].total.ToString()),-22} {"Đã thanh toán",-12} ");
                                Line.LineB(150);

                            }
                        }
                    }
                    Line.LineA(150);
                    Console.WriteLine("Nhập P xem trang trước!!");
                    Console.WriteLine("Nhập N xem trang tiếp!!");
                    Console.WriteLine("Nhập 0 để trở về!!");
                    LineAdd.Line.LineB(50);
                    Console.Write("Choose: ");
                    choose = Console.ReadLine() ?? "";
                    while (true)
                    {
                        if (Regex.Match(choose, @"([PpNn]|[1-9]|^0$)").Success)
                        {
                            break;
                        }
                        else
                        {
                            Console.Write("Invalid selection! re-select: ");
                            choose = Console.ReadLine() ?? "";
                        }
                    }
                    choose = choose.Trim();
                    choose = choose.ToLower();
                    string number = Regex.Match(choose, @"\d+").Value;
                    if (choose == "n")
                    {
                        if (page == pages)
                        {
                            Line.WaitForButton("No next page! Enter any key to continue...");
                        }
                        else
                        {
                            page = page + 1;
                            k = k + 10;
                        }
                    }
                    else if (choose == "p")
                    {
                        if (page == 1)
                        {
                            Line.WaitForButton("No previous page! Enter any key to continue...");
                        }
                        else
                        {
                            page = page - 1;
                            k = k - 10;
                        }
                    }
                    else if (choose == "0")
                    {
                        return;

                    }
                }
            }
        }
    }
}
