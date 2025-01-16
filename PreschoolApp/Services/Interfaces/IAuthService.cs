using PreschoolApp.Models;

namespace PreschoolApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateTokenAsync(User user, string password);
    }
}

