using ExpenseTracker.Data.Entities;

namespace ExpenseTracker.Data.Repos.Interfaces
{
    public interface IExpenseRepository
    {
        Task<Guid> CreateAsync(ExpenseEntity expenseEntity);
        Task<bool> DeleteAsync(Guid id); 
        ExpenseEntity? GetById(Guid id);
        Task<ExpenseEntity?> GetAllByBankAccountIdAsync(Guid bankAccountId);
    }
}