using Persistence;
using DAL;
using System.Text.RegularExpressions;
using LineAdd;

namespace BL
{
    public class CakesBL
    {
        private CakesDAL cakeDal = new CakesDAL();
        public Cake GetCakeByIdT(int keySearch)
        {
            return cakeDal.GetCakeByIdTest(keySearch);
        }
        public List<Cake> GetAllCakeTest(Staff staff)
        {
            return cakeDal.GetAllC(staff);
        }

        public Cake SearchCakeByID(string searchKeyWord)
        {
            Cake cake = new Cake();
            cake = cakeDal.GetCakeById(searchKeyWord, cake);
            string search = '"' + searchKeyWord + '"';
            if (cake.cakeId <= 0)
            {
                Console.WriteLine($"Không tồn tại sản phẩm phù hợp với từ khoá là {search}");
            }
            else
            {
                ShowCakeDetail(cake, searchKeyWord);
            }
            return cake;
        }


        public Cake SearchName(string key)

        {
            Cake cake = new Cake();
            cake = cakeDal.GetByName(key, cake);
            string search = '"' + key + '"';
            if (cake.cakeCategory != key)
            {
                MenuName(cake, key);
            }
            else
            {
                Console.WriteLine("Không có sản phẩm!");
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            return cake;
        }

        public Cake SearchCT(string key)
        {
            Cake cake = new Cake();
            cake = cakeDal.GetByCT(key, cake);
            string search = '"' + key + '"';
            if (cake.cakeCategory != key)
            {
                MenuName(cake, key);

            }
            else
            {
                Console.WriteLine("Không có sản phẩm!");
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            return cake;
        }



        // IN TẤT CẢ DANH SÁCH BÁNH CHO USER



        // IN DANH SÁNH THEO TÊN 
        public void MenuName(Cake cake, string key)
        {
            List<Cake> list = new List<Cake>();
            list = cakeDal.GetByNameList(list, key);
            list = cakeDal.GetByCtList(list, key);
            if (list.Count == 0)
            {
                Console.WriteLine($"Không tồn tại sản phẩm phù hợp với từ khoá là '{key}'");
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            else
            {
                int size = 10;
                int page = 1;
                int pages = (int)Math.Ceiling((double)list.Count / size);
                int i, k = 0;
                string choice, price;
                for (; ; )
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    LineAdd.Line.LineA(150);
                    Console.WriteLine($@"                                                                                                      
 _____ _                          _ _          ___    _   
|_   _| |_ ___    ___ ___ ___ _ _| | |_    ___|  _|  |_|  
  | | |   | -_|  |  _| -_|_ -| | | |  _|  | . |  _|   _   
  |_| |_|_|___|  |_| |___|___|___|_|_|    |___|_|    |_|   {key,-49} ");
                    LineAdd.Line.LineB(50);
                    Console.WriteLine($" Tìm thấy khoảng {list.Count} sản phẩm                                                                                       Trang {page}/{pages} ");
                    LineAdd.Line.LineA(150);
                    Console.WriteLine("| Mã Bánh | Tên bánh                                                            | Giá           | Loại                         |");
                    if (list.Count < size)
                    {
                        for (i = 0; i < list.Count; i++)
                        {
                            price = Line.FormatCurrency(list[i].cakePrice.ToString());
                            LineAdd.Line.LineB(50);
                            Console.WriteLine($"| {list[i].cakeId,-7} | {list[i].cakeName,-67} | {price,-13} | {list[i].cakeCategory,-28} |");
                        }
                    }
                    else
                    {
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            if (i == list.Count) break;
                            price = Line.FormatCurrency(list[i].cakePrice.ToString());
                            LineAdd.Line.LineB(50);
                            Console.WriteLine($"| {list[i].cakeId,-7} | {list[i].cakeName,-67} | {price,-13} | {list[i].cakeCategory,-28} |");
                        }
                    }
                    LineAdd.Line.LineA(150);
                    Console.WriteLine("Nhập P để xem trang trước.");
                    Console.WriteLine("Nhập N để xem trang sau.");
                    Console.WriteLine("Nhập P kèm số trang để xem trang mong muốn (VD: P1, P2,...).");
                    Console.WriteLine("Nhập 0 để quay lại.");
                    LineAdd.Line.LineB(50);
                    Console.Write("Chọn: ");
                    choice = Console.ReadLine() ?? "";
                    while (!(Regex.IsMatch(choice, @"([PpNn]|[1-9]|^0$)")))
                    {
                        Console.Write("Lựa chọn không hợp lệ! Chọn lại: ");
                        choice = Console.ReadLine() ?? "";
                    }
                    choice = choice.Trim();
                    choice = choice.ToLower();
                    string number = Regex.Match(choice, @"\d+").Value;
                    string pageNum = "p" + number;
                    if (choice == "n")
                    {
                        if (page == pages)
                        {
                            Line.WaitForButton("Không có trang sau! Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            page = page + 1;
                            k = k + 10;
                        }
                    }
                    else if (choice == "p")
                    {
                        if (page == 1)
                        {
                            Line.WaitForButton("Không có trang trước! Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            page = page - 1;
                            k = k - 10;
                        }
                    }
                    else if (choice == pageNum)
                    {
                        if (int.Parse(number) < 0 || int.Parse(number) > pages || int.Parse(number) == 0)
                        {
                            Console.WriteLine($"Không tồn tại trang {int.Parse(number)}");
                            Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            page = int.Parse(number);
                            k = (int.Parse(number) - 1) * 10;
                        }
                    }
                    else if (choice == "0") return;
                    else
                    {
                        bool found = false;
                        string search1 = '"' + choice + '"';
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            try
                            {
                                if (int.Parse(choice) == list[i].cakeId)
                                {
                                    ShowCakeDetail(list[i], search1);
                                    Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                                    found = true;
                                    break;
                                }
                            }
                            catch (System.FormatException) { }
                            catch (System.ArgumentOutOfRangeException) { }
                        }
                        if (!(found))
                        {
                            Console.WriteLine("ID không phù hợp!");
                            Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        }
                    }
                }
            }
        }



        // IN THÔNG TIN CHI TIẾT
        private void ShowCakeDetail(Cake cake, string search)

        {

            Console.Clear();
            string price = Line.FormatCurrency(cake.cakePrice.ToString());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($@"                                                                         
 _____ _                          _ _          ___    _   
|_   _| |_ ___    ___ ___ ___ _ _| | |_    ___|  _|  |_|  
  | | |   | -_|  |  _| -_|_ -| | | |  _|  | . |  _|   _   
  |_| |_|_|___|  |_| |___|___|___|_|_|    |___|_|    |_|    ");
            LineAdd.Line.LineA(60);
            Console.WriteLine($"| Mã bánh:           | {cake.cakeId,-50} ");
            LineAdd.Line.LineB(50);
            Console.WriteLine($"| Tên bánh:          | {cake.cakeName,-50} ");
            LineAdd.Line.LineB(50);
            Console.WriteLine($"| Giá:               | {price,-50} ");
            LineAdd.Line.LineB(50);
            Console.WriteLine($"| Phân loại:         | {cake.cakeCategory,-50} ");
            LineAdd.Line.LineB(50);
            Console.WriteLine($"| Số lượng:          | {cake.cakeQuantity,-50} ");
            LineAdd.Line.LineB(50);
            Console.Write("| Mô tả:             |");
            string str = ' ' + cake.cakeDescription;
            string subStr;
            int i = 65;
            try
            {
                while (str.Length > 0 && i < str.Length)
                {
                    i = 65;
                    while (str[i] != ' ')
                    {
                        i = i + 1;
                        if (i >= str.Length) break;
                    }
                    subStr = str.Substring(1, i);
                    Console.WriteLine($" {subStr,-70} ");
                    Console.Write("                    ");
                    str = str.Remove(0, i);
                }
            }
            catch (System.ArgumentOutOfRangeException) { }
            finally
            {
                Console.WriteLine($" {str.Remove(0, 1),-70} ");
                LineAdd.Line.LineA(60);
            }
        }
        public string FormatCurrency(string currency)
        {
            for (int k = currency.Length - 3; k > 0; k = k - 3)
            {
                currency = currency.Insert(k, ".");
            }
            currency = currency + " VND";
            return currency;
        }
        public void ShowCakeDetailTest(Cake cake, string search)
        {
            Console.Clear();
            string price = (cake.cakePrice.ToString());
            LineAdd.Line.LineA(80);
            Console.WriteLine($@"                                                                                
 _____ _                          _ _          ___    _   
|_   _| |_ ___    ___ ___ ___ _ _| | |_    ___|  _|  |_|  
  | | |   | -_|  |  _| -_|_ -| | | |  _|  | . |  _|   _   
  |_| |_|_|___|  |_| |___|___|___|_|_|    |___|_|    |_|         {search,-30}   ");
            LineAdd.Line.LineA(80);
            Console.WriteLine($"| Cake id:           | {cake.cakeId,-70} ");
            LineAdd.Line.LineB(50);
            Console.WriteLine($"| Cake Name:         | {cake.cakeName,-70} ");
            LineAdd.Line.LineB(50);
            Console.WriteLine($"| Price:             | {price,-70} ");
            LineAdd.Line.LineB(50);
            Console.WriteLine($"| Type:              | {cake.cakeCate!.categoryName,-70} ");
            LineAdd.Line.LineB(50);
            Console.WriteLine($"| Quantity:          | {cake.cakeQuantity,-70} ");
            LineAdd.Line.LineB(50);
            Console.Write("| Description:       ");
            string str = ' ' + cake.cakeDescription;
            string subStr;
            int i = 65;
            try
            {
                while (str.Length > 0 && i < str.Length)
                {
                    i = 65;
                    while (str[i] != ' ')
                    {
                        i = i + 1;
                        if (i >= str.Length)
                        {
                            break;
                        }
                    }
                    subStr = str.Substring(1, i);
                    Console.WriteLine($" {subStr,-70} ");
                    Console.Write("|                    ");
                    str = str.Remove(0, i);
                }
            }
            catch (System.ArgumentOutOfRangeException) { }
            finally
            {
                Console.WriteLine($" {str.Remove(0, 1),-70} |");
                LineAdd.Line.LineA(80);
            }
        }



        public int UpdatePro(Cake cake)
        {
            Console.Clear();
            int rs;
            Console.ForegroundColor = ConsoleColor.Green;
            LineAdd.Line.LineA(80);
            Console.WriteLine(@"                                                                                               
 _____       _     _          _____           _         _       
|  |  |___ _| |___| |_ ___   |  _  |___ ___ _| |_ _ ___| |_ ___ 
|  |  | . | . | .'|  _| -_|  |   __|  _| . | . | | |  _|  _|_ -|
|_____|  _|___|__,|_| |___|  |__|  |_| |___|___|___|___|_| |___|
      |_|                                                                         ");
            LineAdd.Line.LineA(80);
            Console.ResetColor();
            Console.Write("Nhập Id sản phẩm: ");
            // int.TryParse(Console.ReadLine(), out id);
            cake.cakeId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Nhập tên sản phẩm: ");
            cake.cakeName = Console.ReadLine();
            Console.Write("Nhập danh mục sản phẩm: ");
            cake.cakeCategory = Console.ReadLine();
            Console.Write("Nhập mô tả sản phẩm: ");
            cake.cakeDescription = Console.ReadLine();
            Console.Write("Nhập giá sản phẩm: ");
            cake.cakePrice = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Nhập số lượng sản phẩm: ");
            cake.cakeAmount = Convert.ToDecimal(Console.ReadLine());
            rs = cakeDal.Update(cake);
            if (rs == 1)
            {

                Console.WriteLine("Update thành công");
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            else
            {
                Console.WriteLine("Không tìm thấy ID sản phẩm");
            }
            return rs;
        }

        public int deletePro(Cake cake)
        {
            Console.Clear();
            int rs;
            Console.ForegroundColor = ConsoleColor.Green;
            LineAdd.Line.LineA(80);
            Console.WriteLine(@"                                                                                             
 ____      _     _          _____           _         _       
|    \ ___| |___| |_ ___   |  _  |___ ___ _| |_ _ ___| |_ ___ 
|  |  | -_| | -_|  _| -_|  |   __|  _| . | . | | |  _|  _|_ -|
|____/|___|_|___|_| |___|  |__|  |_| |___|___|___|___|_| |___|
                                                                                ");
            LineAdd.Line.LineA(80);
            Console.ResetColor();
            Console.Write("Nhập Id sản phẩm: ");
            cake.cakeId = Convert.ToInt32(Console.ReadLine());
            rs = cakeDal.Delete(cake);
            if (rs == 1)
            {

                Console.WriteLine($"Đã xóa sản phẩm với ID là {cake.cakeId}");
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            else
            {
                Console.WriteLine($"Không tìm thấy ID sản phẩm la {cake.cakeId} ");
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");

            }
            return rs;

        }

        public int InPro(Cake cake)
        {
            int result;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            LineAdd.Line.LineA(80);
            Console.WriteLine(@"                                                                             
 _____   _   _    _____           _         _       
|  _  |_| |_| |  |  _  |___ ___ _| |_ _ ___| |_ ___ 
|     | . | . |  |   __|  _| . | . | | |  _|  _|_ -|
|__|__|___|___|  |__|  |_| |___|___|___|___|_| |___|
                                                                  ");
            LineAdd.Line.LineA(80);
            Console.ResetColor();
            Console.Write("Mã danh mục sản phẩm: ");
            cake.cakeCategory = Console.ReadLine();
            Console.Write("Tên sản phẩm: ");
            cake.cakeName = Console.ReadLine();
            Console.Write("Giá sản phẩm: ");
            cake.cakePrice = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Mô tả sản phẩm: ");
            cake.cakeDescription = Console.ReadLine();
            Console.Write("Số lượng sản phẩm: ");
            cake.cakeAmount = Convert.ToInt32(Console.ReadLine());
            result = cakeDal.InS(cake);
            if (result == 1)
            {
                Console.WriteLine("Thêm thành công!!!");
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");

            }
            else
            {
                Console.WriteLine("Thêm không thành công!! Đã tồn tại sản phẩm");
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            return result;


        }

        public void ShowCakeAdmin()
        {
            var list = new List<Cake>();
            list = cakeDal.getAll();
            if (list.Count == 0)
            {
                Console.WriteLine("Không có sản phẩm!");
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            else
            {
                int size = 10;
                int page = 1;
                int pages = (int)Math.Ceiling((double)list.Count / size);
                int i, k = 0;
                string choice, price;
                for (; ; )
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    LineAdd.Line.LineA(150);
                    Console.WriteLine(@"                                                                                                               
                                         _____ __    __       _____           _         _       
                                        |  _  |  |  |  |     |  _  |___ ___ _| |_ _ ___| |_ ___ 
                                        |     |  |__|  |__   |   __|  _| . | . | | |  _|  _|_ -|
                                        |__|__|_____|_____|  |__|  |_| |___|___|___|___|_| |___|
                                                                                                         ");
                    Console.WriteLine($"                                                                                                                      Trang {page}/{pages}         ");
                    LineAdd.Line.LineA(150);
                    Console.WriteLine("| Mã bánh  | Tên bánh                                                            | Giá           | Loại                                 ");
                    if (list.Count < size)
                    {
                        for (i = 0; i < list.Count; i++)
                        {
                            price = Line.FormatCurrency(list[i].cakePrice.ToString());
                            LineAdd.Line.LineB(150);
                            Console.WriteLine($"| {list[i].cakeId,-7}  | {list[i].cakeName,-67} | {price,-13} | {list[i].cakeCategory,-28} ");
                        }
                    }
                    else
                    {
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            if (i == list.Count) break;
                            price = Line.FormatCurrency(list[i].cakePrice.ToString());
                            LineAdd.Line.LineB(150);

                            Console.WriteLine($"| {list[i].cakeId,-7}  | {list[i].cakeName,-67} | {price,-13} | {list[i].cakeCategory,-28} ");
                        }
                    }
                    LineAdd.Line.LineA(150);
                    Console.WriteLine("Nhập R để xem trang trước.");
                    Console.WriteLine("Nhập N để xem trang sau.");
                    Console.WriteLine("Nhập 0 để quay lại.");
                    LineAdd.Line.LineB(150);
                    Console.Write("Chọn: ");
                    choice = Console.ReadLine() ?? "";
                    while (!(Regex.IsMatch(choice, @"([RrNn]|[1-9]|^0$)")))
                    {
                        Console.Write("Lựa chọn không hợp lệ! Chọn lại: ");
                        choice = Console.ReadLine() ?? "";
                    }
                    choice = choice.Trim();
                    choice = choice.ToLower();
                    string number = Regex.Match(choice, @"\d+").Value;
                    string pageNum = "r" + number;
                    if (choice == "n")
                    {
                        if (page == pages)
                        {
                            Line.WaitForButton("Không có trang sau! Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            page = page + 1;
                            k = k + 10;
                        }
                    }
                    else if (choice == "r")
                    {
                        if (page == 1)
                        {
                            Line.WaitForButton("Không có trang trước! Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            page = page - 1;
                            k = k - 10;
                        }
                    }
                    else if (choice == pageNum)
                    {
                        if (int.Parse(number) < 0 || int.Parse(number) > pages || int.Parse(number) == 0)
                        {
                            Console.WriteLine($"Không tồn tại trang {int.Parse(number)}");
                            Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                        }
                        else
                        {
                            page = int.Parse(number);
                            k = (int.Parse(number) - 1) * 10;
                        }
                    }
                    else if (choice == "0")
                    {
                        return;
                    }
                    else
                    {
                        Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                    }
                }

            }
        }
    }
}
