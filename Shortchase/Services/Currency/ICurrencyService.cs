using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface ICurrencyService
    {
        
        Task<ICollection<Currency>> GetAll(bool? isDeleted = null);

        Task<Currency> GetById(Guid id);

        Task<bool> Insert(Currency address);
        Task<bool> Update(Currency address);
        Task<bool> Delete(Guid id);
        Task<bool> Activate(Guid id);

        Task<bool> DeleteBatch(Guid[] Ids);
    }
}