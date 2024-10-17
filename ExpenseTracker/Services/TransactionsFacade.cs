using ExpenseTracker.Models;
using ExpenseTracker.Services.Interfaces;

namespace ExpenseTracker.Services;

public class TransactionsFacade : ITransactionsFacade
{
    private readonly IIncomeService _incomeService;
    private readonly IExpenseService _expenseService;
    private readonly IBankAccountService _bankAccountService;

    public TransactionsFacade(IIncomeService incomeService, IExpenseService expenseService, IBankAccountService bankAccountService)
    {
        _incomeService = incomeService ?? throw new ArgumentNullException(nameof(incomeService));
        _expenseService = expenseService ?? throw new ArgumentNullException(nameof(expenseService));
        _bankAccountService = bankAccountService ?? throw new ArgumentNullException(nameof(bankAccountService));
    }

    #region CRUD on BankAccount
    public async Task<Guid> CreateBankAccountAsync(BankAccount bankAccount)
    {
        return await _bankAccountService.CreateBankAccountAsync(bankAccount);
    }
    
    public async Task<bool> DeleteBankAccountAsync(Guid id)
    {
        return await _bankAccountService.DeleteBankAccountAsync(id);
    }
    
    public async Task<bool> UpdateBankAccountAsync(BankAccount bankAccount)
    {
        return await _bankAccountService.UpdateBankAccountAsync(bankAccount);
    }
    
    public async Task<BankAccount?> GetBankAccountByIdAsync(Guid id)
    {
        return await _bankAccountService.GetBankAccountByIdAsync(id);
    }
    #endregion
    
    #region add/delete income/expense
    
    public async Task<Guid> AddIncomeThenAddToHistoryAsync(Guid bankAccountId, Income income)
    {
        var incomeId = await _incomeService.CreateIncomeAsync(income);
        if (incomeId.Equals(Guid.Empty))
            return Guid.Empty;
        
        var isSuccess = await _bankAccountService.AddIncomeToHistoryAsync(bankAccountId, income.Sum);
        
        return !isSuccess ? Guid.Empty : incomeId;
    }
    
    public async Task<Guid> AddExpenseThenAddToHistoryAsync(Guid bankAccountId, Expense expense)
    {
        var expenseId = await _expenseService.CreateExpenseAsync(expense);
        if (expenseId.Equals(Guid.Empty))
            return Guid.Empty;
        
        var isSuccess = await _bankAccountService.AddExpenseToHistoryAsync(bankAccountId, expense.Sum);
        
        return !isSuccess ? Guid.Empty : expenseId;
    }
    
    public async Task<bool> DeleteIncomeAsyncThenDeleteFromHistoryAsync(Guid id, Guid bankAccountId)
    {
        var isSuccess = await _bankAccountService.DeleteIncomeFromHistoryAsync(id, bankAccountId);
        if (!isSuccess)
            return false;
        
        return await _incomeService.DeleteIncomeAsync(id);
    }
    
    public async Task<bool> DeleteExpenseAsyncThenDeleteFromHistoryAsync(Guid id, Guid bankAccountId)
    {
        var isSuccess = await _bankAccountService.DeleteExpenseFromHistoryAsync(id, bankAccountId);
        if (!isSuccess)
            return false;
        
        return await _expenseService.DeleteExpenseAsync(id);
    }
    #endregion

    #region get income/expense by id
    public async Task<Income?> GetIncomeByIdAsync(Guid id)
    {
        return await _incomeService.GetIncomeByIdAsync(id);
    }
    
    public async Task<Expense?> GetExpenseByIdAsync(Guid id)
    {
        return await _expenseService.GetExpenseByIdAsync(id);
    }
    #endregion

    #region get income/expense by bank account id
    public List<Income> GetIncomesByBankAccountId(Guid bankAccountId)
    {
        return _incomeService.GetIncomesByBankAccountId(bankAccountId).ToList();
    }
    
    public List<Expense> GetExpensesByBankAccountId(Guid bankAccountId)
    {
        return _expenseService.GetExpensesByBankAccountId(bankAccountId).ToList();
    }
    #endregion
}