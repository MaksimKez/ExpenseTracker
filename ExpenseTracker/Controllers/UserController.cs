using ExpenseTracker.Models;
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
    public async Task<ActionResult<Guid>> RegisterUserAsync([FromBody]string username, [FromBody] string password)
    {
        var id = await _userService.RegisterUserAsync(username, password);
        if (id.Equals(Guid.Empty))
            return BadRequest();
        
        return Created(nameof(GetUserById), id);
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