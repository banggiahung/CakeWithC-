using Persistence;
using BL;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System;
using DAL;
using LineAdd;

Console.InputEncoding = System.Text.Encoding.Unicode;
Console.OutputEncoding = System.Text.Encoding.Unicode;

StaffBL staffBl = new StaffBL();
OrderBL orderBl = new OrderBL();
CakesBL cakesBl = new CakesBL();
AdminBL adminBl = new AdminBL();

MainMenu();
// Menu main
void MainMenu()
{
    string[] menu = { "Đăng nhập", "Đăng ký", "Quên mật khẩu", "Admin", "de test", "Thoát" };
    string name = @" _____       _          _____ _                   _    _            _     _ 
                 /  __ \     | |        /  ___| |                 | |  | |          | |   | |
                 | /  \/ __ _| | _____  \ `--.| |__   ___  _ __   | |  | | ___  _ __| | __| |
                 | |    / _` | |/ / _ \  `--. \ '_ \ / _ \| '_ \  | |/\| |/ _ \| '__| |/ _` |
                 | \__/\ (_| |   <  __/ /\__/ / | | | (_) | |_) | \  /\  / (_) | |  | | (_| |
                  \____/\__,_|_|\_\___| \____/|_| |_|\___/| .__/   \/  \/ \___/|_|  |_|\__,_|
                                                          | |                                
                                                          |_|                                ";
    int choice;
    Staff staff = new Staff();


    while (true)
    {
        choice = Line.Menu(menu, name);
        switch (choice)
        {
            case 1:
                staffBl.LoginUser();
                MenuStore(staff);
                break;
            case 2:
                staffBl.RegU(staff);
                MainMenu();
                break;
            case 3:
                CreateToken();
                break;
            case 4:
                AdminS();
                break;
            case 5:
                orderBl.CreateOrderTest(staff);
                break;
            case 6:
                if (Line.IsContinue("Bạn có chắc là muốn thoát? (Y/N): "))
                {
                    Console.WriteLine("Đã thoát ứng dụng!");
                    Environment.Exit(0);
                }
                break;
        }
    }
}

// Menu cho Admin

void MenuAdmin(AdminS admin)
{
    string[] menuA = { "Chức năng dành cho ADMIN ", "Xem tổng hóa đơn", "Đăng xuất" };
    string nameM = @"    ___  _____ ___  __ __ __  __ 
                    ||=|| ||  ) || \/ | || ||\\|| 
                    || || ||_// ||    | || || \|| ";
    int ch;
    do
    {
        ch = Line.Menu(menuA, nameM);
        switch (ch)
        {
            case 1:
                MenuUp(admin);
                break;
            case 2:
                orderBl.ShowTran();
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                break;
            case 3:
                if (Line.IsContinue("Bạn có muốn đăng xuất? (Y/N): "))
                {
                    Console.WriteLine("Đăng xuất thành công!");
                    Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                }
                else
                {
                    MenuAdmin(admin);
                }
                break;
        }
    } while (ch != menuA.Length);
}



// Menu cập nhật cho admin

void MenuUp(AdminS admin)
{
    string[] menu = { "Thêm sản phẩm", "Xem tất cả sản phẩm", "Xóa sản phẩm", "Sửa sản phẩm ", "Thoát" };
    string name = @"___________    .___.__   __   .__            ____      __________                     .___               __           
                 \_   _____/  __| _/|__|_/  |_ |__|  ____    / ___\     \______   \_______   ____    __| _/__ __   ____ _/  |_  ______ 
                  |    __)_  / __ | |  |\   __\|  | /    \  / /_/  >     |     ___/\_  __ \ /  _ \  / __ ||  |  \_/ ___\\   __\/  ___/ 
                  |        \/ /_/ | |  | |  |  |  ||   |  \ \___  /      |    |     |  | \/(  <_> )/ /_/ ||  |  /\  \___ |  |  \___ \  
                 /_______  /\____ | |__| |__|  |__||___|  //_____/       |____|     |__|    \____/ \____ ||____/  \___  >|__| /____  > 
                         \/      \/                     \/                                              \/            \/           \/  
                                                                                                                      ";
    int ch;
    Cake cake = new Cake();
    do
    {
        ch = Line.Menu(menu, name);
        switch (ch)
        {
            case 1:
                cakesBl.InPro(cake);
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                break;
            case 2:
                cakesBl.ShowCakeAdmin();
                break;
            case 3:
                cakesBl.deletePro(cake);
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                break;
            case 4:
                cakesBl.UpdatePro(cake);
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                break;
            case 5:
                if (Line.IsContinue("Bạn có muốn thoát? (Y/N): "))
                {
                    Console.WriteLine("Thoát thành công!");
                    Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                }
                else
                {
                    MenuAdmin(admin);
                }
                break;
            default:
                break;
        }

    } while (ch != menu.Length);


}

// Menu đăng nhập cho admin

void AdminS()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    LineAdd.Line.LineA(100);
    Console.WriteLine(@"                  ___  _____ ___  __ __ __  __ 
                 ||=|| ||  ) || \/ | || ||\\|| 
                 || || ||_// ||    | || || \||         ");
    LineAdd.Line.LineA(100);
    Console.ResetColor();
    Console.Write("Tên đăng nhập: ");
    string user = Console.ReadLine() ?? "";
    Console.Write("Mật khẩu: ");
    string passA = Line.GetPassword();
    AdminS admin = new AdminS() { admin_User = user, admin_pass = passA };
    admin = adminBl.LoginAdmin(admin);
    if (admin.adminId == 1 || admin.adminId == 2)
    {
        MenuAdmin(admin);
    }
    else
    {
        Console.WriteLine("Đăng nhập thất bại, vui lòng thử lại!");
        Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
    }
}

// Tạo token đổi mật khẩu

void CreateToken()
{
    Random res = new Random();
    RanToken ran = new RanToken();
    String str = "abcdefghijklmnopqrstuvwxyz";
    int size = 10;
    String rant = "";
    for (int i = 0; i < size; i++)
    {
        int x = res.Next(26);
        rant = rant + str[x];
    }
    Console.Clear();
    LineAdd.Line.LineA(50);
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine(@"                           
 _____     _               
|_   _|___| |_ ___ ___ ___ 
  | | | . | '_| -_|   |_ -|
  |_| |___|_,_|___|_|_|___|
                           ");
    LineAdd.Line.LineA(50);
    Console.WriteLine("Nhap sdt: ");
    ran.find = Console.ReadLine();
    ran.Token = rant;
    int result = ran.RanDom(DbConfig.OpenConnection());
    if (result == 0)
    {
        Console.WriteLine("Tạo không thành công!!");
        Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
        MainMenu();
    }
    else
    {
        Console.WriteLine("Tạo thành công!!!");
        Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
        Change();
    }
}

// đổi mật khẩu
void Change()
{

    NewPassword newPassword = new NewPassword();
    RanToken ranT = new RanToken();
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    LineAdd.Line.LineA(100);
    Console.WriteLine(@" ____ __  __  ___  __  __  ____  _____    _____  ___    __   __ __    __ _____  _____ _____ 
((    ||==|| ||=|| ||\\|| (( ___ ||==     ||_// ||=||  ((   ((  \\ /\ //((   )) ||_// ||  ) 
 \\__ ||  || || || || \||  \\_|| ||___    ||    || || \_)) \_))  \V/\V/  \\_//  || \\ ||_//               ");
    LineAdd.Line.LineA(100);
    Console.ResetColor();
    Console.Write("Sđt: ");
    newPassword.phoneN = Console.ReadLine();
    Console.WriteLine("Token đã gửi về sdt của b!!");
    Console.Write("Nhập token: ");
    newPassword.tokenNew = Console.ReadLine();
    Console.Write("Nhập Pw: ");
    newPassword.newPass = Line.GetP();
    int result = newPassword.ChangePw(DbConfig.OpenConnection());
    if (result == 1)
    {
        Console.WriteLine("Đổi mật khẩu thành công!!!");
        Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
    }
    else
    {
        Console.WriteLine("Tạo mật không thành công!! vì không đúng token");
        Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
        MainMenu();
    }


}

// menu cho user

void MenuStore(Staff staff)
{
    string[] menu = { "Tìm bánh ", "Tạo đơn hàng mới", "Đăng xuất" };
    string name = @" _       _  _             _           _                                                             _      _   
                 ( )  _  ( )( )           ( )_        ( )                                                           ( )_  /'_`\ 
                 | | ( ) | || |__     _ _ | ,_)      _| |   _       _   _    _    _   _     _   _   _    _ _   ___  | ,_)(_) ) |
                 | | | | | ||  _ `\ /'_` )| |      /'_` | /'_`\    ( ) ( ) /'_`\ ( ) ( )   ( ) ( ) ( ) /'_` )/' _ `\| |     /'/'
                 | (_/ \_) || | | |( (_| || |_    ( (_| |( (_) )   | (_) |( (_) )| (_) |   | \_/ \_/ |( (_| || ( ) || |_   |_|  
                 `\___x___/'(_) (_)`\__,_)`\__)   `\__,_)`\___/'   `\__, |`\___/'`\___/'   `\___x___/'`\__,_)(_) (_)`\__)  (_)  
                                                                   ( )_| |                                                      
                                                                   `\___/'                                                      ";
    int choice;
    do
    {
        choice = Line.Menu(menu, name);
        switch (choice)
        {
            case 1:
                MenuSearchCake();
                break;

            case 2:
                orderBl.CreateOrderTest(staff);
                break;
            case 3:
                if (Line.IsContinue("Bạn có muốn đăng xuất? (Y/N): "))
                {
                    Console.WriteLine("Đăng xuất thành công!");
                    Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                    MainMenu();
                }
                else
                {
                    MenuStore(staff);
                }
                break;
        }
    } while (choice != menu.Length);
}

// tìm sản phẩm cho user

void MenuSearchCake()
{
    string[] menu = { "Xem tất cả loại bánh", "Tìm kiếm bánh theo Id", "Tìm kiếm bánh theo danh mục", "Quay lại" };
    Console.ForegroundColor = ConsoleColor.Green;
    string name = @"                                                                
 _____                 _      _____           _         _       
|   __|___ ___ ___ ___| |_   |  _  |___ ___ _| |_ _ ___| |_ ___ 
|__   | -_| .'|  _|  _|   |  |   __|  _| . | . | | |  _|  _|_ -|
|_____|___|__,|_| |___|_|_|  |__|  |_| |___|___|___|___|_| |___|
                                                                 ";
    int choice;
    do
    {
        choice = Line.Menu(menu, name);
        switch (choice)
        {
            case 1:
                cakesBl.ShowCakeAdmin();
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                break;
            case 2:
                Console.Write("Nhập id để tìm kiếm: ");
                string id = Console.ReadLine() ?? "";
                cakesBl.SearchCakeByID(id);
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                break;
            case 3:
                Console.WriteLine("Gợi ý từ khoá: \"bánh đám cưới\", \"bánh sinh nhật\",...");
                Console.Write("Nhập từ khoá để tìm kiếm: ");
                string commandText = Console.ReadLine() ?? "";
                cakesBl.SearchCT(commandText);
                Line.WaitForButton("Nhập phím bất kỳ để tiếp tục...");
                break;
            default:
                break;
        }
    } while (choice != menu.Length);
}








