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


        

        public async Task<List<EntryViewModel>> GetAllAsync()
        {
            var entry = await _httpClient.GetFromJsonAsync<List<EntryViewModel>>($"{api}/GetAll");
            return entry;
        }

        public async Task<List<EntryViewModel>> FilterByDateAsync(DateTime startDate, DateTime endDate)
        {
            var response = await _httpClient.GetAsync($"{api}/filter?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");

            if (response.IsSuccessStatusCode)
            {
                var entries = await response.Content.ReadFromJsonAsync<List<EntryViewModel>>(jsonSerializerOptions);
                return entries ?? new List<EntryViewModel>();
            }

            throw new Exception("Error fetching data");
        }

    }
}
