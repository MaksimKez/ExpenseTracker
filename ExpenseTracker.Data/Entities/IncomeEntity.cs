using System.Security.AccessControl;

namespace ExpenseTracker.Data.Entities;

public class IncomeEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Sum { get; set; }
    public IncomeSourceEnum IncomeSource { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid BankAccountId { get; set; }
}