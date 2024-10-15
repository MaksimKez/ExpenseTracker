using ExpenseTracker.Data.Entities;
using ExpenseTracker.Mappers.Interface;
using ExpenseTracker.Models;

namespace ExpenseTracker.Mappers;

public class IncomeMapper : IMapper<Income, IncomeEntity>
{
    public Income MapToModel(IncomeEntity from)
    {
        return new Income
        {
            Id = from.Id,
            Title = from.Title,
            Sum = from.Sum,
            IncomeSource = from.IncomeSource,
            CreatedAt = from.CreatedAt
        };
    }

    public IncomeEntity MapToEntity(Income from)
    {
        return new IncomeEntity
        {
            Id = from.Id,
            Title = from.Title,
            Sum = from.Sum,
            IncomeSource = from.IncomeSource,
            CreatedAt = from.CreatedAt
        };
    }
}