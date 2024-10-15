using ExpenseTracker.Data.Entities;
using ExpenseTracker.Mappers.Interface;
using ExpenseTracker.Models;

namespace ExpenseTracker.Mappers;

public class ExpenseMapper : IMapper<Expense, ExpenseEntity>
{
    /// <summary>
    /// Maps the given <see cref="ExpenseEntity"/> to a <see cref="Expense"/>.
    /// </summary>
    /// <param name="from">The <see cref="ExpenseEntity"/> to map.</param>
    /// <returns>The mapped <see cref="Expense"/>.</returns>
    public Expense MapToModel(ExpenseEntity from)
    {
        return new Expense()
        {
            Id = from.Id,
            Sum = from.Sum,
            Title = from.Title,
            CreatedAt = from.CreatedAt
        };
    }

    /// <summary>
    /// Maps the given <see cref="Expense"/> to a <see cref="ExpenseEntity"/>.
    /// </summary>
    /// <param name="from">The <see cref="Expense"/> to map.</param>
    /// <returns>The mapped <see cref="ExpenseEntity"/>.</returns>
    public ExpenseEntity MapToEntity(Expense from)
    {
        return new ExpenseEntity()
        {
            Id = from.Id,
            Sum = from.Sum,
            Title = from.Title,
            CreatedAt = from.CreatedAt
        };
    }
}