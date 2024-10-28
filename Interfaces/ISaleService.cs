using EccomerceBlazorWasm.Models;

namespace EccomerceBlazorWasm.Interfaces
{
    public interface ISaleService
    {
        Task<OrderRequest> CreateOrderAsync(OrderRequest orderCreateModel);
        Task<List<OrdersViewModel>> GetAllOrdersAsync();
        Task<OrderViewModel> GetOrderByIdAsync(int id);
        Task<bool> DeleteOrderAsync(int id);
        Task<List<OrderViewModel>> SearchOrdersAsync(string searchTerm);
    }
}
