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
    public class BlogPostsService : IBlogPostsService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public BlogPostsService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<BlogPost>> GetAll(bool? isPublished = null)
        {
            try
            {
                if (isPublished.HasValue) return await _context.BlogPosts.Where(a => a.IsPublished == isPublished.Value).ToListAsync().ConfigureAwait(false);
                else return await _context.BlogPosts.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<BlogListItemDto>> GetAllForbackend()
        {
            try
            {
                var items = await _context.BlogPosts
                    .Select(a => new BlogListItemDto { Id = a.Id, DateCreated = a.RowDate, IsPublished = a.IsPublished, Title = a.Title, Slug = a.Slug, DatePublished = a.DatePublished.HasValue ? a.DatePublished.Value : DateTime.MinValue })
                    .ToListAsync().ConfigureAwait(false);
                return items;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<BlogListWebsiteItemDto>> GetAllForWebsite()
        {
            try
            {
                var items = await _context.BlogPosts.Where(a => a.IsPublished || a.DatePublished.Value <= DateTime.UtcNow)
                    .Select(a => new BlogListWebsiteItemDto { Excerpt = a.Excerpt, IsPublished = a.IsPublished, Title = a.Title, Slug = a.Slug, DatePublished = a.DatePublished.HasValue ? a.DatePublished.Value : DateTime.MinValue, FeaturedImage = a.FeaturedImage })
                    .ToListAsync().ConfigureAwait(false);
                return items;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<BlogPost> GetById(int id)
        {
            try
            {
                return await _context.BlogPosts.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
        public async Task<BlogPost> GetBySlug(string slug)
        {
            try
            {
                return await _context.BlogPosts.Where(c => c.Slug == slug).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(BlogPost post)
        {
            try
            {
                if (post != null)
                {
                    var slugsUsed = await _context.BlogPosts.Where(i => i.Slug.ToLower() == post.Slug.ToLower()).ToListAsync().ConfigureAwait(false);
                    if (slugsUsed.Count > 0) {
                        post.Slug = post.Slug + "-" + Guid.NewGuid().ToString();
                    }
                    post.RowDate = DateTime.UtcNow;
                    _context.BlogPosts.Add(post);
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


        public async Task<bool> Update(BlogPost post)
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
                BlogPost item = await _context.BlogPosts.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.BlogPosts.Remove(item);
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
                BlogPost item = await _context.BlogPosts.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
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