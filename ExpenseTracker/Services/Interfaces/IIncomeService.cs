using ExpenseTracker.Models;

namespace ExpenseTracker.Services.Interfaces;

public interface IIncomeService
{
    Task<Guid> CreateIncomeAsync(Income income, Guid bankAccountId);
    Task<bool> DeleteIncomeAsync(Guid id);
    Task<Income?> GetIncomeByIdAsync(Guid id);
    IEnumerable<Income> GetIncomesByBankAccountId(Guid bankAccountId);
}
