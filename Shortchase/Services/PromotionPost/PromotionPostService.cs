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
    public class PromotionPostService : IPromotionPostService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public PromotionPostService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<PromotionPost>> GetAll(bool? isPublished = null)
        {
            try
            {
                if (isPublished.HasValue) return await _context.PromotionPosts.Where(a => a.IsPublished == isPublished.Value).ToListAsync().ConfigureAwait(false);
                else return await _context.PromotionPosts.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<PromotionListItemDto>> GetAllForbackend()
        {
            try
            {
                var items = await _context.PromotionPosts
                    .Select(a => new PromotionListItemDto { Id = a.Id, DateCreated = a.RowDate, IsPublished = a.IsPublished, Title = a.Title, Slug = a.Slug, DatePublished = a.DatePublished.HasValue ? a.DatePublished.Value : DateTime.MinValue })
                    .ToListAsync().ConfigureAwait(false);
                return items;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<PromotionListWebsiteItemDto>> GetAllForWebsite()
        {
            try
            {
                var items = await _context.PromotionPosts.Where(a => a.IsPublished || a.DatePublished.Value <= DateTime.UtcNow)
                    .Select(a => new PromotionListWebsiteItemDto { Excerpt= a.Excerpt, IsPublished = a.IsPublished, Title = a.Title, Slug = a.Slug, DatePublished = a.DatePublished.HasValue ? a.DatePublished.Value : DateTime.MinValue, FeaturedImage = a.FeaturedImage })
                    .ToListAsync().ConfigureAwait(false);
                return items;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<PromotionPost> GetById(int id)
        {
            try
            {
                return await _context.PromotionPosts.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
        public async Task<PromotionPost> GetBySlug(string slug)
        {
            try
            {
                return await _context.PromotionPosts.Where(c => c.Slug == slug).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(PromotionPost post)
        {
            try
            {
                if (post != null)
                {
                    var slugsUsed = await _context.PromotionPosts.Where(i => i.Slug.ToLower() == post.Slug.ToLower()).ToListAsync().ConfigureAwait(false);
                    if (slugsUsed.Count > 0)
                    {
                        post.Slug = post.Slug + "-" + Guid.NewGuid().ToString();
                    }
                    post.RowDate = DateTime.UtcNow;
                    _context.PromotionPosts.Add(post);
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


        public async Task<bool> Update(PromotionPost post)
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
                PromotionPost item = await _context.PromotionPosts.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.PromotionPosts.Remove(item);
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
                PromotionPost item = await _context.PromotionPosts.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
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