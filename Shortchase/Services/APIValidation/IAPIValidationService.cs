using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IAPIValidationService
    {
        Task<string> GetAPIResponse(string url);
        Task<string> StandardCall(string url, string method = "GET", string ContentType = "application/json; charset=UTF-8");
        Task<bool?> ValidateAmericanFootball(string url, BetListing listingData);
        Task<bool?> ValidateBadminton(string url, BetListing listingData);
        Task<bool?> ValidateBaseball(string url, BetListing listingData);
        Task<bool?> ValidateBasketball(string url, BetListing listingData);
        Task<bool?> ValidateCricket(string url, BetListing listingData);
        Task<bool?> ValidateDarts(string url, BetListing listingData);
        Task<bool?> ValidateHandball(string url, BetListing listingData);
        Task<bool?> ValidateIceHockey(string url, BetListing listingData);
        Task<bool?> ValidateUFC(string url, BetListing listingData);
        Task<bool?> ValidateMotorsports(string url, BetListing listingData);
        Task<bool?> ValidateSnooker(string url, BetListing listingData);
        Task<bool?> ValidateSquash(string url, BetListing listingData);
        Task<bool?> ValidateTableTennis(string url, BetListing listingData);
        Task<bool?> ValidateVolleyball(string url, BetListing listingData);
        Task<bool?> ValidateFootball(string url, BetListing listingData);
        Task<bool?> Validate(string url, BetListing listingData);
    }
}