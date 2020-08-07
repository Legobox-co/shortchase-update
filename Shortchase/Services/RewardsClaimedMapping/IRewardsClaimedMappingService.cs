using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IRewardsClaimedMappingService
    {
        Task<ICollection<RewardsClaimedMapping>> GetAll();
        Task<ICollection<RewardsClaimedMapping>> GetAllFromUser(Guid userId, bool? Used = null);
        Task<ICollection<RewardsClaimedMapping>> GetHistoryFromUser(Guid userId);
        Task<RewardsClaimedMapping> GetById(Guid id);
        Task<bool> Insert(RewardsClaimedMapping mapping);
        Task<bool> Update(RewardsClaimedMapping mapping);
        Task<bool> Delete(Guid id);
        Task<ICollection<RewardsClaimedMapping>> GetHistoryFromAllUsers();
    }
}