using ExpenseTracker.Data.Entities;
using ExpenseTracker.Data.Repos.Interfaces;
using ExpenseTracker.Mappers.Interface;
using ExpenseTracker.Models;
using ExpenseTracker.Services.Interfaces;

namespace ExpenseTracker.Services;

public class BankAccountService : IBankAccountService
{
    private readonly IBankAccountRepository _repository;
    private readonly IMapper<BankAccount, BankAccountEntity> _mapper;

    public BankAccountService(IBankAccountRepository repository, IMapper<BankAccount, BankAccountEntity> mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    private bool IsValidBankAccount(BankAccount? bankAccount)
    {
        if (bankAccount == null) return false;
        return bankAccount.Balance >= 0;
    }
    
    public async Task<Guid> CreateBankAccountAsync(BankAccount bankAccount)
    {
        if (!IsValidBankAccount(bankAccount)) return Guid.Empty;
        
        bankAccount.Id = Guid.NewGuid();
        var entity = _mapper.MapToEntity(bankAccount);
            
        var id = await _repository.CreateAsync(entity);
        return id;
    }
    
    public async Task<bool> DeleteBankAccountAsync(Guid id)
    {
        if (id.Equals(Guid.Empty)) return false;
        
        return await _repository.DeleteAsync(id);
    }
    
    public async Task<BankAccount?> GetBankAccountByIdAsync(Guid id)
    {
        if (id.Equals(Guid.Empty)) return null;
        
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : _mapper.MapToModel(entity);
    }

    public async Task<bool> UpdateBankAccountAsync(BankAccount bankAccount)
    {
        if (!IsValidBankAccount(bankAccount))
        {
            return false;
        }
        
        var entity = _mapper.MapToEntity(bankAccount);
        await _repository.UpdateAsync(entity);
        return true;
    }
    
    public async Task<bool> AddIncomeToHistoryAsync(Guid bankAccountId, decimal value)
    {
        if (bankAccountId.Equals(Guid.Empty) || value <= 0)
        {
            return false;
        }
        
        return await _repository.AddIncomeToHistoryAsync(value, bankAccountId);
    }
    
    public async Task<bool> AddExpenseToHistoryAsync(Guid bankAccountId, decimal value)
    {
        if (bankAccountId.Equals(Guid.Empty) || value <= 0)
        {
            return false;
        }
        
        return await _repository.AddExpenseToHistoryAsync(value, bankAccountId);
    }

    public async Task<bool> DeleteIncomeFromHistoryAsync(Guid id, Guid bankAccountId)
    {
        return await _repository.DeleteIncomeFromHistoryAsync(id, bankAccountId);
    }

    public async Task<bool> DeleteExpenseFromHistoryAsync(Guid id, Guid bankAccountId)
    {
        return await _repository.DeleteExpenseFromHistoryAsync(id, bankAccountId);
    }
}
