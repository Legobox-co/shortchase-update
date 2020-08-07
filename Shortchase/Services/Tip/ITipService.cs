using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface ITipService
    {
        
        Task<ICollection<Tip>> GetAll(bool? isEnabled = null);
        Task<ICollection<TipsJSONDto>> GetAllFromMarket(int id);

        Task<Tip> GetById(int id);

        Task<bool> Insert(Tip item);
        Task<bool> Update(Tip item);
        Task<bool> Delete(int id);

        Task<bool> SwitchStatus(int id, bool newStatus);

        Task<bool> SoftDelete(int id);

        Task<bool> DeleteBatch(int[] Ids);
    }
}