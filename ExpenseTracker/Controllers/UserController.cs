using ExpenseTracker.Models;
using ExpenseTracker.Models.dtos;
using ExpenseTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers;

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

    [HttpPost("login/user")]
    public ActionResult<Guid> Login([FromBody] LoginUserDto loginUserDto)
    {
        var accountId = _userService.Login(loginUserDto.Username , loginUserDto.Password);
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