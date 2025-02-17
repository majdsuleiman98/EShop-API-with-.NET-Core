using Core.DTOs;
using Core.Specifications.SpecParams;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IServices
{
    public interface IAdminOrderService
    {
        Task<PaginatedResponse<OrderDto>> GetAllAsync(OrderSpecParams specParams);
        Task<OrderDto?> GetByIdAsync(int id);
    }
}
