namespace Online_shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public DateTime EnterTime { get; set; }
        public DateTime? ExitTime { get; set; }
    }
}
