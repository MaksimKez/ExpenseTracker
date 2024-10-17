using ExpenseTracker.Data.Entities;

namespace ExpenseTracker.Data.Repos.Interfaces;

public interface IUserRepository
{
    Task<Guid> RegisterAsync(string username, string password);
    Guid Login(string username, string password);
    UserEntity? GetUserById(Guid userId);
}