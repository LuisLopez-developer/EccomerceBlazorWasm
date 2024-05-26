using EccomerceBlazorWasm.Models.ViewModel;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace EccomerceBlazorWasm.Services
{
    public class ProductBrandService
    {
        private readonly string api = "api/ProductBrand";

        private readonly HttpClient _httpClient;

        public ProductBrandService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        private readonly JsonSerializerOptions jsonSerializerOptions =
          new()
          {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          };

        public async Task<ProductBrandViewModel> CreateAsync(ProductBrandViewModel productBrand)
        {
            var response = await _httpClient.PostAsJsonAsync<ProductBrandViewModel>($"{api}", productBrand);
            var productbrandObject = await response.Content.ReadAsStringAsync();
            var createProductBrand = JsonSerializer.Deserialize<ProductBrandViewModel>(productbrandObject, jsonSerializerOptions);
            return createProductBrand;
        }

        public async Task<List<ProductBrandViewModel>> SearchAsync(string name)
        {
            var productBrands = await _httpClient.GetFromJsonAsync<List<ProductBrandViewModel>>($"{api}/search?name={name}");
            return productBrands;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _httpClient.DeleteAsync($"{api}/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<List<ProductBrandViewModel>> GetAllAsync()
        {
            var productBrand = await _httpClient.GetFromJsonAsync<List<ProductBrandViewModel>>($"{api}/GetAll");
            return productBrand;
        }

        public async Task<ProductBrandViewModel> GetByIdAsync(int id)
        {
            var productCategory = await _httpClient.GetFromJsonAsync<ProductBrandViewModel>($"{api}/{id}");
            return productCategory;
        }

        public async Task<bool> UpdateAsync(int id, ProductBrandViewModel productBrand)
        {
            var emptyContent = new StringContent(JsonSerializer.Serialize(productBrand), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{api}/{id}", emptyContent);
            return response.IsSuccessStatusCode;
        }

    }
}
