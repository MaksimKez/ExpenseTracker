using ExpenseTracker.Models;
using ExpenseTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers;

[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionsFacade _transactionsFacade;
    
    public TransactionsController(ITransactionsFacade transactionsFacade)
    {
        _transactionsFacade = transactionsFacade ?? throw new ArgumentNullException(nameof(transactionsFacade));
    }
    
    #region Get income and expenses by bank account id

    [HttpGet("bankaccounts/{bankAccountId}/incomes")]
    public ActionResult GetIncomesByBankAccountId(Guid bankAccountId)
    {
        return Ok(_transactionsFacade.GetIncomesByBankAccountId(bankAccountId));
    }
    
    [HttpGet("bankaccounts/{bankAccountId}/expenses")]
    public ActionResult GetExpensesByBankAccountId(Guid bankAccountId)
    {
        return Ok(_transactionsFacade.GetExpensesByBankAccountId(bankAccountId));
    }

    #endregion


    #region delete income and expense

    [HttpDelete("delete/incomes/{id}/{bankAccountId}")]
    public async Task<ActionResult> DeleteIncome([FromRoute]Guid id, [FromRoute] Guid bankAccountId)
    {
        var isSuccess = await _transactionsFacade.DeleteIncomeAsyncThenDeleteFromHistoryAsync(id, bankAccountId);
        if (!isSuccess)
            return NotFound();
        return Ok();
    }
    
    [HttpDelete("delete/expenses/{id}/{bankAccountId}")]
    public async Task<ActionResult> DeleteExpense([FromRoute]Guid id, [FromRoute]Guid bankAccountId)
    {
        var isSuccess = await _transactionsFacade.DeleteExpenseAsyncThenDeleteFromHistoryAsync(id, bankAccountId);
        if (!isSuccess)
            return NotFound();
        return Ok();
    }

    #endregion


    #region add income and expense

    [HttpPost("add/incomes/{bankAccountId:guid}")]
    public async Task<ActionResult<Guid>> AddIncome([FromRoute]Guid bankAccountId, [FromBody]Income income)
    {
        var incomeId = await _transactionsFacade.AddIncomeThenAddToHistoryAsync(bankAccountId, income);
        if (incomeId.Equals(Guid.Empty))
            return NotFound();
        return Created(nameof(GetIncomeById), incomeId);
    }
    
    [HttpPost("add/expenses/{bankAccountId:guid}")]
    public async Task<ActionResult<Guid>> AddExpense([FromRoute]Guid bankAccountId, [FromBody]Expense expense)
    {
        var expenseId = await _transactionsFacade.AddExpenseThenAddToHistoryAsync(bankAccountId, expense);
        return expenseId.Equals(Guid.Empty) ? NotFound() : Created(nameof(GetExpenseById), expenseId);
    }

    #endregion


    #region get income and expense by id

    [HttpGet("get/expenses/{id:guid}")]
    public async Task<ActionResult<Expense>> GetExpenseById([FromRoute]Guid id)
    {
        var expense = await _transactionsFacade.GetExpenseByIdAsync(id);
        if (expense == null)
            return NotFound();
        
        return Ok(expense); 
    }
    
    [HttpGet("get/incomes/{id:guid}")]
    public async Task<ActionResult<Income>> GetIncomeById([FromRoute]Guid id)
    {
        var income = await _transactionsFacade.GetIncomeByIdAsync(id);
        if (income == null)
            return NotFound();
        
        return Ok(income); 
    }

    #endregion

    #region CRUD on BankAccount

    [HttpPost("create/bankaccount")]
    public async Task<ActionResult<Guid>> CreateBankAccount([FromBody]BankAccount bankAccount)
    {
        var bankAccountId = await _transactionsFacade.CreateBankAccountAsync(bankAccount);
        return bankAccountId.Equals(Guid.Empty) ? NotFound() : Created(nameof(GetBankAccountById), bankAccountId);
    }

    [HttpGet("get/bankaccount/{id:guid}")]
    public async Task<ActionResult<BankAccount>> GetBankAccountById([FromRoute] Guid id)
    {
        var bankAccount = await _transactionsFacade.GetBankAccountByIdAsync(id);
        if (bankAccount == null)
            return NotFound();
        
        return Ok(bankAccount);
    }
    
    [HttpDelete("delete/bankaccount/{id:guid}")]
    public async Task<ActionResult> DeleteBankAccount([FromRoute]Guid id)
    {
        var isSuccess = await _transactionsFacade.DeleteBankAccountAsync(id);
        if (!isSuccess)
            return NotFound();
        return Ok();
    }
    
    [HttpPut("update/bankaccount")]
    public async Task<ActionResult> UpdateBankAccount([FromBody]BankAccount bankAccount)
    {
        var isSuccess = await _transactionsFacade.UpdateBankAccountAsync(bankAccount);
        if (!isSuccess)
            return BadRequest();
        return Ok();
    }

    #endregion
}