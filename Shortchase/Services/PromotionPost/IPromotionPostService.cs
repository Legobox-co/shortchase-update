using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IPromotionPostService
    {
        
        Task<ICollection<PromotionPost>> GetAll(bool? isPublished = null);
        Task<ICollection<PromotionListItemDto>> GetAllForbackend();
        Task<ICollection<PromotionListWebsiteItemDto>> GetAllForWebsite();

        Task<PromotionPost> GetById(int id);
        Task<PromotionPost> GetBySlug(string slug);
        Task<bool> Insert(PromotionPost item);
        Task<bool> Update(PromotionPost item);
        Task<bool> Delete(int id);

        Task<bool> SwitchStatus(int id, bool newStatus);


    }
}