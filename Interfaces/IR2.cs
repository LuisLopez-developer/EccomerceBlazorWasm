using Microsoft.AspNetCore.Components.Forms;

namespace EccomerceBlazorWasm.Interfaces
{
    public interface IR2
    {

        Task<string> UploadImageAsync(IBrowserFile file);
        Task<IEnumerable<string>> UploadImagesAsync(IEnumerable<IBrowserFile> files);
        Task<string> DeleteObjectsByUrlAsync(List<string> urls);

    }
}
