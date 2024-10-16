using ExpenseTracker.Data.Entities;
using ExpenseTracker.Data.Repos.Interfaces;
using ExpenseTracker.Mappers.Interface;
using ExpenseTracker.Models;
using ExpenseTracker.Services.Interfaces;

namespace ExpenseTracker.Services;

public class IncomeService : IIncomeService
{
    private readonly IIncomeRepository _repository;
    private readonly IMapper<Income, IncomeEntity> _mapper;

    public IncomeService(IIncomeRepository repository, IMapper<Income, IncomeEntity> mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    private bool IsValidIncome(Income? income)
    {
        if (income == null) return false;
        return income.Sum > 0 && !string.IsNullOrEmpty(income.Title);
    }

    public async Task<Guid> CreateIncomeAsync(Income income)
    {
        if (!IsValidIncome(income))
        {
            return Guid.Empty;
        }
        income.Id = Guid.NewGuid();
        var entity = _mapper.MapToEntity(income);
        var id = await _repository.CreateAsync(entity);
        return id;
    }

    public async Task<bool> DeleteIncomeAsync(Guid id)
    {
        if (id.Equals(Guid.Empty)) return false;
        return await _repository.DeleteAsync(id);
    }

    public async Task<Income?> GetIncomeByIdAsync(Guid id)
    {
        if (id.Equals(Guid.Empty)) return null;
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : _mapper.MapToModel(entity);
    }

    public IEnumerable<Income> GetIncomesByBankAccountId(Guid bankAccountId)
    {
        var entities = _repository.GetAllByBankAccountId(bankAccountId);
        return entities.Select(e => _mapper.MapToModel(e));
    }
}