using Blazored.LocalStorage;
using EccomerceBlazorWasm;
using EccomerceBlazorWasm.Interfaces;
using EccomerceBlazorWasm.Interfaces.PorductInterface;
using EccomerceBlazorWasm.Services;
using EccomerceBlazorWasm.Services.ProductServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage(); //para alamcenar en el local storage
builder.Services.AddTransient<CutomHttpHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddScoped<IProductCategory, ProductCategoryService>();
builder.Services.AddScoped<IProductBrand, ProductBrandService>();
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<IState, StateService>();
builder.Services.AddScoped<ILossReason, LossReasonService>();
builder.Services.AddScoped<ILoss, LossService>();
builder.Services.AddScoped<IEntry, EntryService>();
builder.Services.AddScoped<IProductPhoto, ProductPhotoService>();
builder.Services.AddScoped<IR2, R2Service>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetSection("Url")["FrontendUrl"] ?? "https://localhost:7033")
});


builder.Services.AddHttpClient("Auth", opt => opt.BaseAddress =
new Uri(builder.Configuration.GetSection("Url")["BackendUrl"] ?? "https://localhost:7239"))
    .AddHttpMessageHandler<CutomHttpHandler>();

await builder.Build().RunAsync();