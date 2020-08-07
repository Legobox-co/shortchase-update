using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IFAQItemService
    {
        
        Task<ICollection<FAQItem>> GetAll(bool? isActive = null);

        Task<FAQItem> GetById(int id);
        Task<bool> Insert(FAQItem item);
        Task<bool> Update(FAQItem item);
        Task<bool> Delete(int id);

        Task<bool> SwitchStatus(int id, bool newStatus);


    }
}