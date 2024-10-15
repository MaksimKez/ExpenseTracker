using ExpenseTracker.Data.Entities;

namespace ExpenseTracker.Models;

public class Income
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Sum { get; set; }
    public IncomeSourceEnum IncomeSource { get; set; }
    public DateTime CreatedAt { get; set; }
}