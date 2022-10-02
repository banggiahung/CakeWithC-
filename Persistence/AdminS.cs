namespace Persistence
{
    public class AdminS
    {
        public int adminId { get; set; }
        public string? admin_User { get; set; }
        public string? admin_pass { get; set; }
        public string? admin_name { get; set; }
        public int checkA { get; set; }
        public Staff? orderStaff { get; set; }
        public Customer? orderCustomer { get; set; }


    }
}

