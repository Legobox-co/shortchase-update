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
    public class ListingSubCategoryService : IListingSubCategoryService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public ListingSubCategoryService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<ListingSubCategory>> GetAll(bool? isEnabled = null, bool? hasDependentData = null)
        {
            try
            {
                if (hasDependentData.HasValue)
                {
                    if (isEnabled.HasValue) return await _context.ListingSubCategories.Include(i => i.Category).Where(a => a.IsEnabled == isEnabled).ToListAsync().ConfigureAwait(false);
                    else return await _context.ListingSubCategories.Include(i => i.Category).ToListAsync().ConfigureAwait(false);
                }
                else {
                    if (isEnabled.HasValue) return await _context.ListingSubCategories.Where(a => a.IsEnabled == isEnabled).ToListAsync().ConfigureAwait(false);
                    else return await _context.ListingSubCategories.ToListAsync().ConfigureAwait(false);
                }
                    
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<SubcategoryJSONDto>> GetAllFromCategory(int Id)
        {
            try
            {
               return await _context.ListingSubCategories.Where(i => i.CategoryId.Value == Id && i.IsEnabled).OrderBy(o => o.Name).Select(i => new SubcategoryJSONDto { Description = i.Description, Name = i.Name, Id = i.Id }).ToListAsync().ConfigureAwait(false);
                

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ListingSubCategory> GetById(int id)
        {
            try
            {
                return await _context.ListingSubCategories.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(ListingSubCategory item)
        {
            try
            {
                if (item != null)
                {
                    item.RowDate = DateTime.Now;
                    _context.ListingSubCategories.Add(item);
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


        public async Task<bool> Update(ListingSubCategory item)
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
                ListingSubCategory item = await _context.ListingSubCategories.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.ListingSubCategories.Remove(item);
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
                ListingSubCategory item = await _context.ListingSubCategories.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
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
                ListingSubCategory item = await _context.ListingSubCategories.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
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




        public async Task<bool> DeleteBatch(int[] Ids)
        {
            try
            {
                var items = await _context.ListingSubCategories.Where(c => Ids.Contains(c.Id)).ToListAsync().ConfigureAwait(false);
                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
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
    }
}