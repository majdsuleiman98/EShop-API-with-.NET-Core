using Core.DTOs;
using Core.Entities;

namespace Core.Mappers
{
    public static class AddressMappingProfile
    {
        public static AddressDto? ToDto(this Address address)
        {
            if (address == null) return null;
            return new AddressDto
            {
                Line = address.Line,
                City = address.City,
                State = address.State,
                Country = address.Country,
                Zipcode = address.Zipcode,
            };
        }

        public static Address? ToEntity(this AddressDto addressDto)
        {
            if (addressDto == null) return null;
            return new Address
            {
                Line = addressDto.Line,
                City = addressDto.City,
                State = addressDto.State,
                Country = addressDto.Country,
                Zipcode = addressDto.Zipcode,
            };
        }
    }
}
