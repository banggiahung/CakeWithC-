using System.Text;
using System.Text.RegularExpressions;

namespace LineAdd
{
    public class Line
    {
        public static int Menu(string[] menu, string name)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            LineAdd.Line.LineA(150);
            Console.WriteLine($"                 {name,-5}               ");
            LineAdd.Line.LineA(150);
            Console.ResetColor();
            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {menu[i]}");
            }
            LineAdd.Line.LineA(50);

            int choice;
            do
            {
                Console.Write("Chọn: ");
                int.TryParse(Console.ReadLine(), out choice);
            } while (choice < 1 || choice > menu.Length);
            return choice;
        }
        public static void LineA(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write("═");
            }
            Console.WriteLine();
        }
        public static void LineB(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
        public static string FormatCurrency(string currency)
        {
            for (int k = currency.Length - 3; k > 0; k = k - 3)
            {
                currency = currency.Insert(k, ".");
            }
            return currency;
        }
        public static void WaitForButton(string msg)
        {
            Console.Write(msg);
            Console.ReadKey();
        }
        public static bool IsContinue(string text)
        {
            string Continue;
            bool isMatch;
            Console.Write(text);
            Continue = Console.ReadLine() ?? "";
            isMatch = Regex.IsMatch(Continue, @"^[yYnN]$");
            while (!isMatch)
            {
                Console.Write(" Chọn (Y/N)!!!: ");
                Continue = Console.ReadLine() ?? "";
                isMatch = Regex.IsMatch(Continue, @"^[yYnN]$");
            }
            if (Continue == "y" || Continue == "Y") return true;
            return false;
        }
        public static string GetPassword()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        sb.Length--;
                    }
                    continue;
                }
                Console.Write('*');

                sb.Append(cki.KeyChar);
            }
            return sb.ToString();
        }
        public static string GetP()
        {
            bool validate = false;
            string output = "";
            string pattern = @"^([^<>\s\W]){8,16}$";
            while (validate == false)
            {
                Console.Write("Mật Khẩu: ");
                output = GetPassword();
                validate = Regex.IsMatch(output, pattern);

                if (validate == false)
                {
                    Console.WriteLine("không nhập khoảng trắng hoặc không đủ kí tự!!");
                }
                else
                {
                }
            }
            return output;
        }
        public static string GetN()
        {
            bool validate = false;
            string output = "";
            string pattern = @"^([^\s\D]{10})$";
            while (validate == false)
            {
                Console.Write("Sđt: ");
                output = Console.ReadLine() ?? "";
                validate = Regex.IsMatch(output, pattern);

                if (validate == false)
                {
                    Console.WriteLine("không nhập khoảng trắng hoặc không đủ 10 số!!");
                }
                else
                {
                }
            }
            return output;
        }
        public static string GetMoney()
        {
            bool validate = false;
            string output = "";
            string pattern = @"^([^\s\D]{9})$";
            while (validate == false)
            {
                output = Console.ReadLine() ?? "";
                validate = Regex.IsMatch(output, pattern);

                if (validate == false)
                {
                    Console.WriteLine("Nhập số tiền không quá 100.000.000 !!!");
                }
                else
                {
                }
            }
            return output;
        }
        public static string GetName()
        {
            bool validate = false;
            string output = "";
            string pattern = @"^([^<>\s\W]){5,30}$";
            while (validate == false)
            {
                Console.Write("Nhập tài khoản: ");
                output = Console.ReadLine() ?? "";
                validate = Regex.IsMatch(output, pattern);

                if (validate == false)
                {
                    Console.WriteLine("không nhập khoảng trắng hoặc không đủ kí tự!!");
                    Console.ReadKey();
                }
                else
                {
                }
            }
            return output;
        }

    }
}