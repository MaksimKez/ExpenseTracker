namespace ExpenseTracker.Mappers.Interface;

public interface IMapper<TModel, TEntity>
{
    TModel MapToModel(TEntity from);
    TEntity MapToEntity(TModel from);
}