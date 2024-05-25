using EccomerceBlazorWasm.Models.ViewModel;

namespace EccomerceBlazorWasm.Interfaces
{
    public interface IProductCategory
    {
        Task<List<ProductCategoryViewModel>> GetAllAsync();
        Task<ProductCategoryViewModel> GetByIdAsync(int id);
        Task<ProductCategoryViewModel> CreateAsync(ProductCategoryViewModel productCategory);
        Task<bool> UpdateAsync(int id, ProductCategoryViewModel productCategory);
        Task<bool> DeleteAsync(int id);
    }
}
