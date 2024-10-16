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
    
    public async Task<Guid> CreateAsync(ExpenseEntity expenseEntity)
    {
        await _context.Expenses.AddAsync(expenseEntity);
        await _context.SaveChangesAsync();
        return expenseEntity.Id;
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        var expenseEntity = await _context.Expenses.FindAsync(id);
        if (expenseEntity == null) return false;

        _context.Expenses.Remove(expenseEntity);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<ExpenseEntity?> GetByIdAsync(Guid id)
    {
        var expenseEntity = await _context.Expenses.FindAsync(id);
        return expenseEntity;
    }
    
    public IEnumerable<ExpenseEntity> GetAllByBankAccountId(Guid bankAccountId)
    {
        var expenseEntity = _context.Expenses.Where(e => e.BankAccountId == bankAccountId).ToList();
        return expenseEntity;
    }
    
    //update is not needed
}