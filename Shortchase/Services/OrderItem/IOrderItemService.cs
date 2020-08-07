using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IOrderItemService
    {
        
        Task<ICollection<OrderItem>> GetAll();

        Task<OrderItem> GetById(Guid id);

        Task<bool> Insert(OrderItem item);
        Task<bool> Update(OrderItem item);
        Task<bool> Delete(Guid id);

    }
}