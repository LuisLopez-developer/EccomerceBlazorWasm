using EccomerceBlazorWasm.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace EccomerceBlazorWasm.Services
{
    public class R2Service : IR2
    {
        private readonly string api = "api/R2";
        private readonly HttpClient _httpClient;

        public R2Service(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        public async Task<string> DeleteObjectsByUrlAsync(List<string> urls)
        {
            var response = await _httpClient.DeleteAsync($"{api}/DeleteObjects/{urls}");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> UploadImageAsync(IBrowserFile file)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 2 * 1024 * 1024)); // Limitar tamaño máximo
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "file", file.Name);

            var response = await _httpClient.PostAsync($"{api}/UploadImage", content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<IEnumerable<string>> UploadImagesAsync(IEnumerable<IBrowserFile> files)
        {
            var content = new MultipartFormDataContent();
            foreach (var file in files)
            {
                var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 2 * 1024 * 1024)); // Limitar tamaño máximo
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "files", file.Name);
            }

            var response = await _httpClient.PostAsync("api/R2/UploadImages", content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        }
    }
}
