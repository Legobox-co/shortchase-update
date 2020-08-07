using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IUserFollowService
    {
        
        Task<ICollection<UserFollow>> GetAll();

        Task<UserFollow> GetById(int id);
        Task<ICollection<UserFollow>> GetByFromId(Guid fromId);
        Task<ICollection<UserFollow>> GetByToId(Guid toId);
        Task<UserFollow> GetByFromTo(Guid fromId, Guid toId);
        Task<bool> IsFollowing(Guid fromId, Guid toId);

        Task<bool> Insert(UserFollow item);
        Task<bool> Update(UserFollow item);
        Task<bool> Delete(int id);


        Task<ICollection<Guid>> GetAllFollowers(Guid fromId);
    }
}