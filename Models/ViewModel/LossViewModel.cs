namespace EccomerceBlazorWasm.Models.ViewModel
{
    public class LossViewModel
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? Amount { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Reason { get; set; }
        public decimal? Total { get; set; }
    }
}
