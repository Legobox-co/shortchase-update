using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IAccessLogService
    {
        
        Task<ICollection<AccessLog>> GetAll();
        Task<AccessLog> GetLatestByUserId(Guid id);
        Task<AccessLog> GetById(int id);

        Task<bool> Insert(AccessLog item);
        Task<bool> Insert(Guid UserId, string Type, string Ip);
        Task<bool> Update(AccessLog item);
        Task<bool> Delete(int id);
        Task<bool> IsAnyAdminOnline();

    }
}