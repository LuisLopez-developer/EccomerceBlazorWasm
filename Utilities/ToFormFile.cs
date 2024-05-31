using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

public static class BrowserFileExtensions
{
    public static async Task<IFormFile> ToFormFile(this IBrowserFile browserFile)
    {
        var memoryStream = new MemoryStream();
        await browserFile.OpenReadStream().CopyToAsync(memoryStream);
        memoryStream.Position = 0; // Reiniciar la posición del stream para leerlo más tarde

        return new FormFile(memoryStream, 0, browserFile.Size, browserFile.Name, browserFile.Name)
        {
            Headers = new HeaderDictionary(),
            ContentType = browserFile.ContentType
        };
    }
}
