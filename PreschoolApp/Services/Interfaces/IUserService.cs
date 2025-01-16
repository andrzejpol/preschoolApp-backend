using PreschoolApp.Models;
using PreschoolApp.Requests;

namespace PreschoolApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task DeleteUserAsync(int id);
        Task<bool> ExistsUserAsync(string login);
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string email);
        Task RegisterUserAsync(RegisterUserRequest user);
    }
}

