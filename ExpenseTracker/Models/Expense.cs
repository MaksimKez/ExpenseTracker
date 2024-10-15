using ExpenseTracker.Data.Entities;

namespace ExpenseTracker.Models;

public class Expense
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Sum { get; set; }
    public ExpenseSourceEnum ExpenseSource { get; set; }
    public DateTime CreatedAt { get; set; }
}