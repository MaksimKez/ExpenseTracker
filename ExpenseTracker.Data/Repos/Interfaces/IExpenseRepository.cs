using ExpenseTracker.Data.Entities;

namespace ExpenseTracker.Data.Repos.Interfaces
{
    public interface IExpenseRepository
    {
        /// <summary>
        /// Creates a new expense.
        /// </summary>
        /// <param name="expenseEntity">The expense to create.</param>
        /// <returns>The id of the newly created expense.</returns>
        Task<Guid> CreateAsync(ExpenseEntity expenseEntity);
        
        
        /// <summary>
        /// Deletes an expense.
        /// </summary>
        /// <param name="id">The id of the expense to delete.</param>
        /// <returns>A task representing the asynchronous operation, returning true if the expense was deleted, false if it was not found.</returns>
        Task<bool> DeleteAsync(Guid id);


        /// <summary>
        /// Retrieves an ExpenseEntity by its unique identifier.
        /// </summary>
        /// <param name="id">The Guid identifier of the ExpenseEntity to retrieve.</param>
        /// <returns type="ExpenseEntity">The ExpenseEntity with the specified id, or null if not found.</returns>
        Task<ExpenseEntity?> GetByIdAsync(Guid id);     
        
        /// <summary>
        /// Gets the expense with the given bank account id.
        /// </summary>
        /// <param name="bankAccountId">The id of the bank account to retrieve the expense from.</param>
        /// <returns>The expense with the given bank account id, or null if no such expense exists.</returns>
        IEnumerable<ExpenseEntity> GetAllByBankAccountId(Guid bankAccountId);
    }
}