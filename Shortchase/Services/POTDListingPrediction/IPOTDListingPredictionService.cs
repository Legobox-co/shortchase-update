using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IPOTDListingPredictionService
    {
        
        Task<ICollection<POTDListingPrediction>> GetAll(bool? isDeleted = null);
        Task<POTDListingPrediction> GetById(Guid id);
        Task<ICollection<POTDListingPrediction>> GetByUserId(Guid id);
        Task<ICollection<POTDListingPrediction>> GetByPOTDId(Guid id);
        Task<POTDListingPrediction> GetUserPredictionForPOTD(Guid id, Guid potdId);
        Task<bool> Insert(POTDListingPrediction item);
        Task<bool> Update(POTDListingPrediction item);
        Task<bool> Delete(Guid id);
        Task<bool> Validate(Guid id, bool Value);
        Task<bool> Validate(ICollection<POTDListingPrediction> predictions);
        Task<ICollection<POTDListingPrediction>> GetAllWaitingValidation();
        Task<bool> InsertComment(PredictionComment item);
        
        Task<ICollection<PredictionComment>> GetAllComment();

    }
}