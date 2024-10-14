using ExpenseTracker.Data.Entities;
using ExpenseTracker.Data.Repos.Interfaces;

namespace ExpenseTracker.Data.Repos;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly ApplicationDbContext _context;

    public BankAccountRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    /// <summary>
    /// Creates a new bank account.
    /// </summary>
    /// <param name="bankAccountEntity">The bank account to create.</param>
    /// <returns>The id of the newly created bank account.</returns>
    public async Task<Guid> CreateAsync(BankAccountEntity bankAccountEntity)
    {
        await _context.BankAccounts.AddAsync(bankAccountEntity);
        await _context.SaveChangesAsync();
        return bankAccountEntity.Id;
    }

    /// <summary>
    /// Gets the bank account with the given id.
    /// </summary>
    /// <param name="id">The id of the bank account to retrieve.</param>
    /// <returns>The bank account with the given id, or null if no such bank account exists.</returns>
    public BankAccountEntity? GetById(Guid id)
    {
        var account = _context.BankAccounts.FirstOrDefault(b => b.Id == id);
        return account;
    }
    
    /// <summary>
    /// Updates an existing bank account.
    /// </summary>
    /// <param name="bankAccountEntity">The bank account to update.</param>
    public async Task UpdateAsync(BankAccountEntity bankAccountEntity)
    {
        _context.BankAccounts.Update(bankAccountEntity);
        await _context.SaveChangesAsync();
    }
    
    /// <summary>
    /// Deletes a bank account.
    /// </summary>
    /// <param name="id">The id of the bank account to delete.</param>
    /// <returns>A task representing the asynchronous operation, returning true if the account was deleted, false if it was not found.</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var bankAccountEntity = await _context.BankAccounts.FindAsync(id);
        if (bankAccountEntity == null) return false;
    
        _context.BankAccounts.Remove(bankAccountEntity);
        await _context.SaveChangesAsync(); return true;
    }
    
    /// <summary>
    /// Adds a new income to the history of the bank account with the given id.
    /// </summary>
    /// <param name="value">The amount of money to add to the bank account.</param>
    /// <param name="bankAccountId">The id of the bank account to add the income to.</param>
    /// <returns>A boolean indicating whether the account was found.</returns>
    public async Task<bool> AddIncomeToHistoryAsync(decimal value, Guid bankAccountId)
    {
        var account = _context.BankAccounts.FirstOrDefault(b => b.Id == bankAccountId);
        if (account == null) return false;
        
        account.Balance += value;
        
        _context.BankAccounts.Update(account);
        await _context.SaveChangesAsync();
        return true;
    }
    
    /// <summary>
    /// Adds a new expense to the history of the bank account with the given id.
    /// </summary>
    /// <param name="value">The amount of money to subtract from the bank account.</param>
    /// <param name="bankAccountId">The id of the bank account to add the expense to.</param>
    /// <returns>A task representing the asynchronous operation, returning a boolean indicating whether the account was found.</returns>
    public async Task<bool> AddExpenseToHistoryAsync(decimal value, Guid bankAccountId)
    {
        var account = _context.BankAccounts.FirstOrDefault(b => b.Id == bankAccountId);
        if (account == null) return false;
        
        account.Balance -= value;
        
        _context.BankAccounts.Update(account);
        await _context.SaveChangesAsync();
        return true;
    }
    
}