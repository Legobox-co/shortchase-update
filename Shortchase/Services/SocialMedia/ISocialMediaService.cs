using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface ISocialMediaService
    {
        
        Task<ICollection<SocialMedia>> GetAll(bool? isEnabled = null);

        Task<SocialMedia> GetById(int id);

        Task<bool> Insert(SocialMedia media);
        Task<bool> Update(SocialMedia media);
        Task<bool> Delete(int id);

        Task<bool> SwitchStatus(int id, bool newStatus);


    }
}