using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IRewardsMappingService
    {
        Task<ICollection<RewardsMapping>> GetAll(bool? Available = null);
        Task<RewardsMapping> GetById(int id);
        Task<bool> Insert(RewardsMapping mapping);
        Task<bool> Update(RewardsMapping mapping);
        Task<bool> Delete(int id);
        Task<bool> Activate(int id);
        Task<bool> Deactivate(int id);

    }
}