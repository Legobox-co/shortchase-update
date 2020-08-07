using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface INotificationService
    {
        
        Task<ICollection<Notification>> GetAllFromUser(Guid id);

        Task<Notification> GetById(int id);

        Task<bool> Insert(Notification item);
        Task<bool> Update(Notification item);
        Task<bool> Delete(int id);

        Task<bool> Insert(Guid id, string message);

        Task<ICollection<Notification>> GetAllNewFromUser(Guid id);

    }
}