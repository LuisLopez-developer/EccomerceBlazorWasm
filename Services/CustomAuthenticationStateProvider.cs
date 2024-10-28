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
        private ClaimsPrincipal _authenticatedUser = new(new ClaimsIdentity());

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
                Console.WriteLine("Already authenticated.");
                return new AuthenticationState(_authenticatedUser);
            }

            var user = Unauthenticated;
            try
            {
                var accessToken = await _localStorageService.GetItemAsync<string>("accessToken");
                if (string.IsNullOrWhiteSpace(accessToken))
                {
                    Console.WriteLine("Access token is null or empty.");
                    return new AuthenticationState(user);
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var userResponse = await _httpClient.GetAsync("/api/eccomerce/Account/GetUserInfo");
                if (!userResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine("Failed to get user info.");
                    return new AuthenticationState(user);
                }

                var userInfo = JsonSerializer.Deserialize<UserInfo>(
                    await userResponse.Content.ReadAsStringAsync(), jsonSerializerOptions);

                if (userInfo != null)
                {
                    var claims = userInfo.Claims
                        .Select(c => new Claim(c.Key, c.Value))
                        .ToList();

                    claims.Add(new Claim(ClaimTypes.PrimarySid, userInfo.Id));
                    claims.Add(new Claim(ClaimTypes.Name, userInfo.UserName));
                    claims.Add(new Claim(ClaimTypes.Email, userInfo.Email));

                    userInfo.Roles?.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

                    _authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, nameof(CustomAuthenticationStateProvider)));
                    _authenticated = true;

                    // Mensaje de depuración
                    Console.WriteLine($"User authenticated: {userInfo.UserName}, ID: {userInfo.Id}");
                }
                else
                {
                    // Mensaje de depuración
                    Console.WriteLine("UserInfo is null after deserialization.");
                }
            }
            catch (Exception ex)
            {
                // Mensaje de depuración
                Console.WriteLine($"Exception in GetAuthenticationStateAsync: {ex.Message}");
            }

            return new AuthenticationState(_authenticatedUser);
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
                    "/api/eccomerce/Account/login", new
                    {
                        UserNameOrEmail = email,
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
                ErrorList = ["Usuario inválido y / o contraseña."]
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

            return [];

        }

    }
}
