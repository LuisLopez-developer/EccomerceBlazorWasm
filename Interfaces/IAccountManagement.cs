using EccomerceBlazorWasm.Models;

namespace EccomerceBlazorWasm.Interfaces
{
    public interface IAccountManagement
    {
        public Task<FormResult> RegisterAsync(string email, string password);

    }
}
