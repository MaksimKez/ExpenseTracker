namespace ExpenseTracker.Data.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Guid BankAccountId { get; set; }
    public BankAccountEntity BankAccountEntity { get; set; }
}