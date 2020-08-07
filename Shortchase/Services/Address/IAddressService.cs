using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IAddressService
    {
        
        Task<ICollection<Address>> GetAll(bool? isEnabled = null, bool? NeedsDependantData = null);

        Task<Address> GetById(int id);
        Task<Address> GetPrimaryAddress();

        Task<bool> Insert(Address address);
        Task<bool> Update(Address address);
        Task<bool> Delete(int id);

        Task<bool> SwitchStatus(int id, bool newStatus);
        Task<bool> SwitchPrimaryAddress(int id);


    }
}