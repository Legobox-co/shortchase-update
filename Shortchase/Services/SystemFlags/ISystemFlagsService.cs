using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface ISystemFlagsService
    {
        
        Task<ICollection<SystemFlags>> GetAll();

        Task<SystemFlags> GetById(int id);
        Task<SystemFlags> GetByName(string name);

        Task<bool> Insert(SystemFlags flag);
        Task<bool> Update(SystemFlags flag);
        Task<bool> Delete(int id);

    }
}