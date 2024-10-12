namespace ExpenseTracker.Data.Entities;

public class ExpenseEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Sum { get; set; }
    public ExpenseSourceEnum ExpenseSource { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid BankAccountId { get; set; }
}