using EccomerceBlazorWasm.Interfaces;
using EccomerceBlazorWasm.Models.ViewModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace EccomerceBlazorWasm.Services
{
    public class ProductService : IProduct
    {
        private readonly string api = "api/Product";

        private readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        private readonly JsonSerializerOptions jsonSerializerOptions =
          new()
          {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          };

        public async Task<List<ProductViewModel>> SearchAsync(string name)
        {
            var product = await _httpClient.GetFromJsonAsync<List<ProductViewModel>>($"{api}/search?name={name}");
            return product;
        }

        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            var product = await _httpClient.GetFromJsonAsync<List<ProductViewModel>>($"{api}/GetAll");
            return product;
        }

    }
}
