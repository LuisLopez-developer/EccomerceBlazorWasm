using EccomerceBlazorWasm.Models;
using EccomerceBlazorWasm.Models.CreateModel;
using EccomerceBlazorWasm.Models.ViewModel;

namespace EccomerceBlazorWasm.Interfaces
{
    public interface ISaleService
    {
        Task<OrderRequest> CreateOrderAsync(OrderRequest orderCreateModel);
        Task<List<OrderViewModel>> GetAllOrdersAsync();
        Task<OrderViewModel> GetOrderByIdAsync(int id);
        Task<bool> DeleteOrderAsync(int id);
        Task<List<OrderViewModel>> SearchOrdersAsync(string searchTerm);
    }
}
