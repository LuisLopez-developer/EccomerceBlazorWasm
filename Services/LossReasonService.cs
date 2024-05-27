using EccomerceBlazorWasm.Interfaces;
using EccomerceBlazorWasm.Models.ViewModel;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace EccomerceBlazorWasm.Services
{
    public class LossReasonService : ILossReason
    {
        private readonly string api = "api/LossReason";

        private readonly HttpClient _httpClient;

        public LossReasonService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        private readonly JsonSerializerOptions jsonSerializerOptions =
          new()
          {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          };

        public async Task<LossReasonViewModel> CreateAsync(LossReasonViewModel lossReason)
        {
            var response = await _httpClient.PostAsJsonAsync($"{api}/create", lossReason);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LossReasonViewModel>(jsonSerializerOptions);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{api}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<LossReasonViewModel>> GetAllAsync()
        {
            var lossReasons = await _httpClient.GetFromJsonAsync<List<LossReasonViewModel>>($"{api}/GetAll");
            return lossReasons;
        }

        public async Task<LossReasonViewModel> GetByIdAsync(int id)
        {
            var lossReason = await _httpClient.GetFromJsonAsync<LossReasonViewModel>($"{api}/{id}");
            return lossReason;
        }

        public async Task<bool> UpdateAsync(int id, LossReasonViewModel lossReason)
        {
            var emptyContent = new StringContent(JsonSerializer.Serialize(lossReason), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{api}/{id}", emptyContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<LossReasonViewModel>> SearchAsync(string reason)
        {
            var productReasons = await _httpClient.GetFromJsonAsync<List<LossReasonViewModel>>($"{api}/search?reason={reason}");
            return productReasons;
        }
    }
}
