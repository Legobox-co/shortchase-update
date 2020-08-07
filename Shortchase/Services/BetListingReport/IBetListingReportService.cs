using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IBetListingReportService
    {
        
        Task<ICollection<BetListingReport>> GetAll(bool? IsCorrect = null);
        Task<ICollection<BetListingReport>> GetAllByListingId(Guid Id, bool? IsCorrect = null);
        Task<BetListingReport> GetById(Guid id);

        Task<bool> Insert(BetListingReport item);
        Task<bool> Update(BetListingReport item);
        Task<bool> Delete(Guid id);
        Task<bool> SwitchStatus(Guid id, bool newStatus);

    }
}