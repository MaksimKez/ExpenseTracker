using ExpenseTracker.Data.Entities;
using ExpenseTracker.Mappers.Interface;
using ExpenseTracker.Models;

namespace ExpenseTracker.Mappers;

public class BankAccountMapper : IMapper<BankAccount, BankAccountEntity>
{
    /// <summary>
    /// Maps a <see cref="BankAccountEntity"/> to a <see cref="BankAccount"/>.
    /// </summary>
    /// <param name="from">The <see cref="BankAccountEntity"/> to map from.</param>
    /// <returns>The mapped <see cref="BankAccount"/>.</returns>
    public BankAccount MapToModel(BankAccountEntity from)
    {
        return new BankAccount
        {
            Id = from.Id,
            Balance = from.Balance
        };
    }

    /// <summary>
    /// Maps a <see cref="BankAccount"/> to a <see cref="BankAccountEntity"/>.
    /// </summary>
    /// <param name="from">The <see cref="BankAccount"/> to map from.</param>
    /// <returns>The mapped <see cref="BankAccountEntity"/>.</returns>
    public BankAccountEntity MapToEntity(BankAccount from)
    {
        return new BankAccountEntity
        {
            Id = from.Id,
            Balance = from.Balance
        };
    }
}