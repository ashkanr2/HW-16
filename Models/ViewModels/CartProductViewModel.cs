namespace My_App.Store.Demo.Models
{
    public class CartProductViewModel
    {
        public string? ProductName { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }

        public int TotalPrice()
        {
            return (Qty*Price);
        }
    }
}
