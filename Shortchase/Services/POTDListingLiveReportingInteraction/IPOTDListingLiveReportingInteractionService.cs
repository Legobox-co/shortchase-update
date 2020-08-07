using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IPOTDListingLiveReportingInteractionService
    {
        
        Task<POTDListingLiveReportingInteraction> GetById(Guid id);
        Task<ICollection<POTDListingLiveReportingInteraction>> GetByUserAndPOTDLiveReportingId(Guid UserId, Guid POTDLiveReportingId);
        Task<bool> Insert(POTDListingLiveReportingInteraction item);
        Task<bool> Update(POTDListingLiveReportingInteraction item);
        Task<bool> Delete(Guid id);
        Task<ICollection<POTDListingLiveReportingInteraction>> GetByPOTDLiveReportingAndType(Guid POTDLiveReportingId, string Type);

    }
}