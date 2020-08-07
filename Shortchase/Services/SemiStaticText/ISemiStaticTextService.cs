using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface ISemiStaticTextService
    {
        
        Task<ICollection<SemiStaticText>> GetAll(bool? isEnabled = null);

        Task<SemiStaticText> GetById(int id);
        Task<SemiStaticText> GetByName(string Name);

        Task<bool> Insert(SemiStaticText item);
        Task<bool> Update(SemiStaticText item);
        Task<bool> Delete(int id);

        Task<bool> SwitchStatus(int id, bool newStatus);

        Task<bool> UpdateAllConfigs(SemiStaticText appName, SemiStaticText appTagline, SemiStaticText appLogo);
    }
}