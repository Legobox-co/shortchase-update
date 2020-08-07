using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shortchase.Entities;
using System;
using Microsoft.AspNetCore.Http;

namespace Shortchase.Dtos
{

    public class POTDListingListDto
    {
        public ICollection<POTDListing> POTDListings { get; set; }
        public ICollection<SelectListItem> CategoriesOptions { get; set; }
    }


    public class CreatePOTDListingItemDto
    {
        public string Title { get; set; }
        public int Pick { get; set; }
        public int Tip { get; set; }
        public int Market { get; set; }
        public string Venue { get; set; }
        public Guid PostedbyId { get; set; }
        public int TimezoneOffset { get; set; }
        public int HasSubcategories { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public Guid? BackgroundImage { get; set; }
        //public IFormFile BackgroundImage { get; set; }
    }



    public class UpdatePOTDListingItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Pick { get; set; }
        public int Tip { get; set; }
        public int Market { get; set; }
        public string Venue { get; set; }
        public int TimezoneOffset { get; set; }
        public int HasSubcategories { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public Guid? BackgroundImage { get; set; }
        //public IFormFile BackgroundImage { get; set; }
    }
    public class ViewPOTDWebsite
    {
        public POTDListing POTD { get; set; }
        public POTDListing NextPOTD { get; set; }
        public POTDListingPrediction UserPrediction { get; set; }
        public ICollection<POTDListingLiveReport> LiveReportings { get; set; }
    }
    public class PredictPOTDDto
    {
        public POTDListing POTD { get; set; }
        public ICollection<MarketJSONDto> MarketOptions { get; set; }
    }

}