using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IListingSubCategoryService
    {
        
        Task<ICollection<ListingSubCategory>> GetAll(bool? isEnabled = null, bool? hasDependentData = null);
        Task<ICollection<SubcategoryJSONDto>> GetAllFromCategory(int Id);

        Task<ListingSubCategory> GetById(int id);
        Task<bool> Insert(ListingSubCategory item);
        Task<bool> Update(ListingSubCategory item);
        Task<bool> Delete(int id);

        Task<bool> SwitchStatus(int id, bool newStatus);

        Task<bool> SoftDelete(int id);
        Task<bool> DeleteBatch(int[] Ids);
    }
}