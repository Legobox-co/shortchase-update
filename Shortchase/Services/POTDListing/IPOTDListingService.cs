using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IPOTDListingService
    {
        
        Task<ICollection<POTDListing>> GetAll(bool? isDeleted = null);
        Task<ICollection<POTDListing>> GetAllAvailable();
        Task<POTDListing> GetById(Guid id);

        Task<bool> Insert(POTDListing item);
        Task<bool> Update(POTDListing item);
        Task<bool> Delete(Guid id);

        Task<bool> SwitchStatus(Guid id, bool newStatus);
        Task<ICollection<POTDListing>> GetAllPredictedByUserId(Guid Id);

        Task<bool> SaveResult(Guid id, string result);

        Task<bool> DeleteBatch(Guid[] id);
    }
}