

using Core.DTOs;
using Core.Entities;

namespace Core.Mappers
{
    public static class DeliveryMethodMappingProfile
    {
        public static DeliveryMethodDto ToDto(this DeliveryMethod deliveryMethod)
        {
            return new DeliveryMethodDto
            {
                Id = deliveryMethod.Id,
                ShortName = deliveryMethod.ShortName,
                Description = deliveryMethod.Description,
                DeliveryTime = deliveryMethod.DeliveryTime,
                Price = deliveryMethod.Price,
            };
        }

        public static DeliveryMethod ToEntity(this DeliveryMethodDto deliveryMethodDto)
        {
            return new DeliveryMethod
            {
                Id = deliveryMethodDto.Id,
                ShortName = deliveryMethodDto.ShortName,
                Description = deliveryMethodDto.Description,
                DeliveryTime = deliveryMethodDto.DeliveryTime,
                Price = deliveryMethodDto.Price ?? 0,
            };
        }

        public static DeliveryMethod ToEntity(this CreateDeliveryMethodDto createDeliveryMethodDto)
        {
            return new DeliveryMethod
            {
                ShortName = createDeliveryMethodDto.ShortName,
                Description = createDeliveryMethodDto.Description,
                DeliveryTime = createDeliveryMethodDto.DeliveryTime,
                Price = createDeliveryMethodDto.Price ?? 0,
            };
        }
    }
}
