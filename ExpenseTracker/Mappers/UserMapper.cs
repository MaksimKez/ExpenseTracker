using ExpenseTracker.Data.Entities;
using ExpenseTracker.Mappers.Interface;
using ExpenseTracker.Models;

namespace ExpenseTracker.Mappers;

public class UserMapper : IMapper<User, UserEntity>
{
    public User MapToModel(UserEntity from)
    {
        return new User
        {
            Id = from.Id,
            Username = from.Username
        };
    }

    public UserEntity MapToEntity(User from)
    {     
        return new UserEntity
        {
            Id = from.Id,
            Username = from.Username
        };
    }
}
