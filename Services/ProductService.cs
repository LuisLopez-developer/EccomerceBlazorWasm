using EccomerceBlazorWasm.Interfaces.ProductService;
using EccomerceBlazorWasm.Models.CreateModel;
using EccomerceBlazorWasm.Models.ViewModel;
using System.Net.Http.Json;
using System.Text;
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
            var products = await _httpClient.GetFromJsonAsync<List<ProductViewModel>>($"{api}/search?name={name}");
            return products;
        }

        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            var products = await _httpClient.GetFromJsonAsync<List<ProductViewModel>>($"{api}/GetAll");
            return products;
        }

        public async Task<ProductCreateModel> GetByIdAsync(int id)
        {
            var product = await _httpClient.GetFromJsonAsync<ProductCreateModel>($"{api}/{id}");
            return product;
        }

        public async Task<ProductCreateModel> CreateAsync(ProductCreateModel product)
        {
            var response = await _httpClient.PostAsJsonAsync($"{api}/create", product);
            var productObject = await response.Content.ReadAsStringAsync();
            var createProduct = JsonSerializer.Deserialize<ProductCreateModel>(productObject, jsonSerializerOptions);

            return createProduct;
        }

        public async Task<bool> UpdateAsync(int id, ProductCreateModel product)
        {
            var emptyContent = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{api}/{id}", emptyContent);
            return response.IsSuccessStatusCode;
        }
    }
}
