namespace EccomerceBlazorWasm.Models.Product
{
    public class ProductPhotoViewModel
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public bool IsMain { get; set; }
        public required int ProductId { get; set; }
    }
}
