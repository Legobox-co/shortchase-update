using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IMarketService
    {
        
        Task<ICollection<Market>> GetAll(bool? isEnabled = null);

        Task<Market> GetById(int id);
        Task<ICollection<MarketJSONDto>> GetAllByCategoryId(int id);

        Task<bool> Insert(Market item);
        Task<bool> Update(Market item);
        Task<bool> Delete(int id);

        Task<bool> SwitchStatus(int id, bool newStatus);
        Task<bool> SoftDelete(int id);
        Task<bool> DeleteBatch(int[] Ids);
    }
}