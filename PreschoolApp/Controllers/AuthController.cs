using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PreschoolApp.DTO;
using PreschoolApp.Models;
using PreschoolApp.Services;
using PreschoolApp.Services.Interfaces;

namespace PreschoolApp.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(LoginDTO), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<LoginDTO>> Login([FromBody] LoginRequest loginRequest)
        {
            User user = await _userService.GetUserAsync(loginRequest.Email);
            if (user is null)
            {
                throw new Exception("Bad user login or password");
            }

            string token = await _authService.GenerateTokenAsync(user, loginRequest.Password);
            return new LoginDTO
            {
                Token = token
            };

        }
    }
}

