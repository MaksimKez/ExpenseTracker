namespace ExpenseTracker.Data.Repos.Interfaces;

public interface IUserRepository
{
    Task<Guid> RegisterAsync(string username, string password);
    bool Login(string username, string password);
    Guid? GetBankAccountId(Guid userId);
}