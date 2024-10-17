using ExpenseTracker.Models;

namespace ExpenseTracker.Services.Interfaces;

public interface ITransactionsFacade
{
    Task<Guid> CreateBankAccountAsync(BankAccount bankAccount);
    Task<bool> DeleteBankAccountAsync(Guid id);
    Task<bool> UpdateBankAccountAsync(BankAccount bankAccount);
    Task<BankAccount?> GetBankAccountByIdAsync(Guid id);
    Task<Guid> AddIncomeThenAddToHistoryAsync(Guid bankAccountId, Income income);
    Task<Guid> AddExpenseThenAddToHistoryAsync(Guid bankAccountId, Expense expense);
    Task<bool> DeleteIncomeAsyncThenDeleteFromHistoryAsync(Guid id, Guid bankAccountId);
    Task<bool> DeleteExpenseAsyncThenDeleteFromHistoryAsync(Guid id, Guid bankAccountId);
    Task<Income?> GetIncomeByIdAsync(Guid id);
    Task<Expense?> GetExpenseByIdAsync(Guid id);
    List<Income> GetIncomesByBankAccountId(Guid bankAccountId);
    List<Expense> GetExpensesByBankAccountId(Guid bankAccountId);
}