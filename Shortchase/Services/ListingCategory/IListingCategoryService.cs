using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IListingCategoryService
    {
        
        Task<ICollection<ListingCategory>> GetAll(bool? isEnabled = null);
        Task<ListingCategory> GetById(int id);
        Task<bool> Insert(ListingCategory item);
        Task<bool> Update(ListingCategory item);
        Task<bool> Delete(int id);

        Task<bool> SwitchStatus(int id, bool newStatus);
        Task<bool> HasChildrenSubcategories(int id);

        Task<bool> SoftDelete(int id);
        Task<bool> DeleteBatch(int[] Ids);

        Task<ICollection<ListingCategory>> GetAGivenNoOfCategories(int NoOfCategories);
    }
}