using ExpenseTracker.Data.Entities;

namespace ExpenseTracker.Data.Repos.Interfaces;

public interface IBankAccountRepository
{
    BankAccountEntity? GetById(Guid id);
    Task<Guid> CreateAsync(BankAccountEntity account);
    Task UpdateAsync(BankAccountEntity account);
    Task<bool>  DeleteAsync(Guid id);
    Task<bool>  AddIncomeToHistoryAsync(decimal value, Guid bankAccountId);
    Task<bool>  AddExpenseToHistoryAsync(decimal value, Guid bankAccountId);
}