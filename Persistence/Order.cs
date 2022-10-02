namespace Persistence
{
    public static class OrderStatus
    {
        public const int CREATE_NEW_ORDER = 1;
    }

    public class Orders
    {
        public int orderId { get; set; }
        public int UserId { get; set; }

        public Customer? orderCustomer { get; set; }
        public Cake? orderC { get; set; }
        public Staff? orderStaff { get; set; }
        public DateTime orderDate { get; set; }
        public int orderStatus { get; set; }
        public List<Cake> cakesList { get; set; }
        public decimal total { get; set; }

        public AdminS? orAdmin { get; set; }

        public Orders()
        {
            this.cakesList = new List<Cake>();
        }
    }

    public class Payment
    {
        public decimal paymentAmount { get; set; }
        public decimal refund { get; set; }
    }
}