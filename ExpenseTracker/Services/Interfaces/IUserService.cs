using ExpenseTracker.Data.Entities;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services.Interfaces;

public interface IUserService
{
    Task<Guid> RegisterUserAsync(string username, string password);
    Guid Login(string username, string password);
    User? GetUserById(Guid userId);
}