using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IUserSubscriptionService
    {
        
        Task<ICollection<UserSubscription>> GetAll(bool? IsActive = null);
        Task<ICollection<UserSubscription>> GetAllFromType(string Type, bool? IsActive = null);

        Task<UserSubscription> GetById(Guid id);
        Task<ICollection<UserSubscription>> GetAllFromUser(Guid id, bool? IsActive = null);
        Task<bool> UserHasActiveSubscriptionPlan(UserSubscription item);
        Task<bool> Insert(UserSubscription item);
        Task<bool> Update(UserSubscription item);
        Task<bool> Delete(Guid id);

        Task<UserSubscription> GetActiveSubscriptionPlan(Guid UserId, string Type);

        Task<bool> Cancel(UserSubscription item);
        Task<ICollection<UserSubscription>> GetSubscriptionsToRenew();

    }
}