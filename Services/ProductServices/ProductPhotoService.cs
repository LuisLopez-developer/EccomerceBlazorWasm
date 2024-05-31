using EccomerceBlazorWasm.Interfaces.PorductInterface;
using EccomerceBlazorWasm.Models.Product;
using System.Net.Http.Json;
using System.Text.Json;

namespace EccomerceBlazorWasm.Services.ProductServices
{
    public class ProductPhotoService : IProductPhoto
    {
        private readonly string api = "api/ProductPhoto";

        private readonly HttpClient _httpClient;

        public ProductPhotoService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        private readonly JsonSerializerOptions jsonSerializerOptions =
         new()
         {
             PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
         };

        public async Task<List<ProductPhotoViewModel>> CreateAsync(int productId, List<ProductPhotoViewModel> productPhoto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{api}/create/{productId}", productPhoto);
            return await response.Content.ReadFromJsonAsync<List<ProductPhotoViewModel>>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{api}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ProductPhotoViewModel>> SearchByProductIdAsync(int productId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<ProductPhotoViewModel>>($"{api}/search?productId={productId}");
            return response;
        }

        public async Task<bool> UpdateAsync(int id, ProductPhotoViewModel productPhoto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{api}/{id}", productPhoto);
            return response.IsSuccessStatusCode;
        }
    }
}
