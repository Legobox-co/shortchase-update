using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IPaypalSettingsService
    {
        
        Task<ICollection<PaypalSettings>> GetAll();
        Task<PaypalSettings> GetDefault();
        Task<PaypalSettings> GetById(int id);

        Task<bool> Insert(PaypalSettings item);
        Task<bool> Update(PaypalSettings item);
        Task<bool> Delete(int id);
        Task<bool> SwitchStatus(int id, bool newStatus);

    }
}