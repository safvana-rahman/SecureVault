using Microsoft.AspNetCore.Mvc;
using SecureVault.Models;
using SecureVault.Services;
using Microsoft.AspNetCore.Identity;

namespace SecureVault.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;
        public AuthController(AuthService authService, TokenService tokenService,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _authService = authService;
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpPost("register")]
public async Task<IActionResult> Register([FromBody] RegisterModel model, [FromQuery] string role = "User")
{
    if (string.IsNullOrWhiteSpace(model.Username) ||
        string.IsNullOrWhiteSpace(model.Email) ||
        string.IsNullOrWhiteSpace(model.Password))
    {
        return BadRequest("All fields are required.");
    }

    var user = new ApplicationUser
    {
        UserName = model.Username,
        Email = model.Email
    };

    var result = await _userManager.CreateAsync(user, model.Password);

    if (result.Succeeded)
    {
        // Ensure role exists before assigning
        if (!await _roleManager.RoleExistsAsync(role))
        {
            await _roleManager.CreateAsync(new IdentityRole(role));
        }

        await _userManager.AddToRoleAsync(user, role);
        return Ok($"User registered successfully with role: {role}");
    }

    return BadRequest(result.Errors);
}
        [HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginModel model)
{
    var user = await _userManager.FindByNameAsync(model.Username);
    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
    {
        var token = _tokenService.GenerateToken(user);
        return Ok(new { token });
    }
    return Unauthorized("Invalid credentials");
}
    }
}