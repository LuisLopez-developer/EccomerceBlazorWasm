using EccomerceBlazorWasm.Interfaces;
using EccomerceBlazorWasm.Models;
using EccomerceBlazorWasm.Models.CreateModel;
using EccomerceBlazorWasm.Models.ViewModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace EccomerceBlazorWasm.Services
{
    public class SaleService : ISaleService
    {

    private readonly string api = "api/system/Order";
    private readonly HttpClient _httpClient;

        public SaleService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        private readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public async Task<OrderRequest> CreateOrderAsync(OrderRequest orderCreateModel)
        {
            var response = await _httpClient.PostAsJsonAsync($"{api}", orderCreateModel);
            var orderObject = await response.Content.ReadAsStringAsync();
            var createdOrder = JsonSerializer.Deserialize<OrderRequest>(orderObject, jsonSerializerOptions);

            return createdOrder;
        }

        public async Task<List<OrderViewModel>> GetAllOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<OrderViewModel>>($"{api}/getAll") ?? [];
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<OrderViewModel>($"{api}/getById/{id}") ?? new OrderViewModel() ;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{api}/delete/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<OrderViewModel>> SearchOrdersAsync(string searchTerm)
        {
            return await _httpClient.GetFromJsonAsync<List<OrderViewModel>>($"{api}/search?term={searchTerm}");
        }
    }
}
