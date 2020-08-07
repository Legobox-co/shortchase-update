using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;
using Shortchase.Helpers;
using Shortchase.Authorization;

namespace Shortchase.Services
{
    public class NewsPostService : INewsPostService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public NewsPostService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<NewsPost>> GetAll(bool? isPublished = null)
        {
            try
            {
                if (isPublished.HasValue) return await _context.NewsPosts.Where(a => a.IsPublished == isPublished.Value).ToListAsync().ConfigureAwait(false);
                else return await _context.NewsPosts.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<NewsListItemDto>> GetAllForbackend()
        {
            try
            {
                var items = await _context.NewsPosts
                    .Select(a => new NewsListItemDto { Id = a.Id, DateCreated = a.RowDate, IsPublished = a.IsPublished, Title = a.Title, Slug = a.Slug, DatePublished = a.DatePublished.HasValue ? a.DatePublished.Value : DateTime.MinValue })
                    .ToListAsync().ConfigureAwait(false);
                return items;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<NewsListWebsiteItemDto>> GetAllForWebsite()
        {
            try
            {
                var items = await _context.NewsPosts.Where(a => a.IsPublished || a.DatePublished.Value <= DateTime.UtcNow)
                    .Select(a => new NewsListWebsiteItemDto { Excerpt= a.Excerpt, IsPublished = a.IsPublished, Title = a.Title, Slug = a.Slug, DatePublished = a.DatePublished.HasValue ? a.DatePublished.Value : DateTime.MinValue, FeaturedImage = a.FeaturedImage })
                    .ToListAsync().ConfigureAwait(false);
                return items;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<NewsPost> GetById(int id)
        {
            try
            {
                return await _context.NewsPosts.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
        public async Task<NewsPost> GetBySlug(string slug)
        {
            try
            {
                return await _context.NewsPosts.Where(c => c.Slug == slug).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(NewsPost post)
        {
            try
            {
                if (post != null)
                {
                    var slugsUsed = await _context.NewsPosts.Where(i => i.Slug.ToLower() == post.Slug.ToLower()).ToListAsync().ConfigureAwait(false);
                    if (slugsUsed.Count > 0)
                    {
                        post.Slug = post.Slug + "-" + Guid.NewGuid().ToString();
                    }
                    post.RowDate = DateTime.UtcNow;
                    _context.NewsPosts.Add(post);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }


        public async Task<bool> Update(NewsPost post)
        {
            try
            {
                if (post != null)
                {
                    _context.Entry(post).State = EntityState.Modified;
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                }
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                NewsPost item = await _context.NewsPosts.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.NewsPosts.Remove(item);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }



        public async Task<bool> SwitchStatus(int id, bool newStatus)
        {
            try
            {
                NewsPost item = await _context.NewsPosts.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.IsPublished = newStatus;
                    _context.Entry(item).State = EntityState.Modified;
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }




    }
}