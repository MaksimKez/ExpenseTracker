namespace ExpenseTracker.Models.dtos;


public class RegisterUserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public Guid BankAccountId { get; set; }
}