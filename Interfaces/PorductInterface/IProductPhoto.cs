using EccomerceBlazorWasm.Models.Product;

namespace EccomerceBlazorWasm.Interfaces.PorductInterface
{
    public interface IProductPhoto
    {
        Task<List<ProductPhotoViewModel>> SearchByProductIdAsync(int productId);
        Task<List<ProductPhotoViewModel>> CreateAsync(int productId, List<ProductPhotoViewModel> productPhoto);
        Task<bool> UpdateAsync(int id, ProductPhotoViewModel productPhoto);
        Task<bool> DeleteAsync(int id);
    }
}
