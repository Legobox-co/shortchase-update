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
    public class ListingCategoryService : IListingCategoryService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public ListingCategoryService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<ListingCategory>> GetAll(bool? isEnabled = null)
        {
            try
            {
                if (isEnabled.HasValue)
                {
                    return await _context.ListingCategories.Where(a => a.IsEnabled == isEnabled).ToListAsync().ConfigureAwait(false);
                }
                else
                {
                    return await _context.ListingCategories.ToListAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ListingCategory> GetById(int id)
        {
            try
            {
                return await _context.ListingCategories.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(ListingCategory item)
        {
            try
            {
                if (item != null)
                {
                    item.RowDate = DateTime.Now;
                    _context.ListingCategories.Add(item);
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


        public async Task<bool> Update(ListingCategory item)
        {
            try
            {
                if (item != null)
                {
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

        public async Task<bool> Delete(int id)
        {
            try
            {
                ListingCategory item = await _context.ListingCategories.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.ListingCategories.Remove(item);
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



        public async Task<bool> SoftDelete(int id)
        {
            try
            {
                ListingCategory item = await _context.ListingCategories.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.Deleted = true;
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


        public async Task<bool> SwitchStatus(int id, bool newStatus)
        {
            try
            {
                ListingCategory item = await _context.ListingCategories.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
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


        public async Task<bool> HasChildrenSubcategories(int id)
        {
            try
            {
                ListingCategory item = await _context.ListingCategories.Where(c => c.Id == id && c.SubCategories.Count() > 0 && c.SubCategories.Any(s => s.IsEnabled)).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null) return true;
                else return false;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }





        public async Task<bool> DeleteBatch(int[] Ids)
        {
            try
            {
                var items = await _context.ListingCategories.Where(c => Ids.Contains(c.Id)).ToListAsync().ConfigureAwait(false);
                if (items != null && items.Count > 0)
                {
                    foreach (var item in items) {
                        item.IsEnabled = false;
                        _context.Entry(item).State = EntityState.Modified;
                    }
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

        public async Task<ICollection<ListingCategory>> GetAGivenNoOfCategories(int NoOfCategories )
        {
            try
            {
               

                if (NoOfCategories  > 0)
                {
                    return await _context.ListingCategories.Take(NoOfCategories).ToListAsync().ConfigureAwait(false);
                }
                else
                {
                    return await _context.ListingCategories.ToListAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
    }
}