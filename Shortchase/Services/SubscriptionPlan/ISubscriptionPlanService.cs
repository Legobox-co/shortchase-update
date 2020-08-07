using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface ISubscriptionPlanService
    {
        
        Task<ICollection<SubscriptionPlan>> GetAll(bool? IsActive = null);
        Task<ICollection<SubscriptionPlan>> GetAllFromType(string Type, bool? IsActive = null);

        Task<SubscriptionPlan> GetById(int id);

        Task<bool> Insert(SubscriptionPlan item);
        Task<bool> Update(SubscriptionPlan item);
        Task<bool> Delete(int id);

        Task<bool> SwitchStatus(int id, bool newStatus);
        Task<bool> UpdatePaypalProductCreated(SubscriptionPlan item);
    }
}