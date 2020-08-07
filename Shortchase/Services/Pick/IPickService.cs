using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IPickService
    {
        Task<ICollection<Pick>> GetAll(bool? isEnabled = null);
        Task<List<Pick>> GetAll();
        Task<ICollection<PickJSONDto>> GetAllByCategoryId(int id, int TimezoneOffset);

        Task<Pick> GetById(int id);
        Task<bool> Insert(Pick item);
        
        Task<bool> Update(Pick item);
        Task<bool> Delete(int id);
        Task<bool> SwitchStatus(int id, bool newStatus);
        Task<bool> SoftDelete(int id);

        Task<bool> DeleteBatch(int[] Ids);
        Task<List<Pick>> GetAll(object p);
    }
}