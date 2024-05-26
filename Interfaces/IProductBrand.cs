using EccomerceBlazorWasm.Models.ViewModel;

namespace EccomerceBlazorWasm.Interfaces
{
    public interface IProductBrand
    {
        Task<List<ProductBrandViewModel>> GetAllAsync();
        Task<List<ProductBrandViewModel>> SearchAsync(string name);
        Task<ProductBrandViewModel> GetByIdAsync(int id);
        Task<ProductBrandViewModel> CreateAsync(ProductBrandViewModel productBrand);
        Task<bool> UpdateAsync(int id, ProductBrandViewModel productBrand);
        Task<bool> DeleteAsync(int id);
    }
}
