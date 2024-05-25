using Blazored.LocalStorage;
using EccomerceBlazorWasm.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using EccomerceBlazorWasm.Models.ViewModel;

namespace EccomerceBlazorWasm.Services
{
    public class ProductCategoryService : IProductCategory
    {
        private readonly HttpClient _httpClient;

        public ProductCategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        private readonly JsonSerializerOptions jsonSerializerOptions =
          new()
          {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          };

        public async Task<ProductCategoryViewModel> CreateAsync(ProductCategoryViewModel productCategory)
        {
            var response = await _httpClient.PostAsJsonAsync<ProductCategoryViewModel>($"api/productCategory", productCategory);
            var productCategoryObject = await response.Content.ReadAsStringAsync();
            var createProductCategory = JsonSerializer.Deserialize<ProductCategoryViewModel>(productCategoryObject, jsonSerializerOptions);
            return createProductCategory;
        }

        public async Task<List<ProductCategoryViewModel>> SearchAsync(string name)
        {
            var productCategories = await _httpClient.GetFromJsonAsync<List<ProductCategoryViewModel>>($"api/ProductCategory/search?name={name}");
            return productCategories;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/productCategory/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<List<ProductCategoryViewModel>> GetAllAsync()
        {
            var productCategory = await _httpClient.GetFromJsonAsync<List<ProductCategoryViewModel>>("api/ProductCategory/GetAll");
            return productCategory;
        }

        public async Task<ProductCategoryViewModel> GetByIdAsync(int id)
        {
            var productCategory = await _httpClient.GetFromJsonAsync<ProductCategoryViewModel>($"api/ProductCategory/{id}");
            return productCategory;
        }

        public async Task<bool> UpdateAsync(int id, ProductCategoryViewModel productCategory)
        {
            var emptyContent = new StringContent(JsonSerializer.Serialize(productCategory), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/ProductCategory/{id}", emptyContent);
            return response.IsSuccessStatusCode;
        }
    }
}
