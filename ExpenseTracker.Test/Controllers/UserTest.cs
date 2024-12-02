using System;
using System.Threading.Tasks;
using ExpenseTracker.Controllers;
using ExpenseTracker.Models;
using ExpenseTracker.Models.dtos;
using ExpenseTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace ExpenseTracker.Test.Controllers;

public class UserTest
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly UserController _controller;

    public UserTest()
    {
        _mockUserService = new Mock<IUserService>();
        _controller = new UserController(_mockUserService.Object);
    }
    
    #region Register User

    [Fact]
    public async Task RegisterUserAsync_ReturnsCreatedResult()
    {
        // Arrange
        var registerUserDto = new RegisterUserDto
        {
            Username = "testUser",
            Password = "password",
            BankAccountId = Guid.NewGuid()
        };
        var userId = Guid.NewGuid();
        _mockUserService.Setup(s => 
                s.RegisterUserAsync(registerUserDto.Username, 
                    registerUserDto.Password, 
                    registerUserDto.BankAccountId))
            .ReturnsAsync(userId);

        // Act
        var result = await _controller.RegisterUserAsync(registerUserDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(userId, createdResult.Value);
        Assert.Equal(nameof(_controller.GetUserById), createdResult.ActionName);
    }

    [Fact]
    public async Task RegisterUserAsync_ReturnsBadRequest()
    {
        // Arrange
        var registerUserDto = new Models.dtos.RegisterUserDto
        {
            Username = "testUser",
            Password = "password",
            BankAccountId = Guid.NewGuid()
        };
        _mockUserService.Setup(s => 
                s.RegisterUserAsync(registerUserDto.Username, 
                    registerUserDto.Password, 
                    registerUserDto.BankAccountId))
                        .ReturnsAsync(Guid.Empty);

        // Act
        var result = await _controller.RegisterUserAsync(registerUserDto);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    #endregion
    
    /*#region Login

    [Fact]
    public void Login_ReturnsOkResult()
    {
        // Arrange
        var loginUserDto = new LoginUserDto
        {
            Username = "testUser",
            Password = "password"
        };
        var bankAccountId = Guid.NewGuid();
        _mockUserService.Setup(s => 
                s.Login(loginUserDto.Username, 
                    loginUserDto.Password))
            .Returns(bankAccountId);

        // Act
        var result = _controller.Login(loginUserDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(bankAccountId, okResult.Value);
    }

    [Fact]
    public void Login_ReturnsBadRequest()
    {
        // Arrange
        var loginUserDto = new LoginUserDto
        {
            Username = "testUser",
            Password = "password"
        };
        _mockUserService.Setup(s => 
                s.Login(loginUserDto.Username, 
                    loginUserDto.Password))
            .Returns(Guid.Empty);

        // Act
        var result = _controller.Login(loginUserDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
    }

    #endregion*/
    
    #region Get User by Id

    [Fact]
    public void GetUserById_ReturnsOkResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId, 
            Username = "testUser"
        }; 
        _mockUserService.Setup(s => 
                s.GetUserById(userId))
            .Returns(user);

        // Act
        var result = _controller.GetUserById(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result); 
        Assert.Equal(user, okResult.Value);
    }

    [Fact]
    public void GetUserById_ReturnsNotFoundResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockUserService.Setup(s => 
                s.GetUserById(userId))
            .Returns((User)null);

        // Act
        var result = _controller.GetUserById(userId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    #endregion
}
