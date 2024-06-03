using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace EccomerceBlazorWasm.Interfaces
{
    public interface IR2
    {

        Task<string> UploadImageAsync(IBrowserFile file);
        Task<IEnumerable<string>> UploadImagesAsync(IEnumerable<IBrowserFile> files);
        Task<string> DeleteObjectsByUrlAsync(List<string> urls);

    }
}
