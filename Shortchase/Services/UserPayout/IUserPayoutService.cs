using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IUserPayoutService
    {
        
        Task<ICollection<UserPayout>> GetAll();
        Task<ICollection<UserPayout>> GetAllByUser(Guid Id);

        Task<UserPayout> GetById(Guid id);

        Task<bool> Insert(UserPayout item);
        Task<bool> Insert(ICollection<UserPayout> items);
        Task<bool> Update(UserPayout item);
        Task<bool> Delete(Guid id);



    }
}