using EccomerceBlazorWasm.Models.CreateModel;
using EccomerceBlazorWasm.Models.ViewModel;

namespace EccomerceBlazorWasm.Interfaces
{
    public interface IProduct
    {
        Task<List<ProductViewModel>> GetAllAsync();
        Task<List<ProductViewModel>> SearchAsync(string name);
        Task<ProductCreateModel> GetByIdAsync(int id);
        Task<ProductCreateModel> CreateAsync(ProductCreateModel product);
        Task<bool> UpdateAsync(int id, ProductCreateModel product);
    }
}
