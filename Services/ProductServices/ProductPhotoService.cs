using EccomerceBlazorWasm.Interfaces.PorductInterface;
using EccomerceBlazorWasm.Models.Product;

namespace EccomerceBlazorWasm.Services.ProductServices
{
    public class ProductPhotoService : IProductPhoto
    {
        private readonly string api = "api/ProductPhoto";

        private readonly HttpClient _httpClient;

        public ProductPhotoService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }


        public Task<List<ProductPhotoViewModel>> CreateAsync(int productId, List<ProductPhotoViewModel> productPhoto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductPhotoViewModel>> SearchByProductIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, ProductPhotoViewModel productPhoto)
        {
            throw new NotImplementedException();
        }
    }
}
