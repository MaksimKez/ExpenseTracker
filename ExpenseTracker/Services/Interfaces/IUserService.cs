namespace ExpenseTracker.Services.Interfaces;

public interface IUserService
{
    Task<Guid> RegisterUserAsync(string username, string password);
    bool Login(string username, string password);
    Guid GetBankAccountId(Guid userId);
}