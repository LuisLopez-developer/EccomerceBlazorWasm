using EccomerceBlazorWasm.Models.ViewModel;

namespace EccomerceBlazorWasm.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<List<PaymentMethodViewModel>> GetAllPaymentMethodsAsync();
    }
}
