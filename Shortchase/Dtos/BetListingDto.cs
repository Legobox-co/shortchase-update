using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;
using System;

namespace Shortchase.Dtos
{

    public class BetListingListDto
    {
        public ICollection<BetListing> BetListings { get; set; }
        public ICollection<SelectListItem> CategoriesOptions { get; set; }
        public ICollection<SelectListItem> BetListingsTypesOptions { get; set; }
        public ICollection<SelectListItem> BetListingsOddFormatOptions { get; set; }
        public ICollection<SelectListItem> BookmakerOptions { get; set; }
    }


    public class BetListingToggleCardDto
    {
        public BetListing BetListing { get; set; }
        public int IterationCounter { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
    }
    public class BetListingReportListDto
    {
        public BetListing BetListing { get; set; }
        public ICollection<BetListingReport> Reports { get; set; }
    }


    public class CreateBetListing
    {
        public string PickType { get; set; }
        public string APIValidationString { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Pick { get; set; }
        public string Odds { get; set; }
        public string OddsFormat { get; set; }
        public int Bookmaker { get; set; }
        public int Market { get; set; }
        public int Tip { get; set; }
        public string Analysis { get; set; }
        public decimal Stake { get; set; }
        public decimal Profit { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }

        public Guid PostedbyId { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int HasSubcategories { get; set; }
        public int TimezoneOffset { get; set; }


    }

    public class UpdateBetListing
    {
        public Guid Id { get; set; }

        public string PickType { get; set; }
        public string APIValidationString { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Pick { get; set; }
        public string Odds { get; set; }
        public string OddsFormat { get; set; }
        public int Bookmaker { get; set; }
        public int Market { get; set; }
        public int Tip { get; set; }
        public string Analysis { get; set; }
        public decimal Stake { get; set; }
        public decimal Profit { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }

        public Guid PostedbyId { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int HasSubcategories { get; set; }
        public int TimezoneOffset { get; set; }

    }



    public class WebsiteBetListingListYourPickDto
    {
        public ICollection<ListingCategory> CategoriesOptions { get; set; }
        public ICollection<SelectListItem> BetListingsTypesOptions { get; set; }
        public ICollection<SelectListItem> BetListingsOddFormatOptions { get; set; }
        public ICollection<SelectListItem> BookmakerOptions { get; set; }
    }

}