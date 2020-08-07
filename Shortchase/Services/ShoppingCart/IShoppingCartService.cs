using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IShoppingCartService
    {

        Task<ShoppingCart> GetByUserId(Guid id);
        Task<ShoppingCart> GetById(Guid id);

        Task<bool> Insert(ShoppingCart item);
        Task<bool> Update(ShoppingCart item);
        Task<int?> CountItems(Guid UserId);

        Task<bool?> AddItemToCart(Guid UserId, Guid ItemId);
        Task<bool> RemoveItemToCart(Guid UserId, Guid ItemId);
        Task<bool> CleanCart(Guid UserId);
        Task<bool?> IsItemInCart(Guid UserId, Guid ItemId);
        Task<ICollection<ShoppingCartItemDto>> GetItemsInCart(Guid UserId);
        Task<ICollection<ShoppingCartItemDto>> GetOnlyValidItemsInCart(Guid UserId);
    }
}