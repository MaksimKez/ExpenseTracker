using ExpenseTracker.Data.Entities;

namespace ExpenseTracker.Data.Repos.Interfaces
{
    public interface IIncomeRepository
    {
        Task<Guid> CreateAsync(IncomeEntity incomeEntity);
        Task<bool> DeleteAsync(Guid id);
        Task<IncomeEntity?> GetByIdAsync(Guid id);
        IEnumerable<IncomeEntity> GetAllByBankAccountId(Guid bankAccountId);
    }
}