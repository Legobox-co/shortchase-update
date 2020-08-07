using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface ISystemConstantsService
    {
        
        Task<SystemConstants> GetById(int id);
        Task<SystemConstants> GetByName(string name);
        Task<bool> Update(SystemConstants item);
        Task<bool> Insert(SystemConstants item);
        Task<bool> UpdateFeeAndTaxes(decimal fees, decimal taxes, decimal boisterousFees);
        Task<bool> UpdateSessionTimeout(int value);


    }
}