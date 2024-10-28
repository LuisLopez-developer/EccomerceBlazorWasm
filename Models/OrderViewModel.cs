using EccomerceBlazorWasm.Models.ViewModel;

namespace EccomerceBlazorWasm.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string CustomerDNI { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string WorkerId { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string Status { get; set; } = string.Empty;
        public int PaymentMethodId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; } = [];
    }
}
