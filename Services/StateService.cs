using EccomerceBlazorWasm.Interfaces;
using EccomerceBlazorWasm.Models.ViewModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace EccomerceBlazorWasm.Services
{
    public class StateService : IState
    {
        private readonly string api = "api/State";

        private readonly HttpClient _httpClient;

        public StateService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        private readonly JsonSerializerOptions jsonSerializerOptions =
          new()
          {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          };

        public async Task<List<StateViewModel>> GetAllAsync()
        {
            var States = await _httpClient.GetFromJsonAsync<List<StateViewModel>>($"{api}/GetAll");
            return States;
        }
    }
}
