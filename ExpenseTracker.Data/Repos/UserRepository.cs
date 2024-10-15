using System.Diagnostics.Metrics;
using ExpenseTracker.Data.Entities;
using ExpenseTracker.Data.Repos.Interfaces;

namespace ExpenseTracker.Data.Repos;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    /// <summary>
    /// Registers a new user with the given username and password.
    /// </summary>
    /// <param name="username">The username of the user to register.</param>
    /// <param name="password">The password of the user to register.</param>
    /// <returns>A task representing the asynchronous operation, returning the id of the newly created user.</returns>
    public async Task<Guid> RegisterAsync(string username, string password)
    {
        var userEntity = new UserEntity
        {
            Id = Guid.NewGuid(),
            Username = username,
            Password = password
        };
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
        return userEntity.Id;
    }

    /// <summary>
    /// Logs a user in with the given username and password.
    /// </summary>
    /// <param name="username">The username of the user to log in.</param>
    /// <param name="password">The password of the user to log in.</param>
    /// <returns>A boolean indicating whether the user was found.</returns>
    public bool Login(string username, string password)
    {
        var userEntity = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        return userEntity != null;
    }
    
    /// <summary>
    /// Gets the bank account id of the user with the given id.
    /// </summary>
    /// <param name="userId">The id of the user to retrieve the bank account id for.</param>
    /// <returns>The bank account id of the user with the given id, or null if no such user exists.</returns>
    public Guid? GetBankAccountId(Guid userId)
    {
        var userEntity = _context.Users.FirstOrDefault(u => u.Id == userId);
        return userEntity?.BankAccountId;
    }
}