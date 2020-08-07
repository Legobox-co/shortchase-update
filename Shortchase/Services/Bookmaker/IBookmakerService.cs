using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IBookmakerService
    {
        
        Task<ICollection<Bookmaker>> GetAll(bool? isEnabled = null);

        Task<Bookmaker> GetById(int id);

        Task<bool> Insert(Bookmaker item);
        Task<bool> Update(Bookmaker item);
        Task<bool> Delete(int id);

        Task<bool> SwitchStatus(int id, bool newStatus);

        Task<bool> SoftDelete(int id);

        Task<bool> DeleteBatch(int[] Ids);
    }
}