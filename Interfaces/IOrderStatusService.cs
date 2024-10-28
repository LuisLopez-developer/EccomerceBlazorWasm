using EccomerceBlazorWasm.Models.ViewModel;

namespace EccomerceBlazorWasm.Interfaces
{
    public interface IOrderStatusService
    {
        Task<List<OrderStatusViewModel>> GetAllAsync();
    }
}
