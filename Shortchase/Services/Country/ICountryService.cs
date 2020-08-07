using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface ICountryService
    {
        
        Task<ICollection<Country>> GetAll();

        Task<Country> GetById(int id);
        Task<ICollection<Country>> GetByName(string CountryName);
        Task<ICollection<Country>> GetByCode(string CountryCode);




    }
}