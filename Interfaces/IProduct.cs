using EccomerceBlazorWasm.Models.ViewModel;

namespace EccomerceBlazorWasm.Interfaces
{
    public interface IProduct
    {
        Task<List<ProductViewModel>> GetAllAsync();
        Task<List<ProductViewModel>> SearchAsync(string name);
    }
}
