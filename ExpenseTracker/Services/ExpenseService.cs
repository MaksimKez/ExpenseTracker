using ExpenseTracker.Data.Entities;
using ExpenseTracker.Data.Repos.Interfaces;
using ExpenseTracker.Mappers.Interface;
using ExpenseTracker.Models;
using ExpenseTracker.Services.Interfaces;

namespace ExpenseTracker.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _repository;
    private readonly IMapper<Expense, ExpenseEntity> _mapper;

    public ExpenseService(IExpenseRepository repository, IMapper<Expense, ExpenseEntity> mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    
    private bool IsValidExpense(Expense? expense)
    {
        if (expense == null) return false;
        return expense.Sum > 0 && !expense.Title.Equals(string.Empty);
    }

    
    public async Task<Guid> CreateExpenseAsync(Expense expense, Guid bankAccountId)
    {
        if (!IsValidExpense(expense))
        {
            return Guid.Empty;
        }

        expense.Id = Guid.NewGuid();
        var entity = _mapper.MapToEntity(expense);
        entity.BankAccountId = bankAccountId;
        var id = await _repository.CreateAsync(entity);
        return id;
    }
    
    public async Task<bool> DeleteExpenseAsync(Guid id)
    {
        if(id.Equals(Guid.Empty)) return false;
        
        return await _repository.DeleteAsync(id);
    }
    
    public async Task<Expense?> GetExpenseByIdAsync(Guid id)
    {
        if(id.Equals(Guid.Empty)) return null;
        
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : _mapper.MapToModel(entity);
    }
    
    public IEnumerable<Expense> GetExpensesByBankAccountId(Guid bankAccountId)
    {
        var entities = _repository.GetAllByBankAccountId(bankAccountId);
        return entities.Select(e => _mapper.MapToModel(e));
    }
}