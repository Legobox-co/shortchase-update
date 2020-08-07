using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IUserDiscountService
    {
        
        Task<ICollection<UserDiscount>> GetAll();

        Task<UserDiscount> GetById(int id);
        Task<UserDiscount> GetActiveByUserId(Guid id);

        Task<bool> Insert(UserDiscount item);
        Task<bool> Update(UserDiscount item);
        Task<bool> Delete(int id);

        Task<bool> IsUserReferred(string Email);
    }
}