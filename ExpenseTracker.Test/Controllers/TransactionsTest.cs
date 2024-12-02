using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Controllers;
using ExpenseTracker.Models;
using ExpenseTracker.Services.Interfaces;
using Assert = Xunit.Assert;

namespace ExpenseTracker.Test.Controllers;

public class TransactionsTest
{ 
    private readonly Mock<ITransactionsFacade> _mockTransactionsFacade;
    private readonly TransactionsController _controller;

    public TransactionsTest()
    {
        _mockTransactionsFacade = new Mock<ITransactionsFacade>();
        _controller = new TransactionsController(_mockTransactionsFacade.Object);
    }

    #region Get income and expenses by bank account id

    [Fact]
    public void GetIncomesByBankAccountId_ReturnsOkResult()
    {
        // Arrange
        var bankAccountId = Guid.NewGuid();
        _mockTransactionsFacade.Setup(f => 
                f.GetIncomesByBankAccountId(bankAccountId))
                               .Returns(new List<Income>());

        // Act
        var result = _controller.GetIncomesByBankAccountId(bankAccountId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetExpensesByBankAccountId_ReturnsOkResult()
    {
        // Arrange
        var bankAccountId = Guid.NewGuid();
        _mockTransactionsFacade.Setup(f => 
                f.GetExpensesByBankAccountId(bankAccountId))
                               .Returns(new List<Expense>());

        // Act
        var result = _controller.GetExpensesByBankAccountId(bankAccountId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    #endregion

    #region delete income and expense

    [Fact]
    public async Task DeleteIncome_ReturnsOkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var bankAccountId = Guid.NewGuid();
        _mockTransactionsFacade.Setup(f => 
                f.DeleteIncomeAsyncThenDeleteFromHistoryAsync(id, bankAccountId))
                               .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteIncome(id, bankAccountId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteIncome_ReturnsNotFoundResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var bankAccountId = Guid.NewGuid();
        _mockTransactionsFacade.Setup(f => 
                f.DeleteIncomeAsyncThenDeleteFromHistoryAsync(id, bankAccountId))
                               .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteIncome(id, bankAccountId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteExpense_ReturnsOkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var bankAccountId = Guid.NewGuid();
        _mockTransactionsFacade.Setup(f => 
                f.DeleteExpenseAsyncThenDeleteFromHistoryAsync(id, bankAccountId))
                               .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteExpense(id, bankAccountId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteExpense_ReturnsNotFoundResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var bankAccountId = Guid.NewGuid();
        _mockTransactionsFacade.Setup(f => 
                f.DeleteExpenseAsyncThenDeleteFromHistoryAsync(id, bankAccountId))
                               .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteExpense(id, bankAccountId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    #endregion

    #region add income and expense

    [Fact]
    public async Task AddIncome_ReturnsCreatedResult()
    {
        // Arrange
        var bankAccountId = Guid.NewGuid();
        var income = new Income();
        _mockTransactionsFacade.Setup(f => 
                f.AddIncomeThenAddToHistoryAsync(bankAccountId, income))
                               .ReturnsAsync(Guid.NewGuid());

        // Act
        var result = await _controller.AddIncome(bankAccountId, income);

        // Assert
        Assert.IsType<CreatedResult>(result.Result);
    }

    [Fact]
    public async Task AddIncome_ReturnsBadRequestResult()
    {
        // Arrange
        var bankAccountId = Guid.NewGuid();
        var income = new Income();
        _mockTransactionsFacade.Setup(f => 
                f.AddIncomeThenAddToHistoryAsync(bankAccountId, income))
                               .ReturnsAsync(Guid.Empty);

        // Act
        var result = await _controller.AddIncome(bankAccountId, income);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task AddExpense_ReturnsCreatedResult()
    {
        // Arrange
        var bankAccountId = Guid.NewGuid();
        var expense = new Expense();
        _mockTransactionsFacade.Setup(f => 
                f.AddExpenseThenAddToHistoryAsync(bankAccountId, expense))
                               .ReturnsAsync(Guid.NewGuid());

        // Act
        var result = await _controller.AddExpense(bankAccountId, expense);

        // Assert
        Assert.IsType<CreatedResult>(result.Result);
    }

    [Fact]
    public async Task AddExpense_ReturnsBadRequestResult()
    {
        // Arrange
        var bankAccountId = Guid.NewGuid();
        var expense = new Expense();
        _mockTransactionsFacade.Setup(f => 
                f.AddExpenseThenAddToHistoryAsync(bankAccountId, expense))
                               .ReturnsAsync(Guid.Empty);

        // Act
        var result = await _controller.AddExpense(bankAccountId, expense);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    #endregion

    #region get income and expense by id

    [Fact]
    public async Task GetExpenseById_ReturnsOkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expense = new Expense();
        _mockTransactionsFacade.Setup(f => 
                f.GetExpenseByIdAsync(id))
                               .ReturnsAsync(expense);

        // Act
        var result = await _controller.GetExpenseById(id);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetExpenseById_ReturnsNotFoundResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockTransactionsFacade.Setup(f => 
                f.GetExpenseByIdAsync(id))
                               .ReturnsAsync((Expense)null);

        // Act
        var result = await _controller.GetExpenseById(id);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetIncomeById_ReturnsOkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var income = new Income();
        _mockTransactionsFacade.Setup(f => 
                f.GetIncomeByIdAsync(id))
                               .ReturnsAsync(income);

        // Act
        var result = await _controller.GetIncomeById(id);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetIncomeById_ReturnsNotFoundResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockTransactionsFacade.Setup(f => 
                f.GetIncomeByIdAsync(id))
                               .ReturnsAsync((Income)null);

        // Act
        var result = await _controller.GetIncomeById(id);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    #endregion
    
    #region CRUD on BankAccount

    [Fact]
    public async Task CreateBankAccount_ReturnsCreatedResult()
    {
        // Arrange
        var bankAccountId = Guid.NewGuid();
        _mockTransactionsFacade.Setup(f => 
                f.CreateBankAccountAsync(It.IsAny<BankAccount>()))
                               .ReturnsAsync(bankAccountId);

        // Act
        var result = await _controller.CreateBankAccount();

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result.Result);
        Assert.Equal(bankAccountId, createdResult.Value);
    }

    [Fact]
    public async Task CreateBankAccount_ReturnsNotFoundResult()
    {
        // Arrange
        _mockTransactionsFacade.Setup(f => 
                f.CreateBankAccountAsync(It.IsAny<BankAccount>()))
                               .ReturnsAsync(Guid.Empty);

        // Act
        var result = await _controller.CreateBankAccount();

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetBankAccountById_ReturnsOkResult()
    {
        // Arrange
        var bankAccountId = Guid.NewGuid();
        var bankAccount = new BankAccount { Id = bankAccountId };
        _mockTransactionsFacade.Setup(f => 
                f.GetBankAccountByIdAsync(bankAccountId))
                               .ReturnsAsync(bankAccount);

        // Act
        var result = await _controller.GetBankAccountById(bankAccountId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(bankAccount, okResult.Value);
    }

    [Fact]
    public async Task GetBankAccountById_ReturnsNotFoundResult()
    {
        // Arrange
        var bankAccountId = Guid.NewGuid();
        _mockTransactionsFacade.Setup(f => 
                f.GetBankAccountByIdAsync(bankAccountId))
                               .ReturnsAsync((BankAccount)null);

        // Act
        var result = await _controller.GetBankAccountById(bankAccountId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task DeleteBankAccount_ReturnsOkResult()
    {
        // Arrange
        var bankAccountId = Guid.NewGuid();
        _mockTransactionsFacade.Setup(f => 
                f.DeleteBankAccountAsync(bankAccountId))
                               .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteBankAccount(bankAccountId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteBankAccount_ReturnsNotFoundResult()
    {
        // Arrange
        var bankAccountId = Guid.NewGuid();
        _mockTransactionsFacade.Setup(f => 
                f.DeleteBankAccountAsync(bankAccountId))
                               .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteBankAccount(bankAccountId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateBankAccount_ReturnsOkResult()
    {
        // Arrange
        var bankAccount = new BankAccount { Id = Guid.NewGuid(), Balance = 100 };
        _mockTransactionsFacade.Setup(f => 
                f.UpdateBankAccountAsync(bankAccount))
                               .ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateBankAccount(bankAccount);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateBankAccount_ReturnsBadRequestResult()
    {
        // Arrange
        var bankAccount = new BankAccount { Id = Guid.NewGuid(), Balance = 100 };
        _mockTransactionsFacade.Setup(f => 
                f.UpdateBankAccountAsync(bankAccount))
                               .ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateBankAccount(bankAccount);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    #endregion
}