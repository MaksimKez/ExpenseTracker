using ExpenseTracker.Models;

namespace ExpenseTracker.Services.Interfaces;

public interface IBankAccountService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BankAccountService"/> class.
    /// </summary>
    /// <param name="repository">The repository to use for database operations.</param>
    /// <param name="mapper">The mapper to use to map between <see cref="BankAccount"/> and <see cref="BankAccountEntity"/>.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="repository"/> or <paramref name="mapper"/> is <see langword="null"/>.</exception>
    Task<Guid> CreateBankAccountAsync(BankAccount bankAccount);
    
    
    /// <summary>
    /// Deletes a bank account.
    /// </summary>
    /// <param name="id">The id of the bank account to delete.</param>
    /// <returns>A task representing the asynchronous operation, returning true if the bank account was deleted, false if it was not found.</returns>
    Task<bool> DeleteBankAccountAsync(Guid id);
    
    /// <summary>
    /// Gets a bank account by its id.
    /// </summary>
    /// <param name="id">The id of the bank account to retrieve.</param>
    /// <returns>The bank account with the given id, or null if no such bank account exists.</returns>
    Task<BankAccount?> GetBankAccountByIdAsync(Guid id);
    
    
    /// <summary>
    /// Updates an existing bank account with the given properties.
    /// </summary>
    /// <param name="bankAccount">The bank account to update.</param>
    /// <returns>
    /// A task representing the asynchronous operation, returning true if the bank account was updated, false if it was not found.
    /// </returns>
    Task<bool> UpdateBankAccountAsync(BankAccount bankAccount);
    
    /// <summary>
    /// Adds income to the history of the bank account with the given id.
    /// </summary>
    /// <param name="bankAccountId">The id of the bank account to add income to.</param>
    /// <param name="value">The value of the income to add.</param>
    /// <returns>A task representing the asynchronous operation, returning true if the income was added, false if the bank account was not found or the value is less than or equal to 0.</returns>
    Task<bool> AddIncomeToHistoryAsync(Guid bankAccountId, decimal value);
    
    
    /// <summary>
    /// Adds a new expense to the history of the given bank account.
    /// </summary>
    /// <param name="bankAccountId">The id of the bank account to add the expense to.</param>
    /// <param name="value">The value of the expense.</param>
    /// <returns>A boolean indicating whether the expense was added successfully.</returns>
    Task<bool> AddExpenseToHistoryAsync(Guid bankAccountId, decimal value);
}