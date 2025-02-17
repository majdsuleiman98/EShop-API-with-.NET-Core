using API.Extentions;
using API.Filters;
using Core.DTOs;
using Core.Entities;
using Core.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(SignInManager<AppUser> signInManager) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = registerDto.ToEntity();
            var result = await signInManager.UserManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }
            return Ok(new
            {
                Message = "Register successfully",
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }

        [LogSensitiveAction]
        [Authorize]
        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            var user = await signInManager.UserManager.GetUserWithEmail(User);
            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.UserName,
                Role = User.FindFirstValue(ClaimTypes.Role)  
            });
        }

        [Authorize]
        [HttpGet("auth-status")]
        public ActionResult GetAuthStatus()
        {
            return Ok(new { IsAuthenticated = User.Identity?.IsAuthenticated ?? false });
        }
    }
}
