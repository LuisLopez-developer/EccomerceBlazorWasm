namespace EccomerceBlazorWasm.Models
{
    public class OrdersViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string CreatedByUserName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCreatedBySameUser { get; set; }
    }
}
