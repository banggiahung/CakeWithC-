namespace Persistence
{
    public class Cake
    {
        public int cakeId { get; set; }
        public string? cakeCategory { get; set; }
        public Category? cakeCate { get; set; }

        public string? cakeName { get; set; }
        public decimal cakePrice { get; set; }
        public string? cakeDescription { get; set; }
        public int cakeQuantity { get; set; }
        public decimal cakeAmount { get; set; }

        // public string? codeC { get; set; }


    }
}