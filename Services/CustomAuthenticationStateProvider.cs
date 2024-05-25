using Blazored.LocalStorage;
using EccomerceBlazorWasm.Interfaces;
using EccomerceBlazorWasm.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace EccomerceBlazorWasm.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, IAccountManagement
    {
        private bool _authenticated = false;

        private readonly ClaimsPrincipal Unauthenticated =
           new(new ClaimsIdentity());

        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions jsonSerializerOptions =
        new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly ILocalStorageService _localStorageService;

        public CustomAuthenticationStateProvider(IHttpClientFactory httpClientFactory, 
            ILocalStorageService localStorageService)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (_authenticated)
            {
                // Si ya está autenticado, no realizar nuevamente la llamada a /manage/info
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())); // devolver el estado actual
            }

            _authenticated = false;
            var user = Unauthenticated;

            try
            {
                var accessToken = await _localStorageService.GetItemAsync<string>("accessToken");
                if (string.IsNullOrWhiteSpace(accessToken))
                {
                    return new AuthenticationState(user);
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var userResponse = await _httpClient.GetAsync("manage/info");
                userResponse.EnsureSuccessStatusCode();

                var userJson = await userResponse.Content.ReadAsStringAsync();
                var userInfo = JsonSerializer.Deserialize<UserInfo>(userJson, jsonSerializerOptions);

                if (userInfo != null)
                {
                    var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userInfo.Email),
                new(ClaimTypes.Email, userInfo.Email)
            };

                    claims.AddRange(
                      userInfo.Claims.Where(c => c.Key != ClaimTypes.Name && c.Key != ClaimTypes.Email)
                    .Select(c => new Claim(c.Key, c.Value)));

                    var rolesResponse = await _httpClient.GetAsync($"api/Role/GetuserRole?userEmail={userInfo.Email}");
                    rolesResponse.EnsureSuccessStatusCode();
                    var rolesJson = await rolesResponse.Content.ReadAsStringAsync();

                    var roles = JsonSerializer.Deserialize<string[]>(rolesJson, jsonSerializerOptions);
                    if (roles != null && roles.Length > 0)
                    {
                        foreach (var role in roles)
                        {
                            claims.Add(new(ClaimTypes.Role, role));
                        }
                    }

                    var id = new ClaimsIdentity(claims, nameof(CustomAuthenticationStateProvider));
                    user = new ClaimsPrincipal(id);
                    _authenticated = true;
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
            }

            return new AuthenticationState(user);
        }


        public async Task<FormResult> RegisterAsync(string email, string password)
        {
            string[] defaultDetail = ["Un error desconocido impidió que el registro se realizara correctamente."];

            try
            {
                var result = await _httpClient.PostAsJsonAsync("register",
                   new { email, password });
                if (result.IsSuccessStatusCode)
                {
                    return new FormResult { Succeeded = true };
                }
                var details = await result.Content.ReadAsStringAsync();
                var problemDetails = JsonDocument.Parse(details);

                var errors = new List<string>();
                var errorList = problemDetails.RootElement.GetProperty("errors");

                foreach (var errorEntry in errorList.EnumerateObject())
                {
                    if (errorEntry.Value.ValueKind == JsonValueKind.String)
                    {
                        errors.Add(errorEntry.Value.GetString()!);
                    }
                    else if (errorEntry.Value.ValueKind == JsonValueKind.Array)
                    {
                        errors.AddRange(
                            errorEntry.Value.EnumerateArray().Select(
                                e => e.GetString() ?? string.Empty)
                            .Where(e => !string.IsNullOrEmpty(e)));
                    }
                }
                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = problemDetails == null ? defaultDetail : [.. errors]
                };
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<FormResult> LoginAsync(string email, string password)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync(
                    //"login?useCookies=true", new //usar cookies
                    "login", new
                    {
                        email,
                        password
                    });

                if (result.IsSuccessStatusCode)
                {
                    var tokenResponse = await result.Content.ReadAsStringAsync();

                    var tokenInfo = JsonSerializer.Deserialize<TokenInfo>(tokenResponse, jsonSerializerOptions);

                    await _localStorageService.SetItemAsync("accessToken", tokenInfo.AccessToken);
                    
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                    return new FormResult { Succeeded = true };
                }
            }
            catch (Exception ex)
            {

            }

            return new FormResult
            {
                Succeeded = false,
                ErrorList = ["Correo electrónico inválido y / o contraseña."]
            };
        }

        public async Task LogoutAsync()
        {
            const string Empty = "{}";
            var emptyContent = new StringContent(Empty, Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync("api/User/logout", emptyContent);

            if (result.IsSuccessStatusCode)
            {
                await _localStorageService.RemoveItemAsync("accessToken");

                // Asegurar que solo una vez se notifique el cambio de estado
                _authenticated = false;
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(Unauthenticated)));
            }
        }

        public async Task<bool> CheckAuthenticatedAsync()
        {
            await GetAuthenticationStateAsync();
            return _authenticated;
        }

        public async Task<List<Role>> GetRoles()
        {
            try
            {
                var result = await _httpClient.GetAsync("api/Role/GetRoles");
                var response = await result.Content.ReadAsStringAsync();
                var rolelist = JsonSerializer.Deserialize<List<Role>>(response, jsonSerializerOptions);
                if (result.IsSuccessStatusCode)
                {
                    return rolelist;
                }
            }
            catch (Exception ex)
            {
            }

            return new List<Role>();

        }

    }
}
