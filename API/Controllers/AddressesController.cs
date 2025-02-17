using API.Extentions;
using Core.DTOs;
using Core.Entities;
using Core.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressesController(SignInManager<AppUser> signInManager) : ControllerBase
    {
        [HttpGet("my-address")]
        public async Task<ActionResult> GetUserAddress()
        {
            var user = await signInManager.UserManager.GetUserWithAddress(User);
            return Ok(new
            {
                Address = user.Address?.ToDto(),
            });
        }

        [HttpPost("update-address")]
        public async Task<ActionResult> CreateOrUpdateAddress(AddressDto addressDto)
        {
            var user = await signInManager.UserManager.GetUserWithAddress(User);
            if(user.Address == null)
            {
                user.Address = addressDto.ToEntity();
            }
            else
            {
                user.Address.UpdateFromDto(addressDto);
            }
            var result = await signInManager.UserManager.UpdateAsync(user);
            if(!result.Succeeded) return BadRequest("Problem updating user address");
            return Ok();
        }
    }
}
