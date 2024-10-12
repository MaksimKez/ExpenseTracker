namespace ExpenseTracker.Data.Entities;

public class BankAccountEntity
{
    public Guid Id { get; set; }
    public decimal Balance { get; set; }
    public List<ExpenseEntity> Expenses { get; set; }
    public List<IncomeEntity> Incomes { get; set; }
}