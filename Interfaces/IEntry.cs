using EccomerceBlazorWasm.Models.ViewModel;

namespace EccomerceBlazorWasm.Interfaces
{
    public interface IEntry
    {
        Task<List<EntryViewModel>> GetAllAsync();

        Task<List<EntryViewModel>> FilterByDateAsync(DateTime startDate, DateTime endDate);

        Task<List<EntryViewModel>> SearchAsync(string name);
    }
}
