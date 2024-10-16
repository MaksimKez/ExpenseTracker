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
    
    public async Task<Guid> CreateAsync(BankAccountEntity bankAccountEntity)
    {
        await _context.BankAccounts.AddAsync(bankAccountEntity);
        await _context.SaveChangesAsync();
        return bankAccountEntity.Id;
    }

    public async Task<BankAccountEntity?> GetByIdAsync(Guid id)
    {
        var account = await _context.BankAccounts.FindAsync(id);
        return account;
    }
    
    public async Task UpdateAsync(BankAccountEntity bankAccountEntity)
    {
        _context.BankAccounts.Update(bankAccountEntity);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        var bankAccountEntity = await _context.BankAccounts.FindAsync(id);
        if (bankAccountEntity == null) return false;
    
        _context.BankAccounts.Remove(bankAccountEntity);
        await _context.SaveChangesAsync(); return true;
    }
    
    public async Task<bool> AddIncomeToHistoryAsync(decimal value, Guid bankAccountId)
    {
        var account = _context.BankAccounts.FirstOrDefault(b => b.Id == bankAccountId);
        if (account == null) return false;
        
        account.Balance += value;
        
        _context.BankAccounts.Update(account);
        await _context.SaveChangesAsync();
        return true;
    }
    
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