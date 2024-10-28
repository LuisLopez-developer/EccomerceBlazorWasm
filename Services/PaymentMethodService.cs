using EccomerceBlazorWasm.Interfaces;
using EccomerceBlazorWasm.Models.ViewModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace EccomerceBlazorWasm.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly HttpClient _httpClient;
        private readonly string api = "api/PaymentMethod/getAll"; // Ajusta la ruta según tu API

        public PaymentMethodService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth"); // Asegúrate de que el nombre del cliente HTTP sea correcto
        }

        public async Task<List<PaymentMethodViewModel>> GetAllPaymentMethodsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PaymentMethodViewModel>>(api);
        }
    }
}
