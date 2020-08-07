using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface ISecondaryEmailTemplateService
    {
        
        Task<ICollection<SecondaryEmailTemplate>> GetAll();

        Task<SecondaryEmailTemplate> GetById(int id);

        Task<bool> Insert(SecondaryEmailTemplate address);
        Task<bool> Update(SecondaryEmailTemplate address);
        Task<bool> Delete(int id);


    }
}