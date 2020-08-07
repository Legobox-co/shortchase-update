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
    public class MediaFileService : IMediaFileService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public MediaFileService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<MediaFile>> GetAll(bool? Deleted = null)
        {
            try
            {
                if (Deleted.HasValue) return await _context.MediaFiles.Include(i => i.Folder).Where(a => a.Deleted == Deleted).OrderByDescending(o => o.RowDate).ToListAsync().ConfigureAwait(false);
                else return await _context.MediaFiles.Include(i => i.Folder).OrderByDescending(o => o.RowDate).ToListAsync().ConfigureAwait(false);
                
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<ICollection<MediaFile>> GetAllFolderId(Guid? Id)
        {
            try
            {
                return await _context.MediaFiles.Include(i => i.Folder).Where(i => i.FolderId == Id && !i.Deleted).OrderByDescending(o => o.RowDate).ToListAsync().ConfigureAwait(false);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<MediaFile> GetById(Guid id)
        {
            try
            {
                return await _context.MediaFiles.Include(i => i.Folder).Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(MediaFile MediaFile)
        {
            try
            {
                if (MediaFile != null)
                {
                    MediaFile.RowDate = DateTime.UtcNow;
                    _context.MediaFiles.Add(MediaFile);
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


        public async Task<bool> Update(MediaFile MediaFile)
        {
            try
            {
                if (MediaFile != null)
                {
                    _context.Entry(MediaFile).State = EntityState.Modified;
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

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                MediaFile item = await _context.MediaFiles.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.MediaFiles.Remove(item);
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



        public async Task<bool> SwitchStatus(Guid id, bool newStatus)
        {
            try
            {
                MediaFile item = await _context.MediaFiles.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.Deleted = newStatus;
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