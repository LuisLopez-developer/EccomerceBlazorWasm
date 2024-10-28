namespace EccomerceBlazorWasm.Models
{
    public class OrderRequest
    {
        public string CustomerDNI { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string WorkerId { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public int PaymentMethodId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderDetailRequestDTO> OrderItems { get; set; } = new List<OrderDetailRequestDTO>();

    }

    public class OrderDetailRequestDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

}
