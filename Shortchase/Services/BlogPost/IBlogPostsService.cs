using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IBlogPostsService
    {
        
        Task<ICollection<BlogPost>> GetAll(bool? isPublished = null);
        Task<ICollection<BlogListItemDto>> GetAllForbackend();
        Task<ICollection<BlogListWebsiteItemDto>> GetAllForWebsite();

        Task<BlogPost> GetById(int id);
        Task<BlogPost> GetBySlug(string slug);
        Task<bool> Insert(BlogPost media);
        Task<bool> Update(BlogPost media);
        Task<bool> Delete(int id);

        Task<bool> SwitchStatus(int id, bool newStatus);


    }
}