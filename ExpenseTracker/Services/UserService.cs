using System.Text.RegularExpressions;
using ExpenseTracker.Data.Entities;
using ExpenseTracker.Data.Repos.Interfaces;
using ExpenseTracker.Mappers.Interface;
using ExpenseTracker.Models;
using ExpenseTracker.Services.Interfaces;

namespace ExpenseTracker.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository; 
    private readonly IMapper<User, UserEntity> _mapper;

    public UserService(IUserRepository repository, IMapper<User, UserEntity> mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Guid> RegisterUserAsync(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || !Regex.IsMatch(username, "^[a-zA-Z0-9]+$"))
        {
            return Guid.Empty;
        }
        
        return await _repository.RegisterAsync(username, password);
    }
    
    public Guid Login(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return Guid.Empty;
        }
        
        return _repository.Login(username, password);
    }
    
    public User? GetUserById(Guid userId)
    {
        var entity = _repository.GetUserById(userId);
        return entity == null ? null : _mapper.MapToModel(entity);
    }
}