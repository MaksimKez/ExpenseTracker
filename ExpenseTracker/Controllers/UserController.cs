using ExpenseTracker.Models;
using ExpenseTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers;

public class RegisterUserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public Guid BankAccountId { get; set; }
}

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }
    
    [HttpPost("register/user")]
    public async Task<ActionResult<Guid>> RegisterUserAsync([FromBody] RegisterUserDto registerUserDto)
    {
        var id = await _userService.RegisterUserAsync(registerUserDto.Username, registerUserDto.Password, registerUserDto.BankAccountId);
        if (id.Equals(Guid.Empty))
            return BadRequest();
    
        return CreatedAtAction(nameof(GetUserById), new { id }, id);
    }

    [HttpGet("login/user")]
    public ActionResult<Guid> Login([FromBody] string username,[FromBody] string password)
    {
        var accountId = _userService.Login(username, password);
        if (accountId.Equals(Guid.Empty))
            return BadRequest();
        
        return Ok(accountId);
    }
    
    [HttpGet("get/user/{id:guid}")]
    public ActionResult<User> GetUserById([FromRoute]Guid id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
            return NotFound();
        
        return Ok(user);
    }
}