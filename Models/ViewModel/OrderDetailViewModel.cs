namespace EccomerceBlazorWasm.Models.ViewModel
{
    public class OrderDetailViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } // nombre del producto
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal => Quantity * UnitPrice; // subtotal del producto
    }
}
