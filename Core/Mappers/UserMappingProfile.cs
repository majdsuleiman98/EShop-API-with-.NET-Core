

using Core.DTOs;
using Core.Entities;

namespace Core.Mappers
{
    public static class UserMappingProfile
    {
        public static AppUser ToEntity(this RegisterDto registerDto)
        {
            return new AppUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };
        }
    }
}
