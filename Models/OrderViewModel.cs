using EccomerceBlazorWasm.Models.ViewModel;

namespace EccomerceBlazorWasm.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string CustomerDNI { get; set; }
        public string CustomerEmail { get; set; }
        public string WorkerId { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; } // descripción del estado
        public int PaymentMethodId { get; set; }
        public string PaymentMethod { get; set; } // descripción del método de pago
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}
