using Microsoft.AspNetCore.Mvc;
using PreschoolApp.Requests;
using PreschoolApp.Services.Interfaces;

namespace PreschoolApp.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterUserRequest registerUserRequest)
    {
        Console.WriteLine(registerUserRequest);
        await _userService.RegisterUserAsync(registerUserRequest);
        return Ok();
    }
}
