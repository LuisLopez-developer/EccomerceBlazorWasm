using EccomerceBlazorWasm.Models.CreateModel;
using EccomerceBlazorWasm.Models.ViewModel;

namespace EccomerceBlazorWasm.Interfaces
{
    public interface ILoss
    {
        Task<List<LossViewModel>> GetAllAsync();
        Task<List<LossViewModel>> SearchAsync(string name);
        Task<LossCreateModel> GetByIdAsync(int id);
        Task<LossCreateModel> CreateAsync(LossCreateModel lossCreateModel);
        Task<bool> UpdateAsync(int id, LossCreateModel lossCreateModel);
        Task<bool> DeleteAsync(int id);
    }
}
