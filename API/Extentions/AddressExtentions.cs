
using Core.DTOs;
using Core.Entities;

namespace API.Extentions
{
    public static class AddressExtentions
    {       
        public static void UpdateFromDto(this Address address, AddressDto addressDto)
        {
            if (addressDto == null) throw new ArgumentNullException(nameof(addressDto));
            if (address == null) throw new ArgumentNullException(nameof(address));

            address.Line = addressDto.Line;
            address.City = addressDto.City;
            address.State = addressDto.State;
            address.Country = addressDto.Country;
            address.Zipcode = addressDto.Zipcode;
        }
    }
}
