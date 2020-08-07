using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IMessageService
    {
        
        Task<ICollection<Message>> GetAll(bool? IsRead = null);

        Task<Message> GetById(int id);

        Task<bool> Insert(Message item);
        Task<bool> Update(Message item);
        Task<bool> Delete(int id);

        Task<ICollection<Message>> GetAllForUser(Guid UserId);

        Task<ICollection<MessagesListDto>> GetAllUsersThatHaveMessages();
        Task<bool> UpdateBatch(ICollection<Message> items);
    }
}