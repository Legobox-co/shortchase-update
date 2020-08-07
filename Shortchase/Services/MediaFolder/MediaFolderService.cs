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
    public class MediaFolderService : IMediaFolderService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public MediaFolderService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<MediaFolder>> GetAll(bool? Deleted = null)
        {
            try
            {
                if (Deleted.HasValue) return await _context.MediaFolders.Include(i => i.ParentFolder).Where(a => a.Deleted == Deleted).OrderBy(o => o.Name).ToListAsync().ConfigureAwait(false);
                else return await _context.MediaFolders.Include(i => i.ParentFolder).OrderBy(o => o.Name).ToListAsync().ConfigureAwait(false);
                
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<ICollection<MediaFolder>> GetAllByParentId(Guid? Id)
        {
            try
            {
                return await _context.MediaFolders.Include(i => i.ParentFolder).Where(i => i.ParentFolderId == Id && !i.Deleted).OrderBy(o => o.Name).ToListAsync().ConfigureAwait(false);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<MediaFolder> GetById(Guid id)
        {
            try
            {
                return await _context.MediaFolders.Include(i => i.ParentFolder).Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(MediaFolder MediaFolder)
        {
            try
            {
                if (MediaFolder != null)
                {
                    MediaFolder.RowDate = DateTime.UtcNow;
                    _context.MediaFolders.Add(MediaFolder);
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


        public async Task<bool> Update(MediaFolder MediaFolder)
        {
            try
            {
                if (MediaFolder != null)
                {
                    _context.Entry(MediaFolder).State = EntityState.Modified;
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
                MediaFolder item = await _context.MediaFolders.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.MediaFolders.Remove(item);
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
                MediaFolder item = await _context.MediaFolders.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
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