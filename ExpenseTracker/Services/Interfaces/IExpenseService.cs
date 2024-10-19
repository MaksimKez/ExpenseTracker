using ExpenseTracker.Models;

namespace ExpenseTracker.Services.Interfaces
{
    /// <summary>
    /// Interface for the ExpenseService class.
    /// </summary>
    public interface IExpenseService
    {
        /// <summary>
        /// Creates a new expense.
        /// </summary>
        /// <param name="expense">The expense to create.</param>
        /// <returns>The id of the newly created expense.</returns>
        Task<Guid> CreateExpenseAsync(Expense expense, Guid bankAccountId);

        /// <summary>
        /// Deletes an expense.
        /// </summary>
        /// <param name="id">The id of the expense to delete.</param>
        /// <returns>A boolean indicating whether the expense was deleted.</returns>
        Task<bool> DeleteExpenseAsync(Guid id);

        /// <summary>
        /// Gets an expense by its id.
        /// </summary>
        /// <param name="id">The id of the expense to retrieve.</param>
        /// <returns>The expense with the given id, or null if no such expense exists.</returns>
        Task<Expense?> GetExpenseByIdAsync(Guid id);

        /// <summary>
        /// Gets the expenses with the given bank account id.
        /// </summary>
        /// <param name="bankAccountId">The id of the bank account to retrieve the expenses from.</param>
        /// <returns>The expenses with the given bank account id.</returns>
        IEnumerable<Expense> GetExpensesByBankAccountId(Guid bankAccountId);
    }
}