using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IBetListingService
    {
        
        Task<ICollection<BetListing>> GetAll(bool? isDeleted = null);
        Task<ICollection<BetListing>> GetAllWaitingForValidation();
        Task<BetListing> GetById(Guid id);

        Task<bool> Insert(BetListing item);
        Task<bool> Update(BetListing item);
        Task<bool> Delete(Guid id);
        Task<bool> DeleteBatch(Guid[] Ids);

        Task<bool> SwitchStatus(Guid id, bool newStatus);

        Task<ICollection<BetListing>> GetAllListingsByUser(Guid id);

        Task<bool> IsListingUnderLimits(BetListing listing);
        Task<bool> IsBoisterouListing(BetListing listing);

        Task<ICollection<BetListing>> GetAllAvailableForMarketplace(SearchFilters filters);
        Task<ICollection<BetListing>> GetAllRelatedAvailableForMarketplace(Guid PostedBy, Guid ExcludeListingId);
        Task<ICollection<BetListing>> GetAllAvailableForDropdownSearch(string Keyword);
        Task<bool> IncreaseViews(Guid id);
    }
}