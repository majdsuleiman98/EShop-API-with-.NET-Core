using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Authentication;


namespace API.Extentions
{
    public static class ClaimsPrincipleExtensions
    {
        public static async Task<AppUser> GetUserWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var des_user = await userManager.Users
                                            .Include(x => x.Address)
                                            .FirstOrDefaultAsync(x => x.Email == user.GetEmail());
            if (des_user == null) throw new AuthenticationException("User not found");
            return des_user;
        }

        public static async Task<AppUser> GetUserWithEmail(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var des_user = await userManager.Users
                                            .FirstOrDefaultAsync(x => x.Email == user.GetEmail());
            if (des_user == null) throw new AuthenticationException("User not found");
            return des_user;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email) ?? throw new ArgumentException("Email claim not found");
            return email;
        }
    }
}
