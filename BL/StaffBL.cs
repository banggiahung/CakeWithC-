using Persistence;
using DAL;
using System.Text;
using System.Text.RegularExpressions;
using LineAdd;

namespace BL
{
    public class StaffBL
    {
        private StaffDAL staffDal = new StaffDAL();

        // đăng nhập user

        public Staff LoginUser()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            LineAdd.Line.LineA(150);
            Console.WriteLine(@" _                  _                              _                                                     _     _ 
(_)                (_)           _             _  | |                                                   | |   | |
 _       ___   ____ _ ____     _| |_ ___     _| |_| |__  _____    ____  _____ _ _ _    _ _ _  ___   ____| | __| |
| |     / _ \ / _  | |  _ \   (_   _) _ \   (_   _)  _ \| ___ |  |  _ \| ___ | | | |  | | | |/ _ \ / ___) |/ _  |
| |____| |_| ( (_| | | | | |    | || |_| |    | |_| | | | ____|  | | | | ____| | | |  | | | | |_| | |   | ( (_| |
|_______)___/ \___ |_|_| |_|     \__)___/      \__)_| |_|_____)  |_| |_|_____)\___/    \___/ \___/|_|    \_)____|
             (_____|                                                                                                                      ");
            LineAdd.Line.LineA(150);
            Console.ResetColor();
            Console.Write("Tên đăng nhập: ");
            string userName = Console.ReadLine() ?? "";
            Console.Write("Mật khẩu: ");
            string password = Line.GetPassword();
            Staff staff = new Staff() { userName = userName, password = password };
            staff = staffDal.Login(staff);
            if (staff.staffRole > 0)
            {
                Console.WriteLine("Xin mời vào!!!");
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            else
            {
                Console.WriteLine("Xin mời ra!!!");
                if (Line.IsContinue("Bạn có chắc là muốn đăng kí hay không? (Y/N): "))
                {
                    RegU(staff);
                }
                else
                {
                    return LoginUser();

                }
            }

            return staffDal.Login(staff);
        }

        // đăng kí user
        public int RegU(Staff staff)
        {
            Console.Clear();
            int rs;
            Console.ForegroundColor = ConsoleColor.Green;
            LineAdd.Line.LineA(100);
            Console.WriteLine(@" ______              _                        
(_____ \            (_)       _               
 _____) )_____  ____ _  ___ _| |_ _____  ____ 
|  __  /| ___ |/ _  | |/___|_   _) ___ |/ ___)
| |  \ \| ____( (_| | |___ | | |_| ____| |    
|_|   |_|_____)\___ |_(___/   \__)_____)_|    
              (_____|                                       ");
            LineAdd.Line.LineA(100);
            Console.ResetColor();
            Console.Write("Tên của bạn: ");
            staff.staffName = Console.ReadLine();
            staff.staffPhone = Line.GetN();
            Console.Write("Thành Phố: ");
            staff.staffAddress = Console.ReadLine();
            staff.userName = Line.GetName();
            staff.password = Line.GetP();
            rs = staffDal.RegUser(staff);
            if (rs == 1)
            {
                Console.WriteLine("Tạo tài khoản thành công!!");
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            else
            {
                Console.WriteLine("Tạo tài khong khoản thành công!!! Đã tồn tại SĐT");
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
            }
            return rs;
        }
    }

}