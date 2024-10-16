using ExpenseTracker.Data.Entities;
using ExpenseTracker.Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Repos;

public class IncomeRepository : IIncomeRepository
{
    private readonly ApplicationDbContext _context;

    public IncomeRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<Guid> CreateAsync(IncomeEntity incomeEntity)
    {
        await _context.Incomes.AddAsync(incomeEntity);
        await _context.SaveChangesAsync();
        return incomeEntity.Id;
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        var incomeEntity = await _context.Incomes.FindAsync(id);
        if (incomeEntity == null) return false;
        
        _context.Incomes.Remove(incomeEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IncomeEntity?> GetByIdAsync(Guid id)
    {
        var incomeEntity = await _context.Incomes.FindAsync(id);
        return incomeEntity;
    }

    public IEnumerable<IncomeEntity> GetAllByBankAccountId(Guid bankAccountId)
    {
        var expenseEntity = _context.Incomes.Where(e => e.BankAccountId == bankAccountId).ToList();
        return expenseEntity;
    }
}