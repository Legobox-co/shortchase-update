using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IUserRatingService
    {
        
        Task<ICollection<UserRating>> GetAll();

        Task<int> GetAverageRating(Guid toId);
        Task<UserRating> GetById(int id);
        Task<ICollection<UserRating>> GetByFromId(Guid fromId);
        Task<ICollection<UserRating>> GetByToId(Guid toId);
        Task<UserRating> GetByFromTo(Guid fromId, Guid toId);

        Task<bool> Insert(UserRating item);
        Task<bool> Update(UserRating item);
        Task<bool> Delete(int id);
        Task<int> GetAverageRatingCorrect(Guid userId);
        Task<int> GetTotalRatingPoints(Guid userId);
    }
}