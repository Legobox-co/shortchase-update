using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IPOTDListingLiveReportService
    {
        
        Task<ICollection<POTDListingLiveReport>> GetAll(bool? isDeleted = null);
        Task<POTDListingLiveReport> GetById(Guid id);
        Task<ICollection<POTDListingLiveReport>> GetByUserId(Guid id);
        Task<ICollection<POTDListingLiveReport>> GetByPOTDId(Guid id);
        Task<bool> Insert(POTDListingLiveReport item);
        Task<bool> Update(POTDListingLiveReport item);
        Task<bool> Delete(Guid id);


    }
}