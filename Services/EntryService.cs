using EccomerceBlazorWasm.Interfaces;
using EccomerceBlazorWasm.Models.ViewModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace EccomerceBlazorWasm.Services
{
    public class EntryService : IEntry
    {

        private readonly string api = "api/Entry";

        private readonly HttpClient _httpClient;

        public EntryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        private readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };


        public async Task<List<EntryViewModel>> FilterByDateAsync(DateTime startDate, DateTime endDate)
        {
            var startDateFormatted = startDate.ToString("yyyy-MM-dd");
            var endDateFormatted = endDate.ToString("yyyy-MM-dd");
            var filterUrl = $"{api}/filter?startDate={startDateFormatted}&endDate={endDateFormatted}";

            var entry = await _httpClient.GetFromJsonAsync<List<EntryViewModel>>(filterUrl);

            return entry;
        }

        public async Task<List<EntryViewModel>> GetAllAsync()
        {
            var entry = await _httpClient.GetFromJsonAsync<List<EntryViewModel>>($"{api}/GetAll");
            return entry;
        }
    }
}
