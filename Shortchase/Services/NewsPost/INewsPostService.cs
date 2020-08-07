using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface INewsPostService
    {
        
        Task<ICollection<NewsPost>> GetAll(bool? isPublished = null);
        Task<ICollection<NewsListItemDto>> GetAllForbackend();
        Task<ICollection<NewsListWebsiteItemDto>> GetAllForWebsite();

        Task<NewsPost> GetById(int id);
        Task<NewsPost> GetBySlug(string slug);
        Task<bool> Insert(NewsPost item);
        Task<bool> Update(NewsPost item);
        Task<bool> Delete(int id);

        Task<bool> SwitchStatus(int id, bool newStatus);


    }
}