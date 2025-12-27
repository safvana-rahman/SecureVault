using Microsoft.AspNetCore.Mvc;
using SecureVault.Models;
using SecureVault.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
namespace SecureVault.Controllers
{
[Authorize(Policy = "RequireUser")]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet("profile")]
    public IActionResult GetUserProfile()
    {
        return Ok("This is your user profile");
    }

    [Authorize(Policy = "RequireUser")]
[HttpGet("user-access")]
public IActionResult UserEndpoint()
{
    return Ok("Hello, User or Admin!");
}

[Authorize(Policy = "RequireGuest")]
[HttpGet("guest-access")]
public IActionResult GuestEndpoint()
{
    return Ok("Accessible by anyone with a role.");
}
}
}