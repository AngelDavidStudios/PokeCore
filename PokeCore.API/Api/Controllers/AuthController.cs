using Microsoft.AspNetCore.Mvc;
using PokeCore.API.Api.Controllers.Others;
using PokeCore.API.Auth.Interfaces;

namespace PokeCore.API.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRequest req)
    {
        var msg = await _authService.RegisterAsync(req.Email, req.Password);
        return Ok(new { message = msg });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequest req)
    {
        var token = await _authService.LoginAsync(req.Email, req.Password);
        return Ok(new { token });
    }
}