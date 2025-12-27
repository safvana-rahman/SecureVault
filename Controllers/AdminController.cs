using Microsoft.AspNetCore.Mvc;
using SecureVault.Models;
using SecureVault.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
namespace SecureVault.Controllers
{
[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    [Authorize(Roles = "Admin")]
[HttpGet("admin-only")]
public IActionResult AdminEndpoint()
{
    return Ok("Welcome, Admin!");
}
    [HttpGet("dashboard")]
    public IActionResult GetAdminDashboard()
    {
        return Ok("Welcome to the Admin Dashboard");
    }

    [HttpPost("create-user")]
    public IActionResult CreateUser()
    {
        // Admin-only logic
        return Ok("User created by Admin");
    }
}
}