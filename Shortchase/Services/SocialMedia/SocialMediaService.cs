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
    public class SocialMediaService : ISocialMediaService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public SocialMediaService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<SocialMedia>> GetAll(bool? isEnabled = null)
        {
            try
            {
                if (isEnabled.HasValue) return await _context.SocialMedias.Where(a => a.IsEnabled == isEnabled).ToListAsync().ConfigureAwait(false);
                else return await _context.SocialMedias.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<SocialMedia> GetById(int id)
        {
            try
            {
                return await _context.SocialMedias.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(SocialMedia media)
        {
            try
            {
                if (media != null)
                {
                    media.RowDate = DateTime.Now;
                    _context.SocialMedias.Add(media);
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


        public async Task<bool> Update(SocialMedia media)
        {
            try
            {
                if (media != null)
                {
                    _context.Entry(media).State = EntityState.Modified;
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
                SocialMedia item = await _context.SocialMedias.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.SocialMedias.Remove(item);
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
                SocialMedia item = await _context.SocialMedias.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.IsEnabled = newStatus;
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