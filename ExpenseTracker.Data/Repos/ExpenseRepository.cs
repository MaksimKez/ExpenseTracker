using ExpenseTracker.Data.Entities;
using ExpenseTracker.Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Repos;

public class ExpenseRepository : IExpenseRepository
{
    private readonly ApplicationDbContext _context;

    public ExpenseRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    /// <summary>
    /// Creates a new expense.
    /// </summary>
    /// <param name="expenseEntity">The expense to create.</param>
    /// <returns>The id of the newly created expense.</returns>
    public async Task<Guid> CreateAsync(ExpenseEntity expenseEntity)
    {
        await _context.Expenses.AddAsync(expenseEntity);
        await _context.SaveChangesAsync();
        return expenseEntity.Id;
    }
    
    /// <summary>
    /// Deletes an expense.
    /// </summary>
    /// <param name="id">The id of the expense to delete.</param>
    /// <returns>A task representing the asynchronous operation, returning true if the expense was deleted, false if it was not found.</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var expenseEntity = await _context.Expenses.FindAsync(id);
        if (expenseEntity == null) return false;

        _context.Expenses.Remove(expenseEntity);
        await _context.SaveChangesAsync();
        return true;
    }
    
    /// <summary>
    /// Gets an expense by its id.
    /// </summary>
    /// <param name="id">The id of the expense to retrieve.</param>
    /// <returns>The expense with the given id, or null if no such expense exists.</returns>
    public ExpenseEntity? GetById(Guid id)
    {
        var expenseEntity = _context.Expenses.FirstOrDefault(e => e.Id == id);
        return expenseEntity;
    }
    
    /// <summary>
    /// Gets the expense with the given bank account id.
    /// </summary>
    /// <param name="bankAccountId">The id of the bank account to retrieve the expense from.</param>
    /// <returns>The expense with the given bank account id, or null if no such expense exists.</returns>
    public async Task<ExpenseEntity?> GetAllByBankAccountIdAsync(Guid bankAccountId)
    {
        var expenseEntity = await _context.Expenses.FirstOrDefaultAsync(e => e.BankAccountId == bankAccountId);
        return expenseEntity;
    }
    
    //update is not needed
}