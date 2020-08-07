using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IOrderService
    {
        
        Task<ICollection<Order>> GetAll();

        Task<Order> GetById(Guid id);

        Task<bool> Insert(Order item);
        Task<bool> Update(Order item);
        Task<bool> Delete(Guid id);
        Task<int> CountTotal();
        Task<bool> HasPurchased(Guid UserId, Guid ItemId);
        Task<bool> PlaceNewOrder(Order order, ICollection<OrderItem> orderItems, ShoppingCart cart, User user);
        Task<ICollection<OrderManagerOrderDto>> GetAllOrdersFromUser(Guid UserId);
        Task<ICollection<OrderManagerOrderDto>> GetAllOrdersToUser(Guid UserId);

        Task<ICollection<Order>> GetAllOrdersForBackend();
        Task<OrderDetailsDto> GetOrderDetails(Guid id);

        Task<int> CountTotalByUser(Guid id);
        Task<decimal> GetTotalAmount();
        Task<bool> UpdateOrderItemPayouts(Guid orderId, Guid userId, Guid payoutId);
        Task<bool> UpdateOrderItemPayouts(Guid orderId, ICollection<UserPayout> NewPayouts);
        Task<OrderDetailsReceiptDto> GetOrderDetailsForReceipt(Guid id);
    }
}