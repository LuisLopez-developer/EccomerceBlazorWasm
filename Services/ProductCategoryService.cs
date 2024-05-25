using Blazored.LocalStorage;
using EccomerceBlazorWasm.Interfaces;
using EccomerceBlazorWasm.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

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

        public async Task<ProductCategory> CreateAsync(ProductCategory productCategory)
        {
            var response = await _httpClient.PostAsJsonAsync<ProductCategory>($"api/productCategory", productCategory);
            var productCategoryObject = await response.Content.ReadAsStringAsync();
            var createProductCategory = JsonSerializer.Deserialize<ProductCategory>(productCategoryObject, jsonSerializerOptions);
            return createProductCategory;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/productCategory{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<List<ProductCategory>> GetAllAsync()
        {
            var productCategory = await _httpClient.GetFromJsonAsync<List<ProductCategory>>("api/ProductCategory/GetAll");
            return productCategory;
        }

        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            var productCategory = await _httpClient.GetFromJsonAsync<ProductCategory>($"api/ProductCategory/{id}");
            return productCategory;
        }

        public async Task<bool> UpdateAsync(int id, ProductCategory productCategory)
        {
            var emptyContent = new StringContent(JsonSerializer.Serialize(productCategory), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/ProductCategory{id}", emptyContent);
            return response.IsSuccessStatusCode;
        }
    }
}
