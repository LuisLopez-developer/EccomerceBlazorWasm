using EccomerceBlazorWasm.Models.ViewModel;

namespace EccomerceBlazorWasm.Interfaces
{
    public interface IState
    {
        Task<List<StateViewModel>> GetAllAsync();
    }
}
