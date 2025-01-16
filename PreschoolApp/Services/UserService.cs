using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PreschoolApp.Configuration;
using PreschoolApp.Data;
using PreschoolApp.Models;
using PreschoolApp.Requests;
using PreschoolApp.Services.Interfaces;
using PreschoolApp.Tools.Interfaces;

namespace PreschoolApp.Services;

public class UserService : IUserService
{
    private readonly AuthenticationConfiguration _authenticationConfiguration;
    private readonly PreschoolDbContext _applicationDbContext;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IPasswordHasher passwordHasher, PreschoolDbContext context, AuthenticationConfiguration authConfiguraion)
    {
        _applicationDbContext = context;
        _passwordHasher = passwordHasher;
        _authenticationConfiguration = authConfiguraion;
    }

    public async Task RegisterUserAsync(RegisterUserRequest registerUserRequest)
    {
        using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
        {
            if (await ExistsUserAsync(registerUserRequest.Email))
            {
                throw new Exception("Użytkownik o podanym adresie email już istnieje");
            }
            
            try
            {
                byte[] salt = _passwordHasher.RandomSalt(_authenticationConfiguration.SaltSize);
                string hash = _passwordHasher.Hash(registerUserRequest.Password, salt);

                User user = new User
                {
                    Email = registerUserRequest.Email,
                    FirstName = registerUserRequest.FirstName,
                    LastName = registerUserRequest.LastName,
                    PasswordHash = hash,
                    Salt = salt,
                };

                _applicationDbContext.Users.Add(user);
                await _applicationDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Błąd podczas rejestracji użytkownika");
            }
        }
    }
    
    public async Task<bool> ExistsUserAsync(string email)
    {
        User user = await GetUserAsync(email);
        return user is not null;
    }
    
    public async Task<List<User>> GetUsersAsync()
    {
        return await _applicationDbContext.Users.ToListAsync();
    }
    
    public async Task<User> GetUserAsync(int id)
    {
        return await _applicationDbContext.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<User> GetUserAsync(string email)
    {
        return await _applicationDbContext.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }
    
    public async Task DeleteUserAsync(int id){}
}