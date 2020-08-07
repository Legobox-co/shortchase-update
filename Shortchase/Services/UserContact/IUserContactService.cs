using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IUserContactService
    {
        
        Task<ICollection<UserContact>> GetAll();
        Task<ICollection<UserContact>> GetAllByUserId(Guid UserId, string Origin = null);

        Task<UserContact> GetById(int id);

        Task<bool> Insert(UserContact item);
        Task<bool> InsertBatch(ICollection<UserContact> items);
        Task<bool> Update(UserContact item);


    }
}