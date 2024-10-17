using ExpenseTracker.Data.Entities;

namespace ExpenseTracker.Data.Repos.Interfaces;

public interface IBankAccountRepository
{
    /// <summary>
    /// Gets a bank account by its id.
    /// </summary>
    /// <param name="id">The id of the bank account to retrieve.</param>
    /// <returns>The bank account with the given id, or null if no such bank account exists.</returns>
    Task<BankAccountEntity?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Creates a new bank account.
    /// </summary>
    /// <param name="bankAccountEntity">The bank account to create.</param>
    /// <returns>The id of the newly created bank account.</returns>
    Task<Guid> CreateAsync(BankAccountEntity account);
    
    /// <summary>
    /// Updates an existing bank account.
    /// </summary>
    /// <param name="bankAccountEntity">The bank account to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateAsync(BankAccountEntity account);
    
    
    /// <summary>
    /// Deletes a bank account.
    /// </summary>
    /// <param name="id">The id of the bank account to delete.</param>
    /// <returns>A task representing the asynchronous operation, returning true if the account was deleted, false if it was not found.</returns>
    Task<bool>  DeleteAsync(Guid id);
    
    /// <summary>
    /// Adds a new income to the history of the bank account with the given id.
    /// </summary>
    /// <param name="value">The amount of money to add to the bank account.</param>
    /// <param name="bankAccountId">The id of the bank account to add the income to.</param>
    /// <returns>A boolean indicating whether the account was found.</returns>
    Task<bool>  AddIncomeToHistoryAsync(decimal value, Guid bankAccountId);
    
    /// <summary>
    /// Adds a new expense to the history of the bank account with the given id.
    /// </summary>
    /// <param name="value">The amount of money to subtract from the bank account.</param>
    /// <param name="bankAccountId">The id of the bank account to add the expense to.</param>
    /// <returns>A task representing the asynchronous operation, returning a boolean indicating whether the account was found.</returns>
    Task<bool>  AddExpenseToHistoryAsync(decimal value, Guid bankAccountId);

    Task<bool> DeleteIncomeFromHistoryAsync(Guid id, Guid bankAccountId);
    Task<bool> DeleteExpenseFromHistoryAsync(Guid id, Guid bankAccountId);
}