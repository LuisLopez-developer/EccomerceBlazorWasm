using EccomerceBlazorWasm.Interfaces;
using EccomerceBlazorWasm.Models.CreateModel;
using EccomerceBlazorWasm.Models.ViewModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace EccomerceBlazorWasm.Services
{
    public class LossService : ILoss
    {
        private readonly string api = "api/Loss";

        private readonly HttpClient _httpClient;

        public LossService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        private readonly JsonSerializerOptions jsonSerializerOptions =
          new()
          {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          };

        public Task<LossCreateModel> CreateAsync(LossCreateModel lossCreateModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LossViewModel>> GetAllAsync()
        {
            var losses = await _httpClient.GetFromJsonAsync<List<LossViewModel>>($"{api}/GetAll");
            return losses;
        }

        public Task<LossCreateModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LossViewModel>> SearchAsync(string name)
        {
            var losses = await _httpClient.GetFromJsonAsync<List<LossViewModel>>($"{api}/search?name={name}");
            return losses;
        }

        public Task<bool> UpdateAsync(int id, LossCreateModel lossCreateModel)
        {
            throw new NotImplementedException();
        }
    }
}
