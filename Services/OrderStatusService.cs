using EccomerceBlazorWasm.Interfaces;
using EccomerceBlazorWasm.Models.ViewModel;
using System.Net.Http.Json;

namespace EccomerceBlazorWasm.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly HttpClient _httpClient;
        private readonly string api = "api/system/Status"; // Endpoint de la API

        public OrderStatusService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth"); // Asegúrate de que el nombre del cliente HTTP sea correcto
        }

        public async Task<List<OrderStatusViewModel>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<OrderStatusViewModel>>(api);
        }
    }
}
